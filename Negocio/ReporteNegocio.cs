using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_Datos;

namespace Negocio
{
    internal class ReporteNegocio
    {

       
            public List<Reporte> listar()
            {
                List<Reporte> lista = new List<Reporte>();
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta("SELECT IdReporte,IdEntrenamiento,Descrpcion AS ReporteDescripcion, Observaciones AS ReporteObservaciones,FROM Reporte");
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {

                        Entrenamiento entrenamiento = new Entrenamiento();
                        EntrenamientoNegocio entrenamientoNegocio = new EntrenamientoNegocio();
                        entrenamiento = entrenamientoNegocio.obtenerPorId((int)datos.Lector["IdEntrenamiento"]);
                     

                        Reporte aux = new Reporte
                        {
                            IdReporte = datos.Lector["IdReporte"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdReporte"]) : 0,
                            Descripcion = datos.Lector["ReporteDescripcion"] != DBNull.Value ? (string)datos.Lector["ReporteDescripcion"] : string.Empty,
                            Observaciones = datos.Lector["ReporteObservaciones"] != DBNull.Value ? (string)datos.Lector["ReporteObservaciones"] : string.Empty,
                            Entrenamiento = entrenamiento
                        };

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

            public void crear(Reporte nuevo)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta("INSERT INTO Reporte (IdEntrenamiento, Descrpcion, Observaciones) VALUES (@IdEntrenamiento, @Descripcion, @Observaciones)");
                    datos.agregarParametro("@IdEntrenamiento", nuevo.Entrenamiento.IdEntrenamiento);
                    datos.agregarParametro("@Descripcion", nuevo.Descripcion);
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

            public void eliminar(int idReporte)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta("DELETE FROM Reporte WHERE IdReporte = @IdReporte");
                    datos.agregarParametro("@IdReporte", idReporte);

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






