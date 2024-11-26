<%@ Page Title="Gestión Plantilla" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigJugador.aspx.cs" Inherits="TPC.ConfigWeb" EnableSessionState="true" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel runat="server" CssClass="container border rounded shadow-lg p-4 position-relative">

        <h4 class="text-center mb-4">
            <% if (tipoPagina == 1)
                { %>
                Eliminar Jugador
            <% }
                else if (tipoPagina == 2)
                { %>
                Modificar Jugador
            <% }
                else
                { %>
                Agregar Jugador
            <% } %>
        </h4>

        <div class="row g-3">
            <% if (Negocio.Seguridad.esAdmin(Session["user"]))
                { %>
            <div class="col-12 col-md-1">
                <label for="txtId" class="form-label fw-bold">ID:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtId" Enabled="false" />
            </div>
            <% } %>
            <div class="col-12 col-md-4">
                <label for="txtNombre" class="form-label fw-bold">Nombre:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtNombre" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" InitialValue="" ErrorMessage="El nombre es obligatorio." ForeColor="Red" />
            </div>
            <div class="col-12 col-md-4">
                <label for="txtApellido" class="form-label fw-bold">Apellido:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtApellido" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApellido" InitialValue="" ErrorMessage="El apellido es obligatorio." ForeColor="Red" />
            </div>
        </div>

        <div class="row g-3 mt-3">
            <div class="col-12 col-md-3">
                <label for="txtDNI" class="form-label fw-bold">DNI:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtDNI" MinLength="7" MaxLength="8" />
                <asp:RegularExpressionValidator runat="server"
                    ControlToValidate="txtDNI"
                    CssClass="text-danger"
                    ValidationExpression="^\d{7,8}$"
                    ErrorMessage="El DNI debe tener entre 7 y 8 dígitos numéricos."
                    Display="Dynamic" />
            </div>
            <div class="col-12 col-md-5">
                <label for="txtPosicion" class="form-label fw-bold">Posición:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtPosicion" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPosicion" InitialValue="" ErrorMessage="La posición es obligatoria." ForeColor="Red" />
            </div>
            <div class="col-12 col-md-4">
                <label for="ddlCategoria" class="form-label fw-bold">Categoría:</label>
                <asp:DropDownList runat="server" CssClass="form-select" ID="ddlCategoria" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCategoria" InitialValue="" ErrorMessage="La provincia es obligatoria." ForeColor="Red" />
            </div>
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="updatePanelDDL" runat="server">
            <ContentTemplate>
                <div class="row g-3 mt-3">

                    <div class="col-12 col-md-3">
                        <label for="txtFechaNacimiento" class="form-label fw-bold">Fecha de Nacimiento:</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txtFechaNacimiento" />
                        <asp:CompareValidator
                            runat="server"
                            ControlToValidate="txtFechaNacimiento"
                            Type="Date"
                            Operator="DataTypeCheck"
                            ErrorMessage="Por favor, ingrese una fecha válida de nacimiento."
                            ForeColor="Red"
                            ValidationGroup="fechaGroup" />
                    </div>

                    <div class="col-12 col-md-3">
                        <label for="ddlPais" class="form-label fw-bold">País:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPais" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPais" InitialValue="" ErrorMessage="El país es obligatorio." ForeColor="Red" />
                    </div>

                    <div class="col-12 col-md-3">
                        <label for="ddlProvincia" class="form-label fw-bold">Provincia:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlProvincia" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProvincia" InitialValue="" ErrorMessage="La provincia es obligatoria." ForeColor="Red" />
                    </div>

                    <div class="col-12 col-md-3">
                        <label for="txtCiudad" class="form-label fw-bold">Ciudad:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCiudad" />
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="row g-3">
            <div class="col-12 col-md-8">
                <label for="txtEmail" class="form-label fw-bold">Email:</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtEmail" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" InitialValue="" ErrorMessage="El email es obligatorio." ForeColor="Red" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="Formato de email no válido." ForeColor="Red" />
            </div>
            <div class="col-12 col-md-2">
                <label for="txtAltura" class="form-label fw-bold">Altura (cm):</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtAltura" />
                <asp:RangeValidator runat="server" ControlToValidate="txtAltura" MinimumValue="0" MaximumValue="250" Type="Integer" ErrorMessage="La altura debe estar entre 0 y 250 cm." ForeColor="Red" />
            </div>
            <div class="col-12 col-md-2">
                <label for="txtPeso" class="form-label fw-bold">Peso (kg):</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="number" ID="txtPeso" />
                <asp:RangeValidator runat="server" ControlToValidate="txtPeso" MinimumValue="1" MaximumValue="300" Type="Double" ErrorMessage="El peso debe estar entre 1 y 300 kg." ForeColor="Red" />
            </div>
        </div>

        <asp:UpdatePanel ID="updatePanelImg" runat="server">
            <ContentTemplate>

                <div class="position-absolute top-0 end-0 p-3">
                    <asp:Image runat="server" ID="imgJugadorPrincipal" CssClass="rounded-circle border border-2"
                        Width="160px" Height="160px" ImageUrl="https://via.placeholder.com/160" />
                </div>

                <div class="row g-3 mt-3">
                    <div class="col-12 col-md-5">
                        <label for="fileInput" class="form-label">Foto del Jugador:</label>
                        <asp:FileUpload ID="fileInput" runat="server" CssClass="form-control mb-2" OnChange="validarTamanoYVistaPreviaJugador(this);" />
                        <img id="imgJugador" src="<%=getImagenJugador()%>" alt="Vista previa" class="preview-image" />
                    </div>
                    <div class="col-12 col-md-7">
                        <label for="txtUrlImagen" class="form-label fw-bold">URL Imagen:</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUrlImagen" AutoPostBack="true" OnTextChanged="txtUrlImagen_TextChanged" />
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtUrlImagen" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <div class="col-12 mt-3">
        <asp:UpdatePanel ID="updatePanelBtn" runat="server">
            <ContentTemplate>
                <% if (tipoPagina == 1)
                    { %>
                <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
                <% }
                    else if (tipoPagina == 2)
                    { %>
                <asp:Button Text="Modificar" CssClass="btn btn-warning" ID="btnModificar" runat="server" OnClick="btnModificar_Click" AutoPostBack="true" />
                <% }
                    else
                    { %>
                <asp:Button Text="Agregar" CssClass="btn btn-primary" ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" />
                <% } %>

                <% if (ConfirmarEliminacion)
                    { %>
                <asp:CheckBox Text="Confirmar eliminacion" ID="chkboxConfirmado" runat="server" />
                <asp:Button Text="Eliminar" CssClass="btn btn-outline-danger" OnClick="BtnEliminarConfirmado_Click" runat="server" />
                <% } %>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
