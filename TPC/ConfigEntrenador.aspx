﻿<%@ Page Title="Gestion Cuerpo Técnico" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigEntrenador.aspx.cs" Inherits="TPC.ConfigEntrenador" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <h3>Modifica los datos de los entrenadores del club, aqui podras modificar lo que necesites en caso de malos registros o cambios en el cuerpo tecnico. </h3>
        <p>Agregar: si completas todos los campos se realizara el alta de un nuevo entrenador</p>
        <p>Modificar: se cargaran todos los datos del entrenador que hayas seleccionado previamente y podras modificar los campos, una vez hecho dale a modificar!</p>
        <p>Eliminar: se eliminara el entrenador que hayas seleccionado previamente, por favor tener precaucion a la hora de utilizar esta opcion.</p>
        <p>Cancelar: si estas en esta pagina y te arrepentis de lo que estas haciendo, simplemente utiliza esta opcion!</p>
        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <label for="txtId" class="form-label">Id:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxId" />
                </div>
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
                    <label for="txtFechaNac" class="form-label">Fecha de contratacion:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txtboxFechaContratacion" />
                </div>

                <div class="mb-3">
                    <label for="txtPosicion" class="form-label">Rol:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxRol" />
                </div>

                <div class="mb-3">
                    <label for="txtCategoria" class="form-label">Categoria:</label>
                    <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlCategoria">
                    </asp:DropDownList>
                </div>

            </div>
        </div>
        <div class="col-12">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="updatePanel" runat="server">
                <ContentTemplate>
                    <asp:Button Text="Agregar" CssClass="btn btn-primary" ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" />
                    <asp:Button Text="Modificar" CssClass="btn btn-warning" ID="btnModificar" runat="server" OnClick="btnModificar_Click" />
                    <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
                    <%if (ConfirmarEliminacion)
                        { %>
                    <asp:CheckBox Text="Confirmar eliminacion" ID="chkboxConfirmado" runat="server" />
                    <asp:Button Text="Eliminar" CssClass="btn btn-outline-danger" OnClick="EliminarConfirmado_Click" runat="server" />

                    <% } %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>

</asp:Content>
