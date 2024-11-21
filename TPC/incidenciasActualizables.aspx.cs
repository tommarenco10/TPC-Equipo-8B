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
    public partial class incidenciasActualizables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IncidenciaNegocio incidenciaNegocio = new IncidenciaNegocio();
            List<Incidencia> listaIncidencias = new List<Incidencia>();



            if (Session["user"] != null)
            {
                Usuario logueado = (Usuario)Session["user"];
                if (Seguridad.esEntrenador(logueado) || Seguridad.esAdmin(logueado))
                {
                    try
                    {
                        if (!IsPostBack)
                        {
                            if (Session["idJugador"] != null)
                            {
                                CargarJugador((int)Session["idJugador"]);
                                listaIncidencias = incidenciaNegocio.listarPorJugador((int)Session["idJugador"]);
                            }
                            dgvIncidencias.DataSource = listaIncidencias;
                            dgvIncidencias.DataBind();

                        }
                    }
                    catch (Exception ex)
                    {
                        Session.Add("error", ex.ToString());
                        Response.Redirect("Error.aspx");
                    }
                }
                else
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

        protected void dgvIncidencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Incidencia incidencia = (Incidencia)e.Row.DataItem;

                int estadoColumnIndex = 4;
                string estadoTexto = "Abierta";

                if (!incidencia.Estado)
                {
                    estadoTexto = "Cerrada";
                }

                e.Row.Cells[estadoColumnIndex].Text = estadoTexto;
            }
        }

        protected void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idIncidencia = Convert.ToInt32(btn.CommandArgument);

                IncidenciaNegocio negocioIncidencia = new IncidenciaNegocio();
                Incidencia incidenciaSeleccionada = negocioIncidencia.ObtenerIncidenciaPorId(idIncidencia);

                List<int> listaObservaciones = new List<int>();
                foreach (ObservacionConFecha observacion in incidenciaSeleccionada.Observaciones)
                {
                    listaObservaciones.Add(observacion.IdObservacion);
                }
                Session["listaDeObservaciones"] = listaObservaciones;
                Session["incidenciaSeleccionada"] = incidenciaSeleccionada;

                if (btn.ID == "btnVerDetalle")
                {
                    Response.Redirect("gestionIncidencias.aspx?id=2"); //FUNCION VER DETALLE
                }
                else if (btn.ID == "btnActualizar")
                {
                    Response.Redirect("gestionIncidencias.aspx?id=3"); //FUNCION ACTUALIZAR
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlantillaJugadores.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionIncidencias.aspx?id=1"); //FUNCION AGREGAR
        }
    }
}