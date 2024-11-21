using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class ConfigWeb : System.Web.UI.Page
    {
        public bool ConfirmarEliminacion { get; set; }
        public int tipoPagina;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                if (Seguridad.esAdmin(Session["user"]))
                {
                    if (!IsPostBack)
                    {
                        tipoPagina = Convert.ToInt32(Request.QueryString["id"]);
                        Session["tipoPagina"] = tipoPagina;
                    }
                }
            }
            else
            {
                Session.Add("error", "Necesitas ser administrador para acceder.");
                Response.Redirect("Error.aspx", false);
            }
            try
            {
                ConfirmarEliminacion = false;

                // VALIDO EN TODAS LAS CARGAS
                if (Session["tipoPagina"] != null)
                {
                    tipoPagina = (int)Session["tipoPagina"];
                }

                if (!IsPostBack)
                {
                    // CONFIGURACIONES INICIALES

                    // GUARDA EL TIPO DE PÁGINA
                    tipoPagina = Convert.ToInt32(Request.QueryString["id"]);
                    Session["tipoPagina"] = tipoPagina;

                    if (tipoPagina == 1 || tipoPagina == 2)
                    {
                        if (Session["idJugador"] != null)
                        {
                            CargarJugador((int)Session["idJugador"]);
                        }
                    }

                    //CARGA DDL
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    ddlCategoria.DataSource = negocio.listar();
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataBind();
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

        }

        private void CargarJugador(int idJugador)
        {
            try
            {
                JugadorNegocio negocioJugador = new JugadorNegocio();
                Jugador jugador = negocioJugador.ObtenerJugadorPorId(idJugador);
                txtId.Text = jugador.IdJugador.ToString();
                txtNombre.Text = jugador.Nombres.ToString();
                txtApellido.Text = jugador.Apellidos.ToString();
                txtFechaNacimiento.Text = jugador.FechaNacimiento.ToString();
                txtPais.Text = jugador.LugarNacimiento.Pais.ToString();
                txtProvincia.Text = jugador.LugarNacimiento.Provincia.ToString();
                txtCiudad.Text = jugador.LugarNacimiento.Ciudad.ToString();
                txtEmail.Text = jugador.Email.ToString();
                txtAltura.Text = jugador.Altura.ToString();
                txtPeso.Text = jugador.Peso.ToString();
                txtPosicion.Text = jugador.Posicion.ToString();
                ddlCategoria.SelectedValue = jugador.Categoria.IdCategoria.ToString();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected bool validaciones()
        {
            int idCategoria;
            DateTime fechaNacimiento;
            string nombre, apellido, pais, provincia, ciudad, email, posicion;
            int altura;
            decimal peso;
            string urlImagen;

            try
            {
                //VALIDAR CATEGORIA
                if (int.Parse(ddlCategoria.SelectedValue) == 0)
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, seleccione una categoría.";
                    return false;
                }
                else
                {
                    idCategoria = int.Parse(ddlCategoria.SelectedValue);
                }

                //VALIDAR FECHAS
                if (string.IsNullOrEmpty(txtFechaNacimiento.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete la fecha de nacimiento.";
                    return false;
                }

                else if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha no válida. Por favor, ingrese una fecha válida.";
                    return false;
                }

                else if (fechaNacimiento.Date >= DateTime.Today)
                {
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = "Fecha no válida. La fecha seleccionada debe ser en el pasado.";
                    return false;
                }

                //VALIDAR STRING
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtPais.Text)
                     || string.IsNullOrEmpty(txtProvincia.Text) || string.IsNullOrEmpty(txtCiudad.Text) || string.IsNullOrEmpty(txtEmail.Text)
                      || string.IsNullOrEmpty(txtPosicion.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Por favor, complete todos los campos.";
                    return false;
                }
                else if (txtNombre.Text.Length > 30)
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "Algunos campos superan el largo máximo.";
                    return false;
                }
                else
                {
                    nombre = txtNombre.Text.ToString();
                    apellido = txtApellido.Text.ToString();
                    pais = txtPais.Text.ToString();
                    provincia = txtProvincia.Text.ToString();
                    ciudad = txtCiudad.Text.ToString();
                    email = txtEmail.Text.ToString();
                    posicion = txtPosicion.Text.ToString();
                }

                if (txtUrlImagen.Text.Length > 300)
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "La URL para la imagen supera el largo máximo.";
                    return false;
                }
                else { 
                    urlImagen = txtUrlImagen.Text.ToString();
                }

                //VALIDAR ENTEROS
                if (string.IsNullOrWhiteSpace(txtAltura.Text) || string.IsNullOrWhiteSpace(txtPeso.Text))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "La altura y el peso no pueden estar vacíos.";
                    return false;
                }

                if (!int.TryParse(txtAltura.Text, out altura))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "La altura ingresada no es un número entero válido.";
                    return false;
                }

                if (!decimal.TryParse(txtPeso.Text, out peso))
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "El peso ingresado no es un número decimal válido.";
                    return false;
                }

                if (altura < 100 || altura > 240)
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "La altura seleccionada no está dentro del rango válido.";
                    return false;
                }

                if (peso < 20 || peso > 150)
                {
                    lblError.CssClass = "alert alert-warning";
                    lblError.Text = "El peso seleccionada no está dentro del rango válido.";
                    return false;
                }

                //GUARDADO DEL OBJETO VALIDADO EN SESSION

                Jugador jugador = (Jugador)Session["jugadorSeleccionado"];

                if (jugador == null)
                {
                    jugador = new Jugador();
                }
                if (jugador.Categoria == null)
                {
                    jugador.Categoria = new Categoria();
                }

                jugador.Categoria.IdCategoria = idCategoria;
                jugador.FechaNacimiento = fechaNacimiento;
                jugador.Nombres = nombre;
                jugador.Apellidos = apellido;
                jugador.LugarNacimiento.Pais = pais;
                jugador.LugarNacimiento.Provincia = provincia;
                jugador.LugarNacimiento.Ciudad = ciudad;
                jugador.Email = email;
                jugador.Posicion = posicion;
                jugador.Altura = altura;
                jugador.Peso = peso;
                jugador.DNI = txtDNI.ToString();
                jugador.UrlImagen = urlImagen;
                Session["jugadorSeleccionado"] = jugador;
                return true;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
                return false;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaciones())
                {
                    Jugador jugador = new Jugador();
                    JugadorNegocio negocio = new JugadorNegocio();

                    jugador = (Jugador)Session["jugadorSeleccionado"];
                    jugador.estadoJugador = new EstadoJugador();
                    jugador.estadoJugador.IdEstado = 1; // DISPONIBLE POR DEFECTO

                    negocio.AgregarConSP(jugador);
                    Response.Redirect("PlantillaJugadores.aspx", false);
                }
                else { lblError.Text = "No valido"; }

            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaciones())
                {
                    Jugador jugador = new Jugador();
                    JugadorNegocio negocio = new JugadorNegocio();

                    jugador = (Jugador)Session["jugadorSeleccionado"];
                    jugador.estadoJugador = new EstadoJugador();
                    jugador.estadoJugador.IdEstado = 1; // DISPONIBLE POR DEFECTO

                    negocio.ModificarJugador(jugador);
                    Response.Redirect("PlantillaJugadores.aspx", false);
                }
            }
            catch (ThreadAbortException) { }
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
                    negocio.EliminarJugador(int.Parse(txtId.Text));
                    Response.Redirect("PlantillaJugadores.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            string urlImagen = txtUrlImagen.Text;

            if (!string.IsNullOrWhiteSpace(urlImagen))
            {
                imgPerfil.ImageUrl = urlImagen;
            }
            else
            {
                imgPerfil.ImageUrl = "https://via.placeholder.com/160";
            }
        }
    }
}