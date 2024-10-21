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
        public enum EstadoEntrenamiento
        {
            Programado = 1,
            EnCurso = 2,
            Finalizado = 3
        }

        public int IdEntrenamiento { get; set; }
        public DateTime FechaHora { get; set; }
        public float Duracion { get; set; }
        public Categoria Categoria { get; set; }
        public string Descripcion { get; set; }
        public List<Jugador> JugadoresCitados { get; set; }
        public List<Jugador> JugadoresPresentes { get; set; }
        public EstadoEntrenamiento Estado { get; set; }
        public string Observaciones { get; set; }
    }
}
