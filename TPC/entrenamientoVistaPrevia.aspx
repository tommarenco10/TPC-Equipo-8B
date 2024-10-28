<%@ Page Title="Vista Previa" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientoVistaPrevia.aspx.cs" Inherits="TPC.entrenamientoVistaPrevia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <h3>Detalles del Entrenamiento</h3>
        <br />
        <asp:Label ID="lblDetallesEntrenamiento" runat="server"></asp:Label>
        <br />
        <br />
    </section>

    <section>
        <h2>Jugadores Seleccionados</h2>
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
    </section>

    <section>
        <div class="col-md-4">
            <label for="txtDuracion" class="form-label">Duración del Entrenamiento:</label>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="Time" ID="txtDuracion" />
        </div>
        <div class="form-group">
            <label for="txtDescripcion">Descripción Planificada del Entrenamiento</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Describe brevemente el entrenamiento planificado..."></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtObservaciones">Observaciones Post Entrenamiento</label>
            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Escribe las observaciones respectivas al entrenamiento..."></asp:TextBox>
        </div>
    </section>

    <br />

    <section>
        <div>
            <h6>¿Desea confirmar el Entrenamiento?</h6>
            <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-success" Text="Sí, confirmar" OnClick="btnConfirmar_Click" />
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-danger" Text="No, volver" OnClick="btnVolver_Click" />
            <br />
            <br />
            <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
        </div>
    </section>



</asp:Content>
