<%@ Page Title="Entrenamientos Programados" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientosProgramados.aspx.cs" Inherits="TPC.entrenamientosProgramados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row mb-3">
        <div class="col-md-4">
            <label for="txtCategoria" class="form-label">Categoria:</label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <section>
        <asp:GridView ID="dgvEntrenamientos" CssClass="table table-dark table-hover" runat="server" AutoGenerateColumns="false" DataKeyNames="IdEntrenamiento">
            <Columns>
                <asp:BoundField DataField="IdJugador" Visible="false" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                <asp:BoundField HeaderText="Altura" DataField="Altura" />
                <asp:BoundField HeaderText="Peso" DataField="Peso" />
                <asp:BoundField HeaderText="Posicion" DataField="Posicion" /> 
            </Columns>
        </asp:GridView>
    </section>

    <br />

    <div>
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary"/>
    </div>

</asp:Content>