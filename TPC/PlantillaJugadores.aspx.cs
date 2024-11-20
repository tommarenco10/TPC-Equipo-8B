using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class PlanillaJugadores : System.Web.UI.Page
    {

        List<Jugador> lista;



        protected void Page_Load(object sender, EventArgs e)
        {

            bool esAdministrador = ((MasterPage)this.Master).esAdmin();
            bool esEntrenador = ((MasterPage)this.Master).esEntrenador();
           


            if (!(esAdministrador || esEntrenador))
            {
                EliminarColumnas(); // Eliminar columnas antes de enlazar los datos
            }

            if (!IsPostBack)
            {
                CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                ddlCategoria.DataSource = negocioCategoria.listar();
                ddlCategoria.DataTextField = "NombreCategoria";
                ddlCategoria.DataBind();

                EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
                ddlEstadoJugador.DataSource = negocioEJ.listar();
                ddlEstadoJugador.DataTextField = "NombreEstado";
                ddlEstadoJugador.DataBind();
                JugadorNegocio negocio = new JugadorNegocio();
                lista = negocio.listar();
                Session["listaJugadores"] = lista; 
                dgvJugadores.DataSource = lista;
                dgvJugadores.DataBind();

            }
            else
            { 
                lista = (List<Jugador>)Session["listaJugadores"];

            }
        
        }


        protected void txtboxFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (lista == null && Session["listaJugadores"] != null)
            {
                lista = (List<Jugador>)Session["listaJugadores"];
            }

            List<Jugador> filtro = lista.FindAll(x => x.Apellidos.ToUpper().Contains(txtboxFiltroNombre.Text.ToUpper())||x.Nombres.ToUpper().Contains(txtboxFiltroNombre.Text.ToUpper()));
            dgvJugadores.DataSource = filtro;
            dgvJugadores.DataBind();
        }

        protected void FiltroAvanzado_Click(object sender, EventArgs e)
        {
            try
            {
                JugadorNegocio negocio = new JugadorNegocio();
                List<Jugador> jugador = negocio.FiltroAvanzado(ddlCategoria.SelectedItem.ToString(), ddlEstadoJugador.SelectedItem.ToString());
                dgvJugadores.DataSource = jugador;
                dgvJugadores.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void txtboxFiltroPosicion_TextChanged(object sender, EventArgs e)
        {
            if (lista == null && Session["listaJugadores"] != null)
            {
                lista = (List<Jugador>)Session["listaJugadores"];
            }
            List<Jugador> filtro = lista.FindAll(x => x.Posicion.ToUpper().Contains(txtboxFiltroPosicion.Text.ToUpper()));
            dgvJugadores.DataSource = filtro;
            dgvJugadores.DataBind();
        }

        protected void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idJugador = Convert.ToInt32(btn.CommandArgument);
                Session["idJugador"] = idJugador;

                if (btn.ID == "btnModificar")
                {
                    Response.Redirect("ConfigJugador.aspx?IdJugador=" + idJugador, false);
                }
                else if (btn.ID == "btnIncidencia")
                {
                    Response.Redirect("gestionIncidencias.aspx", false);
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }




        private void EliminarColumnas()
        {
            // Eliminar las últimas dos columnas (Modificar y Gestión Incidencias)
            // Asegúrate de eliminar en el orden correcto (de atrás hacia adelante)
            if (dgvJugadores.Columns.Count > 0)
            {
                dgvJugadores.Columns.RemoveAt(dgvJugadores.Columns.Count - 1);  // Eliminar la columna "Gestión Incidencias"
            }
            if (dgvJugadores.Columns.Count > 0)
            {
                dgvJugadores.Columns.RemoveAt(dgvJugadores.Columns.Count - 1);  // Eliminar la columna "Modificar"
            }
        }

    }
}