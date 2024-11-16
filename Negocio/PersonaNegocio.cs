using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Negocio
{
    public class PersonaNegocio
    {
        public List<Persona> listar()
        {
            List<Persona> lista = new List<Persona>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
               
                datos.setearConsulta("SELECT Nombres, Apellidos, Edad, FechaNacimiento, Email, UrlImagen FROM Persona");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Persona persona = new Persona
                    {
                        Nombres = datos.Lector["Nombres"] != DBNull.Value ? (string)datos.Lector["Nombres"] : string.Empty,
                        Apellidos = datos.Lector["Apellidos"] != DBNull.Value ? (string)datos.Lector["Apellidos"] : string.Empty,
                        Edad = datos.Lector["Edad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Edad"]) : 0,
                        FechaNacimiento = datos.Lector["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(datos.Lector["FechaNacimiento"]) : DateTime.MinValue,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
                        UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : string.Empty
                    };

                    lista.Add(persona);
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

        public void agregar(Persona persona)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
               
                datos.setearConsulta("INSERT INTO Persona (Nombres, Apellidos, Edad, FechaNacimiento, Email, UrlImagen) VALUES (@Nombres, @Apellidos, @Edad, @FechaNacimiento, @Email, @UrlImagen)");
                datos.agregarParametro("@Nombres", persona.Nombres);
                datos.agregarParametro("@Apellidos", persona.Apellidos);
                datos.agregarParametro("@Edad", persona.Edad);
                datos.agregarParametro("@FechaNacimiento", persona.FechaNacimiento);
                datos.agregarParametro("@Email", persona.Email);
                datos.agregarParametro("@UrlImagen", persona.UrlImagen);
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

        public void modificar(Persona persona)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
                datos.setearConsulta("UPDATE Persona SET Nombres = @Nombres, Apellidos = @Apellidos, Edad = @Edad, FechaNacimiento = @FechaNacimiento, Email = @Email, UrlImagen = @UrlImagen WHERE Email = @Email");
                datos.agregarParametro("@Nombres", persona.Nombres);
                datos.agregarParametro("@Apellidos", persona.Apellidos);
                datos.agregarParametro("@Edad", persona.Edad);
                datos.agregarParametro("@FechaNacimiento", persona.FechaNacimiento);
                datos.agregarParametro("@Email", persona.Email);
                datos.agregarParametro("@UrlImagen", persona.UrlImagen);
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

        public void eliminar(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
                datos.setearConsulta("DELETE FROM Persona WHERE Email = @Email");
                datos.agregarParametro("@Email", email);
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
