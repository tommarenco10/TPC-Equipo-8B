<%@ Page Title="Vista Previa" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientoVistaPrevia.aspx.cs" Inherits="TPC.entrenamientoVistaPrevia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <h3>Detalles del Entrenamiento</h3>
        <br />
        <asp:Label ID="lblDetallesEntrenamiento" runat="server"></asp:Label>
        <br /><br />
    </section>

    <section>

        <h2>Jugadores Seleccionados</h2>
        <asp:GridView ID="dgvJugadoresSeleccionados" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                <asp:BoundField HeaderText="Altura" DataField="Altura" />
                <asp:BoundField HeaderText="Peso" DataField="Peso" />
                <asp:BoundField HeaderText="Posición" DataField="Posicion" />
                <asp:BoundField HeaderText="Categoría" DataField="Categoria.NombreCategoria" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn btn-primary btn-regresar" OnClick="btnVolver_Click">Volver</asp:LinkButton>
        <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>

    </section>

</asp:Content>
