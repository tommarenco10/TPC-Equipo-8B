using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace TPC
{
    public partial class ConfigEstados : System.Web.UI.Page
    {
        public int tipoPagina;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tipoPagina = Convert.ToInt32(Request.QueryString["id"]);
                Session["tipoPagina"] = tipoPagina;

                cargarDataGridView();
            }
        }

        protected void dgvEstados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
                    txtNombreEstado.Enabled = true;
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
                    txtNombreEstado.Enabled = false;
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
            txtNombreEstado.Enabled = true;
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
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
            EstadoJugador estadoJugador = new EstadoJugador();
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
            EstadoEntrenamiento estadoEntrenamiento = new EstadoEntrenamiento();

            try
            {
                if (validarAccion())
                {
                    IEnumerable<dynamic> listaEstados;

                    if ((int)Session["tipoPagina"] == 1)
                    {
                        listaEstados = negocioEJ.listar();
                    }
                    else
                    {
                        listaEstados = negocioEE.listar();
                    }

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
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Visible = true;

                        if ((int)Session["tipoPagina"] == 1)
                        {
                            estadoJugador.NombreEstado = nombreEstado;

                            if (string.IsNullOrEmpty(txtIdEstado.Text))
                            {
                                negocioEJ.agregar(estadoJugador);
                                lblMensaje.Text = "Estado agregado exitosamente.";
                                lblMensaje.ForeColor = System.Drawing.Color.Green;
                                lblMensaje.Visible = true;
                            }
                            else
                            {
                                estadoJugador.IdEstado = int.Parse(txtIdEstado.Text);
                                negocioEJ.modificar(estadoJugador);
                                lblMensaje.Text = "Estado modificado exitosamente.";

                                btnGuardarModificacion.Enabled = false;
                            }
                        }
                        else if ((int)Session["tipoPagina"] == 2)
                        {

                            estadoEntrenamiento.NombreEstado = nombreEstado;

                            if (string.IsNullOrEmpty(txtIdEstado.Text))
                            {
                                negocioEE.agregar(estadoEntrenamiento);
                                lblMensaje.Text = "Estado agregado exitosamente.";
                            }
                            else
                            {
                                estadoEntrenamiento.IdEstado = int.Parse(txtIdEstado.Text);
                                negocioEE.modificar(estadoEntrenamiento);
                                lblMensaje.Text = "Estado modificado exitosamente.";
                                btnGuardarModificacion.Enabled = false;
                            }
                        }

                        txtIdEstado.Text = string.Empty;
                        txtNombreEstado.Text = string.Empty;
                        cargarDataGridView();
                    }
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    if ((int)Session["IdEstadoSeleccionado"] != 1 &&
                        (int)Session["IdEstadoSeleccionado"] != 2 &&
                        (int)Session["IdEstadoSeleccionado"] != 3 &&
                        (int)Session["IdEstadoSeleccionado"] != 4)
                    {
                        lblMensaje.Text = "No se puede realizar la modificación. El estado aún tiene registros asociados.";
                    }
                    else
                    {
                        lblMensaje.Text = "No se puede realizar la modificación. El estado es clave para el funcionamiento del programa.";
                    }
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
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();

            try
            {
                if ((int)Session["tipoPagina"] == 1)
                {
                    dgvEstados.DataSource = negocioEJ.listar();
                }
                else if ((int)Session["tipoPagina"] == 2)
                {
                    dgvEstados.DataSource = negocioEE.listar();
                }
                dgvEstados.DataBind();
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

                    EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
                    EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();

                    IEnumerable<dynamic> listaEstados;

                    if ((int)Session["tipoPagina"] == 1)
                    {
                        listaEstados = negocioEJ.listar();
                    }
                    else if ((int)Session["tipoPagina"] == 2)
                    {
                        listaEstados = negocioEE.listar();
                    }
                    else
                    {
                        listaEstados = null;
                    }

                    var estadoSeleccionado = listaEstados.FirstOrDefault(x => x.IdEstado == idEstado);

                    if (estadoSeleccionado != null)
                    {
                        txtIdEstado.Text = estadoSeleccionado.IdEstado.ToString();
                        txtNombreEstado.Text = estadoSeleccionado.NombreEstado;
                    }
                }
                else
                {
                    txtIdEstado.Text = string.Empty;
                    txtNombreEstado.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected bool validarAccion()
        {
            EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
            JugadorNegocio negocioJugador = new JugadorNegocio();
            EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
            EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();

            try
            {
                if ((int)Session["IdEstadoSeleccionado"] != 1 &&
                    (int)Session["IdEstadoSeleccionado"] != 2 &&
                    (int)Session["IdEstadoSeleccionado"] != 3 &&
                    (int)Session["IdEstadoSeleccionado"] != 4)
                {
                    IEnumerable<dynamic> lista;

                    if ((int)Session["tipoPagina"] == 1)
                    {
                        lista = negocioJugador.listar();
                    }
                    else if ((int)Session["tipoPagina"] == 2)
                    {
                        lista = negocioEntrenamiento.listar();
                    }
                    else
                    {
                        lista = null;
                    }

                    if (Session["IdEstadoSeleccionado"] != null)
                    {
                        if ((int)Session["tipoPagina"] == 1)
                        {
                            foreach (Jugador jugador in lista)
                            {
                                if (jugador.estadoJugador.IdEstado == (int)Session["IdEstadoSeleccionado"])
                                {
                                    return false;
                                }
                            }
                        }
                        else if ((int)Session["tipoPagina"] == 2)
                        {
                            foreach (Entrenamiento entrenamiento in lista)
                            {
                                if (entrenamiento.Estado.IdEstado == (int)Session["IdEstadoSeleccionado"])
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else { return false; }
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
                if (validarAccion())
                {
                    if ((int)Session["tipoPagina"] == 1)
                    {
                        EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();
                        negocioEJ.eliminar((int)Session["IdEstadoSeleccionado"]);
                    }
                    else if ((int)Session["tipoPagina"] == 2)
                    {
                        EstadoEntrenamientoNegocio negocioEE = new EstadoEntrenamientoNegocio();
                        negocioEE.eliminar((int)Session["IdEstadoSeleccionado"]);
                    }
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
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    if ((int)Session["IdEstadoSeleccionado"] != 1 &&
                        (int)Session["IdEstadoSeleccionado"] != 2 &&
                        (int)Session["IdEstadoSeleccionado"] != 3 &&
                        (int)Session["IdEstadoSeleccionado"] != 4)
                    {
                        lblMensaje.Text = "No se puede realizar la eliminación. El estado aún tiene registros asociados.";
                    }
                    else
                    {
                        lblMensaje.Text = "No se puede realizar la eliminación. El estado es clave para el funcionamiento del programa.";
                    }
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