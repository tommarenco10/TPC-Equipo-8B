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
    public partial class entrenamientosProgramados : System.Web.UI.Page
    {
        private List<Entrenamiento> listaEntrenamientosProgramados;

        protected void Page_Load(object sender, EventArgs e)
        {
            EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();


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
                if (!IsPostBack)
                {
                    int idEstadoProgramado = 1;

                    listaEntrenamientosProgramados = negocioEntrenamiento.listarPorFechaAscendente();
                    listaEntrenamientosProgramados = listaEntrenamientosProgramados
                        .Where(entrenamiento => entrenamiento.Estado.IdEstado == idEstadoProgramado)
                        .ToList();
                    Session["listaEntrenamientosProgramados"] = listaEntrenamientosProgramados;

                    List<Categoria> listaCategorias = negocioCategoria.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();

                    // Opción para seleccionar
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

                    negocioEntrenamiento.actualizarEstadosPorFecha(idEstadoProgramado);
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

                listaEntrenamientosProgramados = (List<Entrenamiento>)Session["listaEntrenamientosProgramados"];

                if (listaEntrenamientosProgramados != null)
                {

                    foreach (Entrenamiento entrenamiento in listaEntrenamientosProgramados)
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
                    Response.Redirect("entrenamientoVistaPrevia.aspx?id=3"); //FUNCION VER DETALLE
                }
                else if (btn.ID == "btnActualizar")
                {
                    Response.Redirect("gestionEntrenamiento.aspx?id=2"); //FUNCION ACTUALIZAR
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnCancelar = (Button)sender;
                int idEntrenamientoCancelado = Convert.ToInt32(btnCancelar.CommandArgument);

                EntrenamientoNegocio entrenamientoNegocio = new EntrenamientoNegocio();
                AsistenciaNegocio asistenciaNegocio = new AsistenciaNegocio();
                JugadorNegocio jugadorNegocio = new JugadorNegocio();

                Entrenamiento entrenamientoCancelado = entrenamientoNegocio.ObtenerEntrenamientoPorId(idEntrenamientoCancelado);
                List<int> jugadoresSeleccionadosIds = jugadorNegocio.listarIdPorEntrenamiento(idEntrenamientoCancelado);

                asistenciaNegocio.EliminarAsistenciaMultiple(idEntrenamientoCancelado, jugadoresSeleccionadosIds);

                entrenamientoNegocio.cancelarEntrenamiento(idEntrenamientoCancelado);

                string script = "alert('Entrenamiento cancelado correctamente'); window.location = 'entrenamientosProgramados.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect", script, true);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionEntrenamiento.aspx");
        }
    }
}