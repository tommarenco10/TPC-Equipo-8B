<%@ Page Title="Estados" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigEstados.aspx.cs" Inherits="TPC.ConfigEstados" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% if (tipoPagina == 1)
        { %>
    <h2>Configuración de los Estados de Jugador</h2>
    <% }
        else if (tipoPagina == 2)
        { %>
    <h2>Configuración de los Estados de Entrenamiento</h2>
    <% } %>
    <br />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <div class="table-responsive">
                        <asp:GridView ID="dgvEstados" AutoGenerateColumns="False" CssClass="table table-striped table-hover"
                            runat="server" DataKeyNames="IdEstado" OnRowCommand="dgvEstados_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="IdEstado" HeaderText="ID" Visible="False" />

                                <asp:BoundField DataField="NombreEstado" HeaderText="Estado">
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:BoundField>

                                <asp:TemplateField>
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar"
                                            CommandArgument='<%# Eval("IdEstado") %>' CssClass="btn btn-secondary" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                                            CommandArgument='<%# Eval("IdEstado") %>' CssClass="btn btn-danger" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Button ID="btnAgregarNuevo" runat="server" Text="Agregar Nuevo Estado" CssClass="btn btn-primary" OnClick="btnAgregarNuevo_Click" />
                </div>

                <div class="col-md-6">
                    <asp:Label ID="lblTitulo" runat="server" Text="Aquí podrás realizar la acción que decidas!" CssClass="titulo"></asp:Label>
                    <div class="mb-3">
                        <asp:Label ID="lblIdEstado" runat="server" Text="ID:" CssClass="form-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtIdEstado" runat="server" CssClass="form-control bg-light" ReadOnly="True" Visible="false" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblNombreEstado" runat="server" Text="Nombre del Estado:" CssClass="form-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtNombreEstado" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del estado" Visible="false"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnGuardarModificacion" runat="server" Text="Guardar Modificación" CssClass="btn btn-secondary" OnClick="btnGuardar_Click" Visible="false" />
                    <asp:Button ID="btnGuardarAgregado" runat="server" Text="Agregar Nuevo Estado" CssClass="btn btn-primary" OnClick="btnGuardar_Click" Visible="false" />
                    <asp:Button ID="btnGuardarEliminacion" runat="server" Text="Eliminar Estado" CssClass="btn btn-danger" OnClick="btnGuardarEliminacion_Click" Visible="false" />
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
