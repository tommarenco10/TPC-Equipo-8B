using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


namespace Negocio
{
    public class Seguridad
    {

        public bool sesionActiva(Object user)
        {

            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null)
            {
                return true;

            }
            else { return false; }
        }


        public bool esAdmin(Object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.Tipo == TipoUsuario.Administrador)
            {
                return true;
            }
            else { return false; }
        }


        public bool esEntrenador(Object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.Tipo == TipoUsuario.CuerpoTecnico)
            {
                return true;
            }
            else { return false; }
        }


        public bool esMedico(Usuario user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.Tipo == TipoUsuario.CuerpoMedico)
            {
                return true;
            }
            else { return false; }
        }


        public bool esSocio(Usuario user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.Tipo == TipoUsuario.Socio)
            {
                return true;
            }
            else { return false; }
        }


        public bool esHincha(Usuario user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.Tipo == TipoUsuario.Hincha)
            {
                return true;
            }
            else { return false; }
        }


        public static bool validaTextoVacio(Object control)
        {
            if (control is TextBox)
            {
                if (string.IsNullOrEmpty(((TextBox)control).Text))
                    return true;
                else
                    return false;

            }
            return false;
        }


        public static bool validaTextosVacios(List<String> textos)
        {

            foreach (var textbox in textos)
            {
                if(string.IsNullOrEmpty(textbox)){
                        return true;
                    }
            }
            return false;
        }

    }
}