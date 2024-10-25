<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigJugador.aspx.cs" Inherits="TPC.ConfigWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="despegarBarra">
        <div class="container">
            <div class="row g-3">
                <div class="col-md-4">
                    <label for="txtNombre" class="form-label">Nombre:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxNombre" />
                    <asp:Label for="txtDNI" CssClass="form-label text-danger" ID="lblDniAviso" runat="server" Text="Por favor, ingrese un Nombre" Visible="false"></asp:Label>
                </div>

                <div class="col-md-2">
                    <label for="txtApellido" class="form-label">Apellido:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxApellido" />
                </div>

                <div class="col-md-4">
                    <label for="txtFechaNac" class="form-label">Fecha de nacimiento:</label>
                    <asp:TextBox runat="server" CssClass="form-control" textmode="Date" ID="txtboxFechaNac" />
                </div>

                <div class="col-md-4">
                    <label for="txtLugarNac" class="form-label">Lugar de nacimiento:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxLugarNac" />
                </div>

                <div class="col-md-4">
                    <label for="txtEmail" class="form-label">Email:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtboxEmail" />
                </div>

                <div class="col-md-3">
                    <label for="txtAltura" class="form-label">Altura:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtboxAltura" />
                </div>

                <div class="col-md-2">
                    <label for="txtPeso" class="form-label">Peso:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="number" ID="txtPeso" />
                </div>

                <div class="col-md-6">
                    <label for="txtPosicion" class="form-label">Posicion:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxPosicion" />
                </div>

                <div class="col-md-6">
                    <label for="txtCategoria" class="form-label">Categoria:</label>
                    <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlCategoria">
                    </asp:DropDownList>
                </div>
                <div class="col-md-8">
                    <label for="txtEstadoJugador" class="form-label">Estado del jugador:</label>
                    <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlEstadoJugador">
                    </asp:DropDownList>
                </div>

                <div class="col-12">
                    <asp:Button Text="Confirmar" CssClass="btn btn-primary" ID="btnConfirmar" runat="server" Enabled="false" />
                </div>
            </div>
        </div>
    </section>

</asp:Content>
