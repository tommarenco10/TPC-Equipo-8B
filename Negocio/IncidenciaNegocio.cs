using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class IncidenciaNegocio
    {
        public List<Incidencia> listar()
        {
            List<Incidencia> lista = new List<Incidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdIncidencia, IdJugador, IdEstadoJugador, Descripcion, Estado, FechaRegistro, FechaResolucion FROM incidencia");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();

                    aux.IdIncidencia = datos.Lector["IdIncidencia"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdIncidencia"]) : 0;
                    aux.IdJugador = datos.Lector["IdJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdJugador"]) : 0;
                    aux.IdEstadoJugador = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    aux.Estado = datos.Lector["Estado"] != DBNull.Value ? (bool)datos.Lector["Estado"] : true;
                    aux.FechaRegistro = datos.Lector["FechaRegistro"] != DBNull.Value ? (DateTime)datos.Lector["FechaRegistro"] : DateTime.MinValue;
                    aux.FechaResolución = datos.Lector["FechaResolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaResolucion"] : DateTime.MinValue;
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

        public void agregar(Incidencia nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO incidencia VALUES (@IdJugador, @IdEstadoJugador, @Descripcion, @Estado, @FechaRegistro, @FechaResolucion)");
                datos.agregarParametro("@IdJugador", nuevo.IdJugador);
                datos.agregarParametro("@IdEstadoJugador", nuevo.IdEstadoJugador);
                datos.agregarParametro("@Descripcion", nuevo.Descripcion);
                datos.agregarParametro("@Estado", nuevo.Estado);
                datos.agregarParametro("@FechaRegistro", nuevo.FechaRegistro);
                datos.agregarParametro("@FechaResolucion", nuevo.FechaResolución);
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

        public void modificar(Incidencia modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE incidencia SET IdJugador = @IdJugador, IdEstadoJugador = @IdEstadoJugador, Descripcion = @Descripcion, Estado = @Estado, FechaRegistro = @FechaRegistro, FechaResolucion = @FechaResolucion WHERE IdIncidencia = @IdIncidencia");
                datos.agregarParametro("@IdJugador", modificado.IdJugador);
                datos.agregarParametro("@IdEstadoJugador", modificado.IdEstadoJugador);
                datos.agregarParametro("@Descripcion", modificado.Descripcion);
                datos.agregarParametro("@Estado", modificado.Estado);
                datos.agregarParametro("@FechaRegistro", modificado.FechaRegistro);
                datos.agregarParametro("@FechaResolucion", modificado.FechaResolución);
                datos.agregarParametro("@IdIncidencia", modificado.IdIncidencia);

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
