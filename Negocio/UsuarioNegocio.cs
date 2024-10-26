using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    internal class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Contraseña, Email, IdTipoUsuario FROM USUARIO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IdUsuario"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdUsuario"]) : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Contraseña = datos.Lector["Contraseña"] != DBNull.Value ? (string)datos.Lector["Contraseña"] : string.Empty,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
                        Tipo = datos.Lector["IdTipoUsuario"] != DBNull.Value
                            ? (TipoUsuario)Convert.ToInt32(datos.Lector["IdTipoUsuario"])
                            : TipoUsuario.Hincha  // se puede establecer un valor por defecto en caso de que sea null
                    };

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }





        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET IdUsuario = @id, Nombre = @nombre, Contraseña=@contraseña, Email=@email,IdTipoUsuario=@tipoUsuario WHERE Id = @id");
                datos.agregarParametro("@id", usuario.IdUsuario);
                datos.agregarParametro("@nombre", usuario.Nombre);
                datos.agregarParametro("@contraseña", usuario.Contraseña);
                datos.agregarParametro("@email", usuario.Email);
                datos.agregarParametro("@tipoUsuario", usuario.Tipo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el artículo", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public void eliminar(int id) 
        {
            AccesoDatos datos=new AccesoDatos();

            try
            {
                datos.setearConsulta("Delete from Usuarios where Id=@id");
                datos.agregarParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerrarConexion();

            }
        }




        public void agregar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuario (Nombre, Contraseña, Email, IdTipoUsuario) VALUES (@Nombre, @Contraseña, @Email, @IdTipoUsuario)");
                datos.agregarParametro("@Nombre", usuario.Nombre);
                datos.agregarParametro("@Contraseña", usuario.Contraseña);
                datos.agregarParametro("@Email", usuario.Email);
                datos.agregarParametro("@IdTipoUsuario", usuario.Tipo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }




        public bool usuarioExistente(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id FROM Usuario WHERE Nombre = @nombre");
                datos.agregarParametro("@nombre", nombre);

                // ejecutar la consulta y leer los resultados
                SqlDataReader lector = datos.ejecutarLectura();

                //verificar si hay alguna row con el nombre ya registrado.
                return lector.HasRows;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el código de artículo.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool loguear(Usuario usuario)
        {
            AccesoDatos datos=new AccesoDatos();
            try
            {
                datos.setearConsulta("Select id,IdTipoUsuario from Usuario Where Nombre=@nombre AND Contraseña=@contraseña");
                datos.agregarParametro("@nombre",usuario.Nombre);
                datos.agregarParametro("@contraseña", usuario.Contraseña);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["id"];
                    usuario.Tipo = (TipoUsuario)(int)datos.Lector["IdTipoUsuario"];
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerrarConexion();
            }

        }



    }
}
