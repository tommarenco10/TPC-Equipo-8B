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
        private List<Jugador> listaJugadores;

        protected void Page_Load(object sender, EventArgs e)
        {
            JugadorNegocio negocioJugador = new JugadorNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();

            try
            {
                if (!IsPostBack)
                {
                    listaJugadores = negocioJugador.listar();
                    Session["listaJugadores"] = listaJugadores;

                    List<Categoria> listaCategorias = negocioCategoria.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();
                    ddlJugadoresAdicionales.DataSource = listaCategorias;
                    ddlJugadoresAdicionales.DataTextField = "NombreCategoria";
                    ddlJugadoresAdicionales.DataValueField = "IdCategoria";
                    ddlJugadoresAdicionales.DataBind();
                    // Opción para seleccionar
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
                    ddlJugadoresAdicionales.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

                    //RECUPERAR DATOS

                    // Categoria Seleccionada
                    if (Session["categoriaSeleccionada"] != null)
                    {
                        int idCategoriaSeleccionada = (int)Session["categoriaSeleccionada"];
                        ddlCategoria.SelectedValue = idCategoriaSeleccionada.ToString();
                    }

                    // Fecha y Hora del Entrenamiento
                    if (Session["fechaHoraEntrenamiento"] != null)
                    {
                        DateTime fechaHoraEntrenamiento = (DateTime)Session["fechaHoraEntrenamiento"];

                        txtFechaEntrenamiento.Text = fechaHoraEntrenamiento.ToString("yyyy-MM-dd");
                        txtHoraEntrenamiento.Text = fechaHoraEntrenamiento.ToString("HH:mm");
                    }
                    else
                    {
                        txtFechaEntrenamiento.Text = string.Empty;
                        txtHoraEntrenamiento.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnPreseleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCategoriaSeleccionada = int.Parse(ddlCategoria.SelectedValue);

                List<Jugador> listaFiltrada = new List<Jugador>();

                listaJugadores = (List<Jugador>)Session["listaJugadores"];

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
                    chkCitado.Checked = true;
                    int idJugador = Convert.ToInt32(dgvEntrenamiento.DataKeys[row.RowIndex].Value);
                    // Agregar el jugador a la lista de jugadores seleccionados si no está ya en la lista
                    if (!jugadoresSeleccionados.Contains(idJugador))
                    {
                        jugadoresSeleccionados.Add(idJugador);
                    }
                }

                Session["jugadoresSeleccionados"] = jugadoresSeleccionados;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
        
        protected void ddlJugadoresAdicionales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCategoriaSeleccionada = int.Parse(ddlJugadoresAdicionales.SelectedValue);
    
                List<Jugador> listaFiltrada = new List<Jugador>();

                listaJugadores = (List<Jugador>)Session["listaJugadores"];

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

                // Restaurar el estado de los checkboxes de los jugadores seleccionados 
                List<int> jugadoresSeleccionados = (List<int>)Session["jugadoresSeleccionados"];
                if (jugadoresSeleccionados != null)
                {
                    foreach (GridViewRow row in dgvEntrenamiento.Rows)
                    {
                        CheckBox chkCitado = (CheckBox)row.FindControl("chkCitado");
                        int idJugador = Convert.ToInt32(dgvEntrenamiento.DataKeys[row.RowIndex].Value);

                        if (jugadoresSeleccionados.Contains(idJugador))
                        {
                            chkCitado.Checked = true;
                        }
                    }
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

        protected void btnMostrarSeleccionados_Click(object sender, EventArgs e)
        {
            DateTime fechaEntrenamiento;
            DateTime horaEntrenamiento;

            try
            {
                int idCategoriaSeleccionada = int.Parse(ddlCategoria.SelectedValue);
                Session["categoriaSeleccionada"] = idCategoriaSeleccionada;

                if (string.IsNullOrEmpty(txtFechaEntrenamiento.Text) || string.IsNullOrEmpty(txtHoraEntrenamiento.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete los campos de fecha y hora.";
                    return;
                }

                else if (!DateTime.TryParse(txtFechaEntrenamiento.Text, out fechaEntrenamiento))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha no válida. Por favor, ingrese una fecha válida.";
                    return;
                }

                else if (!DateTime.TryParse(txtHoraEntrenamiento.Text, out horaEntrenamiento))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Hora no válida. Por favor, ingrese una hora válida.";
                    return;
                }

                else if (fechaEntrenamiento.Date < DateTime.Today)
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha no válida. La fecha seleccionada no puede ser en el pasado.";
                    return;
                }

                else
                {
                    DateTime fechaHoraEntrenamiento = fechaEntrenamiento.Date.Add(horaEntrenamiento.TimeOfDay);
                    Session["fechaHoraEntrenamiento"] = fechaHoraEntrenamiento;
                    Response.Redirect("entrenamientoVistaPrevia.aspx?id=1", false); //FUNCION AGREGADO
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

    }
}