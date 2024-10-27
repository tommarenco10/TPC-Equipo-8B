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
                datos.setearConsulta("Select J.IdJugador, P.Nombre, P.Apellido, J.Altura, J.Peso, J.Posicion, c.IdCategoria, c.nombre as NombreCategoria, ej.IdEstadoJugador, ej.nombre as EstadoJugador From Jugador J Left Join Persona P on J.IdPersona = P.IdPersona inner join Categoria c on c.IdCategoria = j.Idcategoria inner join EstadoJugador ej on ej.IdEstadoJugador = j.IdEstadoJugador");
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
                    aux.Categoria = new Categoria();
                    aux.Categoria.IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdCategoria"]) : 0;
                    aux.Categoria.NombreCategoria = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.estadoJugador = new EstadoJugador();
                    aux.estadoJugador.IdEstadoJugador = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
                    aux.estadoJugador.NombreEstado = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;

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


        public List<Jugador> ObtenerJugadoresPorIds(List<int> ids)
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            // Convertir la lista de IDs a un string para usar en la consulta SQL
            string idsString = string.Join(",", ids);

            try
            {
                // Ajusta la consulta para filtrar por IDs
                datos.setearConsulta($"SELECT J.IdJugador, P.Nombre, P.Apellido, J.Altura, J.Peso, J.Posicion, c.IdCategoria, c.Nombre AS NombreCategoria, ej.IdEstadoJugador, ej.Nombre AS EstadoJugador " +
                                     $"FROM Jugador J " +
                                     $"LEFT JOIN Persona P ON J.IdPersona = P.IdPersona " +
                                     $"INNER JOIN Categoria c ON c.IdCategoria = J.IdCategoria " +
                                     $"INNER JOIN EstadoJugador ej ON ej.IdEstadoJugador = J.IdEstadoJugador " +
                                     $"WHERE J.IdJugador IN ({idsString})");

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
                    aux.Categoria = new Categoria();
                    aux.Categoria.IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdCategoria"]) : 0;
                    aux.Categoria.NombreCategoria = datos.Lector["NombreCategoria"] != DBNull.Value ? (string)datos.Lector["NombreCategoria"] : string.Empty;
                    aux.estadoJugador = new EstadoJugador();
                    aux.estadoJugador.IdEstadoJugador = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
                    aux.estadoJugador.NombreEstado = datos.Lector["EstadoJugador"] != DBNull.Value ? (string)datos.Lector["EstadoJugador"] : string.Empty;

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