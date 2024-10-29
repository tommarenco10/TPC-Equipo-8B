using System;
using System.Collections.Generic;
using Dominio;
using Acceso_Datos;

namespace Negocio
{
    public class EntrenadorNegocio
    {
        // Alta
        public void Agregar(Entrenador nuevo,Persona persona)
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

        // Baja
        public void Eliminar(int idEntrenador)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Eliminar_Entrenador");
                datos.agregarParametro("@IdEntrenador", idEntrenador);
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

        // Modificación
        public void Modificar(Entrenador entrenador)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Modificar_Entrenador");
                datos.agregarParametro("@IdEntrenador", entrenador.IdEntrenador);
                datos.agregarParametro("@Nombre", entrenador.Nombres);
                datos.agregarParametro("@Apellido", entrenador.Apellidos);
                datos.agregarParametro("@FechaNacimiento", entrenador.FechaNacimiento);
                datos.agregarParametro("@Pais", entrenador.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", entrenador.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", entrenador.LugarNacimiento.Ciudad);
                datos.agregarParametro("@Email", entrenador.Email);
                datos.agregarParametro("@Rol", entrenador.Rol);

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

        // Listar
        public List<Entrenador> Listar()
        {
            List<Entrenador> lista = new List<Entrenador>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT E.IdEntrenador, P.Nombre, P.Apellido, E.Rol FROM Entrenador E INNER JOIN Persona P ON E.IdPersona = P.IdPersona");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Entrenador aux = new Entrenador();
                    aux.IdEntrenador = (int)datos.Lector["IdEntrenador"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Rol = (string)datos.Lector["Rol"];
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
