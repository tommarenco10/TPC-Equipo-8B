using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ObservacionConFecha
    {
        public int IdObservacion {  get; set; }
        public int IdIncidencia { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
    }
}
