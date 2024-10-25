using Dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class ConfigWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();

                    ddlCategoria.DataSource = negocio.listar();
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataBind();

                    EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();

                    ddlEstadoJugador.DataSource = negocioEJ.listar();
                    ddlEstadoJugador.DataTextField = "NombreEstado";
                    ddlEstadoJugador.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}