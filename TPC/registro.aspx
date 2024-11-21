<%@ Page Title="Registrate!" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="TPC.registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />
    <section>
        <h2 class="mb-4">Formulario de Registro</h2>
        <div class="row g-3">

            <!--Usuario y contraseña -->
            <div class="col-md-4">
                <label for="txtUserName" class="form-label">Nombre de usuario:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtUserName" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtUserName" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtUserName" ErrorMessage="Solo se permiten letras y números" CssClass="text-danger" runat="server"
                    ValidationExpression="^[a-zA-Z0-9]*$" />
            </div>

            <div class="col-md-4">
                <label for="txtPassword" class="form-label">Contraseña:</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPassword" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPassword" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtPassword" ErrorMessage="Debe incluir letras y números, mínimo 6 caracteres" CssClass="text-danger" runat="server"
                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$" />
            </div>

            <div class="col-md-4">
                <label for="txtConfirmPassword" class="form-label">Repetir Contraseña:</label>
                <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtConfirmPassword" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtConfirmPassword" CssClass="text-danger" runat="server" />
                <asp:CompareValidator ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" CssClass="text-danger" runat="server"
                    ErrorMessage="Las contraseñas no coinciden" />
            </div>

            <!--Linea separadora -->
            <div class="col-12">
                <hr />
            </div>

            <!--Datos Personales -->
            <div class="col-md-4">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtNombre" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtNombre" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtNombre" ErrorMessage="Solo se permiten letras y espacios" CssClass="text-danger" runat="server"
                    ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚ\s]*$" />
            </div>

            <div class="col-md-4">
                <label for="txtApellido" class="form-label">Apellido:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtApellido" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtApellido" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtApellido" ErrorMessage="Solo se permiten letras y espacios" CssClass="text-danger" runat="server"
                    ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚ\s]*$" />
            </div>

            <div class="col-md-4">
                <label for="txtDNI" class="form-label">DNI:</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtDNI" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtDNI" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtDNI" ErrorMessage="Solo se permiten números" CssClass="text-danger" runat="server" ValidationExpression="^\d+$" />
                <asp:RangeValidator ErrorMessage="Por favor, ingrese un DNI válido (10,000,000 a 100,000,000)" CssClass="text-danger" ControlToValidate="txtDNI" MinimumValue="10000000" MaximumValue="100000000" runat="server" Type="Integer" />
                <span class="text-danger" id="errDNI"></span>
            </div>

            <div class="col-md-4">
                <label for="txtFechaNacimiento" class="form-label">Fecha de Nacimiento:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="date" ID="txtFechaNacimiento" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtFechaNacimiento" CssClass="text-danger" runat="server" />
            </div>

            <div class="col-md-6">
                <label for="txtEmail" class="form-label">Email:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="email" ID="txtEmail" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtEmail" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtEmail" ErrorMessage="Ingrese un correo válido" CssClass="text-danger" runat="server"
                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
            </div>


            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="col-md-4">
                        <label for="ddlPais" class="form-label">País:</label>
                        <asp:DropDownList runat="server" CssClass="form-select" ID="ddlPais" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="ddlPais" InitialValue="" CssClass="text-danger" runat="server" />
                    </div>

                    <div class="col-md-4">
                        <label for="ddlProvincia" class="form-label">Provincia:</label>
                        <asp:DropDownList runat="server" CssClass="form-select" ID="ddlProvincia" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="ddlProvincia" InitialValue="" CssClass="text-danger" runat="server" />
                    </div>

                    <div class="col-md-4">
                        <label for="ddlCiudad" class="form-label">Ciudad:</label>
                        <asp:DropDownList runat="server" CssClass="form-select" ID="ddlCiudad">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="ddlCiudad" InitialValue="" CssClass="text-danger" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


            <div class="row g-3">
                <!-- Campo para subir la imagen -->
                <asp:FileUpload ID="fileInput" runat="server" CssClass="form-control mb-2" OnChange="validarTamanoYVistaPrevia(this);" />
                <asp:Label ID="Label1" runat="server" CssClass="text-danger" Text=""></asp:Label>

                <!-- Vista previa de la imagen, centrada y con tamaño limitado -->
                <div class="col-md-6 d-flex justify-content-center align-items-center mt-3">
                    <img id="imgPreview" src="/Images/placeholder.png" alt="Vista previa" class="preview-image" />
                </div>
                </div>




                <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>



                <!-- Otros Campos -->

                <div class="col-12">
                    <asp:Button Text="Confirmar" CssClass="btn btn-primary" ID="btnConfirmar" runat="server" Enabled="true" OnClick="btnConfirmar_Click" />
                </div>
            </div>
    </section>


</asp:Content>