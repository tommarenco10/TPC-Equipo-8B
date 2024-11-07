using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class PlanillaJugadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JugadorNegocio negocio = new JugadorNegocio();
            Session.Add("listaJugadores", negocio.ListarJugador());
            dgvJugadores.DataSource = Session["listaJugadores"];
            dgvJugadores.DataBind();

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
            }
        }

        protected void dgv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvJugadores.SelectedDataKey != null)
            {
                var IdJugador = dgvJugadores.SelectedDataKey.Value.ToString();
                Response.Redirect("ConfigJugador.aspx?IdJugador=" + IdJugador, false);
            }
        }

        protected void txtboxFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            List<Jugador> lista = (List<Jugador>)Session["listajugadores"];
            List<Jugador> filtro = lista.FindAll(x => x.Apellidos.ToUpper().Contains(txtboxFiltroNombre.Text.ToUpper()));
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
            List<Jugador> lista = (List<Jugador>)Session["listajugadores"];
            List<Jugador> filtro = lista.FindAll(x => x.Posicion.ToUpper().Contains(txtboxFiltroPosicion.Text.ToUpper()));
            dgvJugadores.DataSource = filtro;
            dgvJugadores.DataBind();
        }
    }
}