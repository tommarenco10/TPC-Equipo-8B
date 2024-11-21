using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Acceso_Datos;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;

namespace Negocio
{
    public class JugadorNegocio
    {


        public List<Jugador> listar()
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                J.IdJugador, 
                P.Nombre, 
                P.Apellido, 
                P.FechaNacimiento, 
                P.Pais, 
                P.Provincia, 
                P.Ciudad, 
                P.Email, 
                P.UrlImagen, 
                P.DNI, 
                J.Altura, 
                J.Peso, 
                J.Posicion, 
                C.IdCategoria, 
                C.nombre AS NombreCategoria, 
                EJ.IdEstadoJugador, 
                EJ.nombre AS EstadoJugador
            FROM Jugador J 
            LEFT JOIN Persona P ON J.IdPersona = P.IdPersona
            INNER JOIN Categoria C ON C.IdCategoria = J.IdCategoria
            INNER JOIN EstadoJugador EJ ON EJ.IdEstadoJugador = J.IdEstadoJugador");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Jugador jugador = MapearJugador(datos.Lector); // Usamos el mapeo para crear el objeto
                    lista.Add(jugador);
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



        public List<Jugador> ListarJugadores()
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT P.UrlImagen, P.Email, P.pais, P.provincia, P.ciudad, 
                                   J.IdJugador, J.IdPersona, P.Nombre, P.Apellido, 
                                   P.FechaNacimiento, J.Altura, J.Peso, J.Posicion, 
                                   C.IdCategoria, C.nombre as NombreCategoria, 
                                   EJ.IdEstadoJugador, EJ.nombre as EstadoJugador 
                            FROM Jugador J 
                            LEFT JOIN Persona P ON J.IdPersona = P.IdPersona 
                            INNER JOIN Categoria C ON C.IdCategoria = J.IdCategoria 
                            INNER JOIN EstadoJugador EJ ON EJ.IdEstadoJugador = J.IdEstadoJugador");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Jugador aux = MapearJugador((SqlDataReader)datos.Lector);
                    lista.Add(aux);
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

        public List<Jugador> ListarJugadoresPorIds(List<int> ids)
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            string idsString = string.Join(",", ids);

            try
            {
                datos.setearConsulta($"SELECT J.IdJugador, P.Nombre, P.Apellido, P.FechaNacimiento, P.Pais, P.Provincia, P.Ciudad, " +
                                     $"P.Email, P.UrlImagen, J.Altura, J.Peso, J.Posicion, C.IdCategoria, C.Nombre AS NombreCategoria, " +
                                     $"EJ.IdEstadoJugador, EJ.Nombre AS EstadoJugador " +
                                     $"FROM Jugador J " +
                                     $"LEFT JOIN Persona P ON J.IdPersona = P.IdPersona " +
                                     $"INNER JOIN Categoria C ON C.IdCategoria = J.IdCategoria " +
                                     $"INNER JOIN EstadoJugador EJ ON EJ.IdEstadoJugador = J.IdEstadoJugador " +
                                     $"WHERE J.IdJugador IN ({idsString})");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearJugador(datos.Lector));
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }



        public Jugador ObtenerJugadorPorId(int id)
        {
            Jugador jugador = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT J.IdJugador, P.Nombre, P.Apellido, P.FechaNacimiento, 
                                   P.pais, P.provincia, P.ciudad, P.Email, P.UrlImagen, 
                                   J.Altura, J.Peso, J.Posicion, C.IdCategoria, 
                                   C.nombre AS NombreCategoria, EJ.IdEstadoJugador, 
                                   EJ.nombre AS EstadoJugador 
                            FROM Jugador J 
                            LEFT JOIN Persona P ON J.IdPersona = P.IdPersona 
                            INNER JOIN Categoria C ON C.IdCategoria = J.IdCategoria 
                            INNER JOIN EstadoJugador EJ ON EJ.IdEstadoJugador = J.IdEstadoJugador 
                            WHERE J.IdJugador = @IdJugador");

                datos.agregarParametro("@IdJugador", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    jugador = MapearJugador((SqlDataReader)datos.Lector);
                }

                return jugador;
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

        public void AgregarConSP(Jugador nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Agregar_Jugador");

                datos.agregarParametro("@Nombre", nuevo.Nombres);
                datos.agregarParametro("@Apellido", nuevo.Apellidos);
                datos.agregarParametro("@FechaNacimiento", nuevo.FechaNacimiento);
                datos.agregarParametro("@Pais", nuevo.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", nuevo.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", nuevo.LugarNacimiento.Ciudad);
                datos.agregarParametro("@Email", nuevo.Email);
                datos.agregarParametro("@Altura", nuevo.Altura);
                datos.agregarParametro("@Peso", nuevo.Peso);
                datos.agregarParametro("@Posicion", nuevo.Posicion);
                datos.agregarParametro("@IdCategoria", nuevo.Categoria.IdCategoria);
                datos.agregarParametro("@IdEstadoJugador", nuevo.estadoJugador.IdEstado);

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

        public List<Jugador> ListarJugador()
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Listar_Jugador");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearJugador(datos.Lector));
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }


        public List<Jugador> FiltroAvanzado(string categoria, string estado)
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("FiltroAvanzado");
                datos.agregarParametro("@NombreCategoria", categoria);
                datos.agregarParametro("@NombreEstado", estado);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearJugador(datos.Lector));
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }


        public void ModificarJugador(Jugador modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Modificar_Jugador");

                datos.agregarParametro("@IdJugador", modificado.IdJugador);
                datos.agregarParametro("@Nombre", modificado.Nombres);
                datos.agregarParametro("@Apellido", modificado.Apellidos);
                datos.agregarParametro("@FechaNacimiento", modificado.FechaNacimiento);
                datos.agregarParametro("@Pais", modificado.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", modificado.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", modificado.LugarNacimiento.Ciudad);
                datos.agregarParametro("@Email", modificado.Email);
                datos.agregarParametro("@Altura", modificado.Altura);
                datos.agregarParametro("@Peso", modificado.Peso);
                datos.agregarParametro("@Posicion", modificado.Posicion);
                datos.agregarParametro("@IdCategoria", modificado.Categoria.IdCategoria);
                datos.agregarParametro("@IdEstadoJugador", modificado.estadoJugador.IdEstado);
                datos.agregarParametro("@UrlImagen", modificado.UrlImagen);
                datos.agregarParametro("@DNI", modificado.DNI); // Agregar el parámetro DNI

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


        public void EliminarJugador(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Eliminar_Jugador");
                datos.agregarParametro("IdJugador", id);
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


        public List<Jugador> listarPorEntrenamiento(int idEntrenamiento)
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT j.IdJugador, p.Nombre, p.Apellido, p.FechaNacimiento, p.Pais, 
                                p.Provincia, p.Ciudad, p.Email, j.Altura, j.Peso, j.Posicion, 
                                c.IdCategoria, c.Nombre AS NombreCategoria, 
                                ej.IdEstadoJugador, ej.Nombre AS EstadoJugador, p.UrlImagen
                              FROM Persona p
                              INNER JOIN Jugador j ON j.IdPersona = p.IdPersona
                              INNER JOIN Categoria c ON c.IdCategoria = j.IdCategoria
                              INNER JOIN EstadoJugador ej ON ej.IdEstadoJugador = j.IdEstadoJugador
                              INNER JOIN Asistencia a ON a.IdJugador = j.IdJugador
                              WHERE a.IdEntrenamiento = @IdEntrenamiento");
                datos.agregarParametro("@IdEntrenamiento", idEntrenamiento);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearJugador(datos.Lector));
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



        public List<int> listarIdPorEntrenamiento(int idEntrenamiento)
        {
            List<int> lista = new List<int>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT j.IdJugador FROM Jugador j
                              INNER JOIN Asistencia a ON a.IdJugador = j.IdJugador
                              WHERE a.IdEntrenamiento = @IdEntrenamiento");
                datos.agregarParametro("@IdEntrenamiento", idEntrenamiento);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int jugador = Convert.ToInt32(datos.Lector["IdJugador"]);

                    lista.Add(jugador);
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

        public List<Jugador> listarPresentesPorEntrenamiento(int idEntrenamiento)
        {
            List<Jugador> lista = new List<Jugador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT j.IdJugador, p.Nombre, p.Apellido, p.FechaNacimiento, p.Pais, 
                                p.Provincia, p.Ciudad, p.Email, j.Altura, j.Peso, j.Posicion, 
                                c.IdCategoria, c.Nombre AS NombreCategoria, 
                                ej.IdEstadoJugador, ej.Nombre AS EstadoJugador, p.UrlImagen
                              FROM Persona p
                              INNER JOIN Jugador j ON j.IdPersona = p.IdPersona
                              INNER JOIN Categoria c ON c.IdCategoria = j.IdCategoria
                              INNER JOIN EstadoJugador ej ON ej.IdEstadoJugador = j.IdEstadoJugador
                              INNER JOIN Asistencia a ON a.IdJugador = j.IdJugador
                              WHERE a.IdEntrenamiento = @IdEntrenamiento AND a.Asistio = 1");
                datos.agregarParametro("@IdEntrenamiento", idEntrenamiento);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearJugador(datos.Lector));
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


        public void actualizarEstadoPorNuevaIncidencia(int idJugador, Incidencia incidencia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE jugador SET IdEstadoJugador = @IdEstadoJugador WHERE IdJugador = @IdJugador");
                datos.agregarParametro("@IdEstadoJugador", incidencia.EstadoJugador.IdEstado);
                datos.agregarParametro("@IdJugador", idJugador);

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

        public void actualizarEstadoPorFechaYGravedadIncidencia(int idJugador)
        {
            AccesoDatos datos = new AccesoDatos();
            int IdEstadoJugador;
            DateTime FechaResolucion;

            try
            {
                datos.setearConsulta("SELECT TOP (1) IdEstadoJugador, FechaResolucion FROM Incidencia WHERE Estado = 1 AND IdJugador = @IdJugador ORDER BY FechaResolucion DESC, CASE WHEN IdEstadoJugador = 3 THEN 1 WHEN IdEstadoJugador = 4 THEN 2 WHEN IdEstadoJugador = 2 THEN 3 WHEN IdEstadoJugador = 1 THEN 4 ELSE 5 END ASC");
                datos.agregarParametro("@IdJugador", idJugador);
                datos.ejecutarAccion();
                if (datos.Lector.Read())
                {
                    IdEstadoJugador = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
                    FechaResolucion = datos.Lector["FechaResolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaResolucion"] : DateTime.MinValue;
                
                
                
                
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

    }







        private Jugador MapearJugador(SqlDataReader lector)
        {
            Jugador jugador = new Jugador();

            jugador.IdJugador = lector["IdJugador"] != DBNull.Value ? Convert.ToInt32(lector["IdJugador"]) : 0;
            jugador.Nombres = lector["Nombre"] != DBNull.Value ? (string)lector["Nombre"] : string.Empty;
            jugador.Apellidos = lector["Apellido"] != DBNull.Value ? (string)lector["Apellido"] : string.Empty;
            jugador.FechaNacimiento = lector["FechaNacimiento"] != DBNull.Value ? (DateTime)lector["FechaNacimiento"] : DateTime.MinValue;
            jugador.DNI = lector["DNI"] != DBNull.Value ? (string)lector["DNI"] : string.Empty;

            jugador.LugarNacimiento = new LugarNacimiento();
            jugador.LugarNacimiento.Pais = lector["pais"] != DBNull.Value ? (string)lector["pais"] : string.Empty;
            jugador.LugarNacimiento.Provincia = lector["provincia"] != DBNull.Value ? (string)lector["provincia"] : string.Empty;
            jugador.LugarNacimiento.Ciudad = lector["ciudad"] != DBNull.Value ? (string)lector["ciudad"] : string.Empty;

            jugador.Email = lector["Email"] != DBNull.Value ? (string)lector["Email"] : string.Empty;
            jugador.UrlImagen = lector["UrlImagen"] != DBNull.Value && !string.IsNullOrWhiteSpace((string)lector["UrlImagen"])
                ? (string)lector["UrlImagen"]
                : "~/Images/placeholder.png"; // Ruta placeHolder

            jugador.Altura = lector["Altura"] != DBNull.Value ? Convert.ToInt32(lector["Altura"]) : 0;
            jugador.Peso = lector["Peso"] != DBNull.Value ? Convert.ToDecimal(lector["Peso"]) : 0m;
            jugador.Posicion = lector["Posicion"] != DBNull.Value ? (string)lector["Posicion"] : string.Empty;

            jugador.Categoria = new Categoria();
            jugador.Categoria.IdCategoria = lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(lector["IdCategoria"]) : 0;
            jugador.Categoria.NombreCategoria = lector["NombreCategoria"] != DBNull.Value ? (string)lector["NombreCategoria"] : string.Empty;

            jugador.estadoJugador = new EstadoJugador();
            jugador.estadoJugador.IdEstado = lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(lector["IdEstadoJugador"]) : 0;
            jugador.estadoJugador.NombreEstado = lector["EstadoJugador"] != DBNull.Value ? (string)lector["EstadoJugador"] : string.Empty;

            return jugador;
        }











    }
}