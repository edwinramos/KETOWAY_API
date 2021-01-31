using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace KETOWAY.Utitlities.Helpers
{
    public static class MailHelper
    {
        private static bool SendMail(string mail, string userCode)
        {
            bool result = true;
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Admin",
                "edwinramos.93@gmail.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("User",
                mail);
                message.To.Add(to);

                message.Subject = "Password recovery email";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<h1>Hello World!</h1> <a href='https://localhost:44393/recoverPassword/{userCode}'>Recover Password</a>";
                //bodyBuilder.HtmlBody = $"<h1>Hello World!</h1> <a href='https://localhost:44393/food'>Recover Password</a>";
                bodyBuilder.TextBody = "Hello World!";

                //Attachments
                //bodyBuilder.Attachments.Add(env.WebRootPath + "\\file.png");

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("edwinramos.93@gmail.com", "Dream0fLife");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public static bool ForgotPasswordMail(string mail, string userCode)
        {
            return SendMail(mail, userCode);
        }
    }
}
