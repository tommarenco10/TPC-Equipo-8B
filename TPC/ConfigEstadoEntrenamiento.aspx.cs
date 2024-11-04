using Dominio;
using negocio;
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

        protected void dgvEstados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblIdEstado.Visible = true;
            txtIdEstado.Visible = true;
            lblNombreEstado.Visible = true;
            txtNombreEstado.Visible = true;
            lblMensaje.Visible = false;
            btnGuardarAgregado.Visible = false;

            if (e.CommandName == "Modificar")
            {
                lblTitulo.Text = "Modificar Estado Existente:";
                btnGuardarModificacion.Visible = true;
                btnGuardarModificacion.Enabled = true;
                btnGuardarEliminacion.Visible = false;
                int idEstado = Convert.ToInt32(e.CommandArgument);
                Session["IdEstadoSeleccionado"] = idEstado;
                CargarFormulario();
            }

            if (e.CommandName == "Eliminar")
            {
                lblTitulo.Text = "Está seguro que desea eliminar el Estado?";
                btnGuardarModificacion.Visible = false;
                btnGuardarEliminacion.Visible = true;
                btnGuardarEliminacion.Enabled = true;
                int idEstado = Convert.ToInt32(e.CommandArgument);
                Session["IdEstadoSeleccionado"] = idEstado;
                CargarFormulario();
            }
        }

        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Agregar Nuevo Estado:";
            lblIdEstado.Visible = true;
            txtIdEstado.Visible = true;
            lblNombreEstado.Visible = true;
            txtNombreEstado.Visible = true;
            lblMensaje.Visible = false;
            txtIdEstado.Text = string.Empty;
            txtNombreEstado.Text = string.Empty;
            btnGuardarModificacion.Visible = false;
            btnGuardarAgregado.Visible = true;
            btnGuardarEliminacion.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
            EstadoEntrenamiento estadoJugador = new EstadoEntrenamiento();

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
                estadoJugador.NombreEstado = nombreEstado;

                if (string.IsNullOrEmpty(txtIdEstado.Text))
                {
                    negocioEE.agregar(estadoJugador);
                    lblMensaje.Text = "Estado agregado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                }
                else
                {
                    estadoJugador.IdEstadoEntrenamiento = int.Parse(txtIdEstado.Text);
                    negocioEE.modificar(estadoJugador);
                    lblMensaje.Text = "Estado modificado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                    btnGuardarModificacion.Enabled = false;
                }

                txtIdEstado.Text = string.Empty;
                txtNombreEstado.Text = string.Empty;
                cargarDataGridView();
            }
        }

        protected void cargarDataGridView()
        {
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
            dgvEstados.DataSource = negocioEE.listar();
            dgvEstados.DataBind();
        }

        protected void CargarFormulario()
        {
            if (Session["IdEstadoSeleccionado"] != null)
            {
                int idEstado = Convert.ToInt32(Session["IdEstadoSeleccionado"]);

                EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
                List<EstadoEntrenamiento> listaEstados = negocioEE.listar();

                EstadoEntrenamiento estadoSeleccionado = listaEstados.FirstOrDefault(x => x.IdEstadoEntrenamiento == idEstado);
                if (estadoSeleccionado != null)
                {
                    txtIdEstado.Text = estadoSeleccionado.IdEstadoEntrenamiento.ToString();
                    txtNombreEstado.Text = estadoSeleccionado.NombreEstado;
                }
            }
            else
            {
                txtIdEstado.Text = string.Empty;
                txtNombreEstado.Text = string.Empty;
            }
        }

        protected bool validarEliminacion()
        {

            EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();

            List<Entrenamiento> listaEntrenamientos = negocioEntrenamiento.listar();

            if (Session["IdEstadoSeleccionado"] != null)
            {
                foreach (Entrenamiento entrenamiento in listaEntrenamientos)
                {
                    if (entrenamiento.Estado.IdEstadoEntrenamiento == (int)Session["IdEstadoSeleccionado"])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected void btnGuardarEliminacion_Click(object sender, EventArgs e)
        {
            if (validarEliminacion())
            {
                EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
                negocioEE.eliminar((int)Session["IdEstadoSeleccionado"]);
                lblMensaje.Text = "Estado eliminado exitosamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Visible = true;
                txtIdEstado.Text = string.Empty;
                txtNombreEstado.Text = string.Empty;
                btnGuardarEliminacion.Enabled = false;
                cargarDataGridView();
            }
            else
            {
                btnGuardarEliminacion.Enabled = false;
                lblMensaje.Visible = true;
                lblMensaje.Text = "No se puede realizar la eliminación. El estado aún tiene registros asociados";
            }
        }
    }
}