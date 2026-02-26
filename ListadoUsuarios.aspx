<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ListadoUsuarios.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="dialog" runat="server" id="modalModify" role="dialog">
    <div class="contenedor__modal contenedor__modal--modify">
        <h2 class="modal__titulo">Modificación de Usuarios</h2>
        <div class="modal__formulario">

            <div class="formulario__contenedor">
                <fieldset class="formulario__fieldset formulario__fieldset--g3">
                    <legend>Datos del usuario:</legend>
                    <!-- Nombre -->
                    <div class="formulario__contenedor-input">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="formulario__label">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="formulario__input" placeholder="John"></asp:TextBox>
                        </asp:Label>
                        <%-- Validación del nombre --%>
                        <div class="formulario__contenedor-mensajes">
                            <p class="formulario__mensaje">
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Es necesario ingresar el nombre." ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>
                            </p>
                        </div>
                    </div>
                    <!-- Primer Apellido -->
                    <div class="formulario__contenedor-input">
                        <asp:Label ID="lblApellidoUno" runat="server" Text="Primer Apellido:" CssClass="formulario__label">
                            <asp:TextBox ID="txtApellidoUno" runat="server" CssClass="formulario__input" placeholder="Doe"></asp:TextBox>
                        </asp:Label>
                        <%-- Validación del primer apellido --%>
                        <div class="formulario__contenedor-mensajes">
                            <p class="formulario__mensaje">
                                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="Es necesario ingresar su primer apellido." ControlToValidate="txtApellidoUno" Display="Dynamic"></asp:RequiredFieldValidator>
                            </p>
                        </div>
                    </div>
                    <!-- Segundo Apellido (Opcional)-->
                    <div class="formulario__contenedor-input">
                        <asp:Label ID="lblApellidoDos" runat="server" Text="Segundo Apellido:" CssClass="formulario__label">Segundo Apellido: <span class="formulario__opcional" title="Opcional">(?)</span>
                            <asp:TextBox ID="txtApellidoDos" runat="server" CssClass="formulario__input" placeholder="Ingrese el segundo apellido"></asp:TextBox>
                        </asp:Label>
                        <div class="formulario__contenedor-mensajes">
                            <p class="formulario__mensaje"></p>
                        </div>
                    </div>
                    <!-- Correo electrónico -->
                    <div class="formulario__contenedor-input formulario__contenedor-input-sp3">
                        <asp:Label ID="lblCorreoUsuario" runat="server" Text="Correo Electrónico:" CssClass="formulario__label">
                            <asp:TextBox ID="txtCorreoUsuario" runat="server" CssClass="formulario__input" TextMode="Email" placeholder="correo@correo.com"></asp:TextBox>
                        </asp:Label>
                        <%-- Validación del correo electrónico --%>
                        <div class="formulario__contenedor-mensajes">
                            <p class="formulario__mensaje">
                                <asp:RequiredFieldValidator ID="rfvCorreoUsuario" runat="server" ErrorMessage="Es necesario ingresar un correo electrónico." ControlToValidate="txtCorreoUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                            </p>
                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="formulario__contenedor">
                <fieldset class="formulario__fieldset formulario__fieldset--g2">
                    <legend>Atributos del usuario:</legend>
                    <div class="formulario__contenedor-input">
                        <!-- Estado del usuario -->
                        <asp:Label ID="lblEstadoUsuario" runat="server" Text="El usuario se encuentra:" CssClass="formulario__label">Estado:
                            <asp:DropDownList ID="ddlEstadoUsuario" runat="server" CssClass="formulario__input">
                                <asp:ListItem Value="" Text="Seleccione una opción" />
                                <asp:ListItem Value="4" Text="Activo" />
                                <asp:ListItem Value="3" Text="Inactivo" />
                            </asp:DropDownList>
                        </asp:Label>
                        <%-- Validación del estado del usuario --%>
                        <div class="formulario__contenedor-mensajes">
                            <p class="formulario__mensaje">
                                <asp:RequiredFieldValidator ID="rfvEstadoUsuario" runat="server" ErrorMessage="Es necesario seleccionar el estado del usuario." ControlToValidate="ddlEstadoUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                            </p>
                        </div>
                    </div>
                    <!-- Rol del usuario -->
                    <div class="formulario__contenedor-input">
                        <asp:Label ID="lblRol" runat="server" Text="Rol del usuario:" CssClass="formulario__label">Rol en el sistema:
                            <asp:DropDownList ID="ddlRoles" runat="server" CssClass="formulario__input">
                                <%-- Esto realmente debe cargar los roles definidos anteriormente --%>
                                <asp:ListItem Value="" Text="Seleccione una opción" />
                                <asp:ListItem Value="1" Text="Administrador" />
                                <asp:ListItem Value="2" Text="Digitador Facturas" />
                                <asp:ListItem Value="3" Text="Digitador Documentos Pago" />
                                <asp:ListItem Value="4" Text="Asociador Pagos" />
                                <asp:ListItem Value="5" Text="Reportes" />
                            </asp:DropDownList>
                        </asp:Label>
                        <%-- Validación del rol del usuario --%>
                        <div class="formulario__contenedor-mensajes">
                            <p class="formulario__mensaje">
                                <asp:RequiredFieldValidator ID="rfvRolUsuario" runat="server" ErrorMessage="Es necesario seleccionar el rol del usuario." ControlToValidate="ddlRoles" Display="Dynamic"></asp:RequiredFieldValidator>
                            </p>
                        </div>
                    </div>
                </fieldset>
            </div>

            <footer class="formulario__contenedor formulario__footer">
                <button id="btnGuardar" runat="server" class="boton boton__modificar">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-pencil">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                        <path d="M4 20h4l10.5 -10.5a2.828 2.828 0 1 0 -4 -4l-10.5 10.5v4" />
                        <path d="M13.5 6.5l4 4" />
                    </svg>
                    
                </button>

            <asp:Button ID="btnModificarUsuario" runat="server" Text="Modificar Usuario" CssClass="boton boton__modificar" OnClick="btnModificarUsuario_Click" ToolTip="Guardar cambios" />

            </footer>
            <asp:Button ID="btnCerrarModal" runat="server" Text="x" CssClass="boton__cierre-modal" OnClick="btnCerrarModal_Click" ToolTip="Cerrar Modal" />
        </div>
    </div>
