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
                datos.setearConsulta("INSERT INTO Observacion (Fecha, Descripcion) VALUES (@Fecha, @Descripcion)");

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
