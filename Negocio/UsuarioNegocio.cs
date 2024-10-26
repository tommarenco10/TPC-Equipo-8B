using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
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












    }
}
