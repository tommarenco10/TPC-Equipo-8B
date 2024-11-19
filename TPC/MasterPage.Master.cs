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
        Seguridad comprobaciones;
        protected void Page_Load(object sender, EventArgs e)
        {
       
        }

        public bool esAdmin()
        {
            if (sesionActiva())
            {
                return comprobaciones.esAdmin((Usuario)Session["user"]);
            }
            else return false;
        }


        public bool esEntrenador()
        {
            if (sesionActiva())
            {
                return comprobaciones.esEntrenador((Usuario)Session["user"]);
            }
            else return false;

        }


        public bool esMedico()
        {
            if (sesionActiva())
            {
                return comprobaciones.esMedico((Usuario)Session["user"]);
            }
            else return false;
        }




        public bool esSocio()
        {
            if (sesionActiva())
            {
                return comprobaciones.esSocio((Usuario)Session["user"]);
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

    }
}