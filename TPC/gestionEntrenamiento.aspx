<%@ Page Title="Entrenamiento" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gestionEntrenamiento.aspx.cs" Inherits="TPC.gestionEntrenamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>

            <section>
                <% if (tipoPagina == 2)
                    {%>
                <h1>Modificar Entrenamiento</h1>
                <% }
                    else
                    { %>
                <h1>Agendar Entrenamiento</h1>
                <% }%>
                <br />
                <br />

                <h3>Detalles Generales</h3>
                <br />
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label for="txtFechaEntrenamiento" class="form-label">Fecha de Entrenamiento:</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txtFechaEntrenamiento" />
                    </div>

                    <div class="col-md-4">
                        <label for="txtHoraEntrenamiento" class="form-label">Hora de Entrenamiento:</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Time" ID="txtHoraEntrenamiento" />
                    </div>

                    <div class="col-md-4">
                        <label for="txtDuracion" class="form-label">Duración del Entrenamiento:</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Time" ID="txtDuracion" />
                    </div>
                </div>

                <br />

                <div class="col-md-6">
                    <label for="ddlCategoria" class="form-label">Categoría:</label>
                    <div class="d-flex">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" AutoPostBack="true" />
                        <asp:Button ID="btnPreseleccionar" runat="server" CssClass="btn btn-primary ms-2" Text="Preseleccionar Categoría" OnClick="cargaDGVJugadores" />
                    </div>
                </div>

                <br />
                <br />

                <div class="form-group">
                    <label for="txtDescripcion">Descripción Planificada del Entrenamiento</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Describe brevemente el entrenamiento planificado..."></asp:TextBox>
                </div>

                <br />
                <br />

                <h5>Detalles Adicionales</h5>

                <div class="row mb-3">
                    <div class="col-md-4">
                        <label for="ddlJugadoresAdicionales" class="form-label">Jugadores Adicionales:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlJugadoresAdicionales" AutoPostBack="true" OnSelectedIndexChanged="cargaDGVJugadores">
                        </asp:DropDownList>
                    </div>
                </div>
            </section>

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
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnPreseleccionar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlJugadoresAdicionales" EventName="SelectedIndexChanged" />
        </Triggers>

    </asp:UpdatePanel>

    <br />

    <br />
    <% if (tipoPagina != 2)
        {%>
    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success"
        OnClientClick="return openConfirmModal(this);" OnClick="btnGuardar_Click" />
    <% } %>

    <asp:Button ID="btnVistaPrevia" CssClass="btn btn-secondary" runat="server" Text="Ver Vista Previa" OnClick="btnVistaPrevia_Click" />
    
    <% if (tipoPagina == 2)
        { %>
    <br />
    <br />
    <asp:Button ID="btnModificar" CssClass="btn btn-warning" runat="server" Text="Guardar Modificación" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnVolverSinGuardar" CssClass="btn btn-danger" runat="server" Text="Volver sin guardar" OnClick="btnVolverSinGuardar_Click" />
    <% } %>

    <asp:Label ID="lblError" runat="server"></asp:Label>

    <!-- Modal de Confirmación -->
    <div class="modal fade" id="confirmGuardarModal" tabindex="-1" aria-labelledby="confirmGuardarLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmGuardarLabel">Confirmar Guardado</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    ¿Estás seguro de que deseas guardar el entrenamiento?
       
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">No</button>
                    <button type="button" class="btn btn-success" id="confirmGuardarButton">Sí, Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Código JavaScript para el Modal -->
    <script type="text/javascript">
        let selectedButton = null;

        function openGuardarModal(button) {
            selectedButton = button;  // Guarda el botón "Cancelar" presionado
            $('#confirmGuardarModal').modal('show');  // Muestra el modal de confirmación
            return false;  // Previene la ejecución inmediata del evento de servidor
        }

        document.getElementById("confirmGuardarButton").addEventListener("click", function () {
            $('#confirmGuardarModal').modal('hide');  // Cierra el modal
            __doPostBack(selectedButton.name, '');  // Llama al evento OnClick en el servidor
        });
    </script>

</asp:Content>
