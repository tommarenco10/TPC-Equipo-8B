﻿using Acceso_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select IdCategoria, nombre from Categoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdCategoria"]) : 0;
                    aux.NombreCategoria = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;

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

        public Categoria obtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Categoria categoria = null;

            try
            {
                datos.setearConsulta("SELECT IdCategoria, nombre, FROM Categoria WHERE IdCategoria = @Id");
                datos.agregarParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    categoria = new Categoria
                    {
                        IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IdCategoria"]) : 0,
                        NombreCategoria = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty
                    };
                }

                return categoria;
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
