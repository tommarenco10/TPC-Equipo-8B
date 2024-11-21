<%@ Page Title="Gestión Plantilla" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigJugador.aspx.cs" Inherits="TPC.ConfigWeb" EnableSessionState="true" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="container mt-4">

        <h3 class="mb-4">Gestión de Plantilla del Club</h3>
        <p>En esta página podrás gestionar la plantilla del club agregando, modificando o eliminando jugadores, así como gestionar su disponibilidad.</p>

        <div class="alert alert-info">
            <strong>Instrucciones:</strong>
            <ul>
                <li><strong>Agregar:</strong> Completa todos los campos para agregar un nuevo jugador.</li>
                <li><strong>Modificar:</strong> Modifica los campos de un jugador seleccionado.</li>
                <li><strong>Eliminar:</strong> Elimina un jugador seleccionado (ten cuidado con esta opción).</li>
                <li><strong>Cancelar:</strong> Cancela la acción si cambias de opinión.</li>
            </ul>
        </div>

        <div class="card p-4 mb-4">
            <h4 class="card-title">Formulario de Jugador</h4>
            <div class="row">

                <!-- Campo ID (solo visible para Admin) -->
                <div class="col-md-6">
                    <% if (Negocio.Seguridad.esAdmin(Session["user"]))
                        { %>
                    <div class="mb-3">
                        <label for="txtId" class="form-label">ID:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxId" Enabled="false" />
                    </div>
                    <% } %>
                </div>

                <!-- Nombre -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxNombre" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtboxNombre" InitialValue="" ErrorMessage="El nombre es obligatorio." ForeColor="Red" />
                    </div>
                </div>

                <!-- Apellido -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtApellido" class="form-label">Apellido:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxApellido" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtboxApellido" InitialValue="" ErrorMessage="El apellido es obligatorio." ForeColor="Red" />
                    </div>
                </div>

                <!-- Fecha de Nacimiento -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtFechaNacimiento" class="form-label">Fecha de Nacimiento:</label>
                        <asp:TextBox runat="server" ID="txtFechaNacimiento" type="date" CssClass="form-control" />
                        <asp:CompareValidator
                            runat="server"
                            ControlToValidate="txtFechaNacimiento"
                            Type="Date"
                            Operator="DataTypeCheck"
                            ErrorMessage="Por favor, ingrese una fecha válida de nacimiento."
                            ForeColor="Red"
                            ValidationGroup="fechaGroup" />
                    </div>
                </div>

                <asp:ScriptManager runat="server" />
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <!-- País -->
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="ddlPais" class="form-label">País:</label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPais" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPais" InitialValue="" ErrorMessage="El país es obligatorio." ForeColor="Red" />
                            </div>
                        </div>

                        <!-- Provincia -->
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="ddlProvincia" class="form-label">Provincia:</label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlProvincia" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProvincia" InitialValue="" ErrorMessage="La provincia es obligatoria." ForeColor="Red" />
                            </div>
                        </div>

                        <!-- Ciudad -->
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="ddlCiudad" class="form-label">Ciudad:</label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCiudad" />
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- Email -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email:</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtboxEmail" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtboxEmail" InitialValue="" ErrorMessage="El email es obligatorio." ForeColor="Red" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtboxEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="Formato de email no válido." ForeColor="Red" />
                    </div>
                </div>

                <!-- Altura -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtAltura" class="form-label">Altura (cm):</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtboxAltura" />
                        <asp:RangeValidator runat="server" ControlToValidate="txtboxAltura" MinimumValue="0" MaximumValue="250" Type="Integer" ErrorMessage="La altura debe estar entre 0 y 250 cm." ForeColor="Red" />
                    </div>
                </div>

                <!-- Peso -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtPeso" class="form-label">Peso (kg):</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtboxPeso" />
                        <asp:RangeValidator runat="server" ControlToValidate="txtboxPeso" MinimumValue="1" MaximumValue="300" Type="Double" ErrorMessage="El peso debe estar entre 1 y 300 kg." ForeColor="Red" />
                    </div>
                </div>

                <!-- Posición -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtPosicion" class="form-label">Posición:</label>
                        <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxPosicion" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtboxPosicion" InitialValue="" ErrorMessage="La posición es obligatoria." ForeColor="Red" />
                    </div>
                </div>

                <!-- Categoria -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtCategoria" class="form-label">Categoría:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" />
                    </div>
                </div>

                <!-- Estado del Jugador -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtEstadoJugador" class="form-label">Estado del Jugador:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlEstadoJugador" />
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="fileInput" class="form-label">Foto del Jugador:</label>
                        <asp:FileUpload ID="fileInput" runat="server" CssClass="form-control mb-2" OnChange="validarTamanoYVistaPreviaJugador(this);" />
                        <img id="imgJugador" src="/Images/placeholder.png" alt="Vista previa" class="preview-image" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-12">
            <asp:UpdatePanel ID="updatePanel" runat="server">
                <ContentTemplate>
                    <asp:Button Text="Agregar" CssClass="btn btn-primary" ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" />
                    <asp:Button Text="Modificar" CssClass="btn btn-warning" ID="btnModificar" runat="server" OnClick="btnModificar_Click" AutoPostBack="true" />
                    <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
                    <% if (ConfirmarEliminacion)
                        { %>
                    <asp:CheckBox Text="Confirmar eliminacion" ID="chkboxConfirmado" runat="server" />
                    <asp:Button Text="Eliminar" CssClass="btn btn-outline-danger" OnClick="BtnEliminarConfirmado_Click" runat="server" />
                    <% } %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </section>
</asp:Content>
