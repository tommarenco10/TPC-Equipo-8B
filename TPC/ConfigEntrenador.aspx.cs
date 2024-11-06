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
    public partial class ConfigEntrenador : System.Web.UI.Page
    {
        public bool ConfirmarEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtboxId.Enabled = false;
                ConfirmarEliminacion = false;

                if (!(IsPostBack))
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();

                    ddlCategoria.DataSource = negocio.listar();
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataBind();
                }

                if (Request.QueryString["IdEntrenador"] != null && !IsPostBack)
                {
                    List<Entrenador> lista = new List<Entrenador>();
                    CuerpoTecnicoNegocio negocio = new CuerpoTecnicoNegocio();
                    lista = negocio.Listar();

                    int id = int.Parse(Request.QueryString["IdEntrenador"].ToString());
                    txtboxId.Text = id.ToString();

                    Entrenador entrenador = new Entrenador();
                    entrenador = lista.Find(x => x.IdEntrenador == id);

                    txtboxNombre.Text = entrenador.Nombres.ToString();
                    txtboxApellido.Text = entrenador.Apellidos.ToString();
                    txtboxFechaNac.Text = entrenador.FechaNacimiento.ToString();
                    txtboxPais.Text = entrenador.LugarNacimiento.Pais.ToString();
                    txtboxProvincia.Text = entrenador.LugarNacimiento.Provincia.ToString();
                    txtboxCiudad.Text = entrenador.LugarNacimiento.Ciudad.ToString();
                    txtboxEmail.Text = entrenador.Email.ToString();
                    txtboxFechaContratacion.Text = entrenador.FechaContratacion.ToString();
                    txtboxRol.Text = entrenador.Rol.ToString();
                    ddlCategoria.SelectedValue = entrenador.categoria.IdCategoria.ToString();
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
                Entrenador entrenador = new Entrenador();
                CuerpoTecnicoNegocio negocio = new CuerpoTecnicoNegocio();

                entrenador.Nombres = txtboxNombre.Text;
                entrenador.Apellidos = txtboxApellido.Text;
                entrenador.FechaNacimiento = DateTime.Parse(txtboxFechaNac.Text);

                entrenador.LugarNacimiento = new LugarNacimiento();
                entrenador.LugarNacimiento.Pais = txtboxPais.Text;
                entrenador.LugarNacimiento.Provincia = txtboxProvincia.Text;
                entrenador.LugarNacimiento.Ciudad = txtboxCiudad.Text;

                entrenador.Email = txtboxEmail.Text;
                entrenador.Rol = (string)txtboxRol.Text;
                entrenador.FechaContratacion = DateTime.Parse(txtboxFechaContratacion.Text);

                entrenador.categoria = new Categoria();
                entrenador.categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);

                negocio.AgregarEntrenador(entrenador);
                Response.Redirect("CuerpoTecnico.aspx", false);

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
                Entrenador entrenador = new Entrenador();
                CuerpoTecnicoNegocio negocio = new CuerpoTecnicoNegocio();

                entrenador.IdEntrenador = int.Parse(txtboxId.Text);
                entrenador.Nombres = txtboxNombre.Text;
                entrenador.Apellidos = txtboxApellido.Text;
                entrenador.FechaNacimiento = DateTime.Parse(txtboxFechaNac.Text);

                entrenador.LugarNacimiento = new LugarNacimiento();
                entrenador.LugarNacimiento.Pais = txtboxPais.Text;
                entrenador.LugarNacimiento.Provincia = txtboxProvincia.Text;
                entrenador.LugarNacimiento.Ciudad = txtboxCiudad.Text;

                entrenador.Email = txtboxEmail.Text;
                entrenador.Rol = (string)txtboxRol.Text;
                entrenador.FechaContratacion = DateTime.Parse(txtboxFechaContratacion.Text);

                entrenador.categoria = new Categoria();
                entrenador.categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);

                negocio.ModificarEntrenador(entrenador);
                Response.Redirect("CuerpoTecnico.aspx", false);

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

        protected void EliminarConfirmado_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkboxConfirmado.Checked)
                {
                    CuerpoTecnicoNegocio negocio = new CuerpoTecnicoNegocio();
                    negocio.EliminarEntrenador(int.Parse(txtboxId.Text));
                    Response.Redirect("CuerpoTecnico.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}