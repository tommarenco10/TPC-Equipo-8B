using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoEntrenamientoNegocio
    {
        public List<EstadoEntrenamiento> listar()
        {
            List<EstadoEntrenamiento> lista = new List<EstadoEntrenamiento>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdEstadoEntrenamiento, nombre FROM EstadoEntrenamiento");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoEntrenamiento aux = new EstadoEntrenamiento();

                    aux.IdEstadoEntrenamiento = datos.Lector["IdEstadoEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoEntrenamiento"]) : 0;
                    aux.NombreEstado = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;

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
