<%@ Page Title="Incidencias" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gestionIncidencias.aspx.cs" Inherits="TPC.gestionIncidencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Registrar Incidencia para Jugador</h2>
    <asp:Panel ID="pnlJugador" runat="server" CssClass="card p-3 my-3">
        <h4>Detalles del Jugador</h4>
        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="fw-bold"></asp:Label>
        <asp:Label ID="lblNombreValor" runat="server"></asp:Label><br />
        <asp:Label ID="lblApellido" runat="server" Text="Apellido:" CssClass="fw-bold"></asp:Label>
        <asp:Label ID="lblApellidoValor" runat="server"></asp:Label><br />
        <asp:Label ID="lblPosicion" runat="server" Text="Posición:" CssClass="fw-bold"></asp:Label>
        <asp:Label ID="lblPosicionValor" runat="server"></asp:Label><br />
        <asp:Label ID="lblEdad" runat="server" Text="Edad:" CssClass="fw-bold"></asp:Label>
        <asp:Label ID="lblEdadValor" runat="server"></asp:Label><br />
        <asp:Label ID="lblAltura" runat="server" Text="Altura:" CssClass="fw-bold"></asp:Label>
        <asp:Label ID="lblAlturaValor" runat="server"></asp:Label><br />
        <asp:Label ID="lblPeso" runat="server" Text="Peso:" CssClass="fw-bold"></asp:Label>
        <asp:Label ID="lblPesoValor" runat="server"></asp:Label><br />
    </asp:Panel>

    <asp:Panel ID="pnlIncidencia" runat="server" CssClass="card p-3 my-3">
        <h4>Detalles de la Incidencia</h4>
        <asp:Label runat="server" AssociatedControlID="ddlTipoIncidencia" Text="Tipo de Incidencia:" CssClass="fw-bold"></asp:Label><br />
        <asp:DropDownList ID="ddlTipoIncidencia" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlTipoIncidencia_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="0" Text="Seleccione un tipo" />
            <asp:ListItem Value="1" Text="Lesión" />
            <asp:ListItem Value="2" Text="Sanción" />
        </asp:DropDownList><br />
        <br />

        <asp:Label runat="server" AssociatedControlID="txtDescripcion" Text="Descripción:" CssClass="fw-bold"></asp:Label><br />
        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><br />

        <asp:Label runat="server" AssociatedControlID="txtFechaRegistro" Text="Fecha de Registro:" CssClass="fw-bold"></asp:Label><br />
        <asp:TextBox ID="txtFechaRegistro" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />

        <asp:Label runat="server" AssociatedControlID="txtFechaResolucion" Text="Fecha Estimada de Resolución:" CssClass="fw-bold"></asp:Label><br />
        <asp:TextBox ID="txtFechaResolucion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />

        <asp:Button ID="btnGuardarIncidencia" runat="server" Text="Guardar Incidencia" CssClass="btn btn-primary" OnClick="btnGuardarIncidencia_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlObservaciones" runat="server" CssClass="card p-3 my-3" Visible="false">
        <h4>Observaciones (Evolución de la Lesión)</h4>
        <asp:Label runat="server" AssociatedControlID="txtFechaObservacion" Text="Fecha:" CssClass="fw-bold"></asp:Label><br />
        <asp:TextBox ID="txtFechaObservacion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />

        <asp:Label runat="server" AssociatedControlID="txtDescripcionObservacion" Text="Descripción:" CssClass="fw-bold"></asp:Label><br />
        <asp:TextBox ID="txtDescripcionObservacion" runat="server" CssClass="form-control"></asp:TextBox><br />

        <asp:Button ID="btnAgregarObservacion" runat="server" Text="Agregar Observación" CssClass="btn btn-secondary" OnClick="btnAgregarObservacion_Click" /><br />
        <br />

        <asp:GridView ID="gvObservaciones" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

</asp:Content>
