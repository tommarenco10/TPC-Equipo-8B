<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="TPC.ingreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <h2 class="mt-5 mb-4 text-center">Iniciar Sesión</h2>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Nombre de usuario</label>
                            <asp:TextBox runat="server" type="email" CssClass="form-control" ID="txtUserName" />
                        </div>
                        <div class="mb-3">
                            <label for="txtPass" class="form-label">Contraseña</label>
                            <asp:TextBox type="password" CssClass="form-control" ID="txtPass" runat="server" />
                        </div>
                        <asp:Button type="submit" Text="Iniciar Sesión" runat="server" CssClass="btn btn-primary w-100" ID="btnLogIn" OnClick="btnLogIn_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
