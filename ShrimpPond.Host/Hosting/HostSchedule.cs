using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Contract.SmsService;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Domain.Alarm;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Host.Hosting
{
    [DisallowConcurrentExecution]
    public class HostSchedule : IJob
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IGmailSender _gmailSender ;
        // Biến trạng thái để lưu thời gian gửi gần nhất
        private DateTime? lastSentTime = null;

        public HostSchedule(ManagedMqttClient mqttClient, IServiceScopeFactory scopeFactory, IGmailSender gmailSender)
        {
            _mqttClient = mqttClient;
            _scopeFactory = scopeFactory;
            _gmailSender = gmailSender;

        }

        public async Task Execute(IJobExecutionContext context)
        {

            using var scope = _scopeFactory.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            //Lay danh sach ao da kich hoat
            var farmActive = unitOfWork.timeSettingRepository.FindByCondition(x => x.EnableFarm == true).FirstOrDefault();
            if(farmActive != null)
            {
                var ponds = unitOfWork.pondRepository
                        .FindAll()
                        .Where(x => x.Status == Domain.PondData.EPondStatus.Active && x.FarmId == farmActive.FarmId).ToList();
                var dataPonds = string.Join(",", ponds.Select(pond => pond.PondName).OrderBy(name => name));
                // Lấy TimeSettingId lớn nhất
                var timeSettingId = unitOfWork.timeSettingRepository.FindByCondition(x=>x.FarmId == farmActive.FarmId).OrderBy(x => x.TimeSettingId).LastOrDefault();
                if (timeSettingId != null)
                {
                    // Lấy danh sách thời gian từ timeSettingObjects
                    var timeSettingTimes = unitOfWork.timeSettingObjectRepository.FindAll()
                        .Where(x => x.TimeSettingId == timeSettingId.TimeSettingId)
                        .Select(x => Convert.ToDateTime(x.Time))
                        .ToList();

                    foreach (var data in timeSettingTimes)
                    {
                        Console.WriteLine(data);
                    }

                    // Thời gian hiện tại
                    var nowTime = DateTime.UtcNow.AddHours(7);
                    Console.WriteLine(nowTime.ToString());
                    // Kiểm tra nếu bất kỳ thời gian nào nằm trong khoảng ±1 phút
                    var timeMatch = timeSettingTimes.Any(time => (time.Hour == nowTime.Hour && time.Minute == nowTime.Minute)
                       );

                    Console.WriteLine(timeMatch.ToString());
                    // Kiểm tra điều kiện gửi lệnh MQTT
                    if (timeMatch && (lastSentTime == null || (nowTime - lastSentTime.Value).TotalSeconds > 60))
                    {
                        lastSentTime = nowTime;
                        Console.Write(lastSentTime);
                        await _mqttClient.Publish($"SHRIMP_POND/SELECT_POND", dataPonds, true);
                        await _mqttClient.Publish($"SHRIMP_POND/POND/COUNT", ponds.Count().ToString(), true);
                        await Task.Delay(1000);
                        await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
                        await _mqttClient.Publish($"SHRIMP_POND/START_TIME/STATUS", "START", false);
                        await _mqttClient.Publish($"SHRIMP_POND/FarmId/Value", farmActive.FarmId.ToString(), true);
                        //Luu alarm
                        var alarm = new Alarm()
                        {
                            AlarmName = "Thông báo",
                            AlarmDetail = "Gửi tín hiệu đo các ao: " + dataPonds,
                            AlarmDate = DateTime.UtcNow.AddHours(7),
                            FarmId = farmActive.FarmId
                        };
                        unitOfWork.alarmRepository.Add(alarm);
                        await unitOfWork.SaveChangeAsync();
                        //Gui maiil
                        await SendMail("vu34304@gmail.com", "Gửi danh sách Ao đo thành công", "");
                        await SendMail("van048483@gmail.com", "Gửi danh sách ao đo thành công", "");

                    }
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
