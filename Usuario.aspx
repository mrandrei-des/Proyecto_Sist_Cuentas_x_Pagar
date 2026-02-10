<%@ Page Title="Usuario" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuario.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="container p-4 border">

        <div class="row-cols-1">
            <h1 class="text-center font-weight-600">Registro de usuarios</h1>
            <p class="text-muted text-center">Complete la información para ingresar un nuevo usuario al sistema</p>
        </div>

        <div class="row">
            <!-- Nombre Usuario -->
            <div class="col mb-2 input-container">
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="control-label etiqueta-input mx-3"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control input my-1" placeholder="Ingrese el usuario de acceso"></asp:TextBox>
                <%-- Validación del nombre usuario --%>
                <em>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Es necesario ingresar el nombre de usuario." ControlToValidate="txtUsuario" display="Dynamic" CssClass="text-muted input-error"></asp:RequiredFieldValidator>
                </em>
            </div>

            <!-- Contraseña -->
            <div class="col mb-2 input-container">
                <asp:Label ID="lblContrasenna" runat="server" Text="Contraseña:" CssClass="control-label etiqueta-input mx-3"></asp:Label>
                <asp:TextBox ID="txtContrasenna" runat="server" CssClass="form-control input my-1" TextMode="Password" placeholder="Ingrese la contraseña de acceso"></asp:TextBox>
                <%-- Validación de la contraseña --%>
                <em>
                    <asp:RequiredFieldValidator ID="rfvContrasenna" runat="server" ErrorMessage="Es obligatorio establecer una contraseña." ControlToValidate="txtContrasenna" display="Dynamic" CssClass="text-muted input-error"></asp:RequiredFieldValidator>
                </em>
            </div>
        </div>

        <!-- Nombre -->
        <div class="col mb-2 input-container">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="control-label etiqueta-input mx-3"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input my-1" placeholder="Ingrese el nombre de usuario"></asp:TextBox>
            <%-- Validación del nombre --%>
            <em>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Es necesario ingresar el nombre." ControlToValidate="txtNombre" display="Dynamic" CssClass="text-muted input-error"></asp:RequiredFieldValidator>
            </em>
        </div>

        <div class="row">
            <!-- Primer Apellido -->    
            <div class="col mb-2 input-container">
                <asp:Label ID="lblApellidoUno" runat="server" Text="Primer Apellido:" CssClass="control-label etiqueta-input mx-3"></asp:Label>
                <asp:TextBox ID="txtApellidoUno" runat="server" CssClass="form-control input my-1" placeholder="Ingrese el primer apellido del usuario"></asp:TextBox>
                <%-- Validación del primer apellido --%>
                <em>
                    <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="Es necesario ingresar su primer apellido." ControlToValidate="txtApellidoUno" display="Dynamic" CssClass="text-muted"></asp:RequiredFieldValidator>
                </em>
            </div>

            <!-- Segundo Apellido (Opcional)-->    
            <div class="col mb-2 input-container">
                <asp:Label ID="lblApellidoDos" runat="server" Text="Segundo Apellido:" CssClass="control-label etiqueta-input mx-3">Segundo Apellido: <span class="text-muted colored" title="Opcional"> ( ? )</span></asp:Label>
                <asp:TextBox ID="txtApellidoDos" runat="server" CssClass="form-control input my-1" placeholder="Ingrese el segundo apellido del usuario"></asp:TextBox>
            </div>
        </div>

        <!-- Correo electrónico -->    
        <div class="col mb-2 input-container">
            <asp:Label ID="lblCorreoUsuario" runat="server" Text="Correo electrónico:" CssClass="control-label etiqueta-input mx-3"></asp:Label>
            <asp:TextBox ID="txtCorreoUsuario" runat="server" CssClass="form-control input my-1" TextMode="Email" placeholder="Ingrese un correo de contacto"></asp:TextBox>
            <%-- Validación del primer apellido --%>
            <em>
                <asp:RequiredFieldValidator ID="rfvCorreoUsuario" runat="server" ErrorMessage="Es necesario ingresar un correo electrónico." ControlToValidate="txtCorreoUsuario" display="Dynamic" CssClass="text-muted"></asp:RequiredFieldValidator>
            </em>
        </div>

        <!-- Estado del usuario -->
        <div class="col mb-2 input-container">
            <asp:Label ID="lblEstadoUsuario" runat="server" Text="El usuario se encuentra:" CssClass="control-label etiqueta-input mx-3"></asp:Label>
            <asp:DropDownList ID="ddlEstadoUsuario" runat="server" CssClass="form-control input my-1">
                <asp:ListItem Value="" Text="Seleccione el estado del usuario"/>
                <asp:ListItem Value="4" Text="Activo"/>
                <asp:ListItem Value="3" Text="Inactivo"/>
            </asp:DropDownList>
            <%-- Validación del tipo de idenficación --%>
            <em>
                <asp:RequiredFieldValidator ID="rfvEstadoUsuario" runat="server" ErrorMessage="Es necesario seleccionar el estado del usuario." ControlToValidate="ddlEstadoUsuario" display="Dynamic" CssClass="text-muted"></asp:RequiredFieldValidator>
            </em>
        </div>

        <div class="row mt-4">
            <div class="col-12 text-center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary px-5 text-uppercase font-weight-400 letter-spacing-1" OnClick="btnGuardar_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
