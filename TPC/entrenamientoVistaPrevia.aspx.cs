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
    public partial class entrenamientoVistaPrevia : System.Web.UI.Page
    {
        public int tipoPagina;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();

                    tipoPagina = Convert.ToInt32(Request.QueryString["id"]);
                    Session["tipoPagina"] = tipoPagina;

                    if (Session["jugadoresSeleccionados"] != null)
                    {
                        List<int> jugadoresSeleccionados = (List<int>)Session["jugadoresSeleccionados"];
                        JugadorNegocio negocioJugador = new JugadorNegocio();
                        List<Jugador> listaJugadores = negocioJugador.ObtenerJugadoresPorIds(jugadoresSeleccionados);
                        dgvJugadoresSeleccionados.DataSource = listaJugadores;
                        dgvJugadoresSeleccionados.DataBind();
                    }
                    else
                    {
                        lblMensaje.CssClass = "alert alert-warning";
                        lblMensaje.Text = "Aún no hay jugadores seleccionados.";
                        lblMensaje.Visible = true;
                    }

                    if ((int)Session["tipoPagina"] == 1)
                    {
                        txtDuracion.Text = "00:00";
                    }

                    if ((int)Session["tipoPagina"] == 2)
                    {
                        Entrenamiento entrenamiento = (Entrenamiento)Session["entrenamientoSeleccionado"];
                        txtDuracion.Enabled = false;
                        txtDescripcion.Enabled = false;
                        txtObservaciones.Enabled = false;
                        lblDetallesEntrenamiento.CssClass = "alert alert-info";
                        lblDetallesEntrenamiento.Text = $"El entrenamiento de la categoría '{entrenamiento.Categoria.NombreCategoria}' está organizado para el {entrenamiento.FechaHora.ToString("dddd, dd MMMM yyyy")} a las {entrenamiento.FechaHora.ToString("HH:mm")}.";
                        txtDuracion.Text = entrenamiento.Duracion.ToString();
                        txtDescripcion.Text = entrenamiento.Descripcion;
                        txtObservaciones.Text = entrenamiento.Observaciones;
                    }

                    if ((int)Session["tipoPagina"] == 3)
                    {
                        List<Categoria> listaCategorias = negocioCategoria.listar();
                        ddlCategoria.DataSource = listaCategorias;
                        ddlCategoria.DataTextField = "NombreCategoria";
                        ddlCategoria.DataValueField = "IdCategoria";
                        ddlCategoria.DataBind();
                        ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
                        int categoriaSeleccionadaId = (int)Session["categoriaSeleccionada"];
                        ddlCategoria.SelectedValue = categoriaSeleccionadaId.ToString();

                        DateTime fechaHoraEntrenamiento = (DateTime)Session["fechaHoraEntrenamiento"];
                        DateTime fechaEntrenamiento = fechaHoraEntrenamiento.Date;
                        TimeSpan horaEntrenamiento = fechaHoraEntrenamiento.TimeOfDay;
                        txtFechaEntrenamiento.Text = fechaEntrenamiento.ToString("yyyy-MM-dd");            // Formato de fecha
                        txtHoraEntrenamiento.Text = horaEntrenamiento.ToString(@"hh\:mm");

                        txtDuracion.Text = (string)Session["duracionEntrenamiento"];
                        txtDescripcion.Text = (string)Session["descripcionEntrenamiento"];
                        txtObservaciones.Text = (string)Session["observacionesEntrenamiento"];
                    }

                    if ((int)Session["tipoPagina"] == 2 || (int)Session["tipoPagina"] == 3)
                    {
                        txtObservaciones.Visible = true;
                    }

                    if ((int)Session["tipoPagina"] == 1 || (int)Session["tipoPagina"] == 3)
                    {
                        if (Session["fechaHoraEntrenamiento"] != null && Session["categoriaSeleccionada"] != null)
                        {
                            DateTime fechaHoraEntrenamiento = (DateTime)Session["fechaHoraEntrenamiento"];

                            int idCategoriaSeleccionada = (int)Session["categoriaSeleccionada"];
                            List<Categoria> listaCategorias = negocioCategoria.listar();
                            Categoria categoria = listaCategorias.FirstOrDefault(x => x.IdCategoria == idCategoriaSeleccionada);
                            string categoriaSeleccionada = string.Empty;
                            if (categoria != null)
                            {
                                categoriaSeleccionada = categoria.NombreCategoria;
                            }
                            lblDetallesEntrenamiento.CssClass = "alert alert-info";
                            lblDetallesEntrenamiento.Text = $"El entrenamiento de la categoría '{categoriaSeleccionada}' está organizado para el {fechaHoraEntrenamiento.ToString("dddd, dd MMMM yyyy")} a las {fechaHoraEntrenamiento.ToString("HH:mm")}.";
                        }
                    }
                }
                Session["tipoPagina"] = tipoPagina;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionEntrenamiento.aspx");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            Entrenamiento entrenamiento = new Entrenamiento();
            EntrenamientoNegocio entrenamientoNegocio = new EntrenamientoNegocio();
            AsistenciaNegocio asistenciaNegocio = new AsistenciaNegocio();
            JugadorNegocio jugadorNegocio = new JugadorNegocio();

            try
            {
                Button botonPresionado = (Button)sender;

                DateTime fechaEntrenamiento;
                DateTime horaEntrenamiento;

                if (DateTime.TryParse(txtFechaEntrenamiento.Text, out fechaEntrenamiento) &&
                    DateTime.TryParse(txtHoraEntrenamiento.Text, out horaEntrenamiento))
                {
                    entrenamiento.FechaHora = fechaEntrenamiento.Date.Add(horaEntrenamiento.TimeOfDay);
                }

                entrenamiento.Duracion = verificarDuracion(txtDuracion.Text);
                entrenamiento.Descripcion = txtDescripcion.Text;
                entrenamiento.Categoria = new Categoria { IdCategoria = int.Parse(ddlCategoria.SelectedValue) };
                entrenamiento.Estado = new EstadoEntrenamiento { IdEstado = 1 }; // PROGRAMADO POR DEFAULT
                entrenamiento.Observaciones = string.Empty;

                List<int> jugadoresSeleccionadosIds = (List<int>)Session["jugadoresSeleccionados"];

                if (jugadoresSeleccionadosIds != null && jugadoresSeleccionadosIds.Count > 0)
                {
                    entrenamiento.JugadoresCitados = jugadorNegocio.ObtenerJugadoresPorIds(jugadoresSeleccionadosIds);
                }
                else
                {
                    entrenamiento.JugadoresCitados = new List<Jugador>();
                }

                if (botonPresionado.ID == "btnConfirmar")
                {
                    entrenamientoNegocio.agregarEntrenamiento(entrenamiento);
                    int idNuevoEntrenamiento = entrenamientoNegocio.obtenerUltimoEntrenamiento();
                    asistenciaNegocio.AgregarAsistenciaMultiple(idNuevoEntrenamiento, jugadoresSeleccionadosIds);
                }
                else if (botonPresionado.ID == "btnActualizar")
                {
                    entrenamiento.IdEntrenamiento = (int)Session["idEntrenamientoSeleccionado"];
                    entrenamientoNegocio.modificarEntrenamiento(entrenamiento);
                    asistenciaNegocio.ActualizarAsistencias(entrenamiento.IdEntrenamiento, jugadoresSeleccionadosIds);
                }

                Session.Remove("jugadoresSeleccionados");
                Session.Remove("categoriaSeleccionada");
                Session.Remove("fechaHoraEntrenamiento");

               // string script = botonPresionado.ID == "btnConfirmar"
               //  ? "alert('Entrenamiento agregado correctamente'); window.location = 'gestionEntrenamiento.aspx';"
                // : "alert('Entrenamiento modificado correctamente'); window.location = 'entrenamientosProgramados.aspx';";

                //ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect", script, true);
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected TimeSpan verificarDuracion(string duracion)
        {
            if (!string.IsNullOrEmpty(duracion))
            {
                try
                {
                    TimeSpan duracionEntrenamiento = TimeSpan.Parse(duracion);
                    return duracionEntrenamiento;
                }
                catch (FormatException ex)
                {
                    lblMensaje.Text = "Formato de duración inválido. Por favor, usa el formato hh:mm.";
                    lblMensaje.Visible = true;
                    Session.Add("error", ex);
                }
            }
            return TimeSpan.Zero;
        }

        protected void btnVolverDetalle_Click(object sender, EventArgs e)
        {
            Response.Redirect("entrenamientosProgramados.aspx");
        }

        protected void btnAgregarJugadores_Click(object sender, EventArgs e)
        {
            DateTime fechaEntrenamiento;
            DateTime horaEntrenamiento;

            if (DateTime.TryParse(txtFechaEntrenamiento.Text, out fechaEntrenamiento) &&
                DateTime.TryParse(txtHoraEntrenamiento.Text, out horaEntrenamiento))
            {
                DateTime fechaHoraEntrenamiento = fechaEntrenamiento.Date.Add(horaEntrenamiento.TimeOfDay);
                Session["fechaHoraEntrenamiento"] = fechaHoraEntrenamiento;
            }

            Session["categoriaSeleccionada"] = int.Parse(ddlCategoria.SelectedValue);
            Session["duracionEntrenamiento"] = txtDuracion.Text;
            Session["descripcionEntrenamiento"] = txtDescripcion.Text;
            Session["observacionesEntrenamiento"] = txtObservaciones.Text;

            Response.Redirect("gestionEntrenamiento.aspx?id=1");
        }
    }
}