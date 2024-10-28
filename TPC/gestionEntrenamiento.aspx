<%@ Page Title="Entrenamiento" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gestionEntrenamiento.aspx.cs" Inherits="TPC.gestionEntrenamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div class="row mb-3">
            <div class="col-md-4">
                <label for="txtFechaEntrenamiento" class="form-label">Fecha de Entrenamiento:</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txtFechaEntrenamiento" />
            </div>

            <div class="col-md-4">
                <label for="txtHoraEntrenamiento" class="form-label">Hora de Entrenamiento:</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Time" ID="txtHoraEntrenamiento" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-4">
                <label for="txtCategoria" class="form-label">Categoria:</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
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

    <div>
        <asp:Button ID="btnMostrarSeleccionados" runat="server" Text="Mostrar Jugadores Seleccionados" CssClass="btn btn-primary" OnClick="btnMostrarSeleccionados_Click" />

        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>


</asp:Content>
