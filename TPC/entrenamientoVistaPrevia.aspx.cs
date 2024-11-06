﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                if (!IsPostBack)
                {
                    tipoPagina = Convert.ToInt32(Request.QueryString["id"]);

                    if (tipoPagina == 1)
                    {
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
                        txtDuracion.Text = "00:00";
                        txtObservaciones.Visible = true;
                    }
                    else if (tipoPagina == 2)
                    {
                        EntrenamientoNegocio entrenamientoNegocio = new EntrenamientoNegocio();
                        Entrenamiento entrenamiento = new Entrenamiento();

                        entrenamiento = (Entrenamiento)Session["entrenamientoSeleccionado"];

                        if (entrenamiento != null)
                        {
                            dgvJugadoresSeleccionados.DataSource = entrenamiento.JugadoresCitados;
                            dgvJugadoresSeleccionados.DataBind();
                        }
                        else
                        {
                            lblMensaje.CssClass = "alert alert-warning";
                            lblMensaje.Text = "Aún no hay jugadores seleccionados.";
                            lblMensaje.Visible = true;
                        }

                        lblDetallesEntrenamiento.CssClass = "alert alert-info";
                        lblDetallesEntrenamiento.Text = $"El entrenamiento de la categoría '{entrenamiento.Categoria.NombreCategoria}' está organizado para el {entrenamiento.FechaHora.ToString("dddd, dd MMMM yyyy")} a las {entrenamiento.FechaHora.ToString("HH:mm")}.";

                        txtDuracion.Text = entrenamiento.Duracion.ToString();
                        txtDuracion.Enabled = false;

                        txtDescripcion.Text = entrenamiento.Descripcion;
                        txtDescripcion.Enabled = false;

                        txtObservaciones.Visible = true;
                        txtObservaciones.Text = entrenamiento.Observaciones;
                        txtObservaciones.Enabled = false;
                    }
                }
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

            Asistencia asistencia = new Asistencia();
            AsistenciaNegocio asistenciaNegocio = new AsistenciaNegocio();

            JugadorNegocio jugadorNegocio = new JugadorNegocio();

            try
            {
                entrenamiento.FechaHora = (DateTime)Session["fechaHoraEntrenamiento"];
                entrenamiento.Duracion = verificarDuracion(txtDuracion.Text);
                entrenamiento.Descripcion = txtDescripcion.Text;
                entrenamiento.Categoria = new Categoria();
                entrenamiento.Categoria.IdCategoria = (int)Session["categoriaSeleccionada"];
                entrenamiento.Estado = new EstadoEntrenamiento();
                entrenamiento.Estado.IdEstadoEntrenamiento = 1; //PROGRAMADO POR DEFAULT
                entrenamiento.Observaciones = string.Empty;

                List<int> jugadoresSeleccionadosIds = (List<int>)Session["jugadoresSeleccionados"];

                if (jugadoresSeleccionadosIds != null && jugadoresSeleccionadosIds.Count > 0)
                {
                    List<Jugador> jugadoresSeleccionados = jugadorNegocio.ObtenerJugadoresPorIds(jugadoresSeleccionadosIds);

                    entrenamiento.JugadoresCitados = jugadoresSeleccionados;
                }
                else
                {
                    entrenamiento.JugadoresCitados = new List<Jugador>();
                }

                entrenamientoNegocio.agregarEntrenamiento(entrenamiento);

                if (entrenamiento.JugadoresCitados != null && entrenamientoNegocio.obtenerUltimoEntrenamiento() != 0)
                {
                    foreach (Jugador jugador in entrenamiento.JugadoresCitados)
                    {
                        asistencia.IdJugador = jugador.IdJugador;
                        asistencia.IdEntrenamiento = entrenamientoNegocio.obtenerUltimoEntrenamiento();
                        asistencia.EstadoAsistencia = false;
                        asistencia.Observaciones = string.Empty;
                        asistenciaNegocio.agregar(asistencia);
                    }
                }
            }
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
    }
}