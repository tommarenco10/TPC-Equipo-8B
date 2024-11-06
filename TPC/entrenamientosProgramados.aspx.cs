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

                if (Session["listaEntrenamientos"] != null)
                {
                    List<Entrenamiento> listaEntrenamientos = (List<Entrenamiento>)Session["listaEntrenamientos"];

                    foreach (Entrenamiento entrenamiento in listaEntrenamientos)
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionEntrenamiento.aspx");
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            
            
            Response.Redirect("entrenamientoVistaPrevia.aspx");
        }
    }
}