using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using Acceso_Datos;

namespace TPC
{
    public partial class registro : System.Web.UI.Page
    {
        Seguridad comprobaciones;
        List<TextBox> formulario;
        string rutaImagen;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                DateTime minDate = DateTime.Now.AddYears(-120);
                DateTime maxDate = DateTime.Now.AddYears(-10);  
                txtFechaNacimiento.Attributes.Add("min", minDate.ToString("yyyy-MM-dd"));
                txtFechaNacimiento.Attributes.Add("max", maxDate.ToString("yyyy-MM-dd"));
                //imgNuevoPerfil.ImageUrl = "https://static.vecteezy.com/system/resources/previews/024/983/914/non_2x/simple-user-default-icon-free-png.png";
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = new Usuario();
            Persona persona = new Persona();
            UsuarioNegocio usuarioNegocio   = new UsuarioNegocio(); 
            PersonaNegocio personaNegocio   =new PersonaNegocio();  


            try
            {
                
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                   
                    formulario = new List<TextBox>
                    {
                        txtNombre,
                        txtApellido,
                        txtEmail,
                        txtDNI,
                        txtPassword,
                        txtConfirmPassword,
                        txtUserName,
                        txtFechaNacimiento
                    };

                    if (!(Seguridad.validaTextosVacios(formulario)))
                    {
                        
                        if (string.IsNullOrEmpty(txtFechaNacimiento.Text))
                        {
                            Session.Add("error", "Debes completar correctamente los campos requeridos.");
                            Response.Redirect("Error.aspx",false);
                            return;
                        }

                        DateTime fechaNacimiento;
                        if (DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
                        {
                            if (fechaNacimiento > DateTime.Now.AddYears(-10) || fechaNacimiento < DateTime.Now.AddYears(-120))
                            {
                                Session.Add("error", "Debes completar correctamente los campos requeridos.");
                                Response.Redirect("Error.aspx", false);
                                return;
                            }
                        }
                        else
                        {
                            Session.Add("error", "Debes completar correctamente los campos requeridos.");
                            Response.Redirect("Error.aspx", false);
                            return;
                        }

                        
                        persona.Nombres = txtNombre.Text;
                        persona.Apellidos = txtApellido.Text;
                        //persona.DNI = txtDNI.Text;
                        persona.FechaNacimiento = fechaNacimiento;
                        persona.Email = txtEmail.Text;
                        usuario.IdPersona = personaNegocio.agregar(persona);
                        usuario.Nombre = txtUserName.Text;
                        usuario.Email = txtEmail.Text;
                        usuario.Contraseña = txtPassword.Text;
                        usuario.Tipo = TipoUsuario.Hincha;
                        usuarioNegocio.agregar(usuario); 
                        Response.Redirect("index.aspx", false);//Podria cambiar el mensaje de bienvenida segun si me registro o si inicio sesion.
                    }
                   
                }
                else
                {
                    Session.Add("error", "Debes completar TODOS los campos.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", "Hubo un error en tu solicitud.");
                Response.Redirect("Error.aspx", false);
                return;
            }
        }

       
    }
}
