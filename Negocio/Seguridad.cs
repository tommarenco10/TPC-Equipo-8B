using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Acceso_Datos;
using System.Web;
using System.IO;


namespace Negocio
{
    public class Seguridad
    {

        public bool sesionActiva(Object user)
        {
            return user is Usuario usuario && usuario != null;
        }

        public bool esAdmin(Object user)
        {
            return user is Usuario usuario && usuario.Tipo == TipoUsuario.Administrador;
        }

        public bool esEntrenador(Object user)
        {
            return user is Usuario usuario && usuario.Tipo == TipoUsuario.CuerpoTecnico;
        }

        public bool esMedico(Object user)
        {
            return user is Usuario usuario && usuario.Tipo == TipoUsuario.CuerpoMedico;
        }

        public bool esSocio(Object user)
        {
            return user is Usuario usuario && usuario.Tipo == TipoUsuario.Socio;
        }

        public bool esHincha(Object user)
        {
            return user is Usuario usuario && usuario.Tipo == TipoUsuario.Hincha;
        }




        public static (bool isValid, string mensaje) ValidarArchivo(HttpPostedFile archivo, int tamañoMaximoMB = 2)
        {
            
            List<string> extensionesPermitidas = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(archivo.FileName).ToLower();

            
            if (!extensionesPermitidas.Contains(extension))
            {
                return (false, "Solo se permiten imágenes con extensiones .jpg, .jpeg, .png o .gif.");
            }

            
            if (archivo.ContentLength > tamañoMaximoMB * 1024 * 1024)
            {
                return (false, $"El archivo supera el tamaño máximo permitido de {tamañoMaximoMB} MB.");
            }

            return (true, "Archivo válido.");
        }

        public static bool ValidarFechaNacimiento(string fechaTexto, out DateTime fechaNacimiento, int minEdad = 10, int maxEdad = 120)
        {
            if (DateTime.TryParse(fechaTexto, out fechaNacimiento))
            {
                DateTime hoy = DateTime.Now;
                DateTime minFecha = hoy.AddYears(-minEdad);
                DateTime maxFecha = hoy.AddYears(-maxEdad);
                return fechaNacimiento <= minFecha && fechaNacimiento >= maxFecha;
            }
            return false;
        }


        public static bool validaTextosVacios(List<TextBox> textboxes)
        {
            foreach (var textbox in textboxes)
            {
                if (string.IsNullOrWhiteSpace(textbox.Text))
                {
                    return true;
                }
            }
            return false;
        }





        public static (bool isSuccess, string mensaje) ComprobarDatosExistentes(string nombreUsuario, string dni, string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearSP("ComprobarUsuarioExistente");  
                datos.agregarParametro("@NombreUsuario", nombreUsuario);
                datos.agregarParametro("@Dni", dni);
                datos.agregarParametro("@Email", email);   
                SqlParameter parametroResultado = new SqlParameter("@Resultado", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                datos.comando.Parameters.Add(parametroResultado);
                datos.abrirConexion();
                datos.comando.ExecuteNonQuery(); 
                int resultado = Convert.ToInt32(parametroResultado.Value);

                switch (resultado)
                {
                    case 1:
                        return (false, "Nombre de usuario ya registrado.");
                    case 2:
                        return (false, "DNI ya registrado.");
                    case 3:
                        return (false, "Correo electrónico ya registrado.");
                    case 0:
                        return (true, "Todos los datos están disponibles.");
                    default:
                        return (false, "Error al verificar los datos.");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al comprobar datos existentes.", ex);
            }
            finally
            {
               
                datos.cerrarConexion();
            }
        }


    }
}