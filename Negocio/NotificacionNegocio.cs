using System;
using System.Collections.Generic;
using Acceso_Datos;
using Dominio;

namespace Negocio
{
    public class NotificacionNegocio
    {
        
        public List<Notificacion> listar()
        {
            List<Notificacion> lista = new List<Notificacion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT n.IdNotificacion, n.IdUsuario, u.Nombre AS NombreUsuario, n.Mensaje, n.FechaEnvio FROM Notificacion n JOIN Usuario u ON n.IdUsuario = u.IdUsuario");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Notificacion notificacion = new Notificacion
                    {
                        IdNotificacion = datos.Lector["IdNotificacion"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdNotificacion"]) : 0,
                        UsuarioDestinatario = new Usuario
                        {
                            IdUsuario = datos.Lector["IdUsuario"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdUsuario"]) : 0,
                            Nombre = datos.Lector["NombreUsuario"] != DBNull.Value ? (string)datos.Lector["NombreUsuario"] : string.Empty
                        },
                        Mensaje = datos.Lector["Mensaje"] != DBNull.Value ? (string)datos.Lector["Mensaje"] : string.Empty,
                        FechaEnvio = datos.Lector["FechaEnvio"] != DBNull.Value ? (DateTime)datos.Lector["FechaEnvio"] : DateTime.MinValue
                    };

                    lista.Add(notificacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }


        public void crear(Notificacion nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Notificacion (IdUsuario, Mensaje, FechaEnvio) VALUES (@IdUsuario, @Mensaje, @FechaEnvio)");
                datos.agregarParametro("@IdUsuario", nueva.UsuarioDestinatario.IdUsuario);
                datos.agregarParametro("@Mensaje", nueva.Mensaje);
                datos.agregarParametro("@FechaEnvio", nueva.FechaEnvio);

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

        public void actualizar(Notificacion notificacion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Notificacion SET IdUsuario = @IdUsuario, Mensaje = @Mensaje, FechaEnvio = @FechaEnvio WHERE IdNotificacion = @IdNotificacion");
                datos.agregarParametro("@IdNotificacion", notificacion.IdNotificacion);
                datos.agregarParametro("@IdUsuario", notificacion.UsuarioDestinatario.IdUsuario);
                datos.agregarParametro("@Mensaje", notificacion.Mensaje);
                datos.agregarParametro("@FechaEnvio", notificacion.FechaEnvio);

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

        public void eliminar(int idNotificacion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Notificacion WHERE IdNotificacion = @IdNotificacion");
                datos.agregarParametro("@IdNotificacion", idNotificacion);

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
