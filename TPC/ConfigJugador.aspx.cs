using Dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class ConfigWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();

                    ddlCategoria.DataSource = negocio.listar();
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataBind();

                    EstadoJugadorNegocio negocioEJ = new EstadoJugadorNegocio();

                    ddlEstadoJugador.DataSource = negocioEJ.listar();
                    ddlEstadoJugador.DataValueField = "IdEstadoJugador";
                    ddlEstadoJugador.DataTextField = "NombreEstado";
                    ddlEstadoJugador.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Jugador jugador = new Jugador();
                JugadorNegocio negocio = new JugadorNegocio();

                jugador.Nombres = txtboxNombre.Text;
                jugador.Apellidos = txtboxApellido.Text;
                jugador.FechaNacimiento = DateTime.Parse(txtboxFechaNac.Text);

                jugador.LugarNacimiento = new LugarNacimiento();
                jugador.LugarNacimiento.Pais = txtboxPais.Text;
                jugador.LugarNacimiento.Provincia = txtboxProvincia.Text;
                jugador.LugarNacimiento.Ciudad = txtboxCiudad.Text;

                jugador.Email = txtboxEmail.Text;
                jugador.Altura = int.Parse(txtboxAltura.Text);
                jugador.Peso = decimal.Parse(txtboxPeso.Text);
                jugador.Posicion = txtboxPosicion.Text;

                jugador.Categoria = new Categoria();
                jugador.Categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
                jugador.Categoria.NombreCategoria = ddlCategoria.Text;

                jugador.estadoJugador = new EstadoJugador();
                jugador.estadoJugador.IdEstadoJugador = int.Parse(ddlCategoria.SelectedValue);
                jugador.estadoJugador.NombreEstado = ddlCategoria.Text;

                negocio.AgregarConSP(jugador);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}