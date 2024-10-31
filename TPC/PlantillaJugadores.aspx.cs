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
    public partial class PlanillaJugadores : System.Web.UI.Page
    {
        public List<Categoria> Categoria { get; set; }
        public string titulo { get; set; }
        public int contador { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            Categoria = categoriaNegocio.listar();

        }

        protected void dgv_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("ConfigJugador.aspx", false);
        }
    }
}