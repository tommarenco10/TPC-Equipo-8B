using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Acceso_Datos;
using System.Text.RegularExpressions;
using System.Collections;

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
                    aux.Categoria.NombreCategoria = datos.Lector["NombreCategoria"] != DBNull.Value ? (string)datos.Lector["NombreCategoria"] : string.Empty;
                    aux.estadoJugador = new EstadoJugador();
                    aux.estadoJugador.IdEstado = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
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

        public List<Jugador> ListarJugadoresPorIds(List<int> ids)
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
                    aux.estadoJugador.IdEstado = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
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

        public Jugador ObtenerJugadorPorId(int id)
        {
            Jugador jugador = new Jugador();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta($"SELECT J.IdJugador, P.Nombre, P.Apellido, P.FechaNacimiento, P.pais, P.provincia, P.ciudad," +
                    $" P.Email, P.UrlImagen, J.Altura, J.peso, J.posicion, C.IdCategoria, C.nombre AS NombreCategoria," +
                    $" EJ.IdEstadoJugador, EJ.nombre AS EstadoJugador FROM Jugador J LEFT JOIN Persona P ON J.IdPersona = P.IdPersona" +
                    $" INNER JOIN Categoria C ON C.IdCategoria = J.IdCategoria INNER JOIN EstadoJugador EJ ON EJ.IdEstadoJugador = J.IdEstadoJugador" +
                    $" WHERE J.IdJugador = {id}");

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    jugador.IdJugador = datos.Lector["IdJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdJugador"]) : 0;
                    jugador.Nombres = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                    jugador.Apellidos = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty;
                    jugador.FechaNacimiento = datos.Lector["FechaNacimiento"] != DBNull.Value ? (DateTime)datos.Lector["FechaNacimiento"] : DateTime.MinValue;
                    jugador.LugarNacimiento = new LugarNacimiento();
                    jugador.LugarNacimiento.Pais = datos.Lector["pais"] != DBNull.Value ? (string)datos.Lector["pais"] : string.Empty;
                    jugador.LugarNacimiento.Provincia = datos.Lector["provincia"] != DBNull.Value ? (string)datos.Lector["provincia"] : string.Empty;
                    jugador.LugarNacimiento.Ciudad = datos.Lector["ciudad"] != DBNull.Value ? (string)datos.Lector["ciudad"] : string.Empty;
                    jugador.Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty;
                    jugador.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : string.Empty;
                    jugador.Altura = datos.Lector["Altura"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Altura"]) : 0;
                    jugador.Peso = datos.Lector["Peso"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Peso"]) : 0m;
                    jugador.Posicion = datos.Lector["Posicion"] != DBNull.Value ? (string)datos.Lector["Posicion"] : string.Empty;
                    jugador.Categoria = new Categoria();
                    jugador.Categoria.IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdCategoria"]) : 0;
                    jugador.Categoria.NombreCategoria = datos.Lector["NombreCategoria"] != DBNull.Value ? (string)datos.Lector["NombreCategoria"] : string.Empty;
                    jugador.estadoJugador = new EstadoJugador();
                    jugador.estadoJugador.IdEstado = datos.Lector["IdEstadoJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoJugador"]) : 0;
                    jugador.estadoJugador.NombreEstado = datos.Lector["EstadoJugador"] != DBNull.Value ? (string)datos.Lector["EstadoJugador"] : string.Empty;
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

            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearSP("Listar_Jugador");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Jugador jugador = new Jugador();
                    jugador.IdJugador = Convert.ToInt32(datos.Lector["IdJugador"]);
                    jugador.Nombres = (string)datos.Lector["Nombre"];
                    jugador.Apellidos = (string)datos.Lector["Apellido"];
                    jugador.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    jugador.LugarNacimiento = new LugarNacimiento();
                    jugador.LugarNacimiento.Pais = (string)datos.Lector["pais"];
                    jugador.LugarNacimiento.Provincia = (string)datos.Lector["provincia"];
                    jugador.LugarNacimiento.Ciudad = (string)datos.Lector["ciudad"];
                    jugador.Email = (string)datos.Lector["email"];
                    jugador.Altura = Convert.ToInt32(datos.Lector["Altura"]);
                    jugador.Peso = Convert.ToDecimal(datos.Lector["Peso"]);
                    jugador.Posicion = (string)datos.Lector["posicion"];
                    jugador.Categoria = new Categoria();
                    jugador.Categoria.IdCategoria = Convert.ToInt32(datos.Lector["IdCategoria"]);
                    jugador.Categoria.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    jugador.estadoJugador = new EstadoJugador();
                    jugador.estadoJugador.IdEstado = Convert.ToInt32(datos.Lector["IdEstadoJugador"]);
                    jugador.estadoJugador.NombreEstado = (string)datos.Lector["EstadoJugador"];

                    lista.Add(jugador);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Jugador> FiltroAvanzado(string categoria, string estado)
        {

            List<Jugador> lista = new List<Jugador>();

            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearSP("FiltroAvanzado");
                datos.agregarParametro("@NombreCategoria", categoria);
                datos.agregarParametro("@NombreEstado", estado);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Jugador jugador = new Jugador();
                    jugador.IdJugador = Convert.ToInt32(datos.Lector["IdJugador"]);
                    jugador.Nombres = (string)datos.Lector["Nombre"];
                    jugador.Apellidos = (string)datos.Lector["Apellido"];
                    jugador.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    jugador.LugarNacimiento = new LugarNacimiento();
                    jugador.LugarNacimiento.Pais = (string)datos.Lector["pais"];
                    jugador.LugarNacimiento.Provincia = (string)datos.Lector["provincia"];
                    jugador.LugarNacimiento.Ciudad = (string)datos.Lector["ciudad"];
                    jugador.Email = (string)datos.Lector["email"];
                    jugador.Altura = Convert.ToInt32(datos.Lector["Altura"]);
                    jugador.Peso = Convert.ToDecimal(datos.Lector["Peso"]);
                    jugador.Posicion = (string)datos.Lector["posicion"];
                    jugador.Categoria = new Categoria();
                    jugador.Categoria.IdCategoria = Convert.ToInt32(datos.Lector["IdCategoria"]);
                    jugador.Categoria.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    jugador.estadoJugador = new EstadoJugador();
                    jugador.estadoJugador.IdEstado = Convert.ToInt32(datos.Lector["IdEstadoJugador"]);
                    jugador.estadoJugador.NombreEstado = (string)datos.Lector["EstadoJugador"];

                    lista.Add(jugador);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                    Jugador jugador = new Jugador();
                    jugador.IdJugador = Convert.ToInt32(datos.Lector["IdJugador"]);
                    jugador.Nombres = (string)datos.Lector["Nombre"];
                    jugador.Apellidos = (string)datos.Lector["Apellido"];
                    jugador.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    jugador.LugarNacimiento = new LugarNacimiento();
                    jugador.LugarNacimiento.Pais = (string)datos.Lector["pais"];
                    jugador.LugarNacimiento.Provincia = (string)datos.Lector["provincia"];
                    jugador.LugarNacimiento.Ciudad = (string)datos.Lector["ciudad"];
                    jugador.Email = (string)datos.Lector["email"];
                    jugador.Altura = Convert.ToInt32(datos.Lector["Altura"]);
                    jugador.Peso = Convert.ToDecimal(datos.Lector["Peso"]);
                    jugador.Posicion = (string)datos.Lector["posicion"];
                    jugador.Categoria = new Categoria();
                    jugador.Categoria.IdCategoria = Convert.ToInt32(datos.Lector["IdCategoria"]);
                    jugador.Categoria.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    jugador.estadoJugador = new EstadoJugador();
                    jugador.estadoJugador.IdEstado = Convert.ToInt32(datos.Lector["IdEstadoJugador"]);
                    jugador.estadoJugador.NombreEstado = (string)datos.Lector["EstadoJugador"];

                    lista.Add(jugador);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    Jugador jugador = new Jugador();
                    jugador.IdJugador = Convert.ToInt32(datos.Lector["IdJugador"]);
                    jugador.Nombres = (string)datos.Lector["Nombre"];
                    jugador.Apellidos = (string)datos.Lector["Apellido"];
                    jugador.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    jugador.LugarNacimiento = new LugarNacimiento();
                    jugador.LugarNacimiento.Pais = (string)datos.Lector["pais"];
                    jugador.LugarNacimiento.Provincia = (string)datos.Lector["provincia"];
                    jugador.LugarNacimiento.Ciudad = (string)datos.Lector["ciudad"];
                    jugador.Email = (string)datos.Lector["email"];
                    jugador.Altura = Convert.ToInt32(datos.Lector["Altura"]);
                    jugador.Peso = Convert.ToDecimal(datos.Lector["Peso"]);
                    jugador.Posicion = (string)datos.Lector["posicion"];
                    jugador.Categoria = new Categoria();
                    jugador.Categoria.IdCategoria = Convert.ToInt32(datos.Lector["IdCategoria"]);
                    jugador.Categoria.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    jugador.estadoJugador = new EstadoJugador();
                    jugador.estadoJugador.IdEstado = Convert.ToInt32(datos.Lector["IdEstadoJugador"]);
                    jugador.estadoJugador.NombreEstado = (string)datos.Lector["EstadoJugador"];

                    lista.Add(jugador);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}