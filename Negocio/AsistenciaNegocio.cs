using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AsistenciaNegocio
    {
        public void ActualizarAsistencias(int idEntrenamiento, List<int> nuevaListaJugadoresCitados)
        {
            List<int> jugadoresCitadosActuales = ObtenerJugadoresCitadosActuales(idEntrenamiento);

            var jugadoresParaEliminar = jugadoresCitadosActuales.Except(nuevaListaJugadoresCitados).ToList();
            var jugadoresParaAgregar = nuevaListaJugadoresCitados.Except(jugadoresCitadosActuales).ToList();

            EliminarAsistenciaMultiple(idEntrenamiento, jugadoresParaEliminar);
            AgregarAsistenciaMultiple(idEntrenamiento, jugadoresParaAgregar);
        }

        public List<int> ObtenerJugadoresCitadosActuales(int idEntrenamiento)
        {
            List<int> jugadoresCitados = new List<int>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdJugador FROM Asistencia WHERE IdEntrenamiento = @idEntrenamiento");
                datos.agregarParametro("@idEntrenamiento", idEntrenamiento);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int aux;
                    aux = datos.Lector["IdJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdJugador"]) : 0;
                    jugadoresCitados.Add(aux);
                }

                return jugadoresCitados;
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

        public void AgregarAsistenciaMultiple(int idEntrenamiento, List<int> jugadoresParaAgregar)
        {
            foreach (var idJugador in jugadoresParaAgregar)
            {
                AgregarAsistenciaSimple(idEntrenamiento, idJugador);
            }
        }

        public void AgregarAsistenciaSimple(int idEntrenamiento, int idJugador)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Asistencia (IdEntrenamiento, IdJugador, Asistio, Observaciones) VALUES (@idEntrenamiento, @idJugador, @estadoAsistencia, @observaciones)");

                datos.agregarParametro("@idEntrenamiento", idEntrenamiento);
                datos.agregarParametro("@idJugador", idJugador);
                datos.agregarParametro("@estadoAsistencia", false);
                datos.agregarParametro("@observaciones", string.Empty);

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

        public void EliminarAsistenciaMultiple(int idEntrenamiento, List<int> jugadoresParaAgregar)
        {
            foreach (var idJugador in jugadoresParaAgregar)
            {
                EliminarAsistenciasSimple(idEntrenamiento, idJugador);
            }
        }

        public void EliminarAsistenciasSimple(int idEntrenamiento, int idJugador)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Asistencia WHERE IdEntrenamiento = @idEntrenamiento AND IdJugador = @idJugador");

                datos.agregarParametro("@idEntrenamiento", idEntrenamiento);
                datos.agregarParametro("@idJugador", idJugador);

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

        //NO UTILIZADAS
        public List<Asistencia> listar()
        {
            List<Asistencia> lista = new List<Asistencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdAsistenia, IdEntrenamiento, IdJugador, Asistio, Observaciones FROM Asistencia");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Asistencia aux = new Asistencia();

                    aux.IdAsistencia = datos.Lector["IdAsistenia"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdAsistenia"]) : 0;
                    aux.IdJugador = datos.Lector["IdJugador"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdJugador"]) : 0;
                    aux.IdEntrenamiento = datos.Lector["IdEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEntrenamiento"]) : 0;
                    aux.EstadoAsistencia = datos.Lector["Asistio"] != DBNull.Value ? (bool)datos.Lector["Asistio"] : false;
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

        public void agregar(Asistencia nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Asistencia VALUES (@IdEntrenamiento, @IdJugador, @Asistio, @Observaciones)");

                datos.agregarParametro("@IdEntrenamiento", nuevo.IdEntrenamiento);
                datos.agregarParametro("@IdJugador", nuevo.IdJugador);
                datos.agregarParametro("@Asistio", nuevo.EstadoAsistencia);
                datos.agregarParametro("@Observaciones", nuevo.Observaciones);

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

        public void modificar(Asistencia modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Asistencia SET Asistio = @Asistio, Observaciones = @Observaciones WHERE IdAsistenia = @IdAsistencia, IdEntrenamiento = @IdEntrenamiento, IdJugador = @IdJugador");

                datos.agregarParametro("@IdAsistencia", modificado.IdAsistencia);
                datos.agregarParametro("@IdEntrenamiento", modificado.IdEntrenamiento);
                datos.agregarParametro("@IdJugador", modificado.IdJugador);
                datos.agregarParametro("@Asistio", modificado.EstadoAsistencia);
                datos.agregarParametro("@Observaciones", modificado.Observaciones);

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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Asistencia WHERE IdAsistenia = @IdAsistencia");
                datos.agregarParametro("@IdAsistencia", id);
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

        public void eliminarAsistenciaPorEntrenamiento(int idEntrenamiento)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Asistencia WHERE IdEntrenamiento = @IdEntrenamiento");
                datos.agregarParametro("@IdEntrenamiento", idEntrenamiento);
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