</div>

<div class="main__wrapper">
    <main class="page__main page__main--users">
        <section class="contenedor__section--titulo">
            <h1>
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-users">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                    <path d="M5 7a4 4 0 1 0 8 0a4 4 0 1 0 -8 0" />
                    <path d="M3 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
                    <path d="M16 3.13a4 4 0 0 1 0 7.75" />
                    <path d="M21 21v-2a4 4 0 0 0 -3 -3.85" />
                </svg>
                Usuarios registrados
            </h1>
        </section>

        <section class="contenedor__section--subtitulo">
            <div class="contenedor__subtitulo">
                <h2 class="section--subtitulo">Lista de usuarios</h2>
                <p class="section--parrafo">Estos son los usuarios registrados en el sistema</p>
            </div>

            <div class="contenedor__agregar--usuario">
                <a href="#" class="enlace__agregar--usuario">
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-plus">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                            <path d="M16 19h6" />
                            <path d="M19 16v6" />
                            <path d="M6 21v-2a4 4 0 0 1 4 -4h4" />
                        </svg>
                    </span>
                    Agregar
                </a>
            </div>
        </section>

        <section class="contenedor__section--herramientas">
            <div class="contenedor__herramientas">
                <div class="filtros">
                    <fieldset class="fieldset__filtros">
                        <legend class="fieldset__leyenda">                        <span>                           <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-search">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                    <path d="M6 21v-2a4 4 0 0 1 4 -4h1.5" />
                                    <path d="M15 18a3 3 0 1 0 6 0a3 3 0 1 0 -6 0" />
                                    <path d="M20.2 20.2l1.8 1.8" />
                                </svg>
                            </span>
                            Filtrar por:
                        </legend>

                        <div class="contenedor__filtro">
                            <label class="filtro__label" for="txtFiltNombre">
                                Nombre:
                            </label>
                            <asp:TextBox ID="txtFiltNombre" runat="server" CssClass="filtro__input" placeholder="Ingrese dato a buscar"></asp:TextBox>
                        </div>

                        <div class="contenedor__filtro">
                            <label class="filtro__label" for="ddlFiltEstado">
                                Estado:
                            </label>
                            <asp:DropDownList ID="ddlFiltEstado" runat="server" CssClass="filtro__input">
                                <asp:ListItem Value="" Text="Seleccione una opción" />
                                <asp:ListItem Value="4" Text="Activo" />
                                <asp:ListItem Value="3" Text="Inactivo" />
                            </asp:DropDownList>
                        </div>

                        <div class="contenedor__filtro">
                            <label class="filtro__label" for="ddlFiltRoles">
                                Roles:
                            </label>
                            <asp:DropDownList ID="ddlFiltRoles" runat="server" CssClass="filtro__input">
                                <%-- Esto realmente debe cargar los roles definidos anteriormente --%>
                                <asp:ListItem Value="" Text="Seleccione una opción" />
                                <asp:ListItem Value="1" Text="Administrador" />
                                <asp:ListItem Value="2" Text="Digitador Facturas" />
                                <asp:ListItem Value="3" Text="Digitador Documentos Pago" />
                                <asp:ListItem Value="4" Text="Asociador Pagos" />
                                <asp:ListItem Value="5" Text="Reportes" />
                            </asp:DropDownList>                        
                        </div>

                        <div class="contenedor__filtro filtro--restablecer">
                            <asp:Button ID="btnRestFiltros" runat="server" Text="Limpiar Filtros" CssClass="boton boton__refrescar boton--filtro" OnClick="btnRestFiltros_Click" ToolTip="Restablecer Filtros" />                        
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>

        <section class="contenedor__section--tabla">
            <div class="contenedor__tabla">
                <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" DataKeyNames="Usuario" DataSourceID="SqlDataSource2" CssClass="tabla" OnRowDeleting="gvUsuarios_RowDeleting">
                    <%-- OnRowEditing="gvUsuarios_RowEditing" --%>
                    <Columns>
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" ReadOnly="True" SortExpression="Usuario" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" ReadOnly="True" SortExpression="NombreCompleto" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                        <asp:BoundField DataField="Rol" HeaderText="Rol" SortExpression="Rol" />
                        <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ControlStyle-CssClass="boton boton__eliminar" DeleteText="<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='icon icon-tabler icons-tabler-outline icon-tabler-trash'><path stroke='none' d='M0 0h24v24H0z' fill='none'/><path d='M4 7l16 0' /><path d='M10 11l0 6' /><path d='M14 11l0 6' /><path d='M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12' /><path d='M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3' /></svg>"/>
    <%--                    <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-warning boton__modificar--accion" EditText ="<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='icon icon-tabler icons-tabler-outline icon-tabler-pencil'><path stroke='none' d='M0 0h24v24H0z' fill='none'/><path d='M4 20h4l10.5 -10.5a2.828 2.828 0 1 0 -4 -4l-10.5 10.5v4' /><path d='M13.5 6.5l4 4' /></svg>"/>--%>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Sist_Cuentas_x_PagarConnectionString %>" SelectCommand="sp_Consultar_Usuarios_Sistema" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
            </div>
        </section>
    </main>
</div>

</asp:Content>