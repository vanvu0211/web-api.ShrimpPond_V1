
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ShrimpPond.API.Hubs;
using ShrimpPond.API.Model;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Domain.Alarm;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.Farm;
using ShrimpPond.Infrastructure.Communication;

namespace ShrimpPond.API.Woker
{

    public class HostMachineWorker : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IGmailSender _gmailSender;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<NotificationHub> _hubContext;
        public int farmId { get; set; }



        public HostMachineWorker(ManagedMqttClient mqttClient, IServiceScopeFactory scopeFactory, IGmailSender gmailSender, IHubContext<NotificationHub> hubContext)
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



            switch (topic1)
            {
                case "FarmId":
                    {
                        farmId = Convert.ToInt32(payloadMessage);
                        break;
                    }
                case "Machine_Status":
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        switch (topic2)
                        {
                            case "CONNECT":
                                {
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);
                                    ////Luu alarm
                                    //var alarm = new Alarm()
                                    //{
                                    //    AlarmName = "Thông báo",
                                    //    AlarmDetail = "Tình trạng kết nối ESP tủ điện 2: " + payloadMessage,
                                    //    AlarmDate = DateTime.UtcNow.AddHours(7),
                                    //    FarmId = FarmId
                                    //};
                                    ////unitOfWork.alarmRepository.Add(alarm);
                                    ////await unitOfWork.SaveChangeAsync();
                                    break;
                                }
                            case "Oxi":
                                {
                                    //Gui tin hieu signalR
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);

                                    ////Luu gia tri vao database
                                    //var oxiMachines = unitOfWork.machineRepository.FindByCondition(x => x.machineName == "Máy quạt oxi").ToList();
                                    //foreach (var oxiMachine in oxiMachines)
                                    //{
                                    //    oxiMachine.status = (payloadMessage == "OFF") ? false : true;
                                    //    unitOfWork.machineRepository.Update(oxiMachine);
                                    //}
                                   

                                    ////Luu alarm
                                    //var alarm = new Alarm()
                                    //{
                                    //    AlarmName = "Thông báo",
                                    //    AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt oxi" : "Bật" ,
                                    //    AlarmDate = DateTime.UtcNow.AddHours(7),
                                    //    FarmId = FarmId
                                    //};
                                    //unitOfWork.alarmRepository.Add(alarm);
                                    //await unitOfWork.SaveChangeAsync();
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

                                    ////Luu gia tri vao database
                                    //var wasteSeparatorMachines = unitOfWork.machineRepository.FindByCondition(x => x.machineName == "Máy lọc phân").ToList();
                                    //foreach (var wasteSeparatorMachine in wasteSeparatorMachines)
                                    //{
                                    //    wasteSeparatorMachine.status = (payloadMessage == "OFF") ? false : true;
                                    //    unitOfWork.machineRepository.Update(wasteSeparatorMachine);
                                    //}
                                    ////Luu alarm
                                    //var alarm = new Alarm()
                                    //{
                                    //    AlarmName = "Thông báo",
                                    //    AlarmDetail = (payloadMessage == "OFF") ? "Tắt máy lọc phân" : "Bật máy lọc phân",
                                    //    AlarmDate = DateTime.UtcNow.AddHours(7),
                                    //    FarmId = FarmId
                                    //};
                                    //unitOfWork.alarmRepository.Add(alarm);
                                    //await unitOfWork.SaveChangeAsync();
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
                                    ////Luu gia tri vao database
                                    //var fan1Machines = unitOfWork.machineRepository.FindByCondition(x => x.machineName == "Máy quạt 1").ToList();
                                    //foreach (var fan1Machine in fan1Machines)
                                    //{
                                    //    fan1Machine.status = (payloadMessage == "OFF") ? false : true;
                                    //    unitOfWork.machineRepository.Update(fan1Machine);
                                    //}
                                    ////Luu alarm
                                    //var alarm = new Alarm()
                                    //{
                                    //    AlarmName = "Thông báo",
                                    //    AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt 1" : "Bật" + " máy quạt 1",
                                    //    AlarmDate = DateTime.UtcNow.AddHours(7),
                                    //    FarmId = FarmId
                                    //};
                                    //unitOfWork.alarmRepository.Add(alarm);
                                    //await unitOfWork.SaveChangeAsync();
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
                                    ////Luu gia tri vao database
                                    //var fan2Machines = unitOfWork.machineRepository.FindByCondition(x => x.machineName == "Máy quạt 2").ToList();
                                    //foreach (var fan2Machine in fan2Machines)
                                    //{
                                    //    fan2Machine.status = (payloadMessage == "OFF") ? false : true;
                                    //    unitOfWork.machineRepository.Update(fan2Machine);
                                    //}
                                    ////Luu alarm
                                    //var alarm = new Alarm()
                                    //{
                                    //    AlarmName = "Thông báo",
                                    //    AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt 2" : "Bật" + " máy quạt 2",
                                    //    AlarmDate = DateTime.UtcNow.AddHours(7),
                                    //    FarmId = FarmId
                                    //};
                                    //unitOfWork.alarmRepository.Add(alarm);
                                    //await unitOfWork.SaveChangeAsync();
                                    //Gui gmail
                                    //await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    break;
                                }
                            case "Fan3":
                                {
                                    var data = new DataNotification(topic2, payloadMessage);
                                    string jsonData = JsonConvert.SerializeObject(data);
                                    await _hubContext.Clients.All.SendAsync("MachineStatusChanged", jsonData);
                                    //Luu gia tri vao database
                                    //var fan3Machines = unitOfWork.machineRepository.FindByCondition(x => x.machineName == "Máy quạt 3").ToList();
                                    //foreach (var fan3Machine in fan3Machines)
                                    //{
                                    //    fan3Machine.status = (payloadMessage == "OFF") ? false : true;
                                    //    unitOfWork.machineRepository.Update(fan3Machine);
                                    //}
                                    ////Luu alarm
                                    //var alarm = new Alarm()
                                    //{
                                    //    AlarmName = "Thông báo",
                                    //    AlarmDetail = (payloadMessage == "OFF") ? "Tắt" + " máy quạt 3" : "Bật" + " máy quạt 3",
                                    //    AlarmDate = DateTime.UtcNow.AddHours(7),
                                    //    FarmId = FarmId
                                    //};
                                    //unitOfWork.alarmRepository.Add(alarm);
                                    //await unitOfWork.SaveChangeAsync();

                                    // Gui gmail
                                    //await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    //await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, jsonData);
                                    break;
                                }

                        }
                        break;
                    }

            }
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
