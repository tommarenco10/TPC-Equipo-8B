using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoJugadorNegocio
    {
        public List<EstadoJugador> listar()
        {
            List<EstadoJugador> lista = new List<EstadoJugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select IdEstadoJugador, nombre from EstadoJugador");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoJugador aux = new EstadoJugador();

                    aux.IdEstadoJugador = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
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
