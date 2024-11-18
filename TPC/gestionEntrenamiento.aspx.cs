using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Acceso_Datos;
using Negocio;
using System.Threading;


namespace TPC
{
    public partial class gestionEntrenamiento : System.Web.UI.Page
    {
        public int tipoPagina;
        private Entrenamiento entrenamiento;

        private List<Jugador> listaJugadores;

        protected void Page_Load(object sender, EventArgs e)
        {
            JugadorNegocio negocioJugador = new JugadorNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();

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

                    // GUARDA EL ENTRENAMIENTO QUE RECIBE DE SESSION
                    entrenamiento = (Entrenamiento)Session["entrenamientoSeleccionado"];

                    // GUARDA EL TIPO DE PÁGINA
                    tipoPagina = Convert.ToInt32(Request.QueryString["id"]);
                    Session["tipoPagina"] = tipoPagina;

                    // GUARDA LA LISTA DE JUGADORES COMPLETA
                    listaJugadores = negocioJugador.listar();
                    Session["listaJugadores"] = listaJugadores;

                    // PRECARGA DDL DE CATEGORIAS PARA ELEGIR JUGADORES
                    List<Categoria> listaCategorias = negocioCategoria.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
                    ddlJugadoresAdicionales.DataSource = listaCategorias;
                    ddlJugadoresAdicionales.DataTextField = "NombreCategoria";
                    ddlJugadoresAdicionales.DataValueField = "IdCategoria";
                    ddlJugadoresAdicionales.DataBind();
                    ddlJugadoresAdicionales.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

                    // CONFIGURACIONES SEGÚN TIPO DE PÁGINA

                    // SI TIPO DE PÁGINA 1: AGREGAR - VUELTA DESDE VISTA PREVIA
                    if ((int)Session["tipoPagina"] == 1)
                    {

                    }

                    // SI TIPO DE PÁGINA 2: MODIFICAR - INGRESO PROGRAMADO O VUELTA DESDE VISTA PREVIA
                    else if ((int)Session["tipoPagina"] == 2)
                    {
                        //BACKUP DE JUGADORES EN CASO DE MODIFICACIÓN
                        Session["auxLista"] = new List<int>((List<int>)Session["jugadoresSeleccionados"]);
                    }

                    // OTRO CASO: AGREGAR - INGRESO NAVBAR, PROGRAMADO, O INESPERADO
                    else
                    {
                        // ARRANQUE VACÍO: REMOVE DE LOS SESSION
                        Session.Remove("jugadoresSeleccionados");
                        Session.Remove("auxLista");
                        Session.Remove("entrenamientoSeleccionado");
                        txtFechaEntrenamiento.Text = string.Empty;
                        txtHoraEntrenamiento.Text = string.Empty;
                        txtDuracion.Text = "00:00";
                        txtDescripcion.Text = string.Empty;
                    }

                    // RECUPERACIÓN DE DATOS

                    if (entrenamiento != null)
                    {
                        // Fecha y Hora del Entrenamiento
                        DateTime fechaHoraEntrenamiento = entrenamiento.FechaHora;
                        txtFechaEntrenamiento.Text = fechaHoraEntrenamiento.ToString("yyyy-MM-dd");
                        txtHoraEntrenamiento.Text = fechaHoraEntrenamiento.ToString("HH:mm");
                        // Categoría Principal del Entrenamiento
                        int idCategoriaSeleccionada = entrenamiento.Categoria.IdCategoria;
                        ddlCategoria.SelectedValue = idCategoriaSeleccionada.ToString();
                        // Duración del Entrenamiento
                        txtDuracion.Text = entrenamiento.Duracion.ToString();
                        // Descripción del Entrenamiento
                        txtDescripcion.Text = entrenamiento.Descripcion.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void cargaDGVJugadores(object sender, EventArgs e)
        {
            try
            {
                bool btnPreseleccionar = sender is Button;

                List<Jugador> listaFiltrada = new List<Jugador>();
                listaJugadores = (List<Jugador>)Session["listaJugadores"];
                int idCategoriaSeleccionada = btnPreseleccionar
                    ? int.Parse(ddlCategoria.SelectedValue)
                        : int.Parse(ddlJugadoresAdicionales.SelectedValue);

                if (listaJugadores != null)
                {
                    foreach (Jugador jugador in listaJugadores)
                    {
                        if (jugador.Categoria.IdCategoria == idCategoriaSeleccionada)
                        {
                            listaFiltrada.Add(jugador);
                        }
                    }
                }

                dgvEntrenamiento.DataSource = listaFiltrada;
                dgvEntrenamiento.DataBind();

                // Inicializar o recuperar la lista de jugadores seleccionados
                List<int> jugadoresSeleccionados = Session["jugadoresSeleccionados"] != null
                    ? (List<int>)Session["jugadoresSeleccionados"]
                    : new List<int>();

                foreach (GridViewRow row in dgvEntrenamiento.Rows)
                {
                    CheckBox chkCitado = (CheckBox)row.FindControl("chkCitado");
                    int idJugador = Convert.ToInt32(dgvEntrenamiento.DataKeys[row.RowIndex].Value);

                    if (jugadoresSeleccionados.Contains(idJugador) || btnPreseleccionar)
                    {
                        chkCitado.Checked = true;

                        if (btnPreseleccionar && !jugadoresSeleccionados.Contains(idJugador))
                        {
                            jugadoresSeleccionados.Add(idJugador);
                        }
                    }
                }

                if (btnPreseleccionar)
                {
                    Session["jugadoresSeleccionados"] = jugadoresSeleccionados;
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void chkCitado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["jugadoresSeleccionados"] == null)
                {
                    Session["jugadoresSeleccionados"] = new List<int>();
                }

                CheckBox chk = (CheckBox)sender;
                GridViewRow row = (GridViewRow)chk.NamingContainer;

                int idJugador = Convert.ToInt32(dgvEntrenamiento.DataKeys[row.RowIndex].Value);

                List<int> jugadoresSeleccionados = (List<int>)Session["jugadoresSeleccionados"];

                if (chk.Checked)
                {
                    // Agregar el jugador a la lista si no está ya en la lista
                    if (!jugadoresSeleccionados.Contains(idJugador))
                    {
                        jugadoresSeleccionados.Add(idJugador);
                    }
                }
                else
                {
                    // Remover el jugador de la lista si se deselecciona
                    jugadoresSeleccionados.Remove(idJugador);
                }
                Session["jugadoresSeleccionados"] = jugadoresSeleccionados;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected bool validaciones()
        {
            int idCategoriaSeleccionada;
            DateTime fechaHoraEntrenamiento;
            DateTime fechaEntrenamiento;
            TimeSpan horaEntrenamiento;
            TimeSpan duracionEntrenamiento;
            string descripcionEntrenamiento;

            try
            {
                //VALIDAR CATEGORIA
                if (int.Parse(ddlCategoria.SelectedValue) == 0)
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, seleccione una categoría.";
                    return false;
                }
                else
                {
                    idCategoriaSeleccionada = int.Parse(ddlCategoria.SelectedValue);
                    //Session["categoriaSeleccionada"] = idCategoriaSeleccionada;
                }

                //VALIDAR FECHA Y HORA
                if (string.IsNullOrEmpty(txtFechaEntrenamiento.Text) || string.IsNullOrEmpty(txtHoraEntrenamiento.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete los campos de fecha y hora.";
                    return false;
                }

                else if (!DateTime.TryParse(txtFechaEntrenamiento.Text, out fechaEntrenamiento))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha no válida. Por favor, ingrese una fecha válida.";
                    return false;
                }

                else if (!TimeSpan.TryParse(txtHoraEntrenamiento.Text, out horaEntrenamiento))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Hora no válida. Por favor, ingrese una hora válida.";
                    return false;
                }

                else if (fechaEntrenamiento.Date < DateTime.Today)
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha no válida. La fecha seleccionada no puede ser en el pasado.";
                    return false;
                }

                else if (fechaEntrenamiento.Date == DateTime.Today && horaEntrenamiento <= DateTime.Now.TimeOfDay)
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Hora no válida. La hora de entrenamiento no puede ser en el pasado.";
                    return false;
                }
                else
                {
                    fechaHoraEntrenamiento = fechaEntrenamiento.Date.Add(horaEntrenamiento);
                }

                //VALIDAR DURACIÓN
                if (string.IsNullOrEmpty(txtDuracion.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete el campo de duración.";
                    return false;
                }
                else if (!TimeSpan.TryParse(txtDuracion.Text, out duracionEntrenamiento))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Duración no válida. Por favor, ingrese una duración válida.";
                    return false;
                }

                //VALIDAR DESCRIPCIÓN
                if (string.IsNullOrEmpty(txtDuracion.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete el campo de duración.";
                    return false;
                }
                else
                {
                    descripcionEntrenamiento = txtDescripcion.Text.ToString();
                }

                //GUARDADO DEL OBJETO VALIDADO EN SESSION

                entrenamiento = (Entrenamiento)Session["entrenamientoSeleccionado"];

                if (entrenamiento == null)
                {
                    entrenamiento = new Entrenamiento();
                }
                if (entrenamiento.Categoria == null)
                {
                    entrenamiento.Categoria = new Categoria();
                }

                entrenamiento.Categoria.IdCategoria = idCategoriaSeleccionada;
                entrenamiento.FechaHora = fechaHoraEntrenamiento;
                entrenamiento.Duracion = duracionEntrenamiento;
                entrenamiento.Descripcion = descripcionEntrenamiento;
                Session["entrenamientoSeleccionado"] = entrenamiento;
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

        protected void btnVistaPrevia_Click(object sender, EventArgs e)
        {

            if (validaciones())
            {
                if ((int)Session["tipoPagina"] != 2)
                {
                    Response.Redirect("entrenamientoVistaPrevia.aspx?id=1");
                }
                else
                {
                    Response.Redirect("entrenamientoVistaPrevia.aspx?id=2");
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Entrenamiento entrenamiento = new Entrenamiento();
            EntrenamientoNegocio entrenamientoNegocio = new EntrenamientoNegocio();
            AsistenciaNegocio asistenciaNegocio = new AsistenciaNegocio();
            JugadorNegocio jugadorNegocio = new JugadorNegocio();

            try
            {
                if (validaciones())
                {
                    entrenamiento = (Entrenamiento)Session["entrenamientoSeleccionado"];
                    entrenamiento.Estado = new EstadoEntrenamiento { IdEstado = 1 }; // PROGRAMADO POR DEFAULT
                    entrenamiento.Observaciones = string.Empty;

                    List<int> jugadoresSeleccionadosIds = (List<int>)Session["jugadoresSeleccionados"];

                    if (jugadoresSeleccionadosIds != null && jugadoresSeleccionadosIds.Count > 0)
                    {
                        entrenamiento.JugadoresCitados = jugadorNegocio.ListarJugadoresPorIds(jugadoresSeleccionadosIds);
                    }
                    else
                    {
                        entrenamiento.JugadoresCitados = new List<Jugador>();
                    }


                    if ((int)Session["tipoPagina"] == 2)
                    {
                        entrenamientoNegocio.modificarEntrenamiento(entrenamiento);
                        asistenciaNegocio.ActualizarAsistencias(entrenamiento.IdEntrenamiento, jugadoresSeleccionadosIds);
                    }
                    else
                    {
                        entrenamientoNegocio.agregarEntrenamiento(entrenamiento);
                        int idNuevoEntrenamiento = entrenamientoNegocio.obtenerUltimoEntrenamiento();
                        asistenciaNegocio.AgregarAsistenciaMultiple(idNuevoEntrenamiento, jugadoresSeleccionadosIds);
                    }

                    string script = (int)Session["tipoPagina"] == 2
                        ? "alert('Entrenamiento modificado correctamente'); window.location = 'entrenamientoVistaPrevia.aspx?id=2';"
                        : "alert('Entrenamiento agregado correctamente'); window.location = 'entrenamientoVistaPrevia.aspx?id=1';";

                    ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect", script, true);
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnVolverSinGuardar_Click(object sender, EventArgs e)
        {
            Session["jugadoresSeleccionados"] = Session["auxLista"];
            Response.Redirect("entrenamientoVistaPrevia.aspx?id=2");
        }
    }
}
