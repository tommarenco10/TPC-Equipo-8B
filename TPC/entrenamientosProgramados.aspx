<%@ Page Title="Entrenamientos Programados" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="entrenamientosProgramados.aspx.cs" Inherits="TPC.entrenamientosProgramados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
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
                        <asp:Button ID="btnVerDetalle" runat="server" Text="Ver Detalle" CommandName="VerDetalle"
                            CommandArgument='<%# Eval("IdEntrenamiento") %>' CssClass="btn btn-primary" OnClick="btnVerDetalle_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Width="8%" />
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CommandName="Actualizar"
                            CommandArgument='<%# Eval("IdEntrenamiento") %>' CssClass="btn btn-outline-warning" OnClick="btnActualizar_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Width="8%" />
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancelar"
                            CommandArgument='<%# Eval("IdEntrenamiento") %>' CssClass="btn btn-danger"
                            OnClientClick="return openConfirmModal(this);" OnClick="btnCancelar_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </section>

    <br />

    <div>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nuevo" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
    </div>

    <!-- Modal de Confirmación -->
    <div class="modal fade" id="confirmCancelModal" tabindex="-1" aria-labelledby="confirmCancelLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmCancelLabel">Confirmar Cancelación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    ¿Estás seguro de que deseas cancelar este entrenamiento?
           
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
                    <button type="button" class="btn btn-danger" id="confirmCancelButton">Sí, Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Código JavaScript para el Modal -->
    <script type="text/javascript">
        let selectedButton = null;

        function openConfirmModal(button) {
            selectedButton = button;  // Guarda el botón "Cancelar" presionado
            $('#confirmCancelModal').modal('show');  // Muestra el modal de confirmación
            return false;  // Previene la ejecución inmediata del evento de servidor
        }

        document.getElementById("confirmCancelButton").addEventListener("click", function () {
            $('#confirmCancelModal').modal('hide');  // Cierra el modal
            __doPostBack(selectedButton.name, '');  // Llama al evento OnClick en el servidor
        });
    </script>

</asp:Content>

