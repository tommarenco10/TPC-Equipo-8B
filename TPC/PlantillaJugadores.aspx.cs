using negocio;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            JugadorNegocio negocio = new JugadorNegocio();
            dgvPrimera.DataSource = negocio.ListarPorCategoria(1);
            dgvPrimera.DataBind();
            dgvReserva.DataSource = negocio.ListarPorCategoria(2);
            dgvReserva.DataBind();
            dgvJuveniles.DataSource = negocio.ListarPorCategoria(3);
            dgvJuveniles.DataBind();
        }

        protected void dgv_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("ConfigJugador.aspx", false);
        }
    }
}