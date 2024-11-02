<%@ Page Title="Configuraciones" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigVarias.aspx.cs" Inherits="TPC.ConfigVarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Configuración de los Estados de Jugador</h2>
    <br />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <div class="table-responsive">
                        <asp:GridView ID="dgvEstadosJugador" AutoGenerateColumns="False" CssClass="table table-striped table-hover"
                            runat="server" DataKeyNames="IdEstadoJugador" OnRowCommand="dgvEstadosJugador_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="IdEstadoJugador" HeaderText="ID" Visible="False" />
                                <asp:BoundField DataField="NombreEstado" HeaderText="Estado" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar"
                                            CommandArgument='<%# Eval("IdEstadoJugador") %>' CssClass="btn btn-secondary" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Button ID="btnAgregarNuevo" runat="server" Text="Agregar Nuevo Estado" CssClass="btn btn-primary" OnClick="btnAgregarNuevo_Click"/>
                </div>

                <div class="col-md-6">
                    <asp:Label ID="lblTitulo" runat="server" Text="Aquí podrás realizar la acción que decidas!" CssClass="titulo"></asp:Label>
                    <div class="mb-3">
                        <asp:Label ID="lblIdEstadoJugador" runat="server" Text="ID:" CssClass="form-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtIdEstadoJugador" runat="server" CssClass="form-control bg-light" ReadOnly="True" Visible="false" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblNombreEstado" runat="server" Text="Nombre del Estado:" CssClass="form-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtNombreEstado" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del estado" Visible="false"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnModificar" runat="server" Text="Guardar Modificación" CssClass="btn btn-secondary" OnClick="btnGuardar_Click" Visible="false"/>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nuevo Estado" CssClass="btn btn-primary" OnClick="btnGuardar_Click" Visible="false" />
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>