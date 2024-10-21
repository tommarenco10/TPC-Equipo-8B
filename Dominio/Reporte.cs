using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Reporte
    {
        public int IdReporte { get; set; }
        public Entrenamiento Entrenamiento { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
    }
}
