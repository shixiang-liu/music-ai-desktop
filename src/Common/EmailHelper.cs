using System;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace MusicAI.Common
{
    public static class EmailHelper
    {
        public static void SendVerificationCode(string toEmail, string code)
        {
            string smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
            string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]);

            using (SmtpClient client = new SmtpClient(smtpServer))
            {
                client.Port = smtpPort;
                client.EnableSsl = enableSsl;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "密码重置服务"),
                    Subject = "您的验证码",
                    Body = $"验证码：{code}（10分钟内有效）",
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    // 你可以改为写日志或直接 throw，便于 UI 显示更友好的错误信息
                    throw new Exception("邮件发送失败，请检查网络和邮箱配置。\n" + ex.Message, ex);
                }
            }
        }
    }
}
