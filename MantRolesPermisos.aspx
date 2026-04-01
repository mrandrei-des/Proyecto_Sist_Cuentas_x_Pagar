<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MantRolesPermisos.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.MantRolesPermisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main__wrapper main__wrapper__flex">        
        <main class="page__main page__config">
            <section class="contenedor__section--create">
                <div class="section__config--titulo">
                    <h2 class="formulario__subtitulo">
                        <span>
                            <i class="fa-solid fa-key"></i>
                        </span>
                        Creación de Roles
                    </h2>
                </div>
                <div class="contenedor__create__list">
                    <div class="create__list__select">

                        <asp:Label ID="lblRolesCreados" runat="server" CssClass="formulario__label" Text="Rol" AssociatedControlID="ddlRolesCreados"></asp:Label>
                        <asp:DropDownList ID="ddlRolesCreados" runat="server" CssClass="formulario__input" AutoPostBack="true" OnSelectedIndexChanged="ddlRolesCreados_SelectedIndexChanged">                                                        
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdfRolSeleccionado" runat="server" />
                    </div>
                    <div class="create__list__new">
                        <asp:LinkButton ID="btnNuevo" runat ="server" CssClass="boton boton__refrescar" OnClick="btnNuevo_Click" CausesValidation="false" ToolTip="Nuevo Rol">
                            <span>Nuevo</span>
                            <span><i class="fa-solid fa-plus"></i></span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="contenedor__create__new">
                    <div class="create__new__input">
                        <%--Validar que si se carga en el input ocultar botón de guardar si es nuevo mantener el botón--%>
                        <asp:Label ID="lblNuevoRol" runat="server" CssClass="formulario__label" Text="Nombre Rol" AssociatedControlID="txtNombreRol"></asp:Label>
                        <asp:TextBox ID="txtNombreRol" runat="server" CssClass="formulario__input" placeholder="Ingrese el nombre del Rol"></asp:TextBox>
                        <asp:HiddenField ID="hfIDRolSelected" runat="server" />
                        <div class="formulario__contenedor-mensajes" id="contenedorMensajesNombreRolNuevo" runat="server">
                        </div>
                    </div>
                    <div class="create__list__toolbox">
                        <asp:LinkButton ID="btnAgregar" runat ="server" CssClass="boton boton__guardar boton__ocultar" OnClick="btnAgregar_Click" ToolTip="Agregar Rol">
                            <span>Agregar</span>
                            <span><i class="fa-solid fa-plus"></i></span>
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnModificar" runat ="server" CssClass="boton boton__modificar boton__ocultar" OnClick="btnModificar_Click" ToolTip="Modificar Rol">
                            <span>Modificar</span>
                            <span class="contenedor__icono">
                               <i class="fa-solid fa-file-pen"></i>
                            </span>  
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnEliminar" runat ="server" CssClass="boton boton__eliminar boton__ocultar" OnClick="btnEliminar_Click" ToolTip="Eliminar Rol">
                            <span>Eliminar</span>
                            <span class="contenedor__icono">
                               <i class="fa-solid fa-trash"></i>
                            </span>  
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnLimpiar" runat ="server" CssClass="boton boton__refrescar" OnClick="btnLimpiar_Click" CausesValidation="false" ToolTip="Limpiar campos">
                            <span>Limpiar</span>
                            <span class="contenedor__icono">
                                <i class="fa-solid fa-eraser"></i>                               
                            </span>  
                        </asp:LinkButton>
                    </div>
                </div>
            </section>

            <section class="contenedor__section--config">
                <div class="section__config--titulo">
                    <h2 class="formulario__subtitulo">
                        <span>
                            <i class="fa-solid fa-gear"></i>
                        </span>
                        Configuración de Permisos
                    </h2>
                </div>
                <div class="contenedor__config__permisos">
                    <div class="config__permisos__roles">
                        <ul class="roles__list" id="rolList">
                            <%--Estas opciones se cargan de la base de datos, son los grupos--%>
                            <li id="rolItem">
                                <p class="rol rol__active" id="rolRegistrado">Usuarios</p>
                            </li>
                        </ul>
                    </div>

                    <%--Estos son los permisos, y se cargan de la base de datos, permisos--%>
                    <div class="config__contenedor__permisos" id="contenedorPermisos">
                    </div>
                </div>
                <div class="contenedor__config__acciones">
                    <asp:LinkButton ID="btnGuardarCambios" runat ="server" CssClass="boton boton__guardar" ToolTip="Guardar cambios Rol" CausesValidation="false">
                        <span>Guardar cambios</span>
                        <span class="contenedor__icono">
                            <i class="fa-solid fa-lock"></i>
                        </span>  
                    </asp:LinkButton>
                </div>
            </section>
        </main>
    </div>
    <script src="Scripts/cxp_Scripts/script_MantRolesPermisos.js"></script>
</asp:Content>