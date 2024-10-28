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
    public partial class entrenamientosProgramados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EntrenamientoNegocio negocioEntrenamiento = new EntrenamientoNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();

            try
            {
                if (!IsPostBack)
                {
                    List<Entrenamiento> listaEntrenamientos = negocioEntrenamiento.listar();
                    Session["listaEntrenamientos"] = listaEntrenamientos;

                    List<Categoria> listaCategorias = negocioCategoria.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();

                    // Opción para seleccionar
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
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

                dgvEntrenamientos.DataSource = listaFiltrada;
                dgvEntrenamientos.DataBind();

                // Restaurar el estado de los checkboxes de los jugadores seleccionados 
                List<int> jugadoresSeleccionados = (List<int>)Session["jugadoresSeleccionados"];
                if (jugadoresSeleccionados != null)
                {
                    foreach (GridViewRow row in dgvEntrenamientos.Rows)
                    {
                        CheckBox chkCitado = (CheckBox)row.FindControl("chkCitado");
                        int idJugador = Convert.ToInt32(dgvEntrenamientos.DataKeys[row.RowIndex].Value);

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
    }
}