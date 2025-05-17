using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Domain.Alarm;
using ShrimpPond.Domain.Configuration;
using ShrimpPond.Host.Hubs;
using ShrimpPond.Host.Model;
using ShrimpPond.Host.MQTTModels;
using ShrimpPond.Infrastructure.Communication;
using System;
using Timer = System.Timers.Timer;


namespace ShrimpPond.Host.Hosting
{
    public class HostWorker : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IGmailSender _gmailSender;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<MachineHub> _hubContext;

        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public DateTime? firstTime { get; set; }
        public double unitTime { get; set; }
        public int count { get; set; } = 1;
        public int tempTimer { get; set; }
        public int temp { get; set; } = 1;
        public int farmId { get; set; }
        public DateTime time { get; set; }

        private static Timer timer;

        public List<string> dataPonds = new List<string>(); //Danh sachs ao

        public int countPond = 0;// so luong ao

        public List<string> MemberFarms = new List<string>();

        public HostWorker(ManagedMqttClient mqttClient, IServiceScopeFactory scopeFactory, IGmailSender gmailSender, IHubContext<MachineHub> hubContext)
        {
            _mqttClient = mqttClient;
            _scopeFactory = scopeFactory;
            _gmailSender = gmailSender;
            _hubContext = hubContext;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToMqttBrokerAsync();
        }

        private async Task ConnectToMqttBrokerAsync()
        {
            _mqttClient.MessageReceived += OnMqttClientMessageReceivedAsync;
            await _mqttClient.ConnectAsync();
            await _mqttClient.Subscribe("SHRIMP_POND/+/+");
        }

        private async Task OnMqttClientMessageReceivedAsync(MqttMessage e)
        {
            var topic = e.Topic;
            var payloadMessage = e.Payload;
            if (topic is null || payloadMessage is null)
            {
                return;
            }

            var topicSegments = topic.Split('/');
            var topic1 = topicSegments[1];
            var topic2 = topicSegments[2];



            payloadMessage = payloadMessage.Replace("false", "\"FALSE\"");
            Thread.Sleep(1000);
            payloadMessage = payloadMessage.Replace("true", "\"TRUE\"");


            using var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();



            switch (topic1)
            {

                case "POND":
                    {
                        count = int.Parse(payloadMessage);
                        temp = 1;
                        double interval = 6 * 60 * 1000 * count; // 5 phút * count (mili giây)
                        timer = new Timer(interval);
                        timer.Elapsed += async (sender, e) => await TimerElapsed(sender, e); ;
                        timer.AutoReset = false; // Đặt thành true nếu muốn lặp lại
                        timer.Start();
                        break;
                    }
                case "START_TIME":
                    {
                        switch (payloadMessage)
                        {
                            case "START":
                                {
                                    tempTimer++;
                                    startTime = DateTime.UtcNow.AddHours(7);

                                    //Luu alarm
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Thông báo",
                                        AlarmDetail = "Thời gian bắt đầu đo: " + startTime.ToString(),
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    break;
                                }
                        }
                        break;
                    }
                case "FarmId":
                    {
                        farmId = Convert.ToInt32(payloadMessage);
                        MemberFarms = unitOfWork.farmRoleRepository.FindByCondition(x => x.FarmId == farmId).Select(x => x.Email).ToList();
                        break;
                    }
                case "ENVIRONMENT":
                    {
                        switch (topic2)
                        {
                            case "TEMP":
                                {
                                    break;
                                }
                            case "VALVE":
                                {
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Cảnh báo",
                                        AlarmDetail = "Van xả cần vệ sinh",
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    await SendNotification("van048483@gmail.com", "Van xả cần vệ sinh", "");
                                    break;
                                }
                            default:
                                {


                                    if (payloadMessage == "EMPTY")
                                    {
                                        try
                                        {
                                            var alarm = new Alarm()
                                            {
                                                AlarmName = "Cảnh báo",
                                                AlarmDetail = "Bơm ao " + topic2.ToString() + " bị hư hoặc không có nước",
                                                AlarmDate = DateTime.UtcNow.AddHours(7),
                                                FarmId = farmId
                                            };
                                            unitOfWork.alarmRepository.Add(alarm);
                                            await unitOfWork.SaveChangeAsync();
                                            //await SendNotification("van048483@gmail.com", "[Cảnh báo] Bơm bị hư bị hư hoặc không có nước", "Ao " + topic2.ToString());
                                            await SendMailForMember(MemberFarms, "[Cảnh báo] Bơm bị hư bị hư hoặc không có nước", "Ao " + topic2.ToString());
                                            return;
                                        }
                                        catch { }
                                    }


                                    var pond = unitOfWork.pondRepository.FindByCondition(x => x.FarmId == farmId && x.PondName == topic2).FirstOrDefault();
                                    if (pond == null) break;

                                    if (temp <= count && startTime != null)
                                    {
                                        var unitTime = (DateTime.UtcNow.AddHours(7) - startTime.Value).TotalMinutes * temp / count;
                                        time = startTime.Value.AddMinutes(unitTime);
                                        temp++;
                                    }
                                    else
                                    {
                                        temp = 1;
                                        time = DateTime.UtcNow.AddHours(7);
                                    }

                                    if (payloadMessage == "van khong dong")
                                    {
                                        var alarm = new Alarm()
                                        {
                                            AlarmName = "Cảnh báo",
                                            AlarmDetail = "Lỗi van không đóng: " + topic2.ToString(),
                                            AlarmDate = DateTime.UtcNow.AddHours(7),
                                            FarmId = farmId
                                        };
                                        unitOfWork.alarmRepository.Add(alarm);
                                        await unitOfWork.SaveChangeAsync();

                                        await SendMailForMember(MemberFarms, "[Cảnh báo] Lỗi van", "Van ao " + topic2 + " Không đóng");
                                        dataPonds.Add(topic2);
                                        countPond++;

                                    }

                                    else
                                    {

                                        var environments = JsonConvert.DeserializeObject<List<EnviromentData>>(payloadMessage)!.ToList();

                                        foreach (var environment in environments)
                                        {
                                            var valueEnvironment = environment.value;
                                            try
                                            {
                                                valueEnvironment = Math.Round(float.Parse(environment.value), 2).ToString();
                                            }
                                            catch
                                            {
                                                ;
                                            }

                                            var data = new Domain.Environments.EnvironmentStatus()
                                            {
                                                PondId = pond.PondId,
                                                Name = environment.name,
                                                Value = valueEnvironment,
                                                Timestamp = time,
                                            };

                                            unitOfWork.environmentStatusRepository.Add(data);
                                            await unitOfWork.SaveChangeAsync();

                                            await SendNotification("van048483@gmail.com", "[Thông báo] Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));

                                            var config = unitOfWork.configurationRepository.FindByCondition(x => x.FarmId == farmId).OrderBy(x => x.Id).LastOrDefault();
                                            if (config == null)
                                            {
                                                config = new Configuration
                                                {
                                                    pHTop = 8.7,
                                                    pHLow = 7.5,
                                                    OxiLow = 3,
                                                    OxiTop = 7,
                                                    TemperatureLow = 25,
                                                    TemperatureTop = 35
                                                };
                                            }

                                            switch (environment.name)
                                            {
                                                case "Temperature":
                                                    {
                                                        if (float.Parse(environment.value) >= config.TemperatureTop || float.Parse(environment.value) <= config.TemperatureLow)
                                                        {
                                                            var alarm = new Alarm()
                                                            {
                                                                AlarmName = "Cảnh báo",
                                                                AlarmDetail = "Nhiệt độ: " + environment.value.ToString() + " (độ) ngoài khoảng cho phép",
                                                                AlarmDate = DateTime.UtcNow.AddHours(7),
                                                                FarmId = farmId
                                                            };
                                                            unitOfWork.alarmRepository.Add(alarm);
                                                            await unitOfWork.SaveChangeAsync();
                                                            string jsonData = JsonConvert.SerializeObject(alarm);
                                                            await _hubContext.Clients.All.SendAsync("AlarmNotify", jsonData);
                                                            
                                                            await SendMailForMember(MemberFarms, "[Cảnh báo] Nhiệt độ ao " + topic2 + " vượt ngưỡng ", "Nhiệt độ: " + environment.value.ToString() + " (độ) ngoài khoảng cho phép");
                                                        }
                                                        break;
                                                    }
                                                case "O2":
                                                    {
                                                        if (float.Parse(environment.value) >= config.OxiTop || float.Parse(environment.value) <= config.OxiLow)
                                                        {
                                                            var alarm = new Alarm()
                                                            {
                                                                AlarmName = "Cảnh báo",
                                                                AlarmDetail = "O2: " + environment.value.ToString() + " (mg/l) ngoài khoảng cho phép",
                                                                AlarmDate = DateTime.UtcNow.AddHours(7),
                                                                FarmId = farmId
                                                            };
                                                            unitOfWork.alarmRepository.Add(alarm);
                                                            await unitOfWork.SaveChangeAsync();
                                                            await SendMailForMember(MemberFarms, "[Cảnh báo] Nồng độ oxi hòa tan ao " + topic2 + " vượt ngưỡng", "O2: " + environment.value.ToString() + " (mg/l) ngoài khoảng cho phép");
                                                        }
                                                        break;
                                                    }
                                                case "Ph":
                                                    {
                                                        if (float.Parse(environment.value) >= config.pHTop || float.Parse(environment.value) <= config.pHLow)
                                                        {
                                                            var alarm = new Alarm()
                                                            {
                                                                AlarmName = "Cảnh báo",
                                                                AlarmDetail = "pH: " + environment.value.ToString() + " ngoài khoảng cho phép",
                                                                AlarmDate = DateTime.UtcNow.AddHours(7),
                                                                FarmId = farmId
                                                            };
                                                            unitOfWork.alarmRepository.Add(alarm);
                                                            await unitOfWork.SaveChangeAsync();
                                                            await SendMailForMember(MemberFarms, "[Cảnh báo] Độ pH ao " + topic2 + " vượt ngưỡng", "pH: " + environment.value.ToString() + " ngoài khoảng cho phép");
                                                        }
                                                        break;
                                                    }
                                                default: break;
                                            }
                                        }

                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case "Machine_Status":
                    {

                        switch (topic2)
                        {
                            case "CONNECT":
                                {
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);
                                    //Luu alarm
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Thông báo",
                                        AlarmDetail = "Tình trạng kết nối ESP tủ điện 2: " + payloadMessage,
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    await SendNotification("van048483@gmail.com", "Tình trạng kết nối ESP tủ điện 2 ", payloadMessage.ToString());

                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    break;
                                }
                            case "Oxi":
                                {
                                    //Gui tin hieu signalR
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);

                                    //Luu gia tri vao database
                                    var oxiMachines = unitOfWork.machineRepository.FindByCondition(x => x.MachineName == "Máy oxi").ToList();
                                    foreach (var oxiMachine in oxiMachines)
                                    {
                                        oxiMachine.Status = (payloadMessage == "OFF") ? false : true;
                                        unitOfWork.machineRepository.Update(oxiMachine);
                                    }


                                    //Luu alarm
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Thông báo",
                                        AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt oxi" : "Bật",
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    //Gui gmail
                                    //await SendNotification("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendNotification("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    break;
                                }
                            case "Waste_separator":
                                {
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);

                                    //Luu gia tri vao database
                                    var wasteSeparatorMachines = unitOfWork.machineRepository.FindByCondition(x => x.MachineName == "Máy lọc phân").ToList();
                                    foreach (var wasteSeparatorMachine in wasteSeparatorMachines)
                                    {
                                        wasteSeparatorMachine.Status = (payloadMessage == "OFF") ? false : true;
                                        unitOfWork.machineRepository.Update(wasteSeparatorMachine);
                                    }
                                    //Luu alarm
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Thông báo",
                                        AlarmDetail = (payloadMessage == "OFF") ? "Tắt máy lọc phân" : "Bật máy lọc phân",
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    //Gui gmail
                                    //await SendNotification("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendNotification("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    break;
                                }
                            case "Fan1":
                                {
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);
                                    //Luu gia tri vao database
                                    var fan1Machines = unitOfWork.machineRepository.FindByCondition(x => x.MachineName == "Máy quạt 1").ToList();
                                    foreach (var fan1Machine in fan1Machines)
                                    {
                                        fan1Machine.Status = (payloadMessage == "OFF") ? false : true;
                                        unitOfWork.machineRepository.Update(fan1Machine);
                                    }
                                    //Luu alarm
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Thông báo",
                                        AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt 1" : "Bật" + " máy quạt 1",
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    //Gui gmail
                                    //await SendNotification("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendNotification("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    break;
                                }
                            case "Fan2":
                                {
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);
                                    //Luu gia tri vao database
                                    var fan2Machines = unitOfWork.machineRepository.FindByCondition(x => x.MachineName == "Máy quạt 2").ToList();
                                    foreach (var fan2Machine in fan2Machines)
                                    {
                                        fan2Machine.Status = (payloadMessage == "OFF") ? false : true;
                                        unitOfWork.machineRepository.Update(fan2Machine);
                                    }
                                    //Luu alarm
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Thông báo",
                                        AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt 2" : "Bật" + " máy quạt 2",
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    break;
                                }
                            case "Fan3":
                                {
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);
                                    //Luu gia tri vao database
                                    var fan3Machines = unitOfWork.machineRepository.FindByCondition(x => x.MachineName == "Máy quạt 3").ToList();
                                    foreach (var fan3Machine in fan3Machines)
                                    {
                                        fan3Machine.Status = (payloadMessage == "OFF") ? false : true;
                                        unitOfWork.machineRepository.Update(fan3Machine);
                                    }
                                    //Luu alarm
                                    var alarm = new Alarm()
                                    {
                                        AlarmName = "Thông báo",
                                        AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt 3" : "Bật" + " máy quạt 3",
                                        AlarmDate = DateTime.UtcNow.AddHours(7),
                                        FarmId = farmId
                                    };
                                    unitOfWork.alarmRepository.Add(alarm);
                                    await unitOfWork.SaveChangeAsync();
                                    break;
                                }

                        }
                        break;
                    }

                case "ERROR_CHECK":
                    {
                        var data = new DataNotification(topic2, payloadMessage);
                        string jsonData = JsonConvert.SerializeObject(data);
                        //Luu alarm
                        var alarm = new Alarm()
                        {
                            AlarmName = "Thông báo",
                            AlarmDetail = "Tình trạng kết nối ESP tủ điện 1: " + payloadMessage,
                            AlarmDate = DateTime.UtcNow.AddHours(7),
                            FarmId = farmId
                        };
                        unitOfWork.alarmRepository.Add(alarm);
                        await unitOfWork.SaveChangeAsync();
                        await SendNotification("van048483@gmail.com", "Tình trạng kết nối ESP tủ điện 1 ", payloadMessage.ToString());
                        //await SendNotification("vu34304@gmail.com", "Tình trạng kết nối ESP tủ điện 1 ", payloadMessage.ToString());
                        break;
                    }
            }
        }

        private async Task TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (countPond != 0)
            {
                var dataponds = string.Join(",", dataPonds);
                await SendMailForMember(MemberFarms, "[Thông báo] Gửi tín lại tín hiệu đo", "Danh sách ao bị lỗi khi đo: " + dataponds);

                // Gửi tín hiệu đo lại
                await _mqttClient.Publish($"SHRIMP_POND/SELECT_POND", dataponds, true);
                await _mqttClient.Publish($"SHRIMP_POND/POND/COUNT", countPond.ToString(), false);
                await Task.Delay(1000);
                await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
                await _mqttClient.Publish($"SHRIMP_POND/START_TIME/STATUS", "START", false);
                countPond = 0;
                dataPonds = new List<string>();
                timer.Stop();
            }

        }

        private async Task SendNotification(string email, string subject, string body)
        {
            var gmail = new GmailMessage
            {
                To = email,
                Subject = subject,
                Body = body
            };
            await _gmailSender.SendNotificationEmailAsync(gmail);
        }

        private async Task SendMailForMember(List<string> Emails, string subject, string body)
        {
            foreach (var email in Emails)
            {
                var gmail = new GmailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body
                };
                await _gmailSender.SendWarningEmailAsync(gmail);
            }
           
        }
    }
}
