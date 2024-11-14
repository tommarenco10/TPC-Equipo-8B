using System;
using System.Data.SqlClient;

namespace Acceso_Datos
{
    public class AccesoDatos
    {
        public SqlConnection conexion { get; }
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=Gestion_Clubes; integrated security=true");
            comando = new SqlCommand();
            comando.Connection = conexion;
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearSP(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void agregarParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public SqlDataReader ejecutarLectura()
        {
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
                return lector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cerrarConexion()
        {
            if (conexion != null)
                conexion.Close();
        }
    }
}
