<%@ Page Title="Usuario" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuario.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%-- Trabajar aquí en la implementación del formulario de creación de usuarios ya con los estilos personalizados --%>

        <div class="formulario__contenedor">
            <div class="formulario" role="form">
                <header class="formulario__header">
                    <h1 class="formulario__titulo">Mantenimiento de usuarios</h1>
                    <p class="formulario__parrafo">Complete cada uno de los campos para crear o modificar un usuario</p>
                </header>
                <div class="formulario__main">
                    <fieldset class="formulario__fieldset">
                        <legend>Datos para acceso:</legend>
                        <!-- Nombre Usuario -->
                        <div class="formulario__contenedor-input">
                            <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="formulario__label">
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="formulario__input" placeholder="Ingrese un usuario"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación del nombre usuario --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Es necesario ingresar el nombre de usuario." ControlToValidate="txtUsuario" display="Dynamic" CssClass="text-muted input-error"></asp:RequiredFieldValidator>
                                </p>
                                <%--<p class="formulario__mensaje"></p>--%>
                            </div>
                        </div>

                        <!-- Contraseña -->
                        <div class="formulario__contenedor-input">                            
                            <asp:Label ID="lblContrasenna" runat="server" Text="Contraseña:" CssClass="formulario__label">
                                <asp:TextBox ID="txtContrasenna" runat="server" CssClass="formulario__input" TextMode="Password" placeholder="Ingrese una contraseña"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación de la contraseña --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvContrasenna" runat="server" ErrorMessage="Es obligatorio establecer una contraseña." ControlToValidate="txtContrasenna" display="Dynamic" CssClass="text-muted input-error"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset class="formulario__fieldset">
                        <legend>Datos del usuario:</legend>

                        <!-- Nombre -->
                        <div class="formulario__contenedor-input">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="formulario__label">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="formulario__input" placeholder="John"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación del nombre --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Es necesario ingresar el nombre." ControlToValidate="txtNombre" display="Dynamic" CssClass="text-muted input-error"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>

                        <div class="formulario__contenedor-flex">
                            <!-- Primer Apellido -->    
                            <div class="formulario__contenedor-input contenedor-input">
                                <asp:Label ID="lblApellidoUno" runat="server" Text="Primer Apellido:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtApellidoUno" runat="server" CssClass="formulario__input" placeholder="Doe"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación del primer apellido --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="Es necesario ingresar su primer apellido." ControlToValidate="txtApellidoUno" display="Dynamic" CssClass="text-muted"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                            <!-- Segundo Apellido (Opcional)-->    
                            <div class="formulario__contenedor-input contenedor-input">
                                <asp:Label ID="lblApellidoDos" runat="server" Text="Segundo Apellido:" CssClass="formulario__label">Segundo Apellido: <span class="formulario__opcional" title="Opcional">(?)</span>
                                    <asp:TextBox ID="txtApellidoDos" runat="server" CssClass="formulario__input" placeholder="Ingrese el segundo apellido"></asp:TextBox>
                                </asp:Label>
                            </div>
                        </div>

                        <!-- Correo electrónico -->
                        <div class="formulario__contenedor-input">
                            <asp:Label ID="lblCorreoUsuario" runat="server" Text="Correo Électrónico:" CssClass="formulario__label">
                                <asp:TextBox ID="txtCorreoUsuario" runat="server" CssClass="formulario__input" TextMode="Email" placeholder="correo@correo.com"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación del primer apellido --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvCorreoUsuario" runat="server" ErrorMessage="Es necesario ingresar un correo electrónico." ControlToValidate="txtCorreoUsuario" display="Dynamic" CssClass="text-muted"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset class="formulario__fieldset">
                        <legend>Estado actual del usuario:</legend>
                        <div class="formulario__contenedor-input">

                             <!-- Estado del usuario -->
                            <asp:Label ID="lblEstadoUsuario" runat="server" Text="El usuario se encuentra:" CssClass="formulario__label">
                                Estado:
                                <asp:DropDownList ID="ddlEstadoUsuario" runat="server" CssClass="formulario__input">
                                    <asp:ListItem Value="" Text="Seleccione una opción"/>
                                    <asp:ListItem Value="4" Text="Activo"/>
                                    <asp:ListItem Value="3" Text="Inactivo"/>
                                </asp:DropDownList>
                            </asp:Label>
                            <%-- Validación del estado del usuario --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvEstadoUsuario" runat="server" ErrorMessage="Es necesario seleccionar el estado del usuario." ControlToValidate="ddlEstadoUsuario" display="Dynamic" CssClass="text-muted"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <footer class="formulario__footer">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" CssClass="boton__guardar" OnClick="btnGuardar_Click"/>
                </footer>
            </div>
        </div>
</asp:Content>