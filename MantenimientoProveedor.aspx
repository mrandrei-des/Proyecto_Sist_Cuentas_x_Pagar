<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MantenimientoProveedor.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.MantenimientoProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main__wrapper">
        <main class="page__main page__main--form" runat="server" id="divMain">
            <section class="contenedor__section--titulo">
                <h1>
                    <span class="fondo__icono">
                        
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-cog">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                            <path d="M6 21v-2a4 4 0 0 1 4 -4h2.5" />
                            <path d="M17.001 19a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
                            <path d="M19.001 15.5v1.5" />
                            <path d="M19.001 21v1.5" />
                            <path d="M22.032 17.25l-1.299 .75" />
                            <path d="M17.27 20l-1.3 .75" />
                            <path d="M15.97 17.25l1.3 .75" />
                            <path d="M20.733 20l1.3 .75" />
                        </svg>

                    </span>
                    Mantenimiento
                </h1>
            </section>
            <section class="contenedor__section--subtitulo">
                <div class="contenedor__subtitulo">
                    <h2 class="formulario__subtitulo">Formulario Proveedores</h2>
                    <p class="formulario__parrafo parrafo">Complete cada uno de los campos para registrar un nuevo proveedor</p>
                </div>
                <div class="contenedor__ver--usuario">
                    <a href="#" class="enlace__ver--usuario" title="Ver proveedores registrados">
                        <span>Listar</span>
                        
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-users">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M5 7a4 4 0 1 0 8 0a4 4 0 1 0 -8 0" />
                            <path d="M3 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
                            <path d="M16 3.13a4 4 0 0 1 0 7.75" />
                            <path d="M21 21v-2a4 4 0 0 0 -3 -3.85" />
                        </svg>

                    </a>
                </div>
            </section>

            <section class="contenedor__section--formulario">
                <div class="formulario formulario--gr3" role="form">
                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc2">
                            <legend>Datos del proveedor:</legend>
                            <!-- Nombre Completo-->
                            <div class="formulario__contenedor-input formulario__contenedor-input-sp2">
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

                            <!-- Tipo Identificación -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo Identificación:" CssClass="formulario__label">Tipo Identificación:
                                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="formulario__input" required="true">
                                        <%-- Esto realmente debe cargar los tipos de identificación definidos anteriormente --%>
                                        <asp:ListItem Value="" Text="Seleccione una opción" />
                                        <asp:ListItem Value="1" Text="Cédula Física" />
                                        <asp:ListItem Value="2" Text="Cédula Jurídica" />
                                        <asp:ListItem Value="3" Text="Pasaporte" />
                                        <asp:ListItem Value="4" Text="DIMEX" />
                                    </asp:DropDownList>
                                </asp:Label>
                                <%-- Validación del tipo de identificación --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvTipoIdentificacion" runat="server" ErrorMessage="Es necesario seleccionar el tipo de identificación del proveedor." ControlToValidate="ddlTipoIdentificacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <!-- Identificación -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="formulario__input" placeholder="101110111" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación de la Identificación --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ErrorMessage="Es necesario ingresar la identificación del proveedor." ControlToValidate="txtIdentificacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <!-- Correo electrónico -->
                            <div class="formulario__contenedor-input formulario__contenedor-input-sp2">
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
                        <fieldset class="formulario__fieldset">
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
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Proveedor" CssClass="boton boton__guardar" OnClick="btnGuardar_Click" />
                    </footer>

                </div>
            </section>
        </main>
    </div>
</asp:Content>