using System;
using System.Collections.Generic;
using Dominio;
using Acceso_Datos;

namespace Negocio
{
    public class SocioNegocio
    {
        // Alta
        public void Agregar(Socio nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Agregar_Socio");
                datos.agregarParametro("@Nombre", nuevo.Nombres);
                datos.agregarParametro("@Apellido", nuevo.Apellidos);
                datos.agregarParametro("@FechaNacimiento", nuevo.FechaNacimiento);
                datos.agregarParametro("@Pais", nuevo.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", nuevo.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", nuevo.LugarNacimiento.Ciudad);
                datos.agregarParametro("@Email", nuevo.Email);

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
        public void Eliminar(int idSocio)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Eliminar_Socio");
                datos.agregarParametro("@IdSocio", idSocio);
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
        public void Modificar(Socio socio)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSP("Modificar_Socio");
                datos.agregarParametro("@IdSocio", socio.IdSocio);
                datos.agregarParametro("@Nombre", socio.Nombres);
                datos.agregarParametro("@Apellido", socio.Apellidos);
                datos.agregarParametro("@FechaNacimiento", socio.FechaNacimiento);
                datos.agregarParametro("@Pais", socio.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", socio.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", socio.LugarNacimiento.Ciudad);
                datos.agregarParametro("@Email", socio.Email);

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
        public List<Socio> Listar()
        {
            List<Socio> lista = new List<Socio>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT S.IdSocio, P.Nombre, P.Apellido, S.FechaAlta FROM Socio S INNER JOIN Persona P ON S.IdPersona = P.IdPersona");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Socio aux = new Socio();
                    aux.IdSocio = (int)datos.Lector["IdSocio"];
                    aux.Nombres = (string)datos.Lector["Nombre"];
                    aux.Apellidos = (string)datos.Lector["Apellido"];
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
