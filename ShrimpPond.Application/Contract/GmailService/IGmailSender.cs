using ShrimpPond.Application.Models.Gmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.GmailService
{
    public interface IGmailSender
    {
        Task<bool> SendGmail(GmailMessage gmail);
        Task<bool> SendConfirmationEmailAsync(GmailMessage gmailMessage);
    }
}
