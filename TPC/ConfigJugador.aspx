<%@ Page Title="Gestión Plantilla" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigJugador.aspx.cs" Inherits="TPC.ConfigWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <h3> En esta pagina podras gestionar la plantilla del club agregando, modificando o eliminando jugadores en casos de ventas, compras o si un jugador se encuentra disponible o no. </h3>
        <p>Agregar: si completas todos los campos se realizara el alta de un nuevo jugador</p>
        <p>Modificar: se cargaran todos los datos del jugador que hayas seleccionado previamente y podras modificar los campos, una vez hecho dale a modificar!</p>
        <p>Eliminar: se eliminara el jugador que hayas seleccionado previamente, por favor tener precaucion a la hora de utilizar esta opcion.</p>
        <p>Cancelar: si estas en esta pagina y te arrepentis de lo que estas haciendo, simplemente utiliza esta opcion!</p>
        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxNombre" />
                    <asp:Label for="txtDNI" CssClass="form-label text-danger" ID="lblDniAviso" runat="server" Text="Por favor, ingrese un Nombre" Visible="false"></asp:Label>
                </div>

                <div class="mb-3">
                    <label for="txtApellido" class="form-label">Apellido:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxApellido" />
                </div>

                <div class="mb-3">
                    <label for="txtFechaNac" class="form-label">Fecha de nacimiento:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txtboxFechaNac" />
                </div>

                <div class="mb-3">
                    <label for="txtpais" class="form-label">Pais:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxPais" />
                </div>

                <div class="mb-3">
                    <label for="txtprovincia" class="form-label">Provincia:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxProvincia" />
                </div>

                <div class="mb-3">
                    <label for="txtCiudad" class="form-label">Ciudad:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxCiudad" />
                </div>
            </div>

            <div class="col-6">

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtboxEmail" />
                </div>

                <div class="mb-3">
                    <label for="txtAltura" class="form-label">Altura:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtboxAltura" />
                </div>

                <div class="mb-3">
                    <label for="txtPeso" class="form-label">Peso:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="number" ID="txtboxPeso" />
                </div>

                <div class="mb-3">
                    <label for="txtPosicion" class="form-label">Posicion:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxPosicion" />
                </div>

                <div class="mb-3">
                    <label for="txtCategoria" class="form-label">Categoria:</label>
                    <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlCategoria">
                    </asp:DropDownList>
                    <label for="txtEstadoJugador" class="form-label">Estado del jugador:</label>
                    <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlEstadoJugador">
                    </asp:DropDownList>
                </div>

            </div>
        </div>
        <div class="col-12">
            <asp:Button Text="Agregar" CssClass="btn btn-primary" ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" />
            <asp:Button Text="Modificar" CssClass="btn btn-warning" ID="btnModificar" runat="server" OnClick="btnModificar_Click" />
            <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
        </div>
    </section>

</asp:Content>
