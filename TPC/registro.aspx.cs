using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using Acceso_Datos;
using System.IO;

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
            try
            {
                // Validar contraseñas
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MostrarError("Las contraseñas no coinciden.");
                    return;
                }

                // Validar campos vacíos
                var formulario = new List<TextBox> { txtNombre, txtApellido, txtEmail, txtDNI, txtPassword, txtConfirmPassword, txtUserName, txtFechaNacimiento };
                if (Seguridad.validaTextosVacios(formulario))
                {
                    MostrarError("Debes completar todos los campos.");
                    return;
                }

                // Validar fecha de nacimiento
                if (!Seguridad.ValidarFechaNacimiento(txtFechaNacimiento.Text, out DateTime fechaNacimiento))
                {
                    MostrarError("La fecha de nacimiento no es válida.");
                    return;
                }

                // Validar imagen
                if (!fileInput.HasFile)
                {
                    MostrarError("No seleccionaste una imagen.");
                    return;
                }

                var validacionArchivo = Seguridad.ValidarArchivo(fileInput.PostedFile);
                if (!validacionArchivo.isValid)
                {
                    MostrarError(validacionArchivo.mensaje);
                    return;
                }

                // Guardar imagen
                string rutaImagen = Server.MapPath("./Images/");
                string fileName = "profile-" + txtUserName.Text + Path.GetExtension(fileInput.FileName);
                fileInput.SaveAs(rutaImagen + fileName);


                var comprobacion = Seguridad.ComprobarDatosExistentes(txtUserName.Text, txtDNI.Text, txtEmail.Text);

                
                if (!comprobacion.isSuccess)
                {
                    lblError.Text = comprobacion.mensaje; 
                    lblError.Visible = true; 
                    return; 
                }


                Persona persona = new Persona
                {
                    DNI = txtDNI.Text,
                    Nombres = txtNombre.Text,
                    Apellidos = txtApellido.Text,
                    FechaNacimiento = fechaNacimiento,
                    Email = txtEmail.Text,
                    LugarNacimiento = new LugarNacimiento
                    {
                        Pais = ddlPais.SelectedValue,
                        Provincia = ddlProvincia.SelectedValue,
                        Ciudad = ddlCiudad.SelectedValue
                    },
                    UrlImagen = "Images/" + fileName
                };

                PersonaNegocio personaNegocio = new PersonaNegocio();
                Usuario usuario = new Usuario
                {
                    Nombre = txtUserName.Text,
                    IdPersona = personaNegocio.agregar(persona),
                    Email = txtEmail.Text,
                    Contraseña = txtPassword.Text,
                    Tipo = TipoUsuario.Hincha
                };

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuarioNegocio.agregar(usuario);

                // Redirigir
                Session.Add("user", usuario);
                Session.Add("registro_nuevo", persona.Nombres.ToString() + " " + persona.Apellidos.ToString());
                Response.Redirect("index.aspx", false);
            }
            catch (Exception ex)
            {
                MostrarError("Hubo un error en tu solicitud.");
            }
        }

       
        
        private void MostrarError(string mensaje)
        {
            Session.Add("error", mensaje);
            Response.Redirect("Error.aspx", false);
        }




        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            ddlProvincia.Items.Clear();
            ddlProvincia.Items.Insert(0, new ListItem("Seleccione una provincia", ""));
            ddlCiudad.Items.Clear();
            ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));

           
            string paisSeleccionado = ddlPais.SelectedValue;
            CargarProvincias(paisSeleccionado);
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            ddlCiudad.Items.Clear();
            ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));

          
            string provinciaSeleccionada = ddlProvincia.SelectedValue;
            CargarCiudades(provinciaSeleccionada);
        }

        private void CargarPaises()
        {
            try
            {
                List<string> paises = lugarNacimientoNegocio.ObtenerPaises();
                ddlPais.DataSource = paises;
                ddlPais.DataBind();
                ddlPais.Items.Insert(0, new ListItem("Seleccione un país", ""));
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar países: " + ex.Message);
            }
        }

        private void CargarProvincias(string pais)
        {
            try
            {
                List<string> provincias = lugarNacimientoNegocio.ObtenerProvincias(pais);
                ddlProvincia.DataSource = provincias;
                ddlProvincia.DataBind();

                ddlProvincia.Items.Insert(0, new ListItem("Seleccione una provincia", ""));
                ddlProvincia.Enabled = provincias.Count > 0; 
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar provincias: " + ex.Message);
            }
        }


        private void CargarCiudades(string provincia)
        {
            try
            {
                List<string> ciudades = lugarNacimientoNegocio.ObtenerCiudades(provincia);
                ddlCiudad.DataSource = ciudades;
                ddlCiudad.DataBind();
                ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar ciudades: " + ex.Message);
               
            }
            
        }

    }
    }
