using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

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
               
                datos.setearConsulta("SELECT DNI,Nombres, Apellidos, Edad, FechaNacimiento, Email, UrlImagen FROM Persona");
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
                        UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : string.Empty,
                        DNI = datos.Lector["DNI"] != DBNull.Value ? (string)datos.Lector["DNI"] : string.Empty
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

        public int agregar(Persona persona)
        {
            AccesoDatos datos = new AccesoDatos();
            int idPersona = 0;

            try
            {
                datos.setearSP("sp_AgregarPersona");

                datos.agregarParametro("@Nombres", persona.Nombres);
                datos.agregarParametro("@Apellidos", persona.Apellidos);
                datos.agregarParametro("@FechaNacimiento", persona.FechaNacimiento);
                datos.agregarParametro("@DNI", persona.DNI);
                datos.agregarParametro("@Email", persona.Email);
                datos.agregarParametro("@UrlImagen", persona.UrlImagen);
                datos.agregarParametro("@Pais", persona.LugarNacimiento.Pais);
                datos.agregarParametro("@Provincia", persona.LugarNacimiento.Provincia);
                datos.agregarParametro("@Ciudad", persona.LugarNacimiento.Ciudad);

                SqlParameter parametroSalida = new SqlParameter
                {
                    ParameterName = "@IdPersona",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };
                datos.comando.Parameters.Add(parametroSalida);

                datos.ejecutarAccion();
                idPersona = Convert.ToInt32(parametroSalida.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return idPersona;
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



        public Persona obtenerPorId(int idPersona)
        {
            AccesoDatos datos = new AccesoDatos();
            Persona persona = null;

            try
            {
                datos.setearConsulta("SELECT IdPersona, Nombre, Apellido, FechaNacimiento, Pais, Provincia, Ciudad, Email, UrlImagen, DNI FROM Persona WHERE IdPersona = @IdPersona");
                datos.agregarParametro("@IdPersona", idPersona);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    persona = new Persona
                    {
                        Id = datos.Lector["IdPersona"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdPersona"]) : 0,
                        Nombres = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellidos = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                        FechaNacimiento = datos.Lector["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(datos.Lector["FechaNacimiento"]) : DateTime.MinValue,
                        DNI = datos.Lector["DNI"] != DBNull.Value ? (string)datos.Lector["DNI"] : string.Empty,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
                        UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : string.Empty,
                        LugarNacimiento = new LugarNacimiento
                        {
                            Pais = datos.Lector["Pais"] != DBNull.Value ? (string)datos.Lector["Pais"] : string.Empty,
                            Provincia = datos.Lector["Provincia"] != DBNull.Value ? (string)datos.Lector["Provincia"] : string.Empty,
                            Ciudad = datos.Lector["Ciudad"] != DBNull.Value ? (string)datos.Lector["Ciudad"] : string.Empty
                        }
                    };
                }

                return persona;
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
