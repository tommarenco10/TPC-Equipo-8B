using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TPC
{
    public partial class ingreso : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            UsuarioNegocio nuevo = new UsuarioNegocio();
            Usuario login = new Usuario();
            login.Nombre = txtUserName.Text;
            login.Contraseña = txtPass.Text;

            if (nuevo.loguear(login))
            {
                Session.Add("user", login);
            }




        }
    }
}