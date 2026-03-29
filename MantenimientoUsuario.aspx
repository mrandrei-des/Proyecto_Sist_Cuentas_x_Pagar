<%@ Page Title="Usuario" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MantenimientoUsuario.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.Usuario" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main__wrapper">
        <main class="page__main page__main--registro" runat="server" id="divMain">
            <section class="contenedor__section--titulo">
                <h1 class="titulo">
                    <span class="contenedor__icono--titulo">
                        <i class="fa-solid fa-user-plus"></i>
                    </span>
                    <span>Creación de Usuarios
                    </span>
                </h1>
            </section>
            <section class="contenedor__section--subtitulo">
                <div class="contenedor__subtitulo">
                    <h2 class="formulario__subtitulo">Formulario Usuarios</h2>
                    <p class="formulario__parrafo parrafo">Complete cada uno de los campos para registrar un nuevo usuario</p>
                </div>
                <div class="contenedor__accion--externa">
                    <a href="ListadoUsuarios" class="enlace__accion--externa" title="Ver usuarios registrados">
                        <span class="span__flotante__accion--externa">Listar</span>
                        <span class="contenedor__icono__accion--externa">
                            <i class="fa-solid fa-table-list"></i>
                        </span>
                    </a>
                </div>
            </section>
            <section class="contenedor__section--formulario">
                <div class="formulario formulario--gr4" role="form">
                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc2">
                            <legend>Datos para acceso:</legend>
                            <!-- Nombre Usuario -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="formulario__label" AssociatedControlID="txtUsuario">
                                </asp:Label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="formulario__input" placeholder="Ingrese un usuario" required="true"></asp:TextBox>
                                <%-- Validación del nombre usuario --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesNombreUsuario">
<%--                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Es necesario ingresar el nombre de usuario." ControlToValidate="txtUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>--%>
                                </div>
                            </div>
                            <!-- Contraseña -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblContrasenna" runat="server" Text="Contraseña:" CssClass="formulario__label" AssociatedControlID="txtContrasenna">
                                </asp:Label>
                                  <asp:TextBox ID="txtContrasenna" runat="server" CssClass="formulario__input" TextMode="Password" placeholder="Ingrese una contraseña" required="true"></asp:TextBox>
                                <%-- Validación de la contraseña --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesContrasenna">
<%--                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvContrasenna" runat="server" ErrorMessage="Es obligatorio establecer una contraseña." ControlToValidate="txtContrasenna" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>--%>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc3">
                            <legend>Datos del usuario:</legend>
                            <!-- Nombre -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="formulario__label" AssociatedControlID="txtNombre">
                                </asp:Label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="formulario__input" placeholder="John" required="true"></asp:TextBox>
                                <%-- Validación del nombre --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesNombre">
<%--                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Es necesario ingresar el nombre." ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>--%>
                                </div>
                            </div>
                            <!-- Primer Apellido -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblApellidoUno" runat="server" Text="Primer Apellido:" CssClass="formulario__label" AssociatedControlID="txtApellidoUno">
                                </asp:Label>
                               <asp:TextBox ID="txtApellidoUno" runat="server" CssClass="formulario__input" placeholder="Doe" required="true"></asp:TextBox>
                                <%-- Validación del primer apellido --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesApellido1">
<%--                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="Es necesario ingresar su primer apellido." ControlToValidate="txtApellidoUno" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>--%>
                                </div>
                            </div>
                            <!-- Segundo Apellido (Opcional)-->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblApellidoDos" runat="server" Text="Segundo Apellido:" CssClass="formulario__label" AssociatedControlID="txtApellidoDos">Segundo Apellido: <span class="formulario__opcional" title="Opcional"><i class="fa-solid fa-circle-info"></i></span>
                                </asp:Label>
                                <asp:TextBox ID="txtApellidoDos" runat="server" CssClass="formulario__input" placeholder="Ingrese el segundo apellido"></asp:TextBox>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesApellido2">
                                    <%--<p class="formulario__mensaje"></p>--%>
                                </div>
                            </div>
                            <!-- Correo electrónico -->
                            <div class="formulario__contenedor-input formulario__contenedor-input-sp3">
                                <asp:Label ID="lblCorreoUsuario" runat="server" Text="Correo Electrónico:" CssClass="formulario__label" AssociatedControlID="txtCorreoUsuario">
                                </asp:Label>
                                 <asp:TextBox ID="txtCorreoUsuario" runat="server" CssClass="formulario__input" TextMode="Email" placeholder="correo@correo.com" required="true"></asp:TextBox>
                                <%-- Validación del correo electrónico --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesCorreo">
<%--                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvCorreoUsuario" runat="server" ErrorMessage="Es necesario ingresar un correo electrónico." ControlToValidate="txtCorreoUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>--%>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc2">
                            <legend>Atributos del usuario:</legend>
                            <div class="formulario__contenedor-input">
                                <!-- Estado del usuario -->
                                <asp:Label ID="lblEstadoUsuario" runat="server" Text="El usuario se encuentra:" CssClass="formulario__label" AssociatedControlID="ddlEstadoUsuario">Estado:
                                </asp:Label>
                                <asp:DropDownList ID="ddlEstadoUsuario" runat="server" CssClass="formulario__input" required="true">
                                </asp:DropDownList>
                                <%-- Validación del estado del usuario --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesEstado">
<%--                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvEstadoUsuario" runat="server" ErrorMessage="Es necesario seleccionar el estado del usuario." ControlToValidate="ddlEstadoUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>--%>
                                </div>
                            </div>
                            <!-- Rol del usuario -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblRol" runat="server" Text="Rol del usuario:" CssClass="formulario__label" AssociatedControlID="ddlRoles">Rol en el sistema:
                                </asp:Label>
                                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="formulario__input" required="true">
                                </asp:DropDownList>
                                <%-- Validación del rol del usuario --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesRol">
<%--                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvRolUsuario" runat="server" ErrorMessage="Es necesario seleccionar el rol del usuario." ControlToValidate="ddlRoles" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>--%>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <footer class="formulario__contenedor formulario__footer">
                        <asp:LinkButton ID="btnGuardar" runat ="server" CssClass="boton boton__guardar" OnClick="btnGuardar_Click">
                            <span>Guardar</span>
                            <span class="contenedor__icono">
                               <i class="fa-solid fa-user-check"></i>
                            </span>  
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnCancelar" runat ="server" CssClass="boton boton__refrescar" OnClick="btnCancelar_Click" CausesValidation="false" ToolTip="Limpiar campos">
                            <span>Limpiar</span>
                            <span class="contenedor__icono">
                                <i class="fa-solid fa-eraser"></i>                               
                            </span>  
                        </asp:LinkButton>
                    </footer>
                </div>
            </section>
        </main>
    </div>

    <script src="Scripts/cxp_Scripts/script_MantUsuarios.js"></script>
</asp:Content>
