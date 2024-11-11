﻿using Dominio;
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
        private List<Entrenamiento> listaEntrenamientosProgramados;

        protected void Page_Load(object sender, EventArgs e)
        {
            EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();

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

        

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnVerDetalle = (Button)sender;
                int idEntrenamiento = Convert.ToInt32(btnVerDetalle.CommandArgument);

                EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
                Entrenamiento entrenamientoSeleccionado = negocioEntrenamiento.ObtenerEntrenamientoPorId(idEntrenamiento);

                List<int> listaJugadores = new List<int>();
                foreach (Jugador jugador in entrenamientoSeleccionado.JugadoresCitados)
                {
                    listaJugadores.Add(jugador.IdJugador); // Agregar cada IdJugador a la lista
                }
                Session["jugadoresSeleccionados"] = listaJugadores;

                Session["entrenamientoSeleccionado"] = entrenamientoSeleccionado;

                Response.Redirect("entrenamientoVistaPrevia.aspx?id=2"); //FUNCION VER DETALLE
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnActualizar = (Button)sender;
                int idEntrenamiento = Convert.ToInt32(btnActualizar.CommandArgument);

                EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
                Entrenamiento entrenamientoSeleccionado = negocioEntrenamiento.ObtenerEntrenamientoPorId(idEntrenamiento);

                List<int> listaJugadores = new List<int>();
                foreach (Jugador jugador in entrenamientoSeleccionado.JugadoresCitados)
                {
                    listaJugadores.Add(jugador.IdJugador); // Agregar cada IdJugador a la lista
                }
                Session["jugadoresSeleccionados"] = listaJugadores;
                Session["idEntrenamientoSeleccionado"] = entrenamientoSeleccionado.IdEntrenamiento;
                Session["categoriaSeleccionada"] = entrenamientoSeleccionado.Categoria.IdCategoria;
                Session["fechaHoraEntrenamiento"] = entrenamientoSeleccionado.FechaHora;
                Session["duracionEntrenamiento"] = entrenamientoSeleccionado.Duracion.ToString();
                Session["descripcionEntrenamiento"] = entrenamientoSeleccionado.Descripcion;
                Session["observacionesEntrenamiento"] = entrenamientoSeleccionado.Observaciones;

                Response.Redirect("entrenamientoVistaPrevia.aspx?id=3"); //FUNCION ACTUALIZAR
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