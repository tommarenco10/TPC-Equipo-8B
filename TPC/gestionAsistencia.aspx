<%@ Page Title="Lista de Asistencias" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gestionAsistencia.aspx.cs" Inherits="TPC.gestionAsistencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>

            <h1>Lista de Asistencias</h1>
            <br />
            <h3>Detalles Generales del Entrenamiento</h3>
            <br />
            <asp:Label ID="lblDetallesEntrenamiento" runat="server"></asp:Label>
            <br />
            <br />
            <br />

            <h5>Jugadores Citados</h5>

            <asp:GridView ID="dgvJugadores" CssClass="table table-dark table-hover" runat="server" AutoGenerateColumns="false" DataKeyNames="IdJugador">
                <Columns>
                    <asp:BoundField DataField="IdJugador" Visible="false" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                    <asp:BoundField HeaderText="Altura" DataField="Altura" />
                    <asp:BoundField HeaderText="Peso" DataField="Peso" />
                    <asp:BoundField HeaderText="Posicion" DataField="Posicion" />
                    <asp:BoundField HeaderText="Estado" DataField="estadoJugador.NombreEstado" />
                    <asp:TemplateField HeaderText="Asistió?">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAsistencia" runat="server" AutoPostBack="true" OnCheckedChanged="chkAsistencia_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnReporte" runat="server" Text="Añadir Reporte" CommandName="Reporte"
                                CommandArgument='<%# Eval("IdJugador") %>' CssClass="btn btn-outline-warning" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Button ID="btnPreseleccionar" runat="server" CssClass="btn btn-primary ms-2" Text="Seleccionar Todo" OnClick="btnPreseleccionar_Click" />
            <asp:Button ID="btnLimpiarSeleccion" runat="server" CssClass="btn btn-secondary ms-2" Text="Limpiar Selección" OnClick="btnLimpiarSeleccion_Click" />

        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPreseleccionar" EventName="Click" />
        </Triggers>

    </asp:UpdatePanel>

    <br />
    <br />
    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnSalirSinGuardar" runat="server" CssClass="btn btn-danger" Text="Salir" OnClick="btnSalirSinGuardar_Click" />

    <br />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>

</asp:Content>
