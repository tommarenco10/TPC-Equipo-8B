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
        LugarNacimientoNegocio lugarNacimientoNegocio = new LugarNacimientoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DateTime minDate = DateTime.Now.AddYears(-120);
                DateTime maxDate = DateTime.Now.AddYears(-10);
                txtFechaNacimiento.Attributes.Add("min", minDate.ToString("yyyy-MM-dd"));
                txtFechaNacimiento.Attributes.Add("max", maxDate.ToString("yyyy-MM-dd"));
                CargarPaises();
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = new Usuario();
            Persona persona = new Persona();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            PersonaNegocio personaNegocio = new PersonaNegocio();
            string rutaImagen;
            string fileName;


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



                        if (fileInput.HasFile)
                        {

                            string fileExtension = System.IO.Path.GetExtension(fileInput.FileName).ToLower();
                            List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };

                            if (!allowedExtensions.Contains(fileExtension))
                            {
                                Session.Add("error", "Solo se permiten imágenes con las extensiones .jpg, .jpeg, .png o .gif.");
                                Response.Redirect("Error.aspx", false);
                                return;
                            }


                            if (fileInput.PostedFile.ContentLength > 2 * 1024 * 1024) // 2MB
                            {
                                Session.Add("error", "El archivo es demasiado grande. El tamaño máximo permitido es 2MB.");
                                Response.Redirect("Error.aspx", false);
                                return;
                            }

                            rutaImagen = Server.MapPath("./Images/");
                            fileName = "profile-" + txtUserName.Text + fileExtension;
                            fileInput.SaveAs(rutaImagen + fileName);
                        }
                        else
                        {
                            Session.Add("error", "No seleccionaste una imagen.");
                            Response.Redirect("Error.aspx", false);
                            return;
                        }


                        persona.DNI = txtDNI.Text;  // Asignamos el DNI
                        persona.Nombres = txtNombre.Text;
                        persona.Apellidos = txtApellido.Text;
                        persona.FechaNacimiento = fechaNacimiento;
                        persona.Email = txtEmail.Text;
                        persona.LugarNacimiento = new LugarNacimiento
                        {
                            Pais = ddlPais.SelectedValue,
                            Provincia = ddlProvincia.SelectedValue,
                            Ciudad = ddlCiudad.SelectedValue
                        };
                        persona.UrlImagen = "Images/" + fileName;  // Imagen cargada previamente
                        usuario.Nombre = txtUserName.Text;
                        usuario.IdPersona = personaNegocio.agregar(persona);
                        usuario.Email = txtEmail.Text;
                        usuario.Contraseña = txtPassword.Text;
                        usuario.Tipo = TipoUsuario.Hincha;
                        usuarioNegocio.agregar(usuario);
                        Response.Redirect("index.aspx", false);//Podria cambiar el mensaje de bienvenida 
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




        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resetear provincia y ciudad al cambiar de país
            ddlProvincia.Items.Clear();
            ddlProvincia.Items.Insert(0, new ListItem("Seleccione una provincia", ""));
            ddlCiudad.Items.Clear();
            ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));

            // Cargar las provincias correspondientes al país seleccionado
            string paisSeleccionado = ddlPais.SelectedValue;
            CargarProvincias(paisSeleccionado);
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resetear ciudad al cambiar de provincia
            ddlCiudad.Items.Clear();
            ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));

            // Cargar las ciudades correspondientes a la provincia seleccionada
            string provinciaSeleccionada = ddlProvincia.SelectedValue;
            CargarCiudades(provinciaSeleccionada);
        }

        private void CargarPaises()
        {
            List<string> paises = lugarNacimientoNegocio.ObtenerPaises();
            ddlPais.DataSource = paises;
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem("Seleccione un país", ""));
        }

        private void CargarProvincias(string pais)
        {
            List<string> provincias = lugarNacimientoNegocio.ObtenerProvincias(pais);
            ddlProvincia.DataSource = provincias;
            ddlProvincia.DataBind();
            ddlProvincia.Items.Insert(0, new ListItem("Seleccione una provincia", ""));
        }

        private void CargarCiudades(string provincia)
        {
            List<string> ciudades = lugarNacimientoNegocio.ObtenerCiudades(provincia);
            ddlCiudad.DataSource = ciudades;
            ddlCiudad.DataBind();
            ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));
        }







    }
    }
