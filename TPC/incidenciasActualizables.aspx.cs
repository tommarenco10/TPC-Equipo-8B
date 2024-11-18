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
    public partial class incidenciasActualizables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idJugador"] != null)
                {
                    CargarJugador((int)Session["idJugador"]);
                }
                
                IncidenciaNegocio incidenciaNegocio = new IncidenciaNegocio();
                List<Incidencia> listaIncidencias = incidenciaNegocio.listarPorJugador((int)Session["idJugador"]);
                dgvIncidencias.DataSource = listaIncidencias;
                dgvIncidencias.DataBind();
            }
        }

        private void CargarJugador(int idJugador)
        {
            try
            {
                JugadorNegocio negocioJugador = new JugadorNegocio();
                Jugador jugador = negocioJugador.ObtenerJugadorPorId(idJugador);

                txtNombreApellido.Text = jugador.Apellidos + ", " + jugador.Nombres;
                txtNacionalidad.Text = jugador.LugarNacimiento.Pais;
                txtPosicion.Text = jugador.Posicion;
                int edad = DateTime.Now.Year - jugador.FechaNacimiento.Year;
                txtFechaNacimiento.Text = jugador.FechaNacimiento.ToShortDateString() + " (" + edad + ")";
                decimal altura = (decimal)jugador.Altura / 100;
                txtAltura.Text = altura.ToString("N2") + " m";
                txtPeso.Text = jugador.Peso.ToString("N1") + " kg";
                imgJugador.ImageUrl = jugador.UrlImagen;
                txtCategoria.Text = jugador.Categoria.NombreCategoria;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void dgvIncidencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Incidencia incidencia = (Incidencia)e.Row.DataItem;

                int estadoColumnIndex = 4;
                string estadoTexto = "Abierta";

                if (!incidencia.Estado)
                {
                    estadoTexto = "Cerrada";
                }

                e.Row.Cells[estadoColumnIndex].Text = estadoTexto;
            }
        }

        protected void btnAccion_Click(object sender, EventArgs e)
        {

        }


    }
}