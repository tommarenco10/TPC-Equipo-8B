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
                ddlCategoria.Items.Insert(0, new ListItem("Seleccionar", "0"));

                EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
                ddlEstadoJugador.DataSource = negocioEJ.listar();
                ddlEstadoJugador.DataTextField = "NombreEstado";
                ddlEstadoJugador.DataBind();
                ddlEstadoJugador.Items.Insert(0, new ListItem("Seleccionar", "0"));

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
            aplicarFiltros();
        }



        protected void txtboxFiltroPosicion_TextChanged(object sender, EventArgs e)
        {
            aplicarFiltros();
        }


        protected void aplicarFiltros()
        {

            List<Jugador> listaFiltrada = lista;
            bool conCambios=false;

            if (!string.IsNullOrWhiteSpace(txtboxFiltroNombre.Text))
            {
                listaFiltrada = listaFiltrada.FindAll(x => x.Apellidos.ToUpper().Contains(txtboxFiltroNombre.Text.ToUpper()) || x.Nombres.ToUpper().Contains(txtboxFiltroNombre.Text.ToUpper()));
                conCambios = true;

            }


            if (!string.IsNullOrWhiteSpace(txtboxFiltroPosicion.Text))
            {
                listaFiltrada = listaFiltrada.FindAll(x => x.Posicion.ToUpper().Contains(txtboxFiltroPosicion.Text.ToUpper()));
                conCambios = true;
            }


            if (ddlCategoria.SelectedIndex!= 0)
            {
                string seleccionado = ddlCategoria.SelectedItem.Text;
                listaFiltrada = listaFiltrada.FindAll(x => x.Categoria.NombreCategoria == seleccionado);
                conCambios = true;
            }

            if (ddlEstadoJugador.SelectedIndex != 0)
            {
                string seleccionado = ddlEstadoJugador.SelectedItem.Text;
                listaFiltrada = listaFiltrada.FindAll(x => x.estadoJugador.NombreEstado == seleccionado);
                conCambios = true;
            }


            if (!conCambios)
            {
                listaFiltrada = lista;
            }

            dgvJugadores.DataSource = listaFiltrada;
            dgvJugadores.DataBind();

        }

        protected void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idJugador = Convert.ToInt32(btn.CommandArgument);
                Session.Add("idJugador",idJugador);

                if (btn.ID == "btnModificar")
                {
                    Response.Redirect("ConfigJugador.aspx",false);
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

        protected void EliminarColumnas()

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

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            aplicarFiltros();
        }

        protected void ddlEstadoJugador_SelectedIndexChanged(object sender, EventArgs e)
        {
            aplicarFiltros();
        }
    }
}