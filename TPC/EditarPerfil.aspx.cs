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
    public partial class EditarPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]!=null){

            }
            else
            {
                Session.Add("error","Necesitas loguearte correctamente");
                Response.Redirect("Error.aspx", false);
            }

        }
        
           

            
     
    }
}