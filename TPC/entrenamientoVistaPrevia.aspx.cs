using negocio;
using System;
using System.Collections.Generic;
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
                // Verificar si hay jugadores seleccionados en sesión
                if (Session["jugadoresSeleccionados"] != null)
                {
                    List<int> jugadoresSeleccionados = (List<int>)Session["jugadoresSeleccionados"];
                    JugadorNegocio negocioJugador = new JugadorNegocio();

                    // Obtener información de los jugadores seleccionados
                    var listaJugadores = negocioJugador.ObtenerJugadoresPorIds(jugadoresSeleccionados);

                    // Asignar la lista al GridView
                    dgvJugadoresSeleccionados.DataSource = listaJugadores;
                    dgvJugadoresSeleccionados.DataBind();
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionEntrenamiento.aspx");
        }
    }
}