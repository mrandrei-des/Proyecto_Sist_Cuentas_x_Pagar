<%@ Page Title="Usuario" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MantenimientoUsuario.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%-- Trabajar aquí en la implementación del formulario de creación de usuarios ya con los estilos personalizados --%>

        <div class="contenedor__titulo__principal">
            <h1>
                <span class="fondo__icono">
                    <svg xmlns="http://www.w3.org/2000/svg" width="26" height="26" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-cog"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" /><path d="M6 21v-2a4 4 0 0 1 4 -4h2.5" /><path d="M17.001 19a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" /><path d="M19.001 15.5v1.5" /><path d="M19.001 21v1.5" /><path d="M22.032 17.25l-1.299 .75" /><path d="M17.27 20l-1.3 .75" /><path d="M15.97 17.25l1.3 .75" /><path d="M20.733 20l1.3 .75" /></svg>
                </span>
                Mantenimiento
            </h1>
        </div>

        <div class="formulario__contenedor">
            <div class="formulario" role="form">
                <header class="formulario__header">
                    <h2 class="formulario__titulo">Formulario Usuarios</h2>
                    <p class="formulario__parrafo parrafo">Complete cada uno de los campos para crear o modificar un usuario</p>
                </header>
                <div class="formulario__main">
                    <fieldset class="formulario__fieldset">
                        <legend>Datos para acceso:</legend>
                        <div class="formulario__contenedor-flex">
                            <!-- Nombre Usuario -->
                            <div class="formulario__contenedor-input contenedor-input">
                                <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="formulario__input" placeholder="Ingrese un usuario" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación del nombre usuario --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Es necesario ingresar el nombre de usuario." ControlToValidate="txtUsuario" display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                    <%--<p class="formulario__mensaje"></p>--%>
                                </div>
                            </div>

                            <!-- Contraseña -->
                            <div class="formulario__contenedor-input contenedor-input">                            
                                <asp:Label ID="lblContrasenna" runat="server" Text="Contraseña:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtContrasenna" runat="server" CssClass="formulario__input" TextMode="Password" placeholder="Ingrese una contraseña" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación de la contraseña --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                            <asp:RequiredFieldValidator ID="rfvContrasenna" runat="server" ErrorMessage="Es obligatorio establecer una contraseña." ControlToValidate="txtContrasenna" display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset class="formulario__fieldset">
                        <legend>Datos del usuario:</legend>

                        <!-- Nombre -->
                        <div class="formulario__contenedor-input">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="formulario__label">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="formulario__input" placeholder="John" required="true"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación del nombre --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Es necesario ingresar el nombre." ControlToValidate="txtNombre" display="Dynamic"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>

                        <div class="formulario__contenedor-flex">
                            <!-- Primer Apellido -->    
                            <div class="formulario__contenedor-input contenedor-input">
                                <asp:Label ID="lblApellidoUno" runat="server" Text="Primer Apellido:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtApellidoUno" runat="server" CssClass="formulario__input" placeholder="Doe" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación del primer apellido --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="Es necesario ingresar su primer apellido." ControlToValidate="txtApellidoUno" display="Dynamic"></asp:RequiredFieldValidator>
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
                            <asp:Label ID="lblCorreoUsuario" runat="server" Text="Correo Electrónico:" CssClass="formulario__label">
                                <asp:TextBox ID="txtCorreoUsuario" runat="server" CssClass="formulario__input" TextMode="Email" placeholder="correo@correo.com" required="true"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación del primer apellido --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvCorreoUsuario" runat="server" ErrorMessage="Es necesario ingresar un correo electrónico." ControlToValidate="txtCorreoUsuario" display="Dynamic"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset class="formulario__fieldset">
                        <legend>Atributos del usuario:</legend>
                        <div class="formulario__contenedor-flex">
                            <div class="formulario__contenedor-input contenedor-input">
                                 <!-- Estado del usuario -->
                                <asp:Label ID="lblEstadoUsuario" runat="server" Text="El usuario se encuentra:" CssClass="formulario__label">
                                    Estado:
                                    <asp:DropDownList ID="ddlEstadoUsuario" runat="server" CssClass="formulario__input" required="true">
                                        <asp:ListItem Value="" Text="Seleccione una opción"/>
                                        <asp:ListItem Value="4" Text="Activo"/>
                                        <asp:ListItem Value="3" Text="Inactivo"/>
                                    </asp:DropDownList>
                                </asp:Label>
                                <%-- Validación del estado del usuario --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvEstadoUsuario" runat="server" ErrorMessage="Es necesario seleccionar el estado del usuario." ControlToValidate="ddlEstadoUsuario" display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <!-- Rol del usuario -->
                            <div class="formulario__contenedor-input contenedor-input">
                                <asp:Label ID="lblRol" runat="server" Text="Rol del usuario:" CssClass="formulario__label">
                                    Rol en el sistema:
                                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="formulario__input" required="true">
                                        <%-- Esto realmente debe cargar los roles definidos anteriormente --%>
                                        <asp:ListItem Value="" Text="Seleccione una opción"/>
                                        <asp:ListItem Value="1" Text="Administrador"/>
                                        <asp:ListItem Value="2" Text="Digitador Facturas"/>
                                        <asp:ListItem Value="3" Text="Digitador Documentos Pago"/>
                                        <asp:ListItem Value="4" Text="Asociador Pagos"/>
                                        <asp:ListItem Value="5" Text="Reportes"/>
                                    </asp:DropDownList>
                                </asp:Label>
                                <%-- Validación del rol del usuario --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvRolUsuario" runat="server" ErrorMessage="Es necesario seleccionar el rol del usuario." ControlToValidate="ddlRoles" display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                        </div>
                    </fieldset>
                </div>
                <footer class="formulario__footer">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" CssClass="boton boton__guardar" OnClick="btnGuardar_Click"/>
                </footer>
            </div>
        </div>
</asp:Content>