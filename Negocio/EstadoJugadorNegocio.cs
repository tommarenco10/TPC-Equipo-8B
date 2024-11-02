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
                datos.setearConsulta("SELECT IdEstadoJugador, nombre FROM EstadoJugador");
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

        public void agregar(EstadoJugador nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO EstadoJugador VALUES (@Estado)");
                datos.agregarParametro("@Estado", nuevo.NombreEstado);
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

        public void modificar(EstadoJugador modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE EstadoJugador SET nombre = @Estado WHERE IdEstadoJugador = @Id");
                datos.agregarParametro("@Id", modificado.IdEstadoJugador);
                datos.agregarParametro("@Estado", modificado.NombreEstado);
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
