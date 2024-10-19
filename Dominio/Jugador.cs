using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Jugador : Persona
    {
        public int IdJugador { get; set; }
        public bool Estado { get; set; }
        public string Posicion { get; set; }
        public string Categoria { get; set; }
        public int Altura { get; set; }
    }
}
