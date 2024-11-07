using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Incidencia
    {
        public enum EstadoIncidencia
        {
            Cerrada = 0,
            Abierta = 1
        }

        public int IdIncidencia { get; set; }
        public Jugador Jugador { get; set; }
        public EstadoJugador EstadoJugador { get; set; }
        public string Descripcion { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaResolución { get; set; }
    }
}
