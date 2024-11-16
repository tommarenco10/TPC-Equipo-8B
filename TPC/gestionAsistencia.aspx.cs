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
    public partial class gestionAsistencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoriaNegocio negocioCategoria = new CategoriaNegocio();

                Entrenamiento entrenamiento = (Entrenamiento)Session["entrenamientoSeleccionado"];
                if (entrenamiento == null)
                {
                    entrenamiento = new Entrenamiento();
                }
                if (entrenamiento.Categoria == null)
                {
                    entrenamiento.Categoria = new Categoria();
                }

                int idCategoriaSeleccionada = entrenamiento.Categoria.IdCategoria;
                List<Categoria> listaCategorias = negocioCategoria.listar();
                Categoria categoria = listaCategorias.FirstOrDefault(x => x.IdCategoria == idCategoriaSeleccionada);
                string categoriaSeleccionada = string.Empty;
                if (categoria != null)
                {
                    categoriaSeleccionada = categoria.NombreCategoria;
                }

                lblDetallesEntrenamiento.CssClass = "alert alert-info";
                lblDetallesEntrenamiento.Text = $"El entrenamiento de la categoría '{categoriaSeleccionada}' está organizado para el {entrenamiento.FechaHora.ToString("dddd, dd MMMM yyyy")} a las {entrenamiento.FechaHora.ToString("HH:mm")}.";

                if (Session["jugadoresSeleccionados"] != null)
                {
                    List<int> jugadoresSeleccionados = (List<int>)Session["jugadoresSeleccionados"];
                    JugadorNegocio negocioJugador = new JugadorNegocio();
                    List<Jugador> listaJugadores = negocioJugador.ObtenerJugadoresPorIds(jugadoresSeleccionados);
                    dgvJugadores.DataSource = listaJugadores;
                    dgvJugadores.DataBind();
                }
                else
                {
                    lblMensaje.CssClass = "alert alert-warning";
                    lblMensaje.Text = "Aún no hay jugadores seleccionados.";
                    lblMensaje.Visible = true;
                }
            }

            if (Session["jugadoresPresentes"] != null)
            {
                List<int> jugadoresPresentes = (List<int>)Session["jugadoresPresentes"];

                foreach (GridViewRow row in dgvJugadores.Rows)
                {
                    CheckBox chkAsistencia = (CheckBox)row.FindControl("chkAsistencia");
                    int idJugador = Convert.ToInt32(dgvJugadores.DataKeys[row.RowIndex].Value);

                    chkAsistencia.Checked = jugadoresPresentes.Contains(idJugador);
                }
            }
        }

        protected void btnPreseleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                // Inicializar o recuperar la lista de jugadores presentes
                List<int> jugadoresPresentes = Session["jugadoresPresentes"] != null
                    ? (List<int>)Session["jugadoresPresentes"]
                    : new List<int>();

                foreach (GridViewRow row in dgvJugadores.Rows)
                {
                    CheckBox chkAsistencia = (CheckBox)row.FindControl("chkAsistencia");
                    int idJugador = Convert.ToInt32(dgvJugadores.DataKeys[row.RowIndex].Value);

                    // Marcar el CheckBox y agregar el jugador a la lista
                    chkAsistencia.Checked = true;

                    if (!jugadoresPresentes.Contains(idJugador))
                    {
                        jugadoresPresentes.Add(idJugador);
                    }
                }

                // Actualizar la lista en la sesión
                Session["jugadoresPresentes"] = jugadoresPresentes;

                lblMensaje.CssClass = "alert alert-success";
                lblMensaje.Text = "Todos los jugadores han sido preseleccionados.";
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void chkAsistencia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Recuperar o inicializar la lista de jugadores presentes
                List<int> jugadoresPresentes = Session["jugadoresPresentes"] != null
                    ? (List<int>)Session["jugadoresPresentes"]
                    : new List<int>();

                // Identificar la fila que disparó el evento
                CheckBox chkPresente = (CheckBox)sender;
                GridViewRow row = (GridViewRow)chkPresente.NamingContainer;

                // Obtener el ID del jugador de la fila
                int idJugador = Convert.ToInt32(dgvJugadores.DataKeys[row.RowIndex].Value);

                // Agregar o quitar el jugador de la lista según el estado del CheckBox
                if (chkPresente.Checked)
                {
                    if (!jugadoresPresentes.Contains(idJugador))
                    {
                        jugadoresPresentes.Add(idJugador);
                    }
                }
                else
                {
                    if (jugadoresPresentes.Contains(idJugador))
                    {
                        jugadoresPresentes.Remove(idJugador);
                    }
                }

                // Guardar la lista actualizada en la sesión
                Session["jugadoresPresentes"] = jugadoresPresentes;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnLimpiarSeleccion_Click(object sender, EventArgs e)
        {

            try
            {
                // Limpiar todos los CheckBoxes
                foreach (GridViewRow row in dgvJugadores.Rows)
                {
                    CheckBox chkAsistencia = (CheckBox)row.FindControl("chkAsistencia");
                    chkAsistencia.Checked = false;
                }

                // Vaciar la lista en la sesión
                Session["jugadoresPresentes"] = new List<int>();

                lblMensaje.CssClass = "alert alert-info";
                lblMensaje.Text = "La selección de jugadores ha sido limpiada.";
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}