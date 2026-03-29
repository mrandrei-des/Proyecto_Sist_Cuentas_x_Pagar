<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.Login" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Iniciar Sesión</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="~/styles/styles_reset_elementos.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/fonts.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_colores.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_botones.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_icons.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_textos.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_layout.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_header.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_main.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_footer.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_login.css" rel="stylesheet" type="text/css" /> 
    <link href="~/styles/styles_registro.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/styles_formulario.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main__wrapper">
            <main class="page__main page__main--login" runat="server" id="divMain">
                <section class="contenedor__section--titulo">
                    <h1 class="titulo">
                        <span class="contenedor__icono--titulo">
                            <i class="fa-solid fa-key"></i>
                        </span>
                        <span>Inicio de Sesión
                        </span>
                    </h1>
                </section>
                <section class="contenedor__section--subtitulo">
                    <div class="contenedor__subtitulo">
                        <p class="formulario__parrafo parrafo"> </p>
                    </div>
                </section>

                <section class="contenedor__section--formulario">
                    <div class="formulario formulario--gr2" role="form">
                        <div class="formulario__contenedor">
                            <fieldset class="formulario__fieldset formulario__fieldset--gc1">
                                <legend>Datos para acceso:</legend>
                                <!-- Usuario -->
                                <div class="formulario__contenedor-input">
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="formulario__label" AssociatedControlID="txtUsuario">
                                    </asp:Label>
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="formulario__input" required="true" placeholder="Ingrese su usuario"></asp:TextBox>
                                    <%-- Validación del usuario --%>
                                    <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesUsuario">
                                    </div>
                                </div>

                                <!-- Contraseña -->
                                <div class="formulario__contenedor-input">
                                    <asp:Label ID="lblContrasenna" runat="server" Text="Contraseña" CssClass="formulario__label" AssociatedControlID="txtContrasenna">
                                    </asp:Label>
                                    <asp:TextBox ID="txtContrasenna" runat="server" CssClass="formulario__input" TextMode="Password" required="true" placeholder="Ingrese su contraseña"></asp:TextBox>
                                    <%-- Validación de la Contraseña --%>
                                    <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesContrasenna">
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                        <footer class="formulario__contenedor formulario__footer">
                            <asp:Button ID="btnInicioSesion" runat="server" Text="Ingresar" CssClass="boton boton__guardar" OnClick="btnInicioSesion_Click" />
                        </footer>
                    </div>
                </section>
            </main>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        <script src="Scripts/cxp_Scripts/script_Login.js"></script>
    </form>
</body>
</html>
