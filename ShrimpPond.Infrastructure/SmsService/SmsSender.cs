using Microsoft.Extensions.Options;
using ShrimpPond.Application.Contract.SmsService;
using ShrimpPond.Application.Models.Sms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ShrimpPond.Infrastructure.SmsService
{
    public class SmsSender : ISmsSender
    {
        public SmsSettings _smsSettings { get; }
        public SmsSender(IOptions<SmsSettings> smsSettings)
        {
            _smsSettings = smsSettings.Value;
        }

        public bool SendSms(SmsMessage sms)
        {
            try
            {
                TwilioClient.Init(_smsSettings.AccountSid, _smsSettings.AuthToken);


                var message = MessageResource.Create(
                                body: sms.Body,
                                from: new Twilio.Types.PhoneNumber(sms.FROM),
                                to: new Twilio.Types.PhoneNumber(sms.To)
                                );
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
