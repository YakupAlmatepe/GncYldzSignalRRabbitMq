using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderExampleCunsomer
{
     static class EmailSender
    {
        public static void Send(string to, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("ngakademiexample@gmail.com", "1q2w3e4r!'");
            smtpClient.Credentials = credential;

            //mail gonderen
            MailAddress sender = new MailAddress("ngakademiexample@gmail.com", "NGAkademi Example");
            MailAddress reciever = new MailAddress(to);
            
           MailMessage mail = new MailMessage(sender,reciever);
            mail.Subject = "Example";
            mail.Body = message;
            smtpClient.Send(mail);
        }
    }
}
