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
            lblMensaje.Visible = false;
            btnModificar.Visible = true;
            btnModificar.Enabled = true;
            btnAgregar.Visible = false;

            if (e.CommandName == "Modificar")
            {
                int idEstado = Convert.ToInt32(e.CommandArgument);
                Session["IdEstadoSeleccionado"] = idEstado;
                CargarFormulario();
            }
        }

        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Agregar Nuevo Estado:";
            lblIdEstadoJugador.Visible = true;
            txtIdEstadoJugador.Visible = true;
            lblNombreEstado.Visible = true;
            txtNombreEstado.Visible = true;
            lblMensaje.Visible = false;
            txtIdEstadoJugador.Text = string.Empty;
            txtNombreEstado.Text = string.Empty;
            btnModificar.Visible = false;
            btnAgregar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
            EstadoJugador estadoJugador = new EstadoJugador();

            List<EstadoJugador> listaEstados = negocioEJ.listar();

            string nombreEstado = txtNombreEstado.Text;

            bool existeEstado = listaEstados.Any(x => x.NombreEstado.Equals(nombreEstado, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(nombreEstado))
            {
                lblMensaje.Text = "Por favor, complete la casilla de nombre.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }

            else if (existeEstado)
            {
                lblMensaje.Text = "El estado ya existe. Por favor, ingrese un nombre diferente.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }

            else
            {
                estadoJugador.NombreEstado = nombreEstado;

                if (string.IsNullOrEmpty(txtIdEstadoJugador.Text))
                {
                    negocioEJ.agregar(estadoJugador);
                    lblMensaje.Text = "Estado agregado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                }
                else
                {
                    estadoJugador.IdEstadoJugador = int.Parse(txtIdEstadoJugador.Text);
                    negocioEJ.modificar(estadoJugador);
                    lblMensaje.Text = "Estado modificado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                    btnModificar.Enabled = false;
                }

                txtIdEstadoJugador.Text = string.Empty;
                txtNombreEstado.Text = string.Empty;
                cargarDataGridView();
            }
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
    }
}