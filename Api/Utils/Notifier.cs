using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Api.Utils
{

    //Clase para gestionar las notificaciones. He utilizado un simple email, pero se podrían implementar cualquier tipo de sistema, mensajería instantáne como telegram o slack,
    //Jira, notificacioens push en aplicaciones android/ios, etc...
    public class Notifier
    {
        public void OutputNotify(string name)
        {
            try
            {
                Notify("Notificación de inventario", "Se ha producido la retirada del artículo: " + name + " del inventario.", "from_addreess@anything.com", "to_address@anything.com");
            }
            catch (Exception e)
            {
                Logger.Error("Fallo enviar notificación -- Exception:  " + JsonConvert.SerializeObject(e), "OutputNotify");
            }

        }

        public void ExpirationNotify(List<InventoryItem> items)
        {
            string body = "Se ha producido la expiración de los siguientes artículos: " + Environment.NewLine;
            if(items == null || items.Count <= 0)
            {
                try
                {
                    Notify("Notificación de expiración", "No se ha producido ninguna expiración hoy", "from_addreess@anything.com", "to_address@anything.com");
                }
                catch (Exception e)
                {
                    Logger.Error("Fallo enviar notificación -- Exception:  " + JsonConvert.SerializeObject(e), "ExpirationNotify");
                }
                return;
            }           

            foreach (var item in items)
            {
                body += "- " + item.name + Environment.NewLine;
            }

            try
            {
                Notify("Notificación de expiración", body, "from_addreess@anything.com", "to_address@anything.com");
            }
            catch (Exception e)
            {
                Logger.Error("Fallo enviar notificación -- Exception:  " + JsonConvert.SerializeObject(e), "ExpirationNotify");
            }
        }


        private void Notify(string tittle, string message, string from, string to)
        {
            MailMessage mail = new MailMessage(from, to);
            SmtpClient client = new SmtpClient
            {

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("gmail_account@gmail.com", "password_account")
            };

            mail.Subject = tittle;
            mail.Body = message;
            client.Send(mail);
        }
    }
}