using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ObservacionesConFechaNegocio
    {
        public void agregar(ObservacionConFecha nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Observacion (IdIncidencia, Fecha, Descripcion) VALUES (@IdIncidencia, @Fecha, @Descripcion)");

                datos.agregarParametro("@IdIncidencia", nuevo.IdIncidencia);
                datos.agregarParametro("@Fecha", nuevo.Fecha);
                datos.agregarParametro("@Descripcion", nuevo.Descripcion);

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

        public List<ObservacionConFecha> listarAscendentePorIncidencia(int idIncidencia)
        {
            List<ObservacionConFecha> lista = new List<ObservacionConFecha>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdObservacion, IdIncidencia, Fecha, Descripcion FROM Observacion WHERE IdIncidencia = @id ORDER BY Fecha ASC");
                datos.agregarParametro("@id", idIncidencia);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObservacionConFecha aux = new ObservacionConFecha
                    {
                        IdObservacion = datos.Lector["IdObservacion"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdObservacion"]) : 0,
                        IdIncidencia = datos.Lector["IdIncidencia"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdIncidencia"]) : 0,
                        Fecha = datos.Lector["Fecha"] != DBNull.Value ? (DateTime)datos.Lector["Fecha"] : DateTime.MinValue,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
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


        public List<ObservacionConFecha> listarPorFechaAscendente()
        {
            List<ObservacionConFecha> lista = new List<ObservacionConFecha>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdObservacion, IdIncidencia, Fecha, Descripcion FROM Observacion ORDER BY Fecha ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObservacionConFecha aux = new ObservacionConFecha
                    {
                        IdObservacion = datos.Lector["IdObservacion"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdObservacion"]) : 0,
                        IdIncidencia = datos.Lector["IdIncidencia"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdIncidencia"]) : 0,
                        Fecha = datos.Lector["Fecha"] != DBNull.Value ? (DateTime)datos.Lector["Fecha"] : DateTime.MinValue,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
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




        public Incidencia obtenerIncidenciaConObservaciones(int idIncidencia)
        {
            Incidencia incidencia = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consultar la incidencia
                datos.setearConsulta("SELECT IdIncidencia, IdJugador, Estado, FechaRegistro, FechaResolucion, Descripcion FROM Incidencia WHERE IdIncidencia = @id");
                datos.agregarParametro("@id", idIncidencia);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    incidencia = new Incidencia
                    {
                        IdIncidencia = Convert.ToInt32(datos.Lector["IdIncidencia"]),
                        IdJugador = Convert.ToInt32(datos.Lector["IdJugador"]),
                        Estado = datos.Lector["Estado"] != DBNull.Value && (bool)datos.Lector["Estado"],
                        FechaRegistro = datos.Lector["FechaRegistro"] != DBNull.Value ? (DateTime)datos.Lector["FechaRegistro"] : DateTime.MinValue,
                        FechaResolución = datos.Lector["FechaResolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaResolucion"] : DateTime.MinValue,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
                    };
                }
                datos.cerrarConexion();

                // Consultar las observaciones asociadas
                incidencia.Observaciones = listarAscendentePorIncidencia(idIncidencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return incidencia;
        }

    }
}
