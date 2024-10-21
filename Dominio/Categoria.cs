using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public List<Jugador> Jugadores { get; set; }
        public List<Entrenador> Entrenadores { get; set; }
    }
}
