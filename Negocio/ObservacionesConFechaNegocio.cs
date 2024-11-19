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
                    ObservacionConFecha aux = new ObservacionConFecha();

                    aux.IdObservacion = datos.Lector["IdObservacion"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdObservacion"]) : 0;
                    aux.IdIncidencia = datos.Lector["IdIncidencia"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdIncidencia"]) : 0;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    aux.Fecha = datos.Lector["Fecha"] != DBNull.Value ? (DateTime)datos.Lector["Fecha"] : DateTime.MinValue;

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
                datos.setearConsulta("SELECT IdObservacion, Fecha, Descripcion FROM Observacion ORDER BY Fecha ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObservacionConFecha aux = new ObservacionConFecha();

                    aux.IdObservacion = datos.Lector["IdObservacion"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdObservacion"]) : 0;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    aux.Fecha = datos.Lector["Fecha"] != DBNull.Value ? (DateTime)datos.Lector["Fecha"] : DateTime.MinValue;

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

    }
}
