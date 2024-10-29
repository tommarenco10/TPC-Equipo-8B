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
        public List<Categoria> CategoriasAsignadas { get; set; }
        public List<Entrenamiento> EntrenamientosProgramados { get; set; }
    }
}
