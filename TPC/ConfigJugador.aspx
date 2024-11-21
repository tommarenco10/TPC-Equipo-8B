<%@ Page Title="Gestión Plantilla" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigJugador.aspx.cs" Inherits="TPC.ConfigWeb" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" CssClass="container border rounded shadow-lg p-4 position-relative">
                <div class="position-absolute top-0 end-0 p-3">
                    <asp:Image runat="server" ID="imgPerfil" CssClass="rounded-circle border border-2"
                        Width="160px" Height="160px" ImageUrl="https://via.placeholder.com/160" />
                </div>

                <h4 class="text-center mb-4">
                    <% if (tipoPagina == 1)
                        { %>
            Eliminar Jugador
            <% }
                else if (tipoPagina == 2)
                { %>
            Modificar Jugador
            <% }
                else
                { %>
            Agregar Jugador
            <% } %>
                </h4>

                <div class="row g-3">
                    <div class="col-12 col-md-1">
                        <label for="txtId" class="form-label fw-bold">ID:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtId" Enabled="false" />
                    </div>
                    <div class="col-12 col-md-4">
                        <label for="txtNombre" class="form-label fw-bold">Nombre:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtNombre" />
                    </div>
                    <div class="col-12 col-md-4">
                        <label for="txtApellido" class="form-label fw-bold">Apellido:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtApellido" />
                    </div>
                </div>

                <div class="row g-3 mt-3">
                    <div class="col-12 col-md-3">
                        <label for="txtDNI" class="form-label fw-bold">DNI:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtDNI" MinLength="7" MaxLength="8" />
                        <asp:RegularExpressionValidator runat="server"
                            ControlToValidate="txtDNI"
                            CssClass="text-danger"
                            ValidationExpression="^\d{7,8}$"
                            ErrorMessage="El DNI debe tener entre 7 y 8 dígitos numéricos."
                            Display="Dynamic" />
                    </div>
                    <div class="col-12 col-md-5">
                        <label for="txtPosicion" class="form-label fw-bold">Posición:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtPosicion" />
                    </div>
                    <div class="col-12 col-md-4">
                        <label for="ddlCategoria" class="form-label fw-bold">Categoría:</label>
                        <asp:DropDownList runat="server" CssClass="form-select" ID="ddlCategoria">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row g-3 mt-3">
                    <div class="col-12 col-md-3">
                        <label for="txtFechaNacimiento" class="form-label fw-bold">Fecha de Nacimiento:</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txtFechaNacimiento" />
                    </div>
                    <div class="col-12 col-md-3">
                        <label for="txtCiudad" class="form-label fw-bold">Ciudad:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtCiudad" />
                    </div>
                    <div class="col-12 col-md-3">
                        <label for="txtProvincia" class="form-label fw-bold">Provincia:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtProvincia" />
                    </div>
                    <div class="col-12 col-md-3">
                        <label for="txtPais" class="form-label fw-bold">País:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtPais" />
                    </div>
                </div>

                <div class="row g-3 mt-3">
                    <div class="col-12 col-md-8">
                        <label for="txtEmail" class="form-label fw-bold">Email:</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtEmail" />
                    </div>
                    <div class="col-12 col-md-2">
                        <label for="txtAltura" class="form-label fw-bold">Altura (cm):</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtAltura" />
                    </div>
                    <div class="col-12 col-md-2">
                        <label for="txtPeso" class="form-label fw-bold">Peso (kg):</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="number" ID="txtPeso" />
                    </div>
                </div>

                <div class="row g-3 mt-3">
                    <div class="col-12">
                        <label for="txtUrlImagen" class="form-label fw-bold">URL Imagen:</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUrlImagen" AutoPostBack="true" OnTextChanged="txtUrlImagen_TextChanged" />
                    </div>
                </div>

            </asp:Panel>

            <div class="col-12 mt-3">
                <% if (tipoPagina == 1)
                    { %>
                <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
                <% }
                    else if (tipoPagina == 2)
                    { %>
                <asp:Button Text="Modificar" CssClass="btn btn-warning" ID="btnModificar" runat="server" OnClick="btnModificar_Click" />
                <% }
                    else
                    { %>
                <asp:Button Text="Agregar" CssClass="btn btn-primary" ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" CausesValidation="false"/>
                <% } %>

                <%if (ConfirmarEliminacion)
                    { %>
                <asp:CheckBox Text="Confirmar eliminacion" ID="chkboxConfirmado" runat="server" />
                <asp:Button Text="Eliminar" CssClass="btn btn-outline-danger" OnClick="BtnEliminarConfirmado_Click" runat="server" />
                <% } %>

                <asp:Label ID="lblError" runat="server"></asp:Label>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
