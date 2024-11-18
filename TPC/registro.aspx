<%@ Page Title="Registrate!" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="TPC.registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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
                    ValidationExpression="^[a-zA-Z\s]*$" />
            </div>

            <div class="col-md-4">
                <label for="txtApellido" class="form-label">Apellido:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtApellido" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtApellido" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtApellido" ErrorMessage="Solo se permiten letras y espacios" CssClass="text-danger" runat="server"
                    ValidationExpression="^[a-zA-Z\s]*$" />
            </div>

            <div class="col-md-4">
                <label for="txtDNI" class="form-label">DNI:</label>
                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtDNI" />
                <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio" ControlToValidate="txtDNI" CssClass="text-danger" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtDNI" ErrorMessage="Solo se permiten números" CssClass="text-danger" runat="server"
                    ValidationExpression="^\d+$" />
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

            <div class="form-group">
                <label for="fileInput">Subir Imagen</label>
                <input type="file" id="fileInput" class="form-control" accept="image/*" onchange="previewImage(event)" />
            </div>
            <div class="form-group mt-3">
                <img id="imgPreview" src="#" alt="Vista previa" style="display: none; max-width: 100%; height: auto;" />
            </div>

            <!-- Otros Campos -->

            <div class="col-12">
                <asp:Button Text="Confirmar" CssClass="btn btn-primary" ID="btnConfirmar" runat="server" Enabled="true" OnClick="btnConfirmar_Click" />
            </div>
        </div>

    </section>


</asp:Content>
