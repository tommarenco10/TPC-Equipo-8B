using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Jugador;

namespace Dominio
{
    public class Entrenamiento
    {
        public int IdEntrenamiento { get; set; }
        public DateTime FechaHora { get; set; }
        public TimeSpan Duracion { get; set; }
        public Categoria Categoria { get; set; }
        public string Descripcion { get; set; }
        public List<Jugador> JugadoresCitados { get; set; }
        public EstadoEntrenamiento Estado { get; set; }
        public List<Jugador> JugadoresPresentes { get; set; }
        public string Observaciones { get; set; }
    }
}
