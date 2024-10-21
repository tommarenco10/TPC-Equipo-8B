using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Jugador : Persona
    {
        public enum EstadoJugador
        {
            Disponible = 1,
            Lesionado = 2,
            Sancionado = 3,
            CitadoSeleccion = 4,
            Licencia = 5
        }

        public int IdJugador { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public string Posicion { get; set; }
        public Categoria Categoria { get; set; }
        public EstadoJugador Estado { get; set; }
        public List<Incidencia> Incidencias { get; set; }
        public List<Asistencia> Asistencias { get; set; }
        public List<Reporte> Reportes { get; set; }



    }
}
