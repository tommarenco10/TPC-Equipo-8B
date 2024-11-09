<%@ Page Title="Entrenamiento" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gestionEntrenamiento.aspx.cs" Inherits="TPC.gestionEntrenamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (tipoPagina != 1)
        {%>
    <section>
        <h1>Agendar Entrenamiento</h1>
        <br />
        <br />
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
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-8">
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnPreseleccionar" runat="server" CssClass="btn btn-primary" Text="Preseleccionar Categoría" OnClick="btnPreseleccionar_Click" />
            </div>
        </div>
        <br />
        <h5>Detalles Adicionales</h5>

        <% }
            else
            { %>
        <h5>Agregar Jugadores</h5>
        <% }%>

        <div class="row mb-3">
            <div class="col-md-4">
                <label for="ddlJugadoresAdicionales" class="form-label">Jugadores Adicionales:</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlJugadoresAdicionales" AutoPostBack="true" OnSelectedIndexChanged="ddlJugadoresAdicionales_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
    </section>

    <section>
        <asp:GridView ID="dgvEntrenamiento" CssClass="table table-dark table-hover" runat="server" AutoGenerateColumns="false" DataKeyNames="IdJugador">
            <Columns>
                <asp:BoundField DataField="IdJugador" Visible="false" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                <asp:BoundField HeaderText="Altura" DataField="Altura" />
                <asp:BoundField HeaderText="Peso" DataField="Peso" />
                <asp:BoundField HeaderText="Posicion" DataField="Posicion" />
                <asp:BoundField HeaderText="Estado" DataField="estadoJugador.NombreEstado" />
                <asp:TemplateField HeaderText="Citado a Entrenar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkCitado" runat="server" AutoPostBack="true" OnCheckedChanged="chkCitado_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </section>

    <br />
    <% if (tipoPagina != 1)
        {%>
    <div>
        <asp:Button ID="btnMostrarSeleccionados" runat="server" Text="Mostrar Jugadores Seleccionados" CssClass="btn btn-primary" OnClick="btnMostrarSeleccionados_Click" />

        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
    <% }
        else
        { %>
    <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
    <asp:Button ID="btnVolver" CssClass="btn btn-secondary" runat="server" Text="Volver sin guardar" OnClick="btnVolver_Click" />
    <% } %>

</asp:Content>
