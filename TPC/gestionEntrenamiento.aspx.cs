using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Acceso_Datos;
using negocio;


namespace TPC
{
    public partial class gestionEntrenamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JugadorNegocio negocio = new JugadorNegocio();
            dgvEntrenamiento.DataSource = negocio.listar();
            dgvEntrenamiento.DataBind();
        }
    }
}