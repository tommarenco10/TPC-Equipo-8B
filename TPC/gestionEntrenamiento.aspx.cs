using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Acceso_Datos;
using negocio;
using Negocio;


namespace TPC
{
    public partial class gestionEntrenamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JugadorNegocio negocioJugador = new JugadorNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();

            try
            {
                if (!IsPostBack)
                {
                    List<Jugador> listaJugador = negocioJugador.listar();
                    Session["listaJugador"] = listaJugador;
                    
                    List<Categoria> listaCategorias = negocioCategoria.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();

                    // Opción para seleccionar
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
                    
                    // Recuperar Fecha y Hora del Entrenamiento
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
                Session.Add("error", ex);
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCategoriaSeleccionada = int.Parse(ddlCategoria.SelectedValue);

                // Filtro de jugadores en base a la categoría seleccionada
                List<Jugador> listaFiltrada = new List<Jugador>();

                if (Session["listaJugador"] != null)
                {
                    List<Jugador> listaJugadores = (List<Jugador>)Session["listaJugador"];

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
                Session.Add("error", ex);
            }
        }

        protected void chkCitado_CheckedChanged(object sender, EventArgs e)
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

        protected void btnMostrarSeleccionados_Click(object sender, EventArgs e)
        {
            DateTime fechaEntrenamiento;
            DateTime horaEntrenamiento;

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
                Response.Redirect("entrenamientoVistaPrevia.aspx");
            }
        }

    }
}