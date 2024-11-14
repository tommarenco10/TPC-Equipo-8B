<%@ Page Title="Bienvenido!" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TPC.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <asp:Label Text="" ID="lblBienvenida" runat="server" CssClass="welcome-message" />
        <asp:Literal ID="litContent" runat="server" />
    </section>
</asp:Content>
