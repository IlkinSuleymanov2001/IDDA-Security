using Core.Security.Entities;
using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Core.Mailing.MailKitImplementations
{
	public static class Gmail 
	{

		public  static void ConfirmTokenSend(User user , string url)
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

        public static User  OtpSend(User user)
        {
			var message = HeaderPart(user);
            var otpCode = GenerateOTP();
            message.Subject = "Verification OTP code";
            BodyBuilder bodyBuilder = new()
            {
                TextBody = $"Your Otp code {otpCode}"
            };

            user.OtpCode= otpCode;
            user.OptCreatedDate = DateTime.Now;

            message.Body = bodyBuilder.ToMessageBody();
            FooterPart(message);
            return user;
        }

        public static string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString(); // Generate a random 6-digit number
        }

		private static  MimeMessage HeaderPart(User user) 
		{
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("GovermentApp", "projectogani@gmail.com"));

            message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            return message;
        }

        private static void  FooterPart(MimeMessage message)
        {
            using SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("projectogani@gmail.com", "cjywfdxcacwbtixw");
            var send = smtp.Send(message);
            smtp.Disconnect(true);
        }

        /*public void MimeMessageResetPassword(User user, string url, string code)
		{
			var message = new MimeMessage();

			message.From.Add(new MailboxAddress("SmartGalery", "projectogani@gmail.com"));

			message.To.Add(new MailboxAddress(user.FinCode, user.Email));

			message.Subject = "Reset Password";

			//string emailbody = string.Empty;

			//using (StreamReader streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Account/Templates", "Confirm.html")))
			//{
			//    emailbody = streamReader.ReadToEnd();
			//}

			//emailbody = emailbody.Replace("{{username}}", $"{user.UserName}").Replace("{{code}}", $"{url}");

			//message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

			string emailBody = "<h2> Emaili Tesdiq Edin <h2> <hr>";

			emailBody += $"<h5><a href='{url}'> bu linke klik edin. </a></h5>";

			message.Body = new TextPart(TextFormat.Html) { Text = emailBody };

			using var smtp = new SmtpClient();

			smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
			smtp.Authenticate("projectogani@gmail.com", "cjywfdxcacwbtixw");
			smtp.Send(message);
			smtp.Disconnect(true);
		}*/
    }
}