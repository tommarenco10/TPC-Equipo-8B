using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class entrenamientosFinalizados : System.Web.UI.Page
    {
        private List<Entrenamiento> listaEntrenamientosFinalizados;

        protected void Page_Load(object sender, EventArgs e)
        {
            EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();

            try
            {
                if (!IsPostBack)
                {
                    int idEstadoProgramado = 1;
                    int idEstadoEnCurso = 4;
                    int idEstadoFinalizado = 3;

                    listaEntrenamientosFinalizados = negocioEntrenamiento.listarPorFechaAscendente();
                    listaEntrenamientosFinalizados = listaEntrenamientosFinalizados
                        .Where(entrenamiento => entrenamiento.Estado.IdEstado == idEstadoFinalizado)
                        .ToList();
                    Session["listaEntrenamientosFinalizados"] = listaEntrenamientosFinalizados;

                    List<Categoria> listaCategorias = negocioCategoria.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();

                    // Opción para seleccionar
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

                    negocioEntrenamiento.actualizarEstadosPorFecha(idEstadoProgramado);
                    negocioEntrenamiento.actualizarEstadosPorFecha(idEstadoEnCurso);
                }

                if (dgvEntrenamientos.Rows.Count == 0)
                {
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCategoriaSeleccionada = int.Parse(ddlCategoria.SelectedValue);

                List<Entrenamiento> listaFiltrada = new List<Entrenamiento>();

                listaEntrenamientosFinalizados = (List<Entrenamiento>)Session["listaEntrenamientosFinalizados"];

                if (listaEntrenamientosFinalizados != null)
                {

                    foreach (Entrenamiento entrenamiento in listaEntrenamientosFinalizados)
                    {
                        if (entrenamiento.Categoria.IdCategoria == idCategoriaSeleccionada)
                        {
                            listaFiltrada.Add(entrenamiento);
                        }
                    }
                }
                dgvEntrenamientos.DataSource = listaFiltrada;
                dgvEntrenamientos.DataBind();

                if (dgvEntrenamientos.Rows.Count == 0)
                {
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }    

        protected void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idEntrenamiento = Convert.ToInt32(btn.CommandArgument);

                EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
                Entrenamiento entrenamientoSeleccionado = negocioEntrenamiento.ObtenerEntrenamientoPorId(idEntrenamiento);

                List<int> listaJugadores = new List<int>();
                foreach (Jugador jugador in entrenamientoSeleccionado.JugadoresCitados)
                {
                    listaJugadores.Add(jugador.IdJugador);
                }
                Session["jugadoresSeleccionados"] = listaJugadores;
                Session["entrenamientoSeleccionado"] = entrenamientoSeleccionado;

                if (btn.ID == "btnVerDetalle")
                {
                    Response.Redirect("entrenamientoVistaPrevia.aspx?id=4"); //FUNCION VER DETALLE
                }
                else if (btn.ID == "btnAsistencia")
                {
                    Response.Redirect("gestionAsistencia.aspx"); //FUNCION ACTUALIZAR
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