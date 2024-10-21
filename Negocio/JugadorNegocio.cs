using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Acceso_Datos;
using System.Text.RegularExpressions;

namespace negocio
{
    public class JugadorNegocio
    {
        public List<Jugador> listar()
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select J.IdJugador, P.Nombre, P.Apellido, J.Altura, J.Peso, J.Posicion From Jugador J Left Join Persona P on J.IdPersona = P.IdPersona");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Jugador aux = new Jugador();

                    aux.IdJugador = datos.Lector["IdJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdJugador"]) : 0;
                    aux.Nombres = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                    aux.Apellidos = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty;
                    aux.Altura = datos.Lector["Altura"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Altura"]) : 0;
                    aux.Peso = datos.Lector["Peso"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Peso"]) : 0m;
                    aux.Posicion = datos.Lector["Posicion"] != DBNull.Value ? (string)datos.Lector["Posicion"] : string.Empty;

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