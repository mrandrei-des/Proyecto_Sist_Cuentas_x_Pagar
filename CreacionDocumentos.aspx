<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreacionDocumentos.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.CreacionDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main__wrapper">
        <main class="page__main page__main--registro" runat="server">

            <section class="contenedor__section--titulo">
                <h1 class="titulo">
                    <span class="contenedor__icono--titulo">
                        <i class="fa-solid fa-file-circle-plus"></i>
                    </span>
                    <span>Crear Documento
                    </span>
                </h1>
            </section>

            <section class="contenedor__section--subtitulo">
                <div class="contenedor__subtitulo">
                    <h2 class="formulario__subtitulo">Formulario Creación Facturas o Documentos Pago</h2>
                    <p class="formulario__parrafo parrafo">Complete cada uno de los campos para registrar un nuevo documento</p>
                </div>

                <div class="contenedor__accion--externa">
                    <a href="#" class="enlace__accion--externa">
                        <span class="span__flotante__accion--externa">Listar</span>
                        <span class="contenedor__icono__accion--externa">
                            <i class="fa-solid fa-file-invoice-dollar"></i>
                        </span>
                    </a>
                </div>

            </section>

            <section class="contenedor__section--formulario">
                <div class="formulario formulario--gr4" role="form">

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc1">
                            <legend>Categoría Documento:</legend>

                            <!-- Factura/Documento Pago -->
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoría:" CssClass="formulario__label">Categoría:
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="formulario__input" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </asp:Label>
                            </div>
                        </fieldset>
                    </div>

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc2">
                            <legend>Datos Proveedor:</legend>

                            <%-- Buscar Proveedor --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblFiltProveedor" runat="server" Text="Buscar Nombre:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtFiltProveedor" runat="server" CssClass="formulario__input" placeholder="Los Patitos SA"></asp:TextBox>
                                </asp:Label>
                            </div>

                            <%-- Proveedor --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblProveedor" runat="server" Text="Proveedor:" CssClass="formulario__label">Proveedor:
                                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="formulario__input" required="true">
                                </asp:DropDownList>
                                </asp:Label>
                                <%-- Validación del tipo de identificación --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ErrorMessage="Es necesario seleccionar el proveedor del documento." ControlToValidate="ddlProveedor" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc2 formulario--gr4">
                            <legend>Datos Documento:</legend>

                            <%-- Tipo Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Documento:" CssClass="formulario__label">Tipo Documento:
                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="formulario__input" required="true">
                                    </asp:DropDownList>
                                </asp:Label>
                                <%-- Validación del tipo de documento --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvTipoDocumento" runat="server" ErrorMessage="Es necesario seleccionar el tipo de documento." ControlToValidate="ddlTipoDocumento" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Número Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblNumeroDocumento" runat="server" Text="Número Documento:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="formulario__input" placeholder="17056312" required="true"></asp:TextBox>
                                </asp:Label>

                                <%-- Validación Número Documento --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvNumDocumento" runat="server" ErrorMessage="Es necesario indicar el número del documento." ControlToValidate="txtNumDocumento" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Fecha Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblFechaEmision" runat="server" Text="Fecha Emisión:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtFechaEmision" runat="server" CssClass="formulario__input" TextMode="Date" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación Fecha Documento --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvFechaEmision" runat="server" ErrorMessage="Es necesario seleccionar la fecha de emisión del documento." ControlToValidate="txtFechaEmision" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Estado Documento --%>
                            <div class="formulario__contenedor-input formulario__contenedor-input__toggle">
                                <asp:Label ID="lblEstado" runat="server" Text="Estado Documento:" CssClass="formulario__label label__toggle">
                                    <span>Estado: </span>
                                    <div class="contenedor__toggleButton">
                                        <div class="contenedor__barra"></div>
                                        <asp:LinkButton runat="server" class="contenedor__btnCircle" id="btnChkInput" name="btnChkInput" CausesValidation="false">
                                            <asp:CheckBox runat="server" CssClass="contenedor__txtCheck" ID="chkInput"/>
                                        </asp:LinkButton>
                                    </div>
                                </asp:Label>
                            </div>

                            <%-- Observacion Documento --%>
                            <div class="formulario__contenedor-input formulario__contenedor-input-sp2">
                                <asp:Label ID="lblObservacion" runat="server" Text="Observación:" CssClass="formulario__label">
                                    <textarea id="txtAreaObservacion" runat="server" class="formulario__input formulario__textarea" placeholder="Compra de insumos" required></textarea>
                                </asp:Label>
                                <%-- Validación Observación Documento --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvObservacion" runat="server" ErrorMessage="Es necesario indicar una observación para el documento." ControlToValidate="txtAreaObservacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Moneda Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblMoneda" runat="server" Text="Moneda:" CssClass="formulario__label">Moneda:
                                    <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="formulario__input" required="true">
                                    </asp:DropDownList>
                                </asp:Label>
                                <%-- Validación Moneda Documento --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvMoneda" runat="server" ErrorMessage="Es necesario seleccionar la moneda del documento." ControlToValidate="ddlMoneda" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Monto Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblMonto" runat="server" Text="Monto Total:" CssClass="formulario__label">
                                    <asp:TextBox ID="txtMontoTotal" runat="server" CssClass="formulario__input" required="true"></asp:TextBox>
                                </asp:Label>
                                <%-- Validación Monto Documento --%>
                                <div class="formulario__contenedor-mensajes">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvMontoTotal" runat="server" ErrorMessage="Es necesario indicar el monto total del documento." ControlToValidate="txtMontoTotal" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                        </fieldset>
                    </div>

                    <footer class="formulario__contenedor formulario__footer">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Documento" CssClass="boton boton__guardar" OnClick="btnGuardar_Click" />
                    </footer>
                </div>
            </section>
        </main>
    </div>
    <script src="Scripts/cxp_Scripts/toggleButton.js"></script>
</asp:Content>
