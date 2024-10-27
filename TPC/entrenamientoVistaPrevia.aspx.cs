using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class entrenamientoVistaPrevia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["jugadoresSeleccionados"] != null)
                {
                    List<int> jugadoresSeleccionados = (List<int>)Session["jugadoresSeleccionados"];
                    JugadorNegocio negocioJugador = new JugadorNegocio();

                    // Obtener información de los jugadores seleccionados
                    var listaJugadores = negocioJugador.ObtenerJugadoresPorIds(jugadoresSeleccionados);

                    dgvJugadoresSeleccionados.DataSource = listaJugadores;
                    dgvJugadoresSeleccionados.DataBind();
                }
                else
                {
                    lblMensaje.CssClass = "alert alert-warning";
                    lblMensaje.Text = "Aún no hay jugadores seleccionados.";
                    lblMensaje.Visible = true;
                }

                if (Session["fechaHoraEntrenamiento"] != null)
                {
                    DateTime fechaHoraEntrenamiento = (DateTime)Session["fechaHoraEntrenamiento"];
                    lblDetallesEntrenamiento.CssClass = "alert alert-info";
                    lblDetallesEntrenamiento.Text = $"El entrenamiento está organizado para el {fechaHoraEntrenamiento.ToString("dddd, dd MMMM yyyy")} a las {fechaHoraEntrenamiento.ToString("HH:mm")}.";
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionEntrenamiento.aspx");
        }
    }
}