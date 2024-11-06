using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EntrenamientoNegocio
    {
        public List<Entrenamiento> listar()
        {
            List<Entrenamiento> lista = new List<Entrenamiento>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT E.IdEntrenamiento, E.IdCategoria, C.nombre AS Categoria, E.Descripcion, E.IdEstadoEntrenamiento, EE.nombre AS EstadoEntrenamiento, E.FechaHora, E.Duracion, E.Observaciones FROM entrenamiento AS E INNER JOIN Categoria AS C ON E.IdCategoria = C.IdCategoria INNER JOIN EstadoEntrenamiento AS EE ON E.IdEstadoEntrenamiento = EE.IdEstadoEntrenamiento");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Entrenamiento aux = new Entrenamiento();

                    aux.IdEntrenamiento = datos.Lector["IdEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEntrenamiento"]) : 0;
                    aux.Categoria = new Categoria();
                    aux.Categoria.IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdCategoria"]) : 0;
                    aux.Categoria.NombreCategoria = datos.Lector["Categoria"] != DBNull.Value ? (string)datos.Lector["Categoria"] : string.Empty;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    aux.Estado = new EstadoEntrenamiento();
                    aux.Estado.IdEstadoEntrenamiento = datos.Lector["IdEstadoEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoEntrenamiento"]) : 0;
                    aux.Estado.NombreEstado = datos.Lector["EstadoEntrenamiento"] != DBNull.Value ? (string)datos.Lector["EstadoEntrenamiento"] : string.Empty;
                    aux.FechaHora = datos.Lector["FechaHora"] != DBNull.Value ? (DateTime)datos.Lector["FechaHora"] : DateTime.MinValue;
                    aux.Duracion = datos.Lector["Duracion"] != DBNull.Value ? (TimeSpan)(datos.Lector["Duracion"]) : TimeSpan.Zero;
                    aux.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : string.Empty;

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

        public void agregarEntrenamiento(Entrenamiento nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO entrenamiento (FechaHora, Duracion, IdCategoria, Descripcion, Observaciones, IdEstadoEntrenamiento) VALUES (@FechaHora, @Duracion, @IdCategoria, @Descripcion, @Observaciones, @IdEstadoEntrenamiento)");

                datos.agregarParametro("@FechaHora", nuevo.FechaHora);
                datos.agregarParametro("@Duracion", nuevo.Duracion);
                datos.agregarParametro("@IdCategoria", nuevo.Categoria.IdCategoria);
                datos.agregarParametro("@Descripcion", nuevo.Descripcion);
                datos.agregarParametro("@Observaciones", nuevo.Observaciones);
                datos.agregarParametro("@IdEstadoEntrenamiento", nuevo.Estado.IdEstadoEntrenamiento);

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

        public int obtenerUltimoEntrenamiento()
        {
            int aux = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT TOP 1 IdEntrenamiento FROM entrenamiento ORDER BY IdEntrenamiento DESC");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = datos.Lector["IdEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEntrenamiento"]) : 0;
                }

                return aux;
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

        public Entrenamiento ObtenerEntrenamientoPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT E.IdEntrenamiento, E.IdCategoria, C.nombre AS Categoria, E.Descripcion, E.IdEstadoEntrenamiento, EE.nombre AS EstadoEntrenamiento, E.FechaHora, E.Duracion, E.Observaciones FROM entrenamiento AS E INNER JOIN Categoria AS C ON E.IdCategoria = C.IdCategoria INNER JOIN EstadoEntrenamiento AS EE ON E.IdEstadoEntrenamiento = EE.IdEstadoEntrenamiento WHERE E.IdEntrenamiento = @id");
                datos.agregarParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Entrenamiento aux = new Entrenamiento();

                    aux.IdEntrenamiento = datos.Lector["IdEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEntrenamiento"]) : 0;
                    aux.FechaHora = datos.Lector["FechaHora"] != DBNull.Value ? (DateTime)datos.Lector["FechaHora"] : DateTime.MinValue;
                    aux.Duracion = datos.Lector["Duracion"] != DBNull.Value ? (TimeSpan)datos.Lector["Duracion"] : TimeSpan.Zero;
                    aux.Categoria = new Categoria();
                    aux.Categoria.IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdCategoria"]) : 0;
                    aux.Categoria.NombreCategoria = datos.Lector["Categoria"] != DBNull.Value ? (string)datos.Lector["Categoria"] : string.Empty;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    //JUGADORES CITADOS
                    JugadorNegocio jugadorNegocio = new JugadorNegocio();
                    aux.JugadoresCitados = jugadorNegocio.listarPorEntrenamiento(aux.IdEntrenamiento);




                    aux.Estado = new EstadoEntrenamiento();
                    aux.Estado.IdEstadoEntrenamiento = datos.Lector["IdEstadoEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoEntrenamiento"]) : 0;
                    aux.Estado.NombreEstado = datos.Lector["EstadoEntrenamiento"] != DBNull.Value ? (string)datos.Lector["EstadoEntrenamiento"] : string.Empty;
            //JUGADORES PRESENTES
                    aux.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : string.Empty;

                    return aux;
                }
                else
                {
                    return null;
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


    }
}
