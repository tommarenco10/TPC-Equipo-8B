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
    public partial class gestionIncidencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["IdJugador"] != null)
                    {
                        int idJugador = Convert.ToInt32(Request.QueryString["IdJugador"]);
                        CargarJugador(idJugador);
                    }
                }
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
                txtFechaNacimiento.Text = jugador.FechaNacimiento.ToShortDateString() + "(" + edad + ")";
                decimal altura = (decimal)jugador.Altura / 100;
                txtAltura.Text = altura.ToString("N2") + " m";
                txtPeso.Text = jugador.Peso.ToString("N1") + " kg";
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnGuardarIncidencia_Click(object sender, EventArgs e)
        {
            try
            {
                Incidencia incidencia = new Incidencia
                {
                    IdJugador = Convert.ToInt32(Request.QueryString["idJugador"]),
                    Descripcion = txtDescripcion.Text,
                    FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text),
                    FechaResolución = Convert.ToDateTime(txtFechaResolucion.Text),
                    IdEstadoJugador = ddlTipoIncidencia.SelectedValue == "1" ? 2 : 1, // Lesión -> Estado de lesión
                    Estado = true
                };

                IncidenciaNegocio negocioIncidencia = new IncidenciaNegocio();
                negocioIncidencia.agregar(incidencia);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void ddlTipoIncidencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlObservaciones.Visible = ddlTipoIncidencia.SelectedValue == "1"; // Mostrar solo para lesiones
        }

        protected void btnAgregarObservacion_Click(object sender, EventArgs e)
        {
            try
            {
                List<ObservacionConFecha> observaciones = Session["Observaciones"] != null
                    ? (List<ObservacionConFecha>)Session["Observaciones"]
                    : new List<ObservacionConFecha>();

                observaciones.Add(new ObservacionConFecha
                {
                    Fecha = Convert.ToDateTime(txtFechaObservacion.Text),
                    Descripcion = txtDescripcionObservacion.Text
                });

                Session["Observaciones"] = observaciones;

                gvObservaciones.DataSource = observaciones;
                gvObservaciones.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }


    }
}