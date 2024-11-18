using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Jugador : Persona
    {
        public int IdJugador { get; set; }
        public int Altura { get; set; }
        public decimal Peso { get; set; }
        public string Posicion { get; set; }
        public EstadoJugador estadoJugador { get; set; }
        public Categoria Categoria { get; set; }

        public Jugador()
        {
            LugarNacimiento = new LugarNacimiento();
            Categoria = new Categoria();
        }
    }
}
