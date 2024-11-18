using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class gestionIncidencias : System.Web.UI.Page
    {
        private Incidencia incidencia;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EstadoJugadorNegocio negocioEstado = new EstadoJugadorNegocio();

                if (!IsPostBack)
                {
                    if (Request.QueryString["IdJugador"] != null)
                    {
                        int idJugador = Convert.ToInt32(Request.QueryString["IdJugador"]);
                        Session["idJugador"] = idJugador;
                        CargarJugador(idJugador);
                    }

                    int tipoPagina = Convert.ToInt32(Request.QueryString["tipoPagina"]);
                    Session["tipoPagina"] = tipoPagina;
                    configuracionesTipoPagina((int)Session["tipoPagina"]);

                    List<EstadoJugador> listaEstados = negocioEstado.listar();
                    ddlTipoIncidencia.DataSource = listaEstados;
                    ddlTipoIncidencia.DataTextField = "NombreEstado";
                    ddlTipoIncidencia.DataValueField = "IdEstado";
                    ddlTipoIncidencia.DataBind();
                    ddlTipoIncidencia.Items.Insert(0, new ListItem("Seleccione un tipo", "0"));

                    ListItem item = ddlTipoIncidencia.Items.FindByValue("1");
                    if (item != null)
                    {
                        item.Text = "Disponible/Recuperado";
                    }
                }
                configuracionesTipoPagina((int)Session["tipoPagina"]);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
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
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void configuracionesTipoPagina(int tipoPagina)
        {
            try
            {
                if (tipoPagina != 2)
                {
                    txtNombreApellido.Enabled = false;
                    txtPosicion.Enabled = false;
                    txtFechaNacimiento.Enabled = false;
                    txtNacionalidad.Enabled = false;
                    txtAltura.Enabled = false;
                    txtPeso.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected bool validaciones()
        {
            int idTipoIncidencia;
            DateTime fechaRegistro;
            DateTime fechaResolucion;
            string descripcionIncidencia;

            try
            {
                //VALIDAR TIPO DE INCIDENCIA
                if (int.Parse(ddlTipoIncidencia.SelectedValue) == 0)
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, seleccione un tipo de incidencia.";
                    return false;
                }
                else
                {
                    idTipoIncidencia = int.Parse(ddlTipoIncidencia.SelectedValue);
                }

                //VALIDAR FECHAS
                if (string.IsNullOrEmpty(txtFechaRegistro.Text) || string.IsNullOrEmpty(txtFechaResolucion.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete los campos de fecha de registro y resolución.";
                    return false;
                }

                else if (!DateTime.TryParse(txtFechaRegistro.Text, out fechaRegistro))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha de Registro no válida. Por favor, ingrese una fecha válida.";
                    return false;
                }

                else if (!DateTime.TryParse(txtFechaResolucion.Text, out fechaResolucion))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha de Resolución no válida. Por favor, ingrese una fecha válida.";
                    return false;
                }

                else if (fechaResolucion.Date < fechaRegistro.Date)
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha no válida. La fecha de resolución no puede ser anterior a la de registro.";
                    return false;
                }

                //VALIDAR DESCRIPCIÓN
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete el campo de duración.";
                    return false;
                }
                else
                {
                    descripcionIncidencia = txtDescripcion.Text.ToString();
                }

                //GUARDADO DEL OBJETO VALIDADO EN SESSION

                incidencia = (Incidencia)Session["incidenciaSelecccionada"];

                if (incidencia == null)
                {
                    incidencia = new Incidencia();
                }

                incidencia.IdJugador = (int)Session["idJugador"];
                incidencia.IdEstadoJugador = idTipoIncidencia;
                incidencia.Descripcion = descripcionIncidencia;
                incidencia.Estado = true; //ABIERTA POR DEFECTO
                incidencia.FechaRegistro = fechaRegistro;
                incidencia.FechaResolución = fechaResolucion;
                Session["incidenciaSelecccionada"] = incidencia;
                return true;
            }
            catch (ThreadAbortException) { return false; }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
                return false;
            }
        }

        protected void btnGuardarIncidencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaciones())
                {
                    Incidencia incidencia = new Incidencia
                    {
                        IdJugador = Convert.ToInt32(Request.QueryString["idJugador"]),
                        Descripcion = txtDescripcion.Text,
                        FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text),
                        FechaResolución = Convert.ToDateTime(txtFechaResolucion.Text),
                        IdEstadoJugador = Convert.ToInt32(ddlTipoIncidencia.SelectedValue),
                        Estado = true
                    };

                    IncidenciaNegocio negocioIncidencia = new IncidenciaNegocio();
                    negocioIncidencia.agregar(incidencia);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void ddlTipoIncidencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlObservaciones.Visible = ddlTipoIncidencia.SelectedValue == "1"; // Mostrar solo para lesiones
        }

        protected void btnAgregarObservacion_Click(object sender, EventArgs e)
        {
            try
            {
                List<ObservacionConFecha> observaciones = Session["Observaciones"] != null
                    ? (List<ObservacionConFecha>)Session["Observaciones"]
                    : new List<ObservacionConFecha>();

                observaciones.Add(new ObservacionConFecha
                {
                    Fecha = Convert.ToDateTime(txtFechaObservacion.Text),
                    Descripcion = txtDescripcionObservacion.Text
                });

                Session["Observaciones"] = observaciones;

                gvObservaciones.DataSource = observaciones;
                gvObservaciones.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

    
    }
}