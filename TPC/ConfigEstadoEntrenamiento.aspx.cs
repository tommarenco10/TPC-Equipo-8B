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
    public partial class ConfigEstadoEntrenamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDataGridView();
            }
        }

        protected void dgvEstadosEntrenamiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblTitulo.Text = "Modificar Estado Existente:";
            lblIdEstadoEntrenamiento.Visible = true;
            txtIdEstadoEntrenamiento.Visible = true;
            lblNombreEstado.Visible = true;
            txtNombreEstado.Visible = true;
            lblMensaje.Visible = false;
            btnGuardarModificacion.Visible = true;
            btnGuardarModificacion.Enabled = true;
            btnGuardarAgregado.Visible = false;

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
            lblIdEstadoEntrenamiento.Visible = true;
            txtIdEstadoEntrenamiento.Visible = true;
            lblNombreEstado.Visible = true;
            txtNombreEstado.Visible = true;
            lblMensaje.Visible = false;
            txtIdEstadoEntrenamiento.Text = string.Empty;
            txtNombreEstado.Text = string.Empty;
            btnGuardarModificacion.Visible = false;
            btnGuardarAgregado.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
            EstadoEntrenamiento estadoEntrenamiento = new EstadoEntrenamiento();

            List<EstadoEntrenamiento> listaEstados = negocioEE.listar();

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
                estadoEntrenamiento.NombreEstado = nombreEstado;

                if (string.IsNullOrEmpty(txtIdEstadoEntrenamiento.Text))
                {
                    negocioEE.agregar(estadoEntrenamiento);
                    lblMensaje.Text = "Estado agregado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                }
                else
                {
                    estadoEntrenamiento.IdEstadoEntrenamiento = int.Parse(txtIdEstadoEntrenamiento.Text);
                    negocioEE.modificar(estadoEntrenamiento);
                    lblMensaje.Text = "Estado modificado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                    btnGuardarModificacion.Enabled = false;
                }

                txtIdEstadoEntrenamiento.Text = string.Empty;
                txtNombreEstado.Text = string.Empty;
                cargarDataGridView();
            }
        }

        protected void cargarDataGridView()
        {
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
            dgvEstadosEntrenamiento.DataSource = negocioEE.listar();
            dgvEstadosEntrenamiento.DataBind();
        }

        protected void CargarFormulario()
        {
            if (Session["IdEstadoSeleccionado"] != null)
            {
                int idEstado = Convert.ToInt32(Session["IdEstadoSeleccionado"]);

                EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
                List<EstadoEntrenamiento> listaEstados = negocioEE.listar();

                EstadoEntrenamiento estadoSeleccionado = listaEstados.FirstOrDefault(e => e.IdEstadoEntrenamiento == idEstado);
                if (estadoSeleccionado != null)
                {
                    txtIdEstadoEntrenamiento.Text = estadoSeleccionado.IdEstadoEntrenamiento.ToString();
                    txtNombreEstado.Text = estadoSeleccionado.NombreEstado;
                }
            }
            else
            {
                txtIdEstadoEntrenamiento.Text = string.Empty;
                txtNombreEstado.Text = string.Empty;
            }
        }
    }
}