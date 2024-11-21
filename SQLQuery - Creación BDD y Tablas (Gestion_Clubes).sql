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
        LugarNacimientoNegocio lugarNacimientoNegocio = new LugarNacimientoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtboxId.Enabled = false;
                ConfirmarEliminacion = false;

                if (!IsPostBack)
                {
                    
                    CargarCategoriasYEstados();
                    CargarPaises();


                    if (Session["idJugador"] != null)
                    {
                        CargarDatosJugador();
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        private void CargarCategoriasYEstados()
        {
            var negocioCategoria = new CategoriaNegocio();
            ddlCategoria.DataSource = negocioCategoria.listar();
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataTextField = "NombreCategoria";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccionar", "0"));

            var negocioEJ = new EstadoJugadorNegocio();
            ddlEstadoJugador.DataSource = negocioEJ.listar();
            ddlEstadoJugador.DataValueField = "IdEstado";
            ddlEstadoJugador.DataTextField = "NombreEstado";
            ddlEstadoJugador.DataBind();
            ddlEstadoJugador.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }


        private void CargarDatosJugador()
        {
            var negocio = new JugadorNegocio();
            var lista = negocio.ListarJugador();
            int id = int.Parse(Session["idJugador"].ToString());
            var jugador = lista.FirstOrDefault(x => x.IdJugador == id);

            if (jugador != null)
            {
                txtboxId.Text = jugador.IdJugador.ToString();
                txtboxNombre.Text = jugador.Nombres;
                txtboxApellido.Text = jugador.Apellidos;
                txtFechaNacimiento.Text = jugador.FechaNacimiento.ToString("yyyy-MM-dd");

                // Asignación de los valores a los DropDownList
                ddlPais.SelectedValue = jugador.LugarNacimiento.Pais;
                CargarProvincias(ddlPais.SelectedValue); // Cargar provincias del país seleccionado
                ddlProvincia.SelectedValue = jugador.LugarNacimiento.Provincia;
                CargarCiudades(ddlProvincia.SelectedValue); // Cargar ciudades de la provincia seleccionada
                ddlCiudad.SelectedValue = jugador.LugarNacimiento.Ciudad;

                txtboxEmail.Text = jugador.Email;
                txtboxAltura.Text = jugador.Altura.ToString();
                txtboxPeso.Text = jugador.Peso.ToString();
                txtboxPosicion.Text = jugador.Posicion;
                ddlCategoria.SelectedValue = jugador.Categoria.IdCategoria.ToString();
                ddlEstadoJugador.SelectedValue = jugador.estadoJugador.IdEstado.ToString();
            }
        }


        private Jugador ObtenerJugadorDesdeFormulario()
        {
            var jugador = new Jugador
            {
                Nombres = txtboxNombre.Text,
                Apellidos = txtboxApellido.Text,
                FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                LugarNacimiento = new LugarNacimiento
                {
                    Pais = ddlPais.SelectedItem.Text,
                    Provincia = ddlProvincia.SelectedItem.Text,
                    Ciudad = ddlCiudad.SelectedItem.Text,
                },
                Email = txtboxEmail.Text,
                Altura = int.Parse(txtboxAltura.Text),
                Peso = decimal.Parse(txtboxPeso.Text),
                Posicion = txtboxPosicion.Text,
                Categoria = new Categoria
                {
                    IdCategoria = int.Parse(ddlCategoria.SelectedValue),
                    NombreCategoria = ddlCategoria.SelectedItem.Text
                },
                estadoJugador = new EstadoJugador
                {
                    IdEstado = int.Parse(ddlEstadoJugador.SelectedValue),
                    NombreEstado = ddlEstadoJugador.SelectedItem.Text
                }
            };

            return jugador;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var jugador = ObtenerJugadorDesdeFormulario();
                var negocio = new JugadorNegocio();
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
                var jugador = ObtenerJugadorDesdeFormulario();
                jugador.IdJugador = int.Parse(txtboxId.Text);
                var negocio = new JugadorNegocio();
                negocio.ModificarJugador(jugador);
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
                    var negocio = new JugadorNegocio();
                    negocio.EliminarJugador(int.Parse(txtboxId.Text));
                    Response.Redirect("PlantillaJugadores.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }



        private void CargarPaises()
        {
            try
            {
                List<string> paises = lugarNacimientoNegocio.ObtenerPaises();
                ddlPais.DataSource = paises;
                ddlPais.DataBind();
                ddlPais.Items.Insert(0, new ListItem("Seleccione un país", ""));
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar países: " + ex.Message);
            }
        }

        private void CargarProvincias(string pais)
        {
            try
            {
                List<string> provincias = lugarNacimientoNegocio.ObtenerProvincias(pais);
                ddlProvincia.DataSource = provincias;
                ddlProvincia.DataBind();
                ddlProvincia.Items.Insert(0, new ListItem("Seleccione una provincia", ""));
                ddlProvincia.Enabled = provincias.Count > 0;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar provincias: " + ex.Message);
            }
        }

        private void CargarCiudades(string provincia)
        {
            try
            {
                List<string> ciudades = lugarNacimientoNegocio.ObtenerCiudades(provincia);
                ddlCiudad.DataSource = ciudades;
                ddlCiudad.DataBind();
                ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar ciudades: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            Session.Add("error", mensaje);
            Response.Redirect("Error.aspx", false);
        }


    }
}