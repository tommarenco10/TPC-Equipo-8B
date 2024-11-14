using Dominio;
using System;
using System.Web.UI;

namespace TPC
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                lblBienvenida.Text = "Bienvenido a la Plataforma de Gestión del Club de Fútbol!";
                litContent.Text = @"<p>En nuestra plataforma, llevamos la gestión del club a otro nivel. Aquí podrás encontrar todo lo necesario para administrar el día a día de nuestros equipos, jugadores y entrenadores. Desde la planificación de entrenamientos hasta el registro de incidencias y el seguimiento del rendimiento, hemos diseñado una herramienta pensada para optimizar el rendimiento de cada categoría del club.</p>
                                    <p>Entrenadores, médicos y el cuerpo técnico tendrán acceso a herramientas que facilitan la organización de entrenamientos, el seguimiento de las condiciones físicas de los jugadores y la generación de reportes detallados de desempeño.</p>
                                    <p>Hinchas y socios podrán seguir de cerca las actividades del club, con acceso en tiempo real a las actualizaciones sobre los equipos, entrenamientos y sorteos exclusivos.</p>
                                    <p>¡Unite a nosotros y descubrí cómo potenciamos cada aspecto del fútbol!</p>";
            }
            else
            {
                Usuario logueado = (Usuario)Session["user"];
                lblBienvenida.Text = $"Hola {logueado.Nombre}, bienvenido a la Plataforma de Gestión del Club de Fútbol!";
                litContent.Text = "<p class='titulo'>Inicio de sesión exitoso!</p>";
            }
        }
    }
}
