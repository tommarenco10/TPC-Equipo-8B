﻿using System.Collections.Generic;
using System.Data.SqlClient;
using Acceso_Datos;
using System;
using Dominio;

namespace Negocio
{
    public class LugarNacimientoNegocio
    {
        private AccesoDatos accesoDatos;

        public LugarNacimientoNegocio()
        {
            accesoDatos = new AccesoDatos(); // Instanciamos AccesoDatos
        }

        // Obtener los países
        public List<string> ObtenerPaises()
        {
            try
            {
                List<string> paises = new List<string>();

                // Definir la consulta para obtener los países
                accesoDatos.setearConsulta("SELECT Nombre FROM Pais");

                // Ejecutar la consulta y obtener los resultados
                SqlDataReader reader = accesoDatos.ejecutarLectura();

                // Leer los resultados
                while (reader.Read())
                {
                    paises.Add(reader["Nombre"].ToString());
                }

                return paises;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        // Obtener las provincias de un país
        public List<string> ObtenerProvincias(string pais)
        {
            try
            {
                List<string> provincias = new List<string>();

                // Definir la consulta para obtener las provincias filtradas por país
                accesoDatos.setearConsulta("SELECT Provincia.Nombre FROM Provincia JOIN Pais ON Provincia.IdPais = Pais.IdPais WHERE Pais.Nombre = @Pais");
                accesoDatos.agregarParametro("@Pais", pais);

                // Ejecutar la consulta y obtener los resultados
                SqlDataReader reader = accesoDatos.ejecutarLectura();

                // Leer los resultados
                while (reader.Read())
                {
                    provincias.Add(reader["Nombre"].ToString());
                }

                return provincias;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }


        // Obtener las ciudades de una provincia
        public List<string> ObtenerCiudades(string provincia)
        {
            try
            {
                List<string> ciudades = new List<string>();

                // Definir la consulta para obtener las ciudades filtradas por provincia
                accesoDatos.setearConsulta("SELECT Ciudad.Nombre FROM Ciudad JOIN Provincia ON Ciudad.IdProvincia = Provincia.IdProvincia WHERE Provincia.Nombre = @Provincia");
                accesoDatos.agregarParametro("@Provincia", provincia);

                // Ejecutar la consulta y obtener los resultados
                SqlDataReader reader = accesoDatos.ejecutarLectura();

                // Leer los resultados
                while (reader.Read())
                {
                    ciudades.Add(reader["Nombre"].ToString());
                }

                return ciudades;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }
    }
}