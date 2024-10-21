using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Incidencia;

namespace Dominio
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public Usuario UsuarioDestinatario { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
    }
}
