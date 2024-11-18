using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Incidencia
    {

        public int IdIncidencia { get; set; }
        public int IdJugador { get; set; }
        public EstadoJugador EstadoJugador { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; } // 0 - CERRADA, 1 - ABIERTA
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaResolución { get; set; }
        public List<ObservacionConFecha> Observaciones { get; set; }

        public Incidencia()
        {
            EstadoJugador = new EstadoJugador();
        }
    }
}
