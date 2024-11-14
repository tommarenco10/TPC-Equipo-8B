using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;


namespace TPC
{
    public partial class ingreso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] == null))
            {
                Response.Redirect("index.aspx");
            }
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            UsuarioNegocio nuevo = new UsuarioNegocio();
            Usuario login = new Usuario();
            

            try
            {

                if(Seguridad.validaTextoVacio(txtUserName.Text)|| Seguridad.validaTextoVacio(txtPass.Text))
                {
                    Session.Add("error", "Debes completar los campos requeridos.");
                    Response.Redirect("Error.aspx");
                }
          
                login.Nombre = txtUserName.Text;
                login.Contraseña = txtPass.Text;
                if (nuevo.loguear(login))
                {
                    Session.Add("user", login);
                    Response.Redirect("index.aspx",false);
                    //return;
                }
                else
                {
                    txtPass.CssClass = "form-control is-invalid";
                    lbError.Text = "Usuario o contraseña incorrecta.";
                    lbError.Visible = true;
                    UpdatePanel1.Update();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx",false);
            } 


        }
    }
}

