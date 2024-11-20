<%@ Page Title="Incidencias" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gestionIncidencias.aspx.cs" Inherits="TPC.gestionIncidencias" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Registrar Incidencia para Jugador</h2>
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

    <asp:Panel ID="pnlIncidencia" runat="server" CssClass="card p-3 my-3 bg-light">
        <h4>Detalles de la Incidencia</h4>
        <br />
        <div class="row mb-3">
            <div class="col-md-4">
                <asp:Label runat="server" AssociatedControlID="ddlTipoIncidencia" Text="Tipo de Incidencia:" CssClass="fw-bold"></asp:Label><br />
                <asp:DropDownList ID="ddlTipoIncidencia" runat="server" CssClass="form-select">
                </asp:DropDownList><br />
            </div>

            <div class="col-md-4">
                <asp:Label runat="server" AssociatedControlID="txtFechaRegistro" Text="Fecha de Registro:" CssClass="fw-bold"></asp:Label><br />
                <asp:TextBox ID="txtFechaRegistro" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <asp:Label runat="server" AssociatedControlID="txtFechaResolucion" Text="Fecha Estimada de Resolución:" CssClass="fw-bold"></asp:Label><br />
                <asp:TextBox ID="txtFechaResolucion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div>
            <asp:Label runat="server" AssociatedControlID="txtDescripcion" Text="Descripción:" CssClass="fw-bold"></asp:Label><br />
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><br />
        </div>
    </asp:Panel>

    <asp:ScriptManager runat="server" />

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlObservaciones" runat="server" CssClass="card p-3 my-3 bg-light" Visible="false">
                <h4>Observaciones (Evolución de la Lesión)</h4>
                <br />
                <div class="row mb-1">
                    <div class="col-md-2">
                        <asp:Label runat="server" AssociatedControlID="txtFechaObservacion" Text="Fecha:" CssClass="fw-bold"></asp:Label><br />
                        <asp:TextBox ID="txtFechaObservacion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />
                    </div>
                    <div class="col-md-10">
                        <asp:Label runat="server" AssociatedControlID="txtDescripcionObservacion" Text="Descripción:" CssClass="fw-bold"></asp:Label><br />
                        <asp:TextBox ID="txtDescripcionObservacion" runat="server" CssClass="form-control"></asp:TextBox><br />
                    </div>
                </div>

                <div class="row my-1">
                    <div class="col-md-2">
                        <asp:Button ID="btnAgregarObservacion" runat="server" Text="Agregar Observación" CssClass="btn btn-secondary my-2" OnClick="btnAgregarObservacion_Click" /><br />
                    </div>
                    <div class="col-md-6 d-flex align-items-center ms-2">
                        <asp:Label ID="lblErrorObs" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                </div>

                <asp:GridView ID="dgvObservaciones" runat="server" CssClass="table table-striped mt-3" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAgregarObservacion" EventName="Click" />
        </Triggers>

    </asp:UpdatePanel>

    <div class="my-3 d-flex align-items-center">
        <div class="d-flex">
            <asp:Button ID="btnModificar" runat="server" Text="Habilitar Modificación" CssClass="btn btn-outline-warning me-3" OnClick="btnModificar_Click" />
            <asp:Button ID="btnResumen" runat="server" Text="Resumen de Incidencias" CssClass="btn btn-info me-3" OnClick="btnResumen_Click" />
        </div>
        <div class="ms-4 d-flex align-items-center">
            <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </div>
    <div class="d-flex">
        <asp:Button ID="btnGuardarIncidencia" runat="server" Text="Guardar Incidencia" CssClass="btn btn-primary me-3" OnClick="btnGuardarIncidencia_Click" />
        <asp:Button ID="btnActualizarIncidencia" runat="server" Text="Guardar Actualización" CssClass="btn btn-warning me-3" OnClick="btnGuardarIncidencia_Click" />
        <asp:Button ID="btnVolver" runat="server" Text="Volver a Plantilla" CssClass="btn btn-danger" OnClick="btnVolver_Click" />
    </div>

</asp:Content>
