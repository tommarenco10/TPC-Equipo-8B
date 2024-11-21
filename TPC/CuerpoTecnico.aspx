<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CuerpoTecnico.aspx.cs" Inherits="TPC.CuerpoTecnico" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="titulo text-lg-center">
        <h1> Cuerpo Tecnico </h1>
    </div>
    <div class="row">

        <% foreach (Dominio.Entrenador ct in Entrenadores)
            {  %>
        <div class="col-3">
            <div class="card" style="width: 18rem;">
                <img src="<%: ct.UrlImagen %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%: ct.Nombres + " " + ct.Apellidos %></h5>
                    <h5 class="card-title"><%: ct.Rol %></h5>
                    <p class="card-text"><%: ct.Rol %> del Club Atletico Pes United. Se unio al club el <%: ct.FechaContratacion.Date %></p>
                    <a href="ConfigEntrenador.aspx?IdEntrenador=<%: ct.IdEntrenador %>" class="btn btn-primary">Configuracion</a>
                </div>
            </div>
        </div>
        <%    } %>
    </div>
</asp:Content>
