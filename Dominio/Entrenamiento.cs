using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Entrenamiento
    {
        public DateTime FechaHora { get; set; }
        public string Categoria { get; set; }
        public bool Asistencia { get; set; }
        public String Observacion { get; set; }
        public String Estado { get; set; }
    }
}
