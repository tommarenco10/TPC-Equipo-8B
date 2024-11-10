<%@ Page Title="Vista Previa" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientoVistaPrevia.aspx.cs" Inherits="TPC.entrenamientoVistaPrevia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <% if (tipoPagina == 3)
            { %>
        <h1>Modificar Entrenamiento</h1>

        <h3>Detalles Generales</h3>
        <div class="row mb-3">
            <div class="col-md-4">
                <label for="txtFechaEntrenamiento" class="form-label">Fecha de Entrenamiento:</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txtFechaEntrenamiento" />
            </div>

            <div class="col-md-4">
                <label for="txtHoraEntrenamiento" class="form-label">Hora de Entrenamiento:</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Time" ID="txtHoraEntrenamiento" />
            </div>

            <div class="col-md-4">
                <label for="ddlCategoria" class="form-label">Categoría:</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria">
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <% }
            else if (tipoPagina == 1 || tipoPagina == 2)
            { %>
        <h1>Vista Previa del Entrenamiento</h1>
        <br />
        <h3>Detalles</h3>
        <br />
        <asp:Label ID="lblDetallesEntrenamiento" runat="server"></asp:Label>
        <br />
        <br />
        <br />

        <% } %>
    </section>


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
        <% if (tipoPagina == 3)
            { %>
        <asp:Button ID="btnAgregarJugadores" runat="server" CssClass="btn btn-secondary" Text="Agregar Jugadores" OnClick="btnAgregarJugadores_Click" />
        <% } %>
    </section>

    <br />

    <section>
        <div class="col-md-4">
            <label for="txtDuracion" class="form-label">Duración del Entrenamiento:</label>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="Time" ID="txtDuracion" />
        </div>
        <br />
        <div class="form-group">
            <label for="txtDescripcion">Descripción Planificada del Entrenamiento</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Describe brevemente el entrenamiento planificado..."></asp:TextBox>
        </div>
        <br />
        <% if (tipoPagina == 2)
            { %>
        <div class="form-group">
            <label for="txtObservaciones">Observaciones Post Entrenamiento</label>
            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Escribe las observaciones respectivas al entrenamiento..."></asp:TextBox>
        </div>
        <% } %>
    </section>

    <section>
        <br />
        <% if (tipoPagina == 1)
            { %>

        <div>
            <h6>¿Desea confirmar el Entrenamiento?</h6>
            <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-success" Text="Sí, confirmar" OnClick="btnConfirmar_Click" />
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-danger" Text="No, volver" OnClick="btnVolver_Click" />
        </div>

        <% }
            else if (tipoPagina == 2)
            { %>

        <div>
            <asp:Button ID="btnVolverDetalle" runat="server" CssClass="btn btn-primary" Text="Volver" OnClick="btnVolverDetalle_Click" />
        </div>

        <% }
            else if (tipoPagina == 3)
            { %>

        <div>
            <h6>¿Desea confirmar el Entrenamiento?</h6>
            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-warning" Text="Sí, modificar" OnClick="btnConfirmar_Click" />
            <asp:Button ID="btnVolver2" runat="server" CssClass="btn btn-danger" Text="No, volver" OnClick="btnVolverDetalle_Click" />
        </div>

        <% } %>


        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>

    </section>
</asp:Content>
