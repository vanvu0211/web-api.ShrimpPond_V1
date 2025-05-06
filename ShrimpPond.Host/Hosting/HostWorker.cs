using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Domain.Alarm;
using ShrimpPond.Host.Hubs;
using ShrimpPond.Host.Model;
using ShrimpPond.Host.MQTTModels;
using ShrimpPond.Infrastructure.Communication;
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
                        double interval = 5 * 60 * 1000 * count; // 5 phút * count (mili giây)
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

                                    break;
                                }
                            default:
                                {
                                    if (payloadMessage == "EMPTY")
                                    {
                                        try
                                        {
                                            //Luu alarm

                                            var alarm = new Alarm()
                                            {
                                                AlarmName = "Cảnh báo",
                                                AlarmDetail = "Mất dữ liệu ao: " + topic2.ToString(),
                                                AlarmDate = DateTime.UtcNow.AddHours(7),
                                                FarmId = farmId
                                            };
                                            unitOfWork.alarmRepository.Add(alarm);
                                            await unitOfWork.SaveChangeAsync();

                                            //await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: EMPTY ");
                                            await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: EMPTY ");
                                        }
                                        catch { }
                                    }

                                    timer.Stop();


                                    var pond = unitOfWork.pondRepository.FindByCondition(x => x.FarmId == farmId && x.PondName == topic2).FirstOrDefault();
                                    if (pond == null) break;

                                    if (temp <= count && startTime != null)
                                    {
                                        var unitTime = (DateTime.UtcNow.AddHours(7) - startTime.Value).TotalMinutes * temp / count;
                                        time = startTime.Value.AddMinutes(unitTime);// chia thời gian cho mỗi lần đo
                                        temp++;
                                    }
                                    else
                                    {
                                        temp = 1;
                                        time = DateTime.UtcNow.AddHours(7);
                                    }

                                    var environments = JsonConvert.DeserializeObject<List<EnviromentData>>(payloadMessage)!.ToList();

                                    foreach (var environment in environments)
                                    {
                                        //Lưu vào database

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
                                        //Gửi dữ liệu

                                        //await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));
                                        await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));

                                        var config = unitOfWork.configurationRepository.FindByCondition(x => x.FarmId == farmId).OrderBy(x => x.Id).LastOrDefault();
                                        if (config == null)
                                        {
                                            config.pHTop = 8.7;
                                            config.pHLow = 7.5;
                                            config.oxiLow = 3;
                                            config.oxiTop = 7;
                                            config.temperatureLow = 25;
                                            config.temperatureTop = 35;

                                        }
                                        //So sánh dữ liệu gửi thông báo
                                        switch (environment.name)
                                        {

                                            case "Temperature":
                                                {
                                                    if (float.Parse(environment.value) >= config.temperatureTop || float.Parse(environment.value) <= config.temperatureLow)
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
                                                        //Gui tin hieu signalR de canh bao
                                                        string jsonData = JsonConvert.SerializeObject(alarm);
                                                        await _hubContext.Clients.All.SendAsync("AlarmNotify", jsonData);
                                                        //GUi mail
                                                        //await SendMail("vu34304@gmail.com", "Cảnh báo nhiệt độ ao " + topic2, "Nhiệt độ: " + environment.value.ToString() + " (độ) ngoài khoảng cho phép");
                                                        await SendMail("van048483@gmail.com", "Cảnh báo nhiệt độ ao " + topic2, "Nhiệt độ: " + environment.value.ToString() + " (độ) ngoài khoảng cho phép");
                                                    }
                                                    break;
                                                }
                                            case "O2":
                                                {
                                                    if (float.Parse(environment.value) >= config.oxiTop || float.Parse(environment.value) <= config.oxiLow)

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

                                                        //await SendMail("vu34304@gmail.com", "Cảnh báo thông số O2 ao " + topic2, "O2: " + environment.value.ToString() + " (mg/l) ngoài khoảng cho phép");
                                                        await SendMail("van048483@gmail.com", "Cảnh báo thông số O2  ao " + topic2, "O2: " + environment.value.ToString() + " (mg/l) ngoài khoảng cho phép");
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

                                                        //await SendMail("vu34304@gmail.com", "Cảnh báo độ pH ao " + topic2, "pH: " + environment.value.ToString() + " ngoài khoảng cho phép");
                                                        await SendMail("van048483@gmail.com", "Cảnh báo độ pH ao " + topic2, "pH: " + environment.value.ToString() + " ngoài khoảng cho phép");
                                                    }
                                                    break;
                                                }
                                            default: break;
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
                                    var oxiMachines = unitOfWork.machineRepository.FindByCondition(x => x.MachineName == "Máy quạt oxi").ToList();
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
                                    //await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
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
                                    //await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
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
                                    //await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
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
            }
        }

        private async Task TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
            await _mqttClient.Publish($"SHRIMP_POND/START_TIME/STATUS", "START", false);

            await SendMail("vu34304@gmail.com", "Gửi lại   ", "");
            await SendMail("van048483@gmail.com", "Gửi lại  ", "");
        }

        private async Task SendMail(string email, string subject, string body)
        {
            var gmail = new GmailMessage
            {
                To = email,
                Subject = subject,
                Body = body
            };
            await _gmailSender.SendGmail(gmail);
        }
    }
}
