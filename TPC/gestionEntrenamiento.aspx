<%@ Page Title="Entrenamiento" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gestionEntrenamiento.aspx.cs" Inherits="TPC.gestionEntrenamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="dgvEntrenamiento" CssClass="table table-dark table-hover" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="IdJugador" DataField="IdJugador" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
            <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
            <asp:BoundField HeaderText="Altura" DataField="Altura" />
            <asp:BoundField HeaderText="Peso" DataField="Peso" />
            <asp:BoundField HeaderText="Posicion" DataField="Posicion" />
        </Columns>
    </asp:GridView>




</asp:Content>
