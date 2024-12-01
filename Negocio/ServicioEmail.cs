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
    public static class ServicioEmail
    {
        static string emailOrigen="squadrateamutn@gmail.com";
        static string password= "oqlevczertojhbfk";


        public static void enviarMensajeAdjunto(string destinatario, string asunto,string mensaje,string path)
        {
            MailMessage MensajeMail= new MailMessage(emailOrigen,destinatario,asunto,mensaje);
            MensajeMail.IsBodyHtml = true;//Para poder enviarle un texto en formato html
            SmtpClient osmtpClient = new SmtpClient("smtp.gmail.com");  //Hace el trasnporte del mensaje, host de gmail es smtp.gmail.com
            MensajeMail.Attachments.Add(new Attachment(path)); //NUEVO adjunto(attachment=adjunto, junto a su path)
            osmtpClient.EnableSsl = true;//Seguridad activada
            osmtpClient.UseDefaultCredentials = false; //Necesario para poder loguearme correctamente con las credenciales despues
            osmtpClient.Host = "smtp.gmail.com";
            osmtpClient.Port = 587; //Port para que funcione.
            osmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen,password); //Configuramos las credenciales.
            osmtpClient.Send(MensajeMail);//Mandamos el mensaje mediante el cliente smtp.
            osmtpClient.Dispose();
        }






        public static void enviarMensaje(string destinatario, string asunto, string mensaje)
        {
            MailMessage MensajeMail = new MailMessage(emailOrigen, destinatario, asunto, mensaje);
            MensajeMail.IsBodyHtml = true;//Para poder enviarle un texto en formato html
            SmtpClient osmtpClient = new SmtpClient("smtp.gmail.com");  //Hace el trasnporte del mensaje, host de gmail es smtp.gmail.com
            osmtpClient.EnableSsl = true;//Seguridad activada
            osmtpClient.UseDefaultCredentials = false; //Necesario para poder loguearme correctamente con las credenciales despues
            osmtpClient.Host = "smtp.gmail.com";
            osmtpClient.Port = 587; //Port para que funcione.
            osmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, password); //Configuramos las credenciales.
            osmtpClient.Send(MensajeMail);//Mandamos el mensaje mediante el cliente smtp.
            osmtpClient.Dispose();
        }



        public static void setearNuevaCuenta(string direccion,string pass)
        {
            emailOrigen = direccion;
            password = pass;
        }




        public static void notificarJugadoresEntrenamiento(Entrenamiento entrenamiento)
        {
            foreach (Jugador jugador in entrenamiento.JugadoresCitados)
            {
                string mensaje = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                }}
                .header {{
                    background-color: #4CAF50;
                    color: white;
                    text-align: center;
                    padding: 10px 0;
                }}
                .content {{
                    margin: 20px;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 0.9em;
                    text-align: center;
                    color: #888;
                }}
            </style>
        </head>
        <body>
            <div class='header'>
                <h1>¡Nuevo Entrenamiento!</h1>
            </div>
            <div class='content'>
                <p>Hola <strong>{jugador.Nombres} {jugador.Apellidos}</strong>,</p>
                <p>Nos comunicamos para informarle que:</p>
                <ul>
                    <li><strong>Fecha:</strong> {entrenamiento.FechaHora:dd/MM/yyyy}</li>
                    <li><strong>Hora:</strong> {entrenamiento.FechaHora:HH:mm} hs</li>
                </ul>
                <p>¡Esperamos verlo allí!</p>
            </div>
            <div class='footer'>
                <p>GestionPlantillas - Plataforma de Gestión Deportiva</p>
            </div>
        </body>
        </html>";

                enviarMensaje(jugador.Email, "Nuevo entrenamiento", mensaje);
            }
        }

        public static void registroExitoso(Persona registrada)
        {
            string mensaje = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
            }}
            .header {{
                background-color: #4CAF50;
                color: white;
                text-align: center;
                padding: 10px 0;
            }}
            .content {{
                margin: 20px;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 0.9em;
                text-align: center;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='header'>
            <h1>¡Registro Exitoso!</h1>
        </div>
        <div class='content'>
            <p>Hola <strong>{registrada.Nombres} {registrada.Apellidos}</strong>,</p>
            <p>¡Gracias por registrarte en nuestra plataforma!</p>
            <p>Estamos emocionados de que formes parte de nuestra comunidad.</p>
        </div>
        <div class='footer'>
            <p>GestionPlantillas - Plataforma de Gestión Deportiva</p>
        </div>
    </body>
    </html>";

            enviarMensaje(registrada.Email, "Registro exitoso GestionPlantillas", mensaje);
        }








        public static void notificarCancelacionEntrenamiento(Entrenamiento entrenamiento)
        {
            foreach (Jugador jugador in entrenamiento.JugadoresCitados)
            {
                string mensaje = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                }}
                .header {{
                    background-color: #FF5733; /* Color rojo para resaltar la cancelación */
                    color: white;
                    text-align: center;
                    padding: 10px 0;
                }}
                .content {{
                    margin: 20px;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 0.9em;
                    text-align: center;
                    color: #888;
                }}
            </style>
        </head>
        <body>
            <div class='header'>
                <h1>Entrenamiento Cancelado</h1>
            </div>
            <div class='content'>
                <p>Hola <strong>{jugador.Nombres} {jugador.Apellidos}</strong>,</p>
                <p>Lamentamos informarle que el entrenamiento programado ha sido cancelado.</p>
                <ul>
                    <li><strong>Fecha Original:</strong> {entrenamiento.FechaHora:dd/MM/yyyy}</li>
                    <li><strong>Hora Original:</strong> {entrenamiento.FechaHora:HH:mm} hs</li>
                </ul>
                <p>Por favor, manténgase atento a futuras actualizaciones sobre la reprogramación del entrenamiento.</p>
                <p>Disculpe las molestias ocasionadas.</p>
            </div>
            <div class='footer'>
                <p>GestionPlantillas - Plataforma de Gestión Deportiva</p>
            </div>
        </body>
        </html>";

                enviarMensaje(jugador.Email, "Entrenamiento Cancelado", mensaje);
            }
        }





    }

}
