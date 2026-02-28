<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ListadoUsuarios.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- INICIO DEL MODAL --%>
    <div class="dialog" runat="server" id="modalModify" role="dialog">
        <div class="contenedor__modal contenedor__modal--modify">
            <div class="contenedor__modal--titulo">
                <h2 class="modal__titulo">
                    <span class="contenedor__icono--subtitulo">
                        <i class="fa-solid fa-user-gear"></i>
                    </span>
                    <span>Modificación de Usuarios
                    </span>
                </h2>
                <p class="modal__subtitulo" id="pSubtituloModal" runat="server"></p>
            </div>

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
                    <%-- FALTA BUSCAR LA FORMA DE AGREGAR UN ÍCONO A LOS BUTTONS --%>
                    <asp:Button ID="btnModificarUsuario" runat="server" Text="Modificar Usuario" CssClass="boton boton__modificar" OnClick="btnModificarUsuario_Click" ToolTip="Guardar cambios" />
                </footer>
                <asp:Button ID="btnCerrarModal" runat="server" Text="x" CssClass="boton__cierre-modal" OnClick="btnCerrarModal_Click" ToolTip="Cerrar Modal" CausesValidation="false" />
            </div>
        </div>
    </div>
    <%-- FIN DEL MODAL --%>

    <div class="main__wrapper">
        <main class="page__main page__main--users">
            <section class="contenedor__section--titulo">
                <h1 class="titulo">
                    <span class="contenedor__icono--titulo">
                        <i class="fa-solid fa-users"></i>
                    </span>
                    <span>Usuarios registrados
                    </span>
                </h1>
            </section>

            <section class="contenedor__section--subtitulo">
                <div class="contenedor__subtitulo">
                    <h2 class="section--subtitulo">Lista de usuarios</h2>
                    <p class="section--parrafo">Estos son los usuarios registrados en el sistema</p>
                </div>

                <div class="contenedor__accion--externa">
                    <a href="MantenimientoUsuario" class="enlace__accion--externa">
                        <span class="span__flotante__accion--externa">Agregar</span>
                        <span class="contenedor__icono__accion--externa">
                            <i class="fa-solid fa-user-pen"></i>
                        </span>
                    </a>
                </div>
            </section>

            <section class="contenedor__section--herramientas">
                <div class="contenedor__herramientas">
                    <div class="filtros">
                        <fieldset class="fieldset__filtros">
                            <legend class="fieldset__leyenda">
                                <span class="contenedor__icono">
                                    <i class="fa-solid fa-filter"></i>

                                </span>
                                Filtrar por :
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
                                <asp:LinkButton ID="btnLimpiarFiltros" CssClass="boton boton__refrescar boton--filtro" ToolTip="Restablecer Filtros" runat="server" OnClick="btnLimpiarFiltros_Click" CausesValidation="false">
                                <span class="contenedor__icono">
                                    <i class="fa-solid fa-arrows-rotate"></i>
                                </span>
                                </asp:LinkButton>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </section>

            <section class="contenedor__section--tabla">
                <div class="contenedor__tabla">
                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" DataKeyNames="Usuario" DataSourceID="SqlDataSource2" CssClass="tabla" OnRowCommand="gvUsuarios_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" ReadOnly="True" SortExpression="Usuario" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" ReadOnly="True" SortExpression="NombreCompleto" />
                            <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                            <asp:BoundField DataField="Rol" HeaderText="Rol" SortExpression="Rol" />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CssClass="boton boton__eliminar" CommandName="EliminarUsuario" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false" ToolTip="Eliminar Usuario">
                                        <span class="contenedor__icono">                                        
                                            <i class="fa-solid fa-trash"></i>
                                        </span>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnEditar" runat="server" CssClass="boton boton__modificar--accion" CommandName="EditarUsuario" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false" ToolTip="Editar Usuario">
                                        <span class="contenedor__icono">                                        
                                            <i class="fa-solid fa-pen-to-square"></i>
                                        </span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Sist_Cuentas_x_PagarConnectionString %>" SelectCommand="sp_Consultar_Usuarios_Sistema" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                </div>
            </section>
        </main>
    </div>
</asp:Content>
