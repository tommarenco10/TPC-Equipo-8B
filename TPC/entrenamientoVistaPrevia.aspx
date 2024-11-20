<%@ Page Title="Vista Previa" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientoVistaPrevia.aspx.cs" Inherits="TPC.entrenamientoVistaPrevia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
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
        <div>
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
        <% if (tipoPagina == 4)
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
        <% if (tipoPagina == 3 || tipoPagina == 4)
            { %>
        <asp:Button ID="btnVolverListado" runat="server" CssClass="btn btn-primary" Text="Volver a Listado" OnClick="btnVolverListado_Click" />
        <% }
            else if (tipoPagina == 2)
            { %>
        <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-warning" Text="Seguir Modificando" OnClick="btnModificar_Click" />
        <asp:Button ID="btnSalirSinModificar" runat="server" CssClass="btn btn-danger" Text="Salir"
            OnClientClick="event.preventDefault(); mostrarModalCancelacionVP(); return false;" />
        <% }
            else
            { %>
        <asp:Button ID="btnVolverAgregar" runat="server" CssClass="btn btn-primary" Text="Volver" OnClick="btnVolverAgregar_Click" />
        <% } %>

        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>

    </section>

    <!-- Modal de Cancelación -->
    <div class="modal fade" id="cancelacionModalVP" tabindex="-1" aria-labelledby="cancelacionModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="cancelacionModalLabel">Salir de Modificación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    ¿Está seguro de que desea salir del menú de modificación?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModalSalir" class="btn btn-danger" runat="server" Text="Si, Salir" OnClick="btnVolverListado_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Código JavaScript para Modal -->
    <script type="text/javascript">

        function mostrarModalCancelacionVP() {
            var modalElement = new bootstrap.Modal(document.getElementById('cancelacionModalVP'));
            modalElement.show();
        }

    </script>

</asp:Content>
