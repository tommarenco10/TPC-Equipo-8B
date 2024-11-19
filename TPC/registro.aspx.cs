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
                // *** Validaciones Iniciales ***

                // Validar contraseñas
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MostrarError("Las contraseñas no coinciden.");
                    return;
                }

                // Validar campos vacíos
                var camposRequeridos = new List<TextBox>
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

                if (Seguridad.validaTextosVacios(camposRequeridos))
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

                // Validar si se seleccionó un archivo
                if (!fileInput.HasFile)
                {
                    MostrarError("No seleccionaste una imagen.");
                    return;
                }

                // Validar el archivo (tipo, tamaño, etc.)
                var validacionArchivo = Seguridad.ValidarArchivo(fileInput.PostedFile);
                if (!validacionArchivo.isValid)
                {
                    MostrarError(validacionArchivo.mensaje);
                    return;
                }

                // Validar datos existentes (nombre de usuario, DNI o email)
                var comprobacion = Seguridad.ComprobarDatosExistentes(txtUserName.Text, txtDNI.Text, txtEmail.Text);
                if (!comprobacion.isSuccess)
                {
                    MostrarError(comprobacion.mensaje);
                    return;
                }

                // *** Guardar Imagen ***

                // Ruta física para almacenar la imagen
                string rutaCarpeta = Server.MapPath("~/Images/");
                // Nombre único para evitar conflictos
                string nombreArchivo = "profile-" + txtUserName.Text + "-" + Guid.NewGuid() + Path.GetExtension(fileInput.FileName);
                string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                // Guardar la imagen físicamente en el servidor
                fileInput.SaveAs(rutaCompleta);

                // Ruta relativa para guardar en base de datos o sesión
                string rutaRelativa = "Images/" + nombreArchivo;

                // *** Crear Objetos Persona y Usuario ***

                // Crear y configurar el objeto Persona
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
                    UrlImagen = rutaRelativa // Asignar la ruta relativa de la imagen
                };

                // Guardar Persona en la base de datos y obtener su ID
                PersonaNegocio personaNegocio = new PersonaNegocio();
                int idPersona = personaNegocio.agregar(persona);

                // Crear y configurar el objeto Usuario
                Usuario usuario = new Usuario
                {
                    Nombre = txtUserName.Text,
                    IdPersona = idPersona,
                    Email = txtEmail.Text,
                    Contraseña = txtPassword.Text,
                    Tipo = TipoUsuario.Hincha
                };

                // Guardar Usuario en la base de datos
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuarioNegocio.agregar(usuario);

                // *** Configurar Sesión y Redirigir ***

                // Agregar información del usuario a la sesión
                Session["user"] = usuario;
                Session["registro_nuevo"] = $"{persona.Nombres} {persona.Apellidos}";
                Session["userId"] = usuario.IdUsuario;
                Session["userName"] = usuario.Nombre;
                Session["userType"] = (int)usuario.Tipo;
                Session["userProfileImage"] = rutaRelativa;

                // Redirigir al usuario a la página principal
                Response.Redirect("index.aspx", false);
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error genérico
                MostrarError("Hubo un error en tu solicitud: " + ex.Message);
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
