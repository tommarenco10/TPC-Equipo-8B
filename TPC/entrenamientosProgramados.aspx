﻿<%@ Page Title="Entrenamientos Programados" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientosProgramados.aspx.cs" Inherits="TPC.entrenamientosProgramados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h1>Entrenamientos Programados</h1>
    <br />
    <div class="row mb-3">
        <div class="col-md-4">
            <label for="txtCategoria" class="form-label">Categoria:</label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Text="La categoría seleccionada no tiene entrenamientos registrados." Visible="false"></asp:Label>

    <section>
        <asp:GridView ID="dgvEntrenamientos" CssClass="table table-dark table-hover" runat="server" AutoGenerateColumns="false" DataKeyNames="IdEntrenamiento">
            <Columns>
                <asp:BoundField DataField="IdEntrenamiento" Visible="false" />
                <asp:BoundField HeaderText="Fecha y Horario" DataField="FechaHora" />
                <asp:BoundField HeaderText="Duracion" DataField="Duracion" />
                <asp:BoundField HeaderText="Categoria" DataField="Categoria.NombreCategoria" />
                <asp:BoundField HeaderText="Estado" DataField="Estado.NombreEstado" />
                <asp:TemplateField>
                    <HeaderStyle Width="8%" />
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar"
                            CommandArgument='<%# Eval("IdEntrenamiento") %>' CssClass="btn btn-secondary" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Width="8%" />
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Button ID="btnVerDetalle" runat="server" Text="Ver Detalle" CommandName="VerDetalle"
                            CommandArgument='<%# Eval("IdEntrenamiento") %>' CssClass="btn btn-primary" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </section>

    <br />

    <div>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nuevo" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
    </div>

</asp:Content>
