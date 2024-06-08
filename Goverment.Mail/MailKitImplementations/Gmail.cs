using Core.Security.Entities;
using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Core.Mailing.MailKitImplementations
{
    public static class Gmail
    {

        public static void ConfirmTokenSend(User user, string url)
        {
            var message = HeaderPart(user);
            message.Subject = "please Confirm Email";
            string emailBody = "<h2> Emaili Tesdiq Edin <h2> <hr>";

            emailBody += $"<h5><a href='{url}'> bu linke klik edin.... </a></h5>";
            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = emailBody
            };
            message.Body = bodyBuilder.ToMessageBody();

            using SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("projectogani@gmail.com", "cjywfdxcacwbtixw");
            var send = smtp.Send(message);
            smtp.Disconnect(true);
        }

        public static User OtpSend(User user)
        {
            var message = HeaderPart(user);
            message.Subject = "Verification OTP code";
            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = $"Your Otp code <h3>{user.OtpCode}</h3>"
            };

            message.Body = bodyBuilder.ToMessageBody();
            FooterPart(message);
            return user;
        }

        public static void SendWarningMessage(User user)
        {
            var message = HeaderPart(user); ;
            message.Subject = "Warning Message";
            string emailBody = "<h4>Hesabiniza ugursuz giris cehdi olunur " +
                $"ozunuz olgunuzdan emin olun!!!! <h4>";

            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = emailBody
            };
            message.Body = bodyBuilder.ToMessageBody();

            FooterPart(message);
        }

        private static MimeMessage HeaderPart(User user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("GovermentApp", "projectogani@gmail.com"));

            message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            return message;
        }

        private static void FooterPart(MimeMessage message)
        {
            using SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("projectogani@gmail.com", "cjywfdxcacwbtixw");
            var send = smtp.Send(message);
            smtp.Disconnect(true);
        }

    }
}