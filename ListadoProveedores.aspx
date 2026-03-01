<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ListadoProveedores.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.ListadoProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- INICIO DEL MODAL --%>
    <div class="dialog" runat="server" id="modalModify" role="dialog">
        <div class="contenedor__modal contenedor__modal--modify">
            <div class="contenedor__modal--titulo">
                <h2 class="modal__titulo">
                    <span class="contenedor__icono--subtitulo">
                        <i class="fa-solid fa-building-circle-exclamation"></i>
                    </span>
                    <span>Modificación de Proveedores
                    </span>
                </h2>
                <p class="modal__subtitulo" id="pSubtituloModal" runat="server"></p>
            </div>

            <div class="modal__formulario">

                <div class="formulario__contenedor">
                    <fieldset class="formulario__fieldset formulario__fieldset--gc1">
                        <legend>Datos del proveedor:</legend>
                        <!-- Nombre Completo-->
                        <div class="formulario__contenedor-input">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre Completo:" CssClass="formulario__label">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="formulario__input" placeholder="John Doe" required="true"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación del nombre --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Es necesario ingresar el nombre completo del proveedor." ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>

                        <!-- Correo electrónico -->
                        <div class="formulario__contenedor-input">
                            <asp:Label ID="lblCorreo" runat="server" Text="Correo Electrónico:" CssClass="formulario__label">
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="formulario__input" TextMode="Email" placeholder="correo@correo.com" required="true"></asp:TextBox>
                            </asp:Label>
                            <%-- Validación del correo electrónico --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ErrorMessage="Es necesario ingresar un correo electrónico." ControlToValidate="txtCorreo" Display="Dynamic"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>
                    </fieldset>
                </div>

                <div class="formulario__contenedor">
                    <fieldset class="formulario__fieldset formulario__fieldset--gc1">
                        <legend>Atributos del proveedor:</legend>
                        <div class="formulario__contenedor-input">
                            <!-- Estado del proveedor -->
                            <asp:Label ID="lblEstado" runat="server" Text="El proveedor se encuentra:" CssClass="formulario__label">Estado:
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="formulario__input" required="true">
                                        <asp:ListItem Value="" Text="Seleccione una opción" />
                                        <asp:ListItem Value="4" Text="Activo" />
                                        <asp:ListItem Value="3" Text="Inactivo" />
                                    </asp:DropDownList>
                            </asp:Label>
                            <%-- Validación del estado del proveedor --%>
                            <div class="formulario__contenedor-mensajes">
                                <p class="formulario__mensaje">
                                    <asp:RequiredFieldValidator ID="rfvEstadoUsuario" runat="server" ErrorMessage="Es necesario seleccionar el estado del proveedor." ControlToValidate="ddlEstado" Display="Dynamic"></asp:RequiredFieldValidator>
                                </p>
                            </div>
                        </div>
                    </fieldset>
                </div>


                <footer class="formulario__contenedor formulario__footer">
                    <%-- FALTA BUSCAR LA FORMA DE AGREGAR UN ÍCONO A LOS BUTTONS --%>
                    <asp:Button ID="btnModificarProveedor" runat="server" Text="Modificar Proveedor" CssClass="boton boton__modificar" OnClick="btnModificarProveedor_Click" ToolTip="Guardar cambios" />
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
                        <i class="fa-solid fa-building-circle-check"></i>
                    </span>
                    <span>Proveedores registrados
                    </span>
                </h1>
            </section>

            <section class="contenedor__section--subtitulo">
                <div class="contenedor__subtitulo">
                    <h2 class="section--subtitulo">Lista de proveedores</h2>
                    <p class="section--parrafo">Estos son los proveedores registrados en el sistema</p>
                </div>

                <div class="contenedor__accion--externa">
                    <a href="MantenimientoProveedor" class="enlace__accion--externa">
                        <span class="span__flotante__accion--externa">Agregar</span>
                        <span class="contenedor__icono__accion--externa">
                            <i class="fa-solid fa-building"></i>
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
                                <label class="filtro__label" for="ddlFiltTipoIdentificacion">
                                    Tipo Identificación:
                                </label>
                                <asp:DropDownList ID="ddlFiltTipoIdentificacion" runat="server" CssClass="filtro__input">
                                    <%-- Esto realmente debe cargar los tipos de identificación definidos anteriormente --%>
                                    <asp:ListItem Value="" Text="Seleccione una opción" />
                                    <asp:ListItem Value="1" Text="Cédula Física" />
                                    <asp:ListItem Value="2" Text="Cédula Jurídica" />
                                    <asp:ListItem Value="3" Text="Pasaporte" />
                                    <asp:ListItem Value="4" Text="DIMEX" />
                                </asp:DropDownList>
                            </div>

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
                 <asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="False" DataKeyNames="ID Proveedor" DataSourceID="SqlDataSource1" CssClass="tabla" OnRowCommand="gvProveedores_RowCommand">
                     <Columns>
                         <asp:BoundField DataField="ID Proveedor" HeaderText="Proveedor" InsertVisible="False" ReadOnly="True" SortExpression="ID Proveedor" />
                         <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                         <asp:BoundField DataField="Identificación" HeaderText="Identificación" SortExpression="Identificación" />
                         <asp:BoundField DataField="Correo Electrónico" HeaderText="Correo Electrónico" SortExpression="Correo Electrónico" />
                         <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />

                         <asp:TemplateField HeaderText="Acciones">
                             <ItemTemplate>
                                 <asp:LinkButton ID="btnEliminar" runat="server" CssClass="boton boton__eliminar" CommandName="EliminarProveedor" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false" ToolTip="Eliminar Proveedor">
                                     <span class="contenedor__icono">                                        
                                         <i class="fa-solid fa-trash"></i>
                                     </span>
                                 </asp:LinkButton>

                                 <asp:LinkButton ID="btnEditar" runat="server" CssClass="boton boton__modificar--accion" CommandName="EditarProveedor" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false" ToolTip="Editar Proveedor">
                                     <span class="contenedor__icono">                                        
                                         <i class="fa-solid fa-pen-to-square"></i>
                                     </span>
                                 </asp:LinkButton>
                             </ItemTemplate>
                         </asp:TemplateField>

                     </Columns>

                 </asp:GridView>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Sist_Cuentas_x_PagarConnectionString %>" SelectCommand="sp_Consultar_Proveedores_Registrados" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
             </div>
         </section>
        </main>
    </div>
</asp:Content>
