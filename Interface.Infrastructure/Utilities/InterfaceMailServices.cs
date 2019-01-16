using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using Interface.Infrastructure.Entities;

namespace Interface.Infrastructure.Utilities
{
    public class InterfaceMailServices
    {
        private static string _mailBody { get; set; }

        private static string _groupCode { get; set; }

        public static InterfaceMailEntity Settings { get; set; }

        public static void StartSending(string mailBody, string groupCode)
        {
            if (Settings.enable.Equals("0")) return;
            if (Settings.inbox.Count == 0) return;

            if (Settings.smtp.Host.Length == 0) return;
            if (Settings.smtp.Credentials.Address.Length == 0) return;

            NetworkCredential credential = new NetworkCredential();
            credential.UserName = Settings.smtp.Credentials.Address;
            credential.Password = Settings.smtp.Credentials.Password;

            SmtpClient client = new SmtpClient();
            client.Host = Settings.smtp.Host;
            client.Port = int.Parse(Settings.smtp.Port);

            client.Credentials = credential;
            client.EnableSsl = Settings.smtp.Ssl == "0" ? false : true;

            _mailBody = mailBody;
            _groupCode = groupCode;

            CreateMailMessage(client);
        }

        private static void CreateMailMessage(SmtpClient client)
        {
            MailMessage msg = new MailMessage();

            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            msg.Subject = Settings.outbox.Subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;

            msg.From = new MailAddress(Settings.smtp.Credentials.Address);
            
            Group group = Settings.inbox[_groupCode];
            foreach (Email email in group.Emails)
            {
                switch (email.Type)
                {
                    case "To":
                        msg.To.Add(new MailAddress(email.Address));
                        break;
                    case "CC":
                        msg.CC.Add(new MailAddress(email.Address));
                        break;
                    case "Bcc":
                        msg.Bcc.Add(new MailAddress(email.Address));
                        break;
                    default:
                        break;
                }
            }

            Attachment log = AddAttachment();
            if (log != null)
                msg.Attachments.Add(log);

            msg.Body = WriteMailBody();
            msg.BodyEncoding = System.Text.Encoding.UTF8;

            client.Send(msg);
        }

        private static Attachment AddAttachment()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory + Settings.outbox.AttachmentPath;
            string fileName = System.DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string attachment = basePath + fileName;

            if (!File.Exists(attachment)) return null;

            FileStream contentStream = new FileStream(attachment, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Attachment log = new Attachment(contentStream, fileName);
            log.NameEncoding = System.Text.Encoding.UTF8;

            return log;
        }

        private static string WriteMailBody()
        {
            StringBuilder body = new StringBuilder();

            body.Append("<p style=\"font-size: 10pt\">以下内容为系统自动发送，请勿直接回复，谢谢。</p>");

            if (_mailBody.Length > 0) body.AppendFormat("{0}", _mailBody);

            body.Append("<br/>");

            if (Settings.outbox.Url.Length > 0)
            {
                body.AppendFormat("<p style=\"font-size: 10pt\">查看地址：</p>");
                body.AppendFormat("<p style=\"font-size: 10pt\">{0}</p>", Settings.outbox.Url);
                body.Append("<br/>");
            }

            body.Append("<p style=\"font-size: 10pt\">Best Regards</p>");
            body.AppendFormat("<p style=\"font-size: 10pt\">{0}</p>", Settings.outbox.Signature);
            body.AppendFormat("<p style=\"font-size: 10pt\">{0}</p>", System.DateTime.Now.ToString("F"));

            body.Append("</div>");

            return body.ToString();
        }
    }
}
