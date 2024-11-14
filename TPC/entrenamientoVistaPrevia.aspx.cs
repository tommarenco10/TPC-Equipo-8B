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

                    txtDuracion.Text = entrenamiento.Duracion.ToString();
                    txtDescripcion.Text = entrenamiento.Descripcion;
                    txtObservaciones.Text = entrenamiento.Observaciones;

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

                    if ((int)Session["tipoPagina"] == 2)
                    {
                        txtObservaciones.Visible = true;
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

        protected void btnVolverAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionEntrenamiento.aspx?id=1");
        }

        protected void btnVolverListado_Click(object sender, EventArgs e)
        {
            Response.Redirect("entrenamientosProgramados.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionEntrenamiento.aspx?id=2");
        }
    }
}