<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="TPC.ingreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />

    <section>
        <h2 class="mt-5 mb-4 text-center">Iniciar Sesión</h2>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="mb-3">
                                    <label for="txtUser" class="form-label">Nombre de usuario</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtUserName" />
                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" ControlToValidate="txtUserName" Class="error-input" runat="server" />
                                </div>
                                <div class="mb-3">
                                    <label for="txtPass" class="form-label">Contraseña</label>
                                    <asp:TextBox TextMode="Password" CssClass="form-control" ID="txtPass" runat="server" EnableViewState="true" />
                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" ControlToValidate="txtPass" runat="server" Class="error-input" />
                                </div>
                                <asp:Label Text="" ID="lbError" runat="server" ForeColor="Red" />
                                <asp:Button type="submit" Text="Iniciar Sesión" runat="server" CssClass="btn btn-primary w-100" ID="btnLogIn" OnClick="btnLogIn_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
