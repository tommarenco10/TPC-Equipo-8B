<%@ Page Title="Incidencias" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="incidenciasActualizables.aspx.cs" Inherits="TPC.incidenciasActualizables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Incidencia del Jugador</h2>
    <asp:Panel ID="pnlJugador" runat="server" CssClass="card p-3 my-3 bg-light">
        <h4>Detalles del Jugador</h4>

        <div class="row align-items-center">
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label ID="lblNombreApellido" runat="server" Text="Nombre:" CssClass="fw-bold"></asp:Label>
                    <asp:TextBox ID="txtNombreApellido" runat="server" CssClass="form-control w-100 custom-bg-darker"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPosicion" runat="server" Text="Posición:" CssClass="fw-bold"></asp:Label>
                    <asp:TextBox ID="txtPosicion" runat="server" CssClass="form-control w-100 custom-bg-darker"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblCategoria" runat="server" Text="Categoría:" CssClass="fw-bold"></asp:Label>
                    <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control w-100 custom-bg-darker"></asp:TextBox>
                </div>
            </div>

            <div class="col-6 d-flex justify-content-center">
                <asp:Image ID="imgJugador" runat="server" CssClass="img-fluid rounded" />
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha Nacimiento (Edad):" CssClass="fw-bold"></asp:Label>
                    <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control w-100 custom-bg-darker"></asp:TextBox>
                </div>
            </div>
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label ID="lblNacionalidad" runat="server" Text="Nacionalidad:" CssClass="fw-bold"></asp:Label>
                    <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control w-100 custom-bg-darker"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label ID="lblAltura" runat="server" Text="Altura:" CssClass="fw-bold"></asp:Label>
                    <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control w-100 custom-bg-darker"></asp:TextBox>
                </div>
            </div>
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label ID="lblPeso" runat="server" Text="Peso:" CssClass="fw-bold"></asp:Label>
                    <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control w-100 custom-bg-darker"></asp:TextBox>
                </div>
            </div>
        </div>
    </asp:Panel>

    
    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Text="El jugador seleccionado no tiene incidencias abiertas registradas." Visible="false"></asp:Label>

    <section>
        <asp:GridView ID="dgvIncidencias" CssClass="table table-dark table-hover" runat="server" AutoGenerateColumns="false" DataKeyNames="IdIncidencia" OnRowDataBound="dgvIncidencias_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdIncidencia" Visible="false" />
                <asp:BoundField HeaderText="Fecha de Registro" DataField="FechaRegistro" />
                <asp:BoundField HeaderText="Fecha de Resolución" DataField="FechaResolución" />
                <asp:BoundField HeaderText="Estado Jugador" DataField="EstadoJugador.NombreEstado" />
                <asp:BoundField HeaderText="Estado Incidencia" DataField="Estado" />

                <asp:TemplateField>
                    <HeaderStyle Width="8%" />
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Button ID="btnVerDetalle" runat="server" Text="Ver Detalle" CommandName="VerDetalle"
                            CommandArgument='<%# Eval("IdIncidencia") %>' CssClass="btn btn-outline-info" OnClick="btnAccion_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Width="8%" />
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CommandName="Actualizar"
                            CommandArgument='<%# Eval("IdIncidencia") %>' CssClass="btn btn-outline-warning" OnClick="btnAccion_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </section>

</asp:Content>
