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

					Entrenador.Add(aux);
				}

				return Entrenador;

			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
