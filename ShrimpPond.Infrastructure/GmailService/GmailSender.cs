using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Models.Gmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Infrastructure.GmailService
{
    public class GmailSender : IGmailSender
    {
        public GmailSettings _gmailSettings { get; }
        public GmailSender(IOptions<GmailSettings> gmailSettings)
        {
            _gmailSettings = gmailSettings.Value;
        }

        public async Task<bool> SendGmail(GmailMessage gmailMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail); // nguoi gui
            message.From.Add(new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail)); // from

            //message.To.Add(MailboxAddress.Parse(gmail.To));
            message.To.Add(new MailboxAddress(gmailMessage.To, gmailMessage.To));  //nguoi nhan
            message.Subject = gmailMessage.Subject; //subject
            var builder = new BodyBuilder();  //tao html
            builder.HtmlBody = gmailMessage.Body;
            message.Body = builder.ToMessageBody(); //gan vao message

            var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_gmailSettings.Host, _gmailSettings.Port, SecureSocketOptions.StartTls);



            //var credentials = new NetworkCredential("your.email@gmail.com", "your-password");
            //smtp.Authenticate(credentials);




            await smtp.AuthenticateAsync(_gmailSettings.Mail, _gmailSettings.Password);

            try
            {
                await smtp.SendAsync(message);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
