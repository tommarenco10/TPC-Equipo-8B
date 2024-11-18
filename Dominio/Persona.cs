using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Persona
    {
        public long Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string DNI { get; set; }  
        public LugarNacimiento LugarNacimiento { get; set; }
        public string Email { get; set; }
        public string UrlImagen { get; set; }
    }
}
