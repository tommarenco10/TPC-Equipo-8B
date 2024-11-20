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
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (sesionActiva())
                {
                    ImagenPerfil.DataBind();
                }

            }
        }

        public bool esAdmin()
        {
            if (sesionActiva())
            {
                return Seguridad.esAdmin((Usuario)Session["user"]);
            }
            else return false;
        }


        public bool esEntrenador()
        {
            if (sesionActiva())
            {
                return Seguridad.esEntrenador((Usuario)Session["user"]);
            }
            else return false;

        }


        public bool esMedico()
        {
            if (sesionActiva())
            {
                return Seguridad.esMedico((Usuario)Session["user"]);
            }
            else return false;
        }




        public bool esSocio()
        {
            if (sesionActiva())
            {
                return Seguridad.esSocio((Usuario)Session["user"]);
            }
            else return false;
        }



        public bool sesionActiva()
        {
            if (Session["user"] != null)
            {
                return true;
            }
            else return false;
        }

        protected void CerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("index.aspx",false);
        }


        protected string GetImageUrl()
        {
            return Session["userProfileImage"] != null ? Session["userProfileImage"].ToString() : "~/Images/placeholder.png";
        }

        protected void editarPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditarPerfil.aspx",false);
        }
    }
}