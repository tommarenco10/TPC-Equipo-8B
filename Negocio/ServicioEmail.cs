using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Dominio;




namespace Negocio
{
    internal class ServicioEmail
    {
        string emailOrigen="squadrateamutn@gmail.com";
        string password="proyecto2024ts";



       
        public ServicioEmail()
        {
            emailOrigen = "squadrateamutn@gmail.com";
            password = "proyecto2024ts";
        }
        
        public void enviarMensajeAdjunto(string destinatario, string asunto,string mensaje,string path)
        {
            MailMessage MensajeMail= new MailMessage(emailOrigen,destinatario,asunto,mensaje);
            MensajeMail.IsBodyHtml = true;//Para poder enviarle un texto en formato html
            SmtpClient osmtpClient = new SmtpClient("smpt.gmail.com");  //Hace el trasnporte del mensaje, host de gmail es smtp.gmail.com
            MensajeMail.Attachments.Add(new Attachment(path)); //NUEVO adjunto(attachment=adjunto, junto a su path)
            osmtpClient.EnableSsl = true;//Seguridad activada
            osmtpClient.UseDefaultCredentials = false; //Necesario para poder loguearme correctamente con las credenciales despues
            osmtpClient.Host = "smtp.gmail.com";
            osmtpClient.Port = 587; //Port para que funcione.
            osmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen,password); //Configuramos las credenciales.
            osmtpClient.Send(MensajeMail);//Mandamos el mensaje mediante el cliente smtp.
            osmtpClient.Dispose();
        }






        public void enviarMensaje(string destinatario, string asunto, string mensaje)
        {
            MailMessage MensajeMail = new MailMessage(emailOrigen, destinatario, asunto, mensaje);
            MensajeMail.IsBodyHtml = true;//Para poder enviarle un texto en formato html
            SmtpClient osmtpClient = new SmtpClient("smpt.gmail.com");  //Hace el trasnporte del mensaje, host de gmail es smtp.gmail.com
            osmtpClient.EnableSsl = true;//Seguridad activada
            osmtpClient.UseDefaultCredentials = false; //Necesario para poder loguearme correctamente con las credenciales despues
            osmtpClient.Host = "smtp.gmail.com";
            osmtpClient.Port = 587; //Port para que funcione.
            osmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, password); //Configuramos las credenciales.
            osmtpClient.Send(MensajeMail);//Mandamos el mensaje mediante el cliente smtp.
            osmtpClient.Dispose();
        }



        public void setearNuevaCuenta(string direccion,string pass)
        {
            emailOrigen = direccion;
            password = pass;
        }



    }
}
