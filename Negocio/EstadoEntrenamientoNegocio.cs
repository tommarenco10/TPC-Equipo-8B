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

                    aux.IdEstado = datos.Lector["IdEstadoEntrenamiento"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdEstadoEntrenamiento"]) : 0;
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

        public void agregar(EstadoEntrenamiento nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO EstadoEntrenamiento VALUES (@Estado)");
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

        public void modificar(EstadoEntrenamiento modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE EstadoEntrenamiento SET nombre = @Estado WHERE IdEstadoEntrenamiento = @Id");
                datos.agregarParametro("@Id", modificado.IdEstado);
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

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM EstadoEntrenamiento WHERE IdEstadoEntrenamiento = @id");
                datos.agregarParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
