using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace TPC
{
    public partial class gestionIncidencias : System.Web.UI.Page
    {
        public int tipoPagina;
        private Incidencia incidencia;

        protected void Page_Load(object sender, EventArgs e)
        {
            IncidenciaNegocio incidenciaNegocio = new IncidenciaNegocio();
            ObservacionesConFechaNegocio observacionesNegocio = new ObservacionesConFechaNegocio();
            EstadoJugadorNegocio negocioEstado = new EstadoJugadorNegocio();

            if (Session["user"] != null)
            {
                Usuario logueado = (Usuario)Session["user"];
                if (!(Seguridad.esEntrenador(logueado) || Seguridad.esAdmin(logueado)))
                {
                    Session.Add("error", "Se necesitan permisos especiales para usar esta funcionalidad.");
                    Response.Redirect("Error.aspx");
                }
            }
            else
            {
                Session.Add("error", "Se necesitan permisos especiales para usar esta funcionalidad.");
                Response.Redirect("Error.aspx");
            }

            try
            {
                // VALIDO EN TODAS LAS CARGAS
                if (Session["tipoPagina"] != null)
                {
                    tipoPagina = (int)Session["tipoPagina"];
                }

                // 1era EJECUCIÓN
                if (!IsPostBack)
                {
                    // CONFIGURACIONES INICIALES

                    // GUARDA EL TIPO DE PÁGINA
                    tipoPagina = Convert.ToInt32(Request.QueryString["id"]);
                    Session["tipoPagina"] = tipoPagina;

                    // GUARDA LA INCIDENCIA QUE RECIBE DE SESSION
                    incidencia = (Incidencia)Session["incidenciaSeleccionada"];
                    if ((int)Session["tipoPagina"] != 2 && (int)Session["tipoPagina"] != 3)
                    {
                        Session.Remove("incidenciaSeleccionada");
                    }

                    if (Session["idJugador"] != null)
                    {
                        CargarJugador((int)Session["idJugador"]);
                    }

                    configuracionesTipoPagina((int)Session["tipoPagina"]);
                    cargaDGVObservaciones();

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

                    if (incidencia != null)
                    {
                        // Fechas
                        txtFechaRegistro.Text = incidencia.FechaRegistro.ToString("yyyy-MM-dd");
                        txtFechaResolucion.Text = incidencia.FechaResolución.ToString("yyyy-MM-dd");
                        // Tipo de Incidencia
                        int idTipoIncidencia = incidencia.EstadoJugador.IdEstado;
                        ddlTipoIncidencia.SelectedValue = idTipoIncidencia.ToString();
                        // Descripción
                        txtDescripcion.Text = incidencia.Descripcion.ToString();
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
                txtCategoria.Text = jugador.Categoria.NombreCategoria;
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
                txtNombreApellido.Enabled = false;
                txtPosicion.Enabled = false;
                txtFechaNacimiento.Enabled = false;
                txtNacionalidad.Enabled = false;
                txtAltura.Enabled = false;
                txtPeso.Enabled = false;
                txtCategoria.Enabled = false;

                // TIPO DE PÁGINA 1 O NULL: AGREGAR, 2: VER DETALLE, 3: MODIFICAR

                if ((int)Session["tipoPagina"] != 1)
                {
                    pnlObservaciones.Visible = ddlTipoIncidencia.SelectedValue == "2"; // Mostrar solo para lesiones
                }

                if ((int)Session["tipoPagina"] != 2)
                {
                    btnModificar.Visible = false;
                }

                if ((int)Session["tipoPagina"] != 3)
                {
                    btnActualizarIncidencia.Visible = false;
                }

                if ((int)Session["tipoPagina"] == 2)
                {
                    ddlTipoIncidencia.Enabled = false;
                    txtFechaRegistro.Enabled = false;
                    txtFechaResolucion.Enabled = false;
                    txtDescripcion.Enabled = false;
                    ddlTipoIncidencia.CssClass = "form-select custom - bg - darker";
                    txtFechaRegistro.CssClass = "form-control custom - bg - darker";
                    txtFechaResolucion.CssClass = "form-control custom - bg - darker";
                    txtDescripcion.CssClass = "form-control custom - bg - darker";
                    txtFechaObservacion.Enabled = false;
                    txtDescripcionObservacion.Enabled = false;
                    txtFechaObservacion.CssClass = "form-control custom - bg - darker";
                    txtDescripcionObservacion.CssClass = "form-control custom - bg - darker";
                    btnAgregarObservacion.Visible = false;
                }

                if ((int)Session["tipoPagina"] == 2 || (int)Session["tipoPagina"] == 3)
                {
                    btnGuardarIncidencia.Visible = false;
                    btnResumen.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void cargaDGVObservaciones()
        {
            List<ObservacionConFecha> listaObservaciones = new List<ObservacionConFecha>();
            List<ObservacionConFecha> listaFiltrada = new List<ObservacionConFecha>();
            ObservacionesConFechaNegocio observacionesConFechaNegocio = new ObservacionesConFechaNegocio();
            int idIncidencia = 0;
            if ((Incidencia)Session["incidenciaSeleccionada"] != null)
            {
                incidencia = (Incidencia)Session["incidenciaSeleccionada"];
                idIncidencia = incidencia.IdIncidencia;
            }
            listaObservaciones = observacionesConFechaNegocio.listarAscendentePorIncidencia(idIncidencia);

            if (listaObservaciones != null)
            {
                foreach (ObservacionConFecha observacion in listaObservaciones)
                {
                    if (observacion.IdIncidencia == idIncidencia)
                    {
                        listaFiltrada.Add(observacion);
                    }
                }
            }

            dgvObservaciones.DataSource = listaFiltrada;
            dgvObservaciones.DataBind();
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
                    lblError.Text = "Por favor, complete el campo de descripción.";
                    return false;
                }
                else
                {
                    descripcionIncidencia = txtDescripcion.Text.ToString();
                }

                //GUARDADO DEL OBJETO VALIDADO EN SESSION
                if (Session["incidenciaSelecccionada"] != null)
                {
                    incidencia = (Incidencia)Session["incidenciaSelecccionada"];
                }

                if (incidencia == null)
                {
                    incidencia = new Incidencia();
                }

                incidencia.EstadoJugador.IdEstado = idTipoIncidencia;
                incidencia.Descripcion = descripcionIncidencia;
                incidencia.FechaRegistro = fechaRegistro;
                incidencia.FechaResolución = fechaResolucion;
                Session["incidenciaSeleccionada"] = incidencia;
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
            Incidencia incidencia = new Incidencia();
            IncidenciaNegocio incidenciaNegocio = new IncidenciaNegocio();
            JugadorNegocio jugadorNegocio = new JugadorNegocio();

            try
            {
                if (validaciones())
                {
                    incidencia = (Incidencia)Session["incidenciaSeleccionada"];
                    int idJugador = (int)Session["idJugador"];
                    incidencia.IdJugador = idJugador;
                    incidencia.Estado = true; //ABIERTA POR DEFECTO                   

                    if ((int)Session["tipoPagina"] == 2) { }
                    else
                    {
                        incidenciaNegocio.agregar(incidencia);
                        jugadorNegocio.actualizarEstadoPorNuevaIncidencia(idJugador, incidencia);
                        //jugadorNegocio.actualizarEstadoPorFechaYGravedadIncidencia(idJugador);
                    }

                    string script = (int)Session["tipoPagina"] == 2
                        ? "alert('Incidencia modificada correctamente'); window.location = 'PlantillaJugadores.aspx';"
                        : "alert('Incidencia agregada correctamente'); window.location = 'PlantillaJugadores.aspx';";
                    Session.Remove("incidenciaSeleccionada");
                    ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect", script, true);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAgregarObservacion_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaObservacion;
                string descripcion;

                //VALIDAR FECHAS
                if (string.IsNullOrEmpty(txtFechaObservacion.Text))
                {
                    lblErrorObs.CssClass = "alert alert-warning";
                    lblErrorObs.Text = "Por favor, complete el campo de fecha de observacion.";
                    return;
                }

                else if (!DateTime.TryParse(txtFechaObservacion.Text, out fechaObservacion))
                {
                    lblErrorObs.CssClass = "alert alert-danger";
                    lblErrorObs.Text = "Fecha de observación no válida. Por favor, ingrese una fecha válida.";
                    return;
                }

                //VALIDAR DESCRIPCIÓN
                if (string.IsNullOrEmpty(txtDescripcionObservacion.Text))
                {
                    lblErrorObs.CssClass = "alert alert-warning";
                    lblErrorObs.Text = "Por favor, complete el campo de descripción.";
                    return;
                }
                else
                {
                    descripcion = txtDescripcionObservacion.Text.ToString();
                }

                if ((Incidencia)Session["incidenciaSeleccionada"] != null)
                {
                    incidencia = (Incidencia)Session["incidenciaSeleccionada"];
                    ObservacionConFecha observacion = new ObservacionConFecha();
                    observacion.IdIncidencia = incidencia.IdIncidencia;
                    observacion.Fecha = fechaObservacion;
                    observacion.Descripcion = descripcion;
                    ObservacionesConFechaNegocio observacionNegocio = new ObservacionesConFechaNegocio();
                    observacionNegocio.agregar(observacion);
                    cargaDGVObservaciones();
                }
                else
                {
                    Incidencia incidencia = new Incidencia();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnResumen_Click(object sender, EventArgs e)
        {
            Response.Redirect("incidenciasActualizables.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            if ((int)Session["tipoPagina"] == 2 || (int)Session["tipoPagina"] == 3)
            {
                Response.Redirect("incidenciasActualizables.aspx");
            }
            else
            {
                Response.Redirect("PlantillaJugadores.aspx");
            }
        }

        protected void btnActualizarIncidencia_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionIncidencias.aspx?id=3"); //FUNCION ACTUALIZAR
        }
    }
}