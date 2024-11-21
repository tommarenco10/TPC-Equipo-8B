<%@ Page Title="Gestión Plantilla" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfigJugador.aspx.cs" Inherits="TPC.ConfigWeb" EnableSessionState="true" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <h3>En esta pagina podras gestionar la plantilla del club agregando, modificando o eliminando jugadores en casos de ventas, compras o si un jugador se encuentra disponible o no. </h3>
        <p>Agregar: si completas todos los campos se realizara el alta de un nuevo jugador</p>
        <p>Modificar: se cargaran todos los datos del jugador que hayas seleccionado previamente y podras modificar los campos, una vez hecho dale a modificar!</p>
        <p>Eliminar: se eliminara el jugador que hayas seleccionado previamente, por favor tener precaucion a la hora de utilizar esta opcion.</p>
        <p>Cancelar: si estas en esta pagina y te arrepentis de lo que estas haciendo, simplemente utiliza esta opcion!</p>

        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <% if (Negocio.Seguridad.esAdmin(Session["user"]))
                        { %>
                    <label for="txtId" class="form-label">Id:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxId" />
                    <% } %>
                </div>
            </div>

            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxNombre" />
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="txtboxNombre"
                    InitialValue=""
                    ErrorMessage="El nombre es obligatorio."
                    ForeColor="Red" />
            </div>

            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxApellido" />
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="txtboxApellido"
                    InitialValue=""
                    ErrorMessage="El apellido es obligatorio."
                    ForeColor="Red" />
            </div>

            <div class="mb-3">
                <label for="txtFechaNacimiento" class="form-label">Fecha de nacimiento:</label>
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

            <asp:ScriptManager runat="server" />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <div class="mb-3">
                        <label for="ddlPais" class="form-label">País:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPais" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPais" InitialValue="" ErrorMessage="El país es obligatorio." ForeColor="Red" />
                    </div>

                    <div class="mb-3">
                        <label for="ddlProvincia" class="form-label">Provincia:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlProvincia" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProvincia" InitialValue="" ErrorMessage="La provincia es obligatoria." ForeColor="Red" />
                    </div>

                    <div class="mb-3">
                        <label for="ddlCiudad" class="form-label">Ciudad:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCiudad" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="col-6">
                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtboxEmail" />
                    <asp:RequiredFieldValidator
                        runat="server"
                        ControlToValidate="txtboxEmail"
                        InitialValue=""
                        ErrorMessage="El email es obligatorio."
                        ForeColor="Red" />
                    <asp:RegularExpressionValidator
                        runat="server"
                        ControlToValidate="txtboxEmail"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                        ErrorMessage="Formato de email no válido."
                        ForeColor="Red" />
                </div>

                <div class="mb-3">
                    <label for="txtAltura" class="form-label">Altura:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtboxAltura" />
                    <asp:Label runat="server" Text="Por favor, ingrese la altura en centímetros (0 - 250 cm):" />
                    <asp:RangeValidator
                        runat="server"
                        ControlToValidate="txtboxAltura"
                        MinimumValue="0"
                        MaximumValue="250"
                        Type="Integer"
                        ErrorMessage="La altura debe estar entre 0 cm y 250 cm."
                        ForeColor="Red" />
                </div>

                <div class="mb-3">
                    <label for="txtPeso" class="form-label">Peso:</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="number" ID="txtboxPeso" />
                    <asp:RangeValidator
                        runat="server"
                        ControlToValidate="txtboxPeso"
                        MinimumValue="1"
                        MaximumValue="300"
                        Type="Double"
                        ErrorMessage="El peso debe ser entre 1 y 300 kg."
                        ForeColor="Red" />
                </div>

                <div class="mb-3">
                    <label for="txtPosicion" class="form-label">Posicion:</label>
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtboxPosicion" />
                    <asp:RequiredFieldValidator
                        runat="server"
                        ControlToValidate="txtboxPosicion"
                        InitialValue=""
                        ErrorMessage="La posición es obligatoria."
                        ForeColor="Red" />
                </div>

                <div class="mb-3">
                    <label for="txtCategoria" class="form-label">Categoria:</label>
                    <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlCategoria" />
                </div>

                <div class="mb-3">
                    <label for="txtEstadoJugador" class="form-label">Estado del jugador:</label>
                    <asp:DropDownList runat="server" CssClass="btn btn-outline-dark dropdown-toggle" ID="ddlEstadoJugador" />
                </div>
            </div>
        </div>

        <div class="row g-3">
            <!-- Campo para subir la imagen -->
            <div class="col-md-6">
                <label for="fileInput" class="form-label">Subir Imagen</label>
                <asp:FileUpload ID="fileInput" runat="server" CssClass="form-control mb-2" OnChange="validarTamanoYVistaPrevia(this);" />
                <asp:Label ID="Label1" runat="server" CssClass="text-danger" Text=""></asp:Label>
            </div>

            <!-- Vista previa de la imagen, centrada y con tamaño limitado -->
            <div class="col-md-6 d-flex justify-content-center align-items-center mt-3">
                <img id="imgPreview" src="/Images/placeholder.png" alt="Vista previa" class="preview-image" runat="server" />
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
