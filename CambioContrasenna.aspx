<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CambioContrasenna.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.CambioContrasenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main__wrapper">
        <main class="page__main page__main--login" runat="server" id="divMain">
            <section class="contenedor__section--titulo">
                <h1 class="titulo">
                    <span class="contenedor__icono--titulo">
                        <i class="fa-solid fa-key"></i>
                    </span>
                    <span>Cambio de Contraseña
                    </span>
                </h1>
            </section>
            <section class="contenedor__section--subtitulo">
                <div class="contenedor__subtitulo">
                    <h2 class="formulario__subtitulo">Formulario Cambio Contraseña</h2>
                    <p class="formulario__parrafo parrafo">Complete cada uno de los campos para realizar el cambio de contraseña</p>
                </div>
            </section>

            <section class="contenedor__section--formulario">
                <div class="formulario formulario--gr2" role="form">
                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc1">
                            <legend>Datos para acceso:</legend>
                            <!-- Contraseña Nueva-->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblContrasenna" runat="server" Text="Contraseña Nueva:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtContrasenna" runat="server" CssClass="formulario__input" TextMode="Password" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación de la nueva contraseña --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvContraseña" runat="server" ErrorMessage="Es necesario ingresar la contraseña nueva" ControlToValidate="txtContrasenna" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <!-- Confirmar Contraseña -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblConfirmarContrasenna" runat="server" Text="Confirmar Contraseña:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtConfirmarContrasenna" runat="server" CssClass="formulario__input" TextMode="Password" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación de Confirmar Contraseña --%>
                                <div class="formulario__contenedor-mensajes" id="contenedorMensajesConfirmContrasenna" runat="server">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvConfirmarContrasenna" runat="server" ErrorMessage="Es necesario ingresar nuevamente la contraseña" ControlToValidate="txtConfirmarContrasenna" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <footer class="formulario__contenedor formulario__footer">
                        <asp:Button ID="btnCambioContrasenna" runat="server" Text="Guardar Cambios" CssClass="boton boton__guardar" OnClick="btnCambioContrasenna_Click" />
                    </footer>
                </div>
            </section>
        </main>
    </div>
    <script src="Scripts/cxp_Scripts/cambioContrasenna.js"></script>
</asp:Content>
