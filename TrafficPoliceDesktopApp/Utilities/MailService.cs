using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace TrafficPoliceDesktopApp.Utilities
{
    class MailService
    {
        private const string KAT_MAIL = "resiloveca94@gmail.com";
        private const string KAT_SMTP = "smtp.gmail.com";
        private const int SMTP_PORT = 587;
        MailMessage mail;
        SmtpClient SmtpServer;
        public MailService()
        {

                mail = new MailMessage();
                SmtpServer = new SmtpClient(KAT_SMTP);
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("your attachment file");
                //mail.Attachments.Add(attachment);

                SmtpServer.Port = SMTP_PORT;
                SmtpServer.Credentials = new System.Net.NetworkCredential(KAT_MAIL, "Pass");
                SmtpServer.EnableSsl = true;


        }

        public void AddReceiver(string recieverMail)
        {
            mail.To.Add(recieverMail);
        }

        //Get/Set Subject
        private string _subject;
        public string Subject
        {
            private get { return _subject; }
            set
            {
                _subject = value;
                mail.Subject = _subject;
            }
        }

        private string _sender;
        public string Sender
        {
            private get { return _sender; }
            set
            {
                _sender = value;
                mail.From = new MailAddress(_sender);
            }
        }

        private string _body;
        public string Body
        {
            private get { return _body; }
            set
            {
                _body = value;
                mail.Body = _body;
            }
        }

        public bool SendEmail()
        {
            try
            {

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("your attachment file");
                //mail.Attachments.Add(attachment);


                SmtpServer.Send(mail);
                //Sending successfull
                return true; 

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
