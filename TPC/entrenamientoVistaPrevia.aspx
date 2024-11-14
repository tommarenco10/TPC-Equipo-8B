<%@ Page Title="Vista Previa" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientoVistaPrevia.aspx.cs" Inherits="TPC.entrenamientoVistaPrevia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Vista Previa del Entrenamiento</h1>
    <br />
    <h3>Resumen</h3>
    <br />
    <asp:Label ID="lblDetallesEntrenamiento" runat="server"></asp:Label>
    <br />
    <br />
    <br />

    <section>
        <h3>Jugadores Seleccionados</h3>
        <asp:GridView ID="dgvJugadoresSeleccionados" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="IdJugador">
            <Columns>
                <asp:BoundField DataField="IdJugador" Visible="false" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                <asp:BoundField HeaderText="Altura" DataField="Altura" />
                <asp:BoundField HeaderText="Peso" DataField="Peso" />
                <asp:BoundField HeaderText="Posición" DataField="Posicion" />
                <asp:BoundField HeaderText="Estado" DataField="estadoJugador.NombreEstado" />
                <asp:BoundField HeaderText="Categoría" DataField="Categoria.NombreCategoria" />
            </Columns>
        </asp:GridView>
        <br />
    </section>

    <section>
        <div class="col-md-4">
            <label for="txtDuracion" class="form-label">Duración del Entrenamiento:</label>
            <asp:TextBox ID="txtDuracion" runat="server" CssClass="form-control" TextMode="Time" Enabled="false" />
        </div>
        <br />
        <div class="form-group">
            <label for="txtDescripcion">Descripción Planificada del Entrenamiento</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Enabled="false"
                TextMode="MultiLine" Rows="4" placeholder="Describe brevemente el entrenamiento planificado..."></asp:TextBox>
        </div>
        <br />
        <% if (tipoPagina == 2)
            { %>
        <div class="form-group">
            <label for="txtObservaciones">Observaciones Post Entrenamiento</label>
            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" Enabled="false"
                TextMode="MultiLine" Rows="4" placeholder="Escribe las observaciones respectivas al entrenamiento..."></asp:TextBox>
        </div>
        <% } %>
    </section>

    <section>
        <br />
        <% if (tipoPagina == 3)
            { %>
        <asp:Button ID="btnVolverListado" runat="server" CssClass="btn btn-primary" Text="Volver a Listado" OnClick="btnVolverListado_Click" />
        <% }
            else if (tipoPagina == 2)
            { %>
        <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-warning" Text="Seguir Modificando" OnClick="btnModificar_Click" />
        <asp:Button ID="btnSalirSinModificar" runat="server" CssClass="btn btn-danger" Text="Salir sin Modificar" OnClick="btnVolverListado_Click" />
        <% }
            else
            { %>
        <asp:Button ID="btnVolverAgregar" runat="server" CssClass="btn btn-primary" Text="Volver" OnClick="btnVolverAgregar_Click" />
        <% } %>

        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>

    </section>
</asp:Content>
