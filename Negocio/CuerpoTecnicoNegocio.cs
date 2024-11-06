using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CuerpoTecnicoNegocio
    {
        public List<Entrenador> Listar()
        {

			List<Entrenador> Entrenador = new List<Entrenador>();

			try
			{
				AccesoDatos datos = new AccesoDatos();
				datos.setearSP("Listar_Entrenador");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Entrenador aux = new Entrenador();
					aux.IdEntrenador = Convert.ToInt32(datos.Lector["IdEntrandor"]);
					aux.Nombres = (string)datos.Lector["Nombre"];
					aux.Apellidos = (string)datos.Lector["Apellido"];
					aux.Email = (string)datos.Lector["Email"];
					aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
					aux.LugarNacimiento = new LugarNacimiento();
					aux.LugarNacimiento.Pais = (string)datos.Lector["pais"];
					aux.LugarNacimiento.Provincia = (string)datos.Lector["provincia"];
					aux.LugarNacimiento.Ciudad = (string)datos.Lector["ciudad"];
					aux.Rol = (string)datos.Lector["Rol"];
					aux.FechaContratacion = (DateTime)datos.Lector["FechaContratacion"];
					aux.categoria = new Categoria();
					aux.categoria.IdCategoria = Convert.ToInt32(datos.Lector["IdCategoria"]);
					if (!(datos.Lector["UrlImagen"] == DBNull.Value))
						aux.UrlImagen = (string)datos.Lector["UrlImagen"];

					Entrenador.Add(aux);
				}

				return Entrenador;

			}
			catch (Exception ex)
			{

				throw ex;
			}
        }

        public void AgregarEntrenador(Entrenador nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Agregar_Entrenador");

                datos.agregarParametro("@Nombre", nuevo.Nombres);
                datos.agregarParametro("@Apellido", nuevo.Apellidos);
                datos.agregarParametro("@FechaNacimiento", nuevo.FechaNacimiento);
                datos.agregarParametro("@Pais", nuevo.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", nuevo.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", nuevo.LugarNacimiento.Ciudad);
                datos.agregarParametro("@Email", nuevo.Email);
                datos.agregarParametro("@Rol", nuevo.Rol);
                datos.agregarParametro("@FechaContratacion", nuevo.FechaContratacion);
                datos.agregarParametro("@IdCategoria", nuevo.categoria.IdCategoria);

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

		public void ModificarEntrenador(Entrenador modificado)
		{
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Actualizar_Entrenador");

                datos.agregarParametro("@IdEntrenador", modificado.IdEntrenador);
                datos.agregarParametro("@Nombre", modificado.Nombres);
                datos.agregarParametro("@Apellido", modificado.Apellidos);
                datos.agregarParametro("@FechaNacimiento", modificado.FechaNacimiento);
                datos.agregarParametro("@Pais", modificado.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", modificado.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", modificado.LugarNacimiento.Ciudad);
                datos.agregarParametro("@Email", modificado.Email);
                datos.agregarParametro("@Rol", modificado.Rol);
                datos.agregarParametro("@FechaContratacion", modificado.FechaContratacion);
                datos.agregarParametro("@IdCategoria", modificado.categoria.IdCategoria);

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

		public void EliminarEntrenador(int id)
		{
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Eliminar_Entrenador");
                datos.agregarParametro("@IdEntrenador", id);
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
