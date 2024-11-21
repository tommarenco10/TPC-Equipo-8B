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
            var lista = negocio.listar();
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

                Session.Add("jugadorSeleccionado", jugador);
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
                string rutaRelativa = null;

                // Si se ha subido una imagen nueva, procesarla
                if (fileInput.HasFile)
                {
                    string rutaCarpeta = Server.MapPath("~/Images/");
                    string nombreArchivo = "profile-" + txtboxNombre.Text + "-" + Guid.NewGuid() + Path.GetExtension(fileInput.FileName);
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
                jugador.Nombres = txtboxNombre.Text;
                jugador.Apellidos = txtboxApellido.Text;
                jugador.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                jugador.LugarNacimiento.Pais = ddlPais.SelectedItem.Text;
                jugador.LugarNacimiento.Provincia = ddlProvincia.SelectedItem.Text;
                jugador.LugarNacimiento.Ciudad = ddlCiudad.SelectedItem.Text;

                jugador.Email = txtboxEmail.Text;
                jugador.Altura = int.Parse(txtboxAltura.Text);
                jugador.Peso = decimal.Parse(txtboxPeso.Text);
                jugador.Posicion = txtboxPosicion.Text;

                jugador.Categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
                jugador.Categoria.NombreCategoria = ddlCategoria.SelectedItem.Text;

                jugador.estadoJugador.IdEstado = int.Parse(ddlEstadoJugador.SelectedValue);
                jugador.estadoJugador.NombreEstado = ddlEstadoJugador.SelectedItem.Text;

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
                //if (chkboxConfirmado.Checked)
                //{
                //    var negocio = new JugadorNegocio();
                //    negocio.EliminarJugador(int.Parse(txtboxId.Text));
                //    Response.Redirect("PlantillaJugadores.aspx");
                //}
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

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el valor seleccionado del país
                string paisSeleccionado = ddlPais.SelectedValue;

                // Cargar las provincias basadas en el país seleccionado
                CargarProvincias(paisSeleccionado);

                // Limpiar la selección de ciudad y provincia cuando cambie el país
                ddlProvincia.SelectedIndex = 0;
                ddlCiudad.SelectedIndex = 0;
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
                // Obtener el valor seleccionado de la provincia
                string provinciaSeleccionada = ddlProvincia.SelectedValue;

                // Cargar las ciudades basadas en la provincia seleccionada
                CargarCiudades(provinciaSeleccionada);

                // Limpiar la selección de ciudad cuando cambie la provincia
                ddlCiudad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MostrarError("Error al seleccionar provincia: " + ex.Message);
            }
        }




        public List<Control> ComprobarVacio()
        {
            
            List<Control> nueva = new List<Control>();

            
            nueva.Add(txtboxId);
            nueva.Add(txtboxNombre);
            nueva.Add(txtboxApellido);
            nueva.Add(txtFechaNacimiento);
            nueva.Add(txtboxEmail);
            nueva.Add(txtboxAltura);
            nueva.Add(txtboxPeso);
            nueva.Add(txtboxPosicion);

            
            nueva.Add(ddlPais);
            nueva.Add(ddlProvincia);
            nueva.Add(ddlCiudad);
            nueva.Add(ddlCategoria);
            nueva.Add(ddlEstadoJugador);

            return nueva;
        }








    }











}