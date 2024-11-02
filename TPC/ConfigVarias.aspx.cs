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
    public partial class ConfigVarias : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDataGridView();
            }
        }

        protected void cargarDataGridView()
        {
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
            dgvEstadosJugador.DataSource = negocioEJ.listar();
            dgvEstadosJugador.DataBind();
        }

        protected void dgvEstadosJugador_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblTitulo.Text = "Modificar Estado Existente:";
            lblIdEstadoJugador.Visible = true;
            txtIdEstadoJugador.Visible = true;
            lblNombreEstado.Visible = true;
            txtNombreEstado.Visible = true;
            btnModificar.Visible = true;
            btnAgregar.Visible = false;

            if (e.CommandName == "Modificar")
            {
                int idEstado = Convert.ToInt32(e.CommandArgument);
                Session["IdEstadoSeleccionado"] = idEstado;
                CargarFormulario();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
            EstadoJugador estadoJugador = new EstadoJugador();

            estadoJugador.NombreEstado = txtNombreEstado.Text;

            if (string.IsNullOrEmpty(txtIdEstadoJugador.Text))
            {
                negocioEJ.agregar(estadoJugador);
            }
            else
            {
                estadoJugador.IdEstadoJugador = int.Parse(txtIdEstadoJugador.Text);
                negocioEJ.modificar(estadoJugador);
            }

            txtIdEstadoJugador.Text = string.Empty;
            txtNombreEstado.Text = string.Empty;
            cargarDataGridView();
        }
        protected void CargarFormulario()
        {
            if (Session["IdEstadoSeleccionado"] != null)
            {
                int idEstado = Convert.ToInt32(Session["IdEstadoSeleccionado"]);

                EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
                List<EstadoJugador> listaEstados = negocioEJ.listar();

                EstadoJugador estadoSeleccionado = listaEstados.FirstOrDefault(e => e.IdEstadoJugador == idEstado);
                if (estadoSeleccionado != null)
                {
                    txtIdEstadoJugador.Text = estadoSeleccionado.IdEstadoJugador.ToString();
                    txtNombreEstado.Text = estadoSeleccionado.NombreEstado;
                }
            }
            else
            {
                txtIdEstadoJugador.Text = string.Empty;
                txtNombreEstado.Text = string.Empty;
            }
        }

        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Agregar Nuevo Estado:";
            lblIdEstadoJugador.Visible = true;
            txtIdEstadoJugador.Visible = true;
            lblNombreEstado.Visible = true;
            txtNombreEstado.Visible = true;
            txtIdEstadoJugador.Text = string.Empty;
            txtNombreEstado.Text = string.Empty;
            btnModificar.Visible = false;
            btnAgregar.Visible = true;
        }
    }
}