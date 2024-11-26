using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
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
        public int tipoPagina; //1-ELIMINAR, 2-MODIFICAR, OTRO-AGREGAR

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
                    btnAgregar.Visible = false;
                    btnEliminar.Visible = false;
                    btnModificar.Visible = false;

                    // GUARDA EL TIPO DE PÁGINA
                    tipoPagina = Convert.ToInt32(Request.QueryString["id"]);
                    Session["tipoPagina"] = tipoPagina;

                    if (tipoPagina == 1) btnEliminar.Visible = true;
                    if (tipoPagina == 2) btnModificar.Visible = true;
                    if (tipoPagina != 2 && tipoPagina != 1) btnAgregar.Visible = true;


                    if (tipoPagina == 1 || tipoPagina == 2)
                    {
                        if (Session["idJugador"] != null)
                        {
                            CargarDatosJugador();
                            CargarFormularioModificar();
                        }
                    }
                    else
                    {
                        InicializarFormularioAgregar();
                    }

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        private void CargarDatosJugador()
        {
            var negocio = new JugadorNegocio();
            var lista = negocio.listar();
            int id = int.Parse(Session["idJugador"].ToString());
            var jugador = lista.FirstOrDefault(x => x.IdJugador == id);

            if (jugador != null)
            {
                txtId.Text = jugador.IdJugador.ToString();
                txtNombre.Text = jugador.Nombres;
                txtApellido.Text = jugador.Apellidos;
                txtFechaNacimiento.Text = jugador.FechaNacimiento.ToString("yyyy-MM-dd");
                txtEmail.Text = jugador.Email;
                txtAltura.Text = jugador.Altura.ToString();
                txtPeso.Text = jugador.Peso.ToString();
                txtPosicion.Text = jugador.Posicion;
                string nuevaUrlImagen = jugador.UrlImagen;

                if (!string.IsNullOrEmpty(nuevaUrlImagen))
                {
                    imgJugadorPrincipal.ImageUrl = nuevaUrlImagen;
                }
                else
                {
                    imgJugadorPrincipal.ImageUrl = "https://via.placeholder.com/160";
                }

                Session.Add("jugadorSeleccionado", jugador);
            }
        }

        private void InicializarFormularioAgregar()
        {
            CargarPaises();
            ddlProvincia.Items.Clear();
            ddlProvincia.Items.Insert(0, new ListItem("Seleccione una provincia", ""));
            ddlProvincia.Enabled = false;

            ddlCiudad.Items.Clear();
            ddlCiudad.Items.Insert(0, new ListItem("Seleccione una ciudad", ""));
            ddlCiudad.Enabled = false;

            ddlPais.SelectedIndexChanged += (s, e) => CargarProvincias(ddlPais.SelectedValue);
            ddlProvincia.SelectedIndexChanged += (s, e) => CargarCiudades(ddlProvincia.SelectedValue);

            cargarCategorias();
        }

        private void CargarFormularioModificar()
        {
            if (Session["jugadorSeleccionado"] is Jugador jugador)
            {
                CargarPaises();
                ddlPais.SelectedValue = jugador.LugarNacimiento.Pais.ToString();

                CargarProvincias(jugador.LugarNacimiento.Pais.ToString());
                ddlProvincia.SelectedValue = jugador.LugarNacimiento.Provincia;

                CargarCiudades(jugador.LugarNacimiento.Provincia.ToString());
                ddlCiudad.SelectedValue = jugador.LugarNacimiento.Ciudad;

                cargarCategorias();
                ddlCategoria.SelectedValue = jugador.Categoria.IdCategoria.ToString();
            }
            else
            {
                MostrarError("No se encontró información del jugador para modificar.");
            }
        }

        private void cargarCategorias()
        {
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            List<Categoria> listaCategorias = negocioCategoria.listar();
            ddlCategoria.DataSource = listaCategorias;
            ddlCategoria.DataTextField = "NombreCategoria";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
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
                ddlCiudad.Enabled = ciudades.Count > 0;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar ciudades: " + ex.Message);
            }
        }


        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string paisSeleccionado = ddlPais.SelectedValue;
                CargarProvincias(paisSeleccionado);

                // Reiniciar provincias y ciudades
                ddlProvincia.SelectedIndex = 0;
                ddlCiudad.SelectedIndex = 0;
                ddlProvincia.Enabled = true;
                ddlCiudad.Enabled = false;
            }
            catch (Exception ex)
            {
                MostrarError("Error al seleccionar país: " + ex.Message);
            }
        }


        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string provinciaSeleccionada = ddlProvincia.SelectedValue;
                CargarCiudades(provinciaSeleccionada);
                ddlCiudad.SelectedIndex = 0;
                ddlCiudad.Enabled = true;
            }
            catch (Exception ex)
            {
                MostrarError("Error al seleccionar provincia: " + ex.Message);
            }
        }


        private Jugador ObtenerJugadorDesdeFormulario()
        {
            Jugador jugador;

            // Verificar si hay un jugador previamente seleccionado en la sesión
            if (Session["jugadorSeleccionado"] != null)
            {
                jugador = (Jugador)Session["jugadorSeleccionado"];
            }
            else
            {
                jugador = new Jugador();
            }

            try
            {
                
                string nuevaUrlImagen = jugador.UrlImagen;

                if (!string.IsNullOrEmpty(nuevaUrlImagen))
                {
                    imgJugadorPrincipal.ImageUrl = nuevaUrlImagen;
                }
                else
                {
                    imgJugadorPrincipal.ImageUrl = "https://via.placeholder.com/150";
                }
                

                string rutaRelativa = null;

                // Si se ha subido una imagen nueva, procesarla
                if (fileInput.HasFile)
                {
                    string rutaCarpeta = Server.MapPath("~/Images/");
                    string nombreArchivo = "profile-" + txtNombre.Text + "-" + Guid.NewGuid() + Path.GetExtension(fileInput.FileName);
                    string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
                    fileInput.SaveAs(rutaCompleta);
                    rutaRelativa = "Images/" + nombreArchivo;
                }
                else
                {
                    // Si no se subió una nueva imagen, mantener la imagen existente
                    rutaRelativa = jugador.UrlImagen;
                }

                // Asignar valores a las propiedades del jugador
                jugador.UrlImagen = rutaRelativa;

                jugador.Nombres = txtNombre.Text;
                jugador.Apellidos = txtApellido.Text;
                jugador.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                jugador.LugarNacimiento.Pais = ddlPais.SelectedItem.Text;
                jugador.LugarNacimiento.Provincia = ddlProvincia.SelectedItem.Text;
                jugador.LugarNacimiento.Ciudad = ddlCiudad.SelectedItem.Text;

                jugador.Email = txtEmail.Text;
                jugador.Altura = int.Parse(txtAltura.Text);
                jugador.Peso = decimal.Parse(txtPeso.Text);
                jugador.Posicion = txtPosicion.Text;

                jugador.Categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
                jugador.Categoria.NombreCategoria = ddlCategoria.SelectedItem.Text;

                // Eliminar el jugador de la sesión después de obtener los datos
                if (Session["jugadorSeleccionado"] != null)
                {
                    Session.Remove("jugadorSeleccionado");
                }

                return jugador;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
                return null;
            }
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
                jugador.IdJugador = int.Parse(txtId.Text);
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
                    negocio.EliminarJugador(int.Parse(txtId.Text));
                    Response.Redirect("PlantillaJugadores.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }


        private void MostrarError(string mensaje)
        {
            Session.Add("error", mensaje);
            Response.Redirect("Error.aspx", false);
        }


        public List<Control> ComprobarVacio()
        {
            List<Control> nueva = new List<Control>();

            nueva.Add(txtId);
            nueva.Add(txtNombre);
            nueva.Add(txtApellido);
            nueva.Add(txtFechaNacimiento);
            nueva.Add(txtEmail);
            nueva.Add(txtAltura);
            nueva.Add(txtPeso);
            nueva.Add(txtPosicion);

            nueva.Add(ddlPais);
            nueva.Add(ddlProvincia);
            nueva.Add(ddlCiudad);
            nueva.Add(ddlCategoria);

            return nueva;
        }


        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            string url = txtUrlImagen.Text.Trim();
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                imgJugadorPrincipal.ImageUrl = url;
            }
            else
            {
                imgJugadorPrincipal.ImageUrl = "https://via.placeholder.com/160";
            }
        }

        public string getImagenJugador()
        {

            if ((int)Session["tipoPagina"] == 2 || (int)Session["tipoPagina"] == 1)
            {
                Jugador nuevo = (Jugador)(Session["jugadorSeleccionado"]);
                return nuevo.UrlImagen;
            }
            else
            {

                return "/Images/placeholder.png";
            }
        }




    }
}