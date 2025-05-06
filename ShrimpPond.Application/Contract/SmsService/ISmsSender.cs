using ShrimpPond.Application.Models.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.SmsService
{
    public interface ISmsSender
    {
        bool SendSms(SmsMessage sms);
    }
}
