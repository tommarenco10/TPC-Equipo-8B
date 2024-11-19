using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum TipoUsuario
    {
        Administrador = 1,
        CuerpoTecnico = 2,
        CuerpoMedico = 3,
        Socio = 4,
        Hincha = 5,
    }
    public class Usuario
    {

        public long IdUsuario { get; set; }
        public long IdPersona {  get; set; }    
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
