using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Incidencia
    {
        public enum TipoIncidencia
        {
            Fisica = 1,
            Disciplinaria = 2
        }
        public enum EstadoIncidencia
        {
            Abierta = 1,
            Cerrada = 2
        }

        public int IdIncidencia { get; set; }
        public Jugador Jugador { get; set; }
        public TipoIncidencia Tipo { get; set; }
        public string Descripcion { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaResolución { get; set; }
    }
}
