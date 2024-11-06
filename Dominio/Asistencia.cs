using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdEntrenamiento { get; set; } //public Entrenamiento Entrenamiento { get; set; }
        public int IdJugador { get; set; } //public Jugador Jugador { get; set; }
        public bool EstadoAsistencia { get; set; }
        public string Observaciones { get; set; }
    }
}
