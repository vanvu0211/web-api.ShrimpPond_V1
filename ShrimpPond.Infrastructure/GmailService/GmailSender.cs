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

        #region GỬi mail confirm
        public async Task<bool> SendConfirmationEmailAsync(GmailMessage gmailMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail);
            message.From.Add(new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail));
            message.To.Add(new MailboxAddress(gmailMessage.To, gmailMessage.To));
            message.Subject = gmailMessage.Subject ?? "Confirm Your Email Address";

            // Create HTML body for confirmation email
            var builder = new BodyBuilder();
            builder.HtmlBody = GenerateConfirmationHtml(gmailMessage.To, gmailMessage.Body); // Use Body for content
            message.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(_gmailSettings.Host, _gmailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_gmailSettings.Mail, _gmailSettings.Password);
                await smtp.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        // Method to generate HTML for confirmation email
        private string GenerateConfirmationHtml(string recipientName, string otpCode)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Email Confirmation</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            text-align: center;
            padding: 20px 0;
            background-color: #007bff;
            color: #ffffff;
            border-radius: 8px 8px 0 0;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 20px;
            text-align: center;
        }}
        .content p {{
            font-size: 16px;
            color: #333333;
            line-height: 1.6;
        }}
        .otp-code {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #f8f9fa;
            border: 2px solid #007bff;
            border-radius: 5px;
            font-size: 24px;
            font-weight: bold;
            color: #007bff;
            margin: 20px 0;
            letter-spacing: 2px;
        }}
        .footer {{
            text-align: center;
            padding: 10px;
            font-size: 12px;
            color: #777777;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Xác thực Email</h1>
        </div>
        <div class='content'>
            <p>Xin chào {recipientName},</p>
            <p>Vui lòng sử dụng mã OTP dưới đây để xác thực email của bạn:</p>
            <div class='otp-code'>{otpCode}</div>
            <p>Mã này có hiệu lực trong vòng 5 phút.</p>
        </div>
        <div class='footer'>
            <p>© {DateTime.Now.Year} HCMUT. All rights reserved.</p>
            <p>Nếu bạn không yêu cầu mã này, vui lòng bỏ qua email này.</p>
        </div>
    </div>
</body>
</html>";
        }

        #endregion

        #region Gửi thông báo
        public async Task<bool> SendNotificationEmailAsync(GmailMessage gmailMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail);
            message.From.Add(new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail));
            message.To.Add(new MailboxAddress(gmailMessage.To, gmailMessage.To));
            message.Subject = gmailMessage.Subject ?? "Thông Báo Từ Hệ Thống";

            // Create HTML body for notification email
            var builder = new BodyBuilder();
            builder.HtmlBody = GenerateNotificationHtml(gmailMessage.To, gmailMessage.Body);
            message.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(_gmailSettings.Host, _gmailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_gmailSettings.Mail, _gmailSettings.Password);
                await smtp.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send notification email: {ex.Message}");
                return false;
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        // Method to generate HTML for notification email
        private string GenerateNotificationHtml(string recipientName, string content)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Thông Báo</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            text-align: center;
            padding: 20px 0;
            background-color: #28a745;
            color: #ffffff;
            border-radius: 8px 8px 0 0;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 20px;
            text-align: left;
        }}
        .content p {{
            font-size: 16px;
            color: #333333;
            line-height: 1.6;
        }}
        .highlight-box {{
            background-color: #f8f9fa;
            border-left: 4px solid #28a745;
            padding: 15px;
            margin: 15px 0;
            border-radius: 5px;
            font-size: 16px;
            color: #333333;
        }}
        .highlight-box::before {{
            content: 'ℹ️ ';
            font-size: 18px;
            vertical-align: middle;
        }}
        .action-button {{
            display: inline-block;
            padding: 10px 20px;
            margin-top: 15px;
            background-color: #28a745;
            color: #ffffff;
            text-decoration: none;
            border-radius: 5px;
            font-weight: bold;
        }}
        .action-button:hover {{
            background-color: #218838;
        }}
        .footer {{
            text-align: center;
            padding: 10px;
            font-size: 12px;
            color: #777777;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Thông Báo</h1>
        </div>
        <div class='content'>
            <p>Xin chào {recipientName},</p>
            <div class='highlight-box'>{content}</div>
            <p>Vui lòng liên hệ với chúng tôi nếu bạn có bất kỳ câu hỏi nào.</p>
            <a href='https://shrimp-farm-management.vercel.app/dashboard' class='action-button'>Liên Hệ Hỗ Trợ</a>
        </div>
        <div class='footer'>
            <p>© {DateTime.Now.Year} HCMUT. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }
        #endregion

        #region Gửi cảnh báo
        public async Task<bool> SendWarningEmailAsync(GmailMessage gmailMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail);
            message.From.Add(new MailboxAddress(_gmailSettings.DisplayName, _gmailSettings.Mail));
            message.To.Add(new MailboxAddress(gmailMessage.To, gmailMessage.To));
            message.Subject = gmailMessage.Subject ?? "Cảnh Báo Từ Hệ Thống";

            // Create HTML body for warning email
            var builder = new BodyBuilder();
            builder.HtmlBody = GenerateWarningHtml(gmailMessage.To, gmailMessage.Body);
            message.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(_gmailSettings.Host, _gmailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_gmailSettings.Mail, _gmailSettings.Password);
                await smtp.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send warning email: {ex.Message}");
                return false;
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        // Method to generate HTML for warning email
        private string GenerateWarningHtml(string recipientName, string warningMessage)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Cảnh Báo</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            text-align: center;
            padding: 20px 0;
            background-color: #dc3545;
            color: #ffffff;
            border-radius: 8px 8px 0 0;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 20px;
            text-align: left;
        }}
        .content p {{
            font-size: 16px;
            color: #333333;
            line-height: 1.6;
        }}
        .warning-box {{
            background-color: #fff3f4;
            border-left: 4px solid #dc3545;
            padding: 15px;
            margin: 15px 0;
            border-radius: 5px;
            font-size: 16px;
            color: #333333;
            font-weight: bold;
        }}
        .warning-box::before {{
            content: '⚠️ ';
            font-size: 18px;
            vertical-align: middle;
        }}
        .action-button {{
            display: inline-block;
            padding: 10px 20px;
            margin-top: 15px;
            background-color: #dc3545;
            color: #ffffff;
            text-decoration: none;
            border-radius: 5px;
            font-weight: bold;
        }}
        .action-button:hover {{
            background-color: #c82333;
        }}
        .footer {{
            text-align: center;
            padding: 10px;
            font-size: 12px;
            color: #777777;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Cảnh Báo Quan Trọng</h1>
        </div>
        <div class='content'>
            <p>Xin chào {recipientName},</p>
            <p>Chúng tôi đã phát hiện một vấn đề cần bạn chú ý:</p>
            <div class='warning-box'>{warningMessage}</div>
            <p>Vui lòng thực hiện hành động cần thiết ngay lập tức.</p>
            <a href='https://shrimp-farm-management.vercel.app/dashboard' class='action-button'>Thực Hiện Hành Động</a>
        </div>
        <div class='footer'>
            <p>© {DateTime.Now.Year} HCMUT. All rights reserved.</p>
            <p>Nếu bạn không thực hiện hành động này, vui lòng liên hệ ngay để xác minh.</p>
        </div>
    </div>
</body>
</html>";
        }
        #endregion
    }

}
