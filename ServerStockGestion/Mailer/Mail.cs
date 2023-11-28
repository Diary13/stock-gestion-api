using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ServerStockGestion.Mailer
{
    public static class Mail
    {
        public static bool Send(string nomProd)
        {
            MailMessage mail = new MailMessage();
            const MAIL_Name = "";
            const DEST = ""
            mail.From = new MailAddress(MAIL_Name);
            mail.To.Add(new MailAddress(DEST));
            mail.Subject = "Alerte! Stock faible";
            mail.IsBodyHtml = true;
            mail.Body = "Stock de "+ nomProd+" faible";
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(MAIL_Name, "");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            return false;
        }

    }
}