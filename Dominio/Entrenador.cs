using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Entrenador :Persona
    {
        public int IdEntrenador { get; set; }
        public string Rol { get; set; }
        public DateTime FechaContratacion { get; set; }
        public Categoria categoria { get; set; }
    }
}
