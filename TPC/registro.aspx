<%@ Page Title="Registrate!" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="TPC.registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <h2 class="mb-4">Formulario de Registro</h2>
        <div class="row g-3">

            <!--Usuario y Contraseña -->
            <div class="col-md-4">
                <label for="txtUserName" class="form-label">Nombre de usuario:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtUserName" />
            </div>

            <div class="col-md-4">
                <label for="txtPassword" class="form-label">Contraseña:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtPassword" TextMode="Password" />
            </div>

            <!--Linea separadora -->
            <div class="col-12">
                <hr />
            </div>

            <!--Datos Personales -->
            <div class="col-md-4">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtNombre" />
            </div>

            <div class="col-md-4">
                <label for="txtApellido" class="form-label">Apellido:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtApellido" />
            </div>

            <div class="col-md-4">
                <label for="txtDNI" class="form-label">DNI:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtDNI" />
                <asp:Label for="txtDNI" CssClass="form-label text-danger" ID="lblDniAviso" runat="server" Text="Por favor, ingrese un Nro. de Documento." Visible="false"></asp:Label>
            </div>

            <div class="col-md-4">
                <label for="txtDireccion" class="form-label">Dirección:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtDireccion" />
            </div>

            <div class="col-md-3">
                <label for="txtCiudad" class="form-label">Ciudad:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtCiudad" />
            </div>

            <div class="col-md-2">
                <label for="txtCP" class="form-label">Cod. Postal:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtCP" />
            </div>

            <div class="col-md-6">
                <label for="txtEmail" class="form-label">Email:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="email" ID="txtEmail" />
            </div>

            <div class="col-12">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" value="" id="invalidCheck" required>
                    <label class="form-check-label" for="invalidCheck">
                        Acuerdo con los términos y condiciones
                    </label>
                </div>
            </div>

            <div class="col-12">
                <asp:Button Text="Confirmar" CssClass="btn btn-primary" ID="btnConfirmar" runat="server" Enabled="false" />
            </div>
        </div>
    </section>

</asp:Content>
