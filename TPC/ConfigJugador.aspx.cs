using Dominio;
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
        public bool ConfirmarEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtboxId.Enabled = false;
                ConfirmarEliminacion = false;

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

                if (Request.QueryString["IdJugador"] != null && !IsPostBack)
                {
                    List<Jugador> lista = new List<Jugador>();
                    JugadorNegocio negocio = new JugadorNegocio();
                    lista = negocio.ListarJugador();

                    int id = int.Parse(Request.QueryString["IdJugador"].ToString());
                    txtboxId.Text = id.ToString();

                    Jugador jugador = new Jugador();
                    jugador = lista.Find(x => x.IdJugador == id);

                    txtboxNombre.Text = jugador.Nombres.ToString();
                    txtboxApellido.Text = jugador.Apellidos.ToString();
                    txtboxFechaNac.Text = jugador.FechaNacimiento.ToString();
                    txtboxPais.Text = jugador.LugarNacimiento.Pais.ToString();
                    txtboxProvincia.Text = jugador.LugarNacimiento.Provincia.ToString();
                    txtboxCiudad.Text = jugador.LugarNacimiento.Ciudad.ToString();
                    txtboxEmail.Text = jugador.Email.ToString();
                    txtboxAltura.Text = jugador.Altura.ToString();
                    txtboxPeso.Text = jugador.Peso.ToString();
                    txtboxPosicion.Text = jugador.Posicion.ToString();
                    ddlCategoria.SelectedValue = jugador.Categoria.IdCategoria.ToString();
                    ddlEstadoJugador.SelectedValue = jugador.estadoJugador.IdEstado.ToString();
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
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
                jugador.estadoJugador.IdEstado = int.Parse(ddlEstadoJugador.SelectedValue);
                jugador.estadoJugador.NombreEstado = ddlCategoria.Text;

                negocio.AgregarConSP(jugador);
                Response.Redirect("PlantillaJugadores.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Jugador jugador = new Jugador();
                JugadorNegocio negocio = new JugadorNegocio();

                jugador.IdJugador = int.Parse(txtboxId.Text);
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
                jugador.estadoJugador.IdEstado = int.Parse(ddlEstadoJugador.SelectedValue);
                jugador.estadoJugador.NombreEstado = ddlCategoria.Text;

                negocio.AgregarConSP(jugador);
                Response.Redirect("PlantillaJugadores.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmarEliminacion = true;
        }

        protected void BtnEliminarConfirmado_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkboxConfirmado.Checked)
                {
                    JugadorNegocio negocio = new JugadorNegocio();
                    negocio.EliminarJugador(int.Parse(txtboxId.Text));
                    Response.Redirect("PlantillaJugadores.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}