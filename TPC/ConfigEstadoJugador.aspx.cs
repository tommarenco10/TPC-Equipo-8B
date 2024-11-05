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
    public partial class ConfigEstadoJugador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDataGridView();
            }
        }

        protected void dgvEstadosJugador_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                lblIdEstadoJugador.Visible = true;
                txtIdEstadoJugador.Visible = true;
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
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
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
            btnGuardarModificacion.Visible = false;
            btnGuardarAgregado.Visible = true;
            btnGuardarEliminacion.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
            EstadoJugador estadoJugador = new EstadoJugador();
            try
            {
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
                        btnGuardarModificacion.Enabled = false;
                    }

                    txtIdEstadoJugador.Text = string.Empty;
                    txtNombreEstado.Text = string.Empty;
                    cargarDataGridView();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void cargarDataGridView()
        {
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
            try
            {
                dgvEstadosJugador.DataSource = negocioEJ.listar();
                dgvEstadosJugador.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void CargarFormulario()
        {
            try
            {
                if (Session["IdEstadoSeleccionado"] != null)
                {
                    int idEstado = Convert.ToInt32(Session["IdEstadoSeleccionado"]);

                    EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
                    List<EstadoJugador> listaEstados = negocioEJ.listar();

                    EstadoJugador estadoSeleccionado = listaEstados.FirstOrDefault(x => x.IdEstadoJugador == idEstado);
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
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected bool validarEliminacion()
        {
            JugadorNegocio negocioJugador = new JugadorNegocio();
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();

            try
            {
                List<Jugador> listaJugador = negocioJugador.listar();

                if (Session["IdEstadoSeleccionado"] != null)
                {
                    foreach (Jugador jugador in listaJugador)
                    {
                        if (jugador.estadoJugador.IdEstadoJugador == (int)Session["IdEstadoSeleccionado"])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
                return false;
            }
        }

        protected void btnGuardarEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarEliminacion())
                {
                    EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
                    negocioEJ.eliminar((int)Session["IdEstadoSeleccionado"]);
                    lblMensaje.Text = "Estado eliminado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                    txtIdEstadoJugador.Text = string.Empty;
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
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }


    }
}