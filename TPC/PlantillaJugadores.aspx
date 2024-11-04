<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlantillaJugadores.aspx.cs" Inherits="TPC.PlanillaJugadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="titulo text-lg-center">
        <h1>Jugadores del Club</h1>
    </div>
    <div class="row">
        <div class="mb-4">
            <asp:Label Text="Nombre:" runat="server" />
            <asp:TextBox CssClass="col-6" runat="server" ID="txtboxFiltroNombre" AutoPostBack="true" OnTextChanged="txtboxFiltroNombre_TextChanged" />
        </div>
    </div>
    <div class="row">
        <div class="mb-3">
            <asp:Label Text="Posicion:" runat="server" />
            <asp:TextBox runat="server" CssClass="col-6" ID="txtboxFiltroPosicion" AutoPostBack="true" OnTextChanged="txtboxFiltroPosicion_TextChanged"/>
        </div>
    </div>
    <div class="row">
        <div class="col-4">
            <div class="mb-3">
                <asp:Label Text="Categoria:" runat="server" />
                <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlCategoria">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-4">
            <div class="mb-3">
                <asp:Label Text="Estado del jugador:" runat="server" />
                <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlEstadoJugador">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div>
        <asp:Button Text="Buscar" ID="FiltroAvanzado" CssClass="btn btn-primary" runat="server" OnClick="FiltroAvanzado_Click" />
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <asp:GridView runat="server" ID="dgvJugadores" CssClass="table table-dark form-check-input" AutoGenerateColumns="false" DataKeyNames="IdJugador" OnSelectedIndexChanged="dgv_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                    <asp:BoundField HeaderText="Fecha de nacimiento" DataField="FechaNacimiento" />
                    <asp:BoundField HeaderText="Pais" DataField="LugarNacimiento.Pais" />
                    <asp:BoundField HeaderText="Provincia" DataField="LugarNacimiento.Provincia" />
                    <asp:BoundField HeaderText="Ciudad" DataField="LugarNacimiento.Ciudad" />
                    <asp:BoundField HeaderText="Email" DataField="Email" />
                    <asp:BoundField HeaderText="Altura" DataField="Altura" />
                    <asp:BoundField HeaderText="Peso" DataField="Peso" />
                    <asp:BoundField HeaderText="Posicion" DataField="Posicion" />
                    <asp:BoundField HeaderText="Categoria" DataField="Categoria.NombreCategoria" />
                    <asp:BoundField HeaderText="Estado del Jugador" DataField="EstadoJugador.NombreEstado" />
                    <asp:CommandField ShowSelectButton="true" SelectText="Modificar" HeaderText="Modificar" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
