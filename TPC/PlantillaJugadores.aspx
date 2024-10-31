<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlantillaJugadores.aspx.cs" Inherits="TPC.PlanillaJugadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Primera</h1>
    <asp:GridView runat="server" ID="dgvPrimera" CssClass="table" AutoGenerateColumns="false" DataKeyNames="IdJugador" OnSelectedIndexChanged="dgv_SelectedIndexChanged">
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
    <h1>Reserva</h1>
    <asp:GridView runat="server" ID="dgvReserva" CssClass="table" AutoGenerateColumns="false" DataKeyNames="IdJugador" OnSelectedIndexChanged="dgv_SelectedIndexChanged">
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
    <h1>Juveniles</h1>
    <asp:GridView runat="server" ID="dgvJuveniles" CssClass="table" AutoGenerateColumns="false" DataKeyNames="IdJugador" OnSelectedIndexChanged="dgv_SelectedIndexChanged">
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

</asp:Content>
