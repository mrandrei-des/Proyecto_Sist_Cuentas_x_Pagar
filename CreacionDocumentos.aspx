<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreacionDocumentos.aspx.vb" EnableEventValidation="false" Inherits="Proyecto_Sist_Cuentas_x_Pagar.CreacionDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- INICIO DEL MODAL --%>
    <div class="contenedor__dialog" id="contenedor__dialogConfirm" runat="server">
        <div class="dialog__container" id="dialogConfirm">
            <div class="dialog__confirm" id="dialog__confirm" >
                <div class="dialog__icon__container">
                    <span><i class="fa-solid fa-exclamation"></i></span>
                </div>
                <div class="dialog__message__container">
                    <p class="dialog__message">
                        Una vez aplicado el documento no podrá hacerle cambios, ¿Desea continuar?
                    </p>
                </div>
                <div class="dialog__buttons__container">
                    <button type="button" ID="btnCancelarAplicacion" class="boton dialog__button--cancel" title="Regresar">
                        <span>No, Cancelar</span>
                        <span class="contenedor__icono">
                            <i class="fa-solid fa-circle-arrow-left"></i>
                        </span>  
                    </button>

                    <asp:LinkButton ID="btnContinuarAplicacion" runat="server" Text="Sí, Continuar" CssClass="boton dialog__button--continue" ToolTip="Aplicar" OnClick="btnContinuarAplicacion_Click" CausesValidation="false">
                        <span>Sí, Continuar</span>
                        <span class="contenedor__icono">
                            <i class="fa-solid fa-circle-check"></i>
                        </span>  
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <%-- FIN DEL MODAL --%>

    <div class="main__wrapper main__wrapper__flex">

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
                    <%--<h2 class="formulario__subtitulo">Formulario Creación Facturas o Documentos Pago</h2>--%>
                    <p class="formulario__parrafo parrafo">Complete cada uno de los campos para registrar un nuevo documento</p>
                </div>

                <div class="contenedor__accion--externa">
<%--                    <a href="#" class="enlace__accion--externa">
                        <span class="span__flotante__accion--externa">Listar</span>
                        <span class="contenedor__icono__accion--externa">
                            <i class="fa-solid fa-file-lines"></i>
                        </span>
                    </a>--%>
                </div>
            </section>

            <section class="contenedor__section--formulario">
                <div class="formulario formulario--gr4" role="form">

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc2">
                            <legend>Documento</legend>

                            <div class="formulario__contenedor-input">
                                <p class="formulario__label">Categoría</p>
                                <div class="formulario__contenedor__categorias">

                                    <asp:LinkButton ID="btnFiltFacturaForm" runat ="server" CssClass="boton boton__opcion" OnClick="btnFiltFacturaForm_Click" ToolTip="Cargar Tipos de Factura" CausesValidation="false">
                                        <span>Factura</span>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnFiltPagoForm" runat ="server" CssClass="boton boton__opcion" OnClick="btnFiltPagoForm_Click" ToolTip="Cargar Tipos de Documento Pago" CausesValidation="false">
                                        <span>Pago</span>
                                    </asp:LinkButton>

                                    <asp:HiddenField ID="hfCategoria" runat="server" />
                                </div>
                            </div>

                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblTipoDocumento" class="formulario__label" for="ddlTipoDocumento" runat="server">
                                    Tipo de Documento
                                </asp:Label>
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="formulario__input" required="true">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfTipoDocumento" runat="server" />
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesTipoDoc">
                                    <p class="formulario__mensaje">
                                        Es necesario seleccionar el tipo de documento.
                                    </p>
                                </div>
                            </div>                            
                        </fieldset>
                    </div>

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc2">
                            <legend>Identificación</legend>

                            <%-- Proveedor --%>
                            <div class="formulario__contenedor-input formulario__contenedor-input-sp2">
                                <asp:Label ID="lblProveedor" runat="server" CssClass="formulario__label" AssociatedControlID="txtProveedor">Proveedor</asp:Label>

                                <asp:TextBox ID="txtProveedor" runat="server" CssClass="formulario__input" placeholder="Escriba nombre o código..." autocomplete="off" required="true"></asp:TextBox>
                                <asp:HiddenField ID="hfNumProveedor" runat="server" />

                                <!-- CONFORME EL USUARIO ESCRIBA, SE AGREGARÁN SUGERENCIAS -->
                                <div class="contenedor__sugerencias" id="ddProveedores">
                                </div>

                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesProveedor">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ErrorMessage="Es necesario seleccionar el proveedor del documento." ControlToValidate="txtProveedor" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Número Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblNumeroDocumento" runat="server" CssClass="formulario__label" AssociatedControlID="txtNumDocumento">Número Documento</asp:Label>
                                <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="formulario__input" placeholder="17056312" required="true"></asp:TextBox>

                                <%-- Validación Número Documento --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesNumDoc">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvNumDocumento" runat="server" ErrorMessage="Es necesario indicar el número del documento." ControlToValidate="txtNumDocumento" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Fecha Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblFechaEmision" runat="server" CssClass="formulario__label" AssociatedControlID="txtFechaEmision">Fecha
                                </asp:Label>
                                <asp:TextBox ID="txtFechaEmision" runat="server" CssClass="formulario__input" TextMode="Date" required="true"></asp:TextBox>
                                <%-- Validación Fecha Documento --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesFecha">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvFechaEmision" runat="server" ErrorMessage="Es necesario seleccionar la fecha de emisión del documento." ControlToValidate="txtFechaEmision" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="formulario__contenedor">
                        <fieldset class="formulario__fieldset formulario__fieldset--gc--aufr">
                            <legend>Valor</legend>

                            <%-- Moneda Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblMoneda" runat="server" CssClass="formulario__label">Moneda
                                    <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="formulario__input" required="true">
                                    </asp:DropDownList>
                                </asp:Label>
                                <%-- Validación Moneda Documento --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesMoneda">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvMoneda" runat="server" ErrorMessage="Es necesario seleccionar la moneda del documento." ControlToValidate="ddlMoneda" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Monto Documento --%>
                            <div class="formulario__contenedor-input">
                                <asp:Label ID="lblMonto" runat="server" CssClass="formulario__label">Monto Total</asp:Label>
                                <asp:TextBox ID="txtMontoTotal" runat="server" CssClass="formulario__input" required="true" placeholder="₡0.00"></asp:TextBox>
                                <%-- Validación Monto Documento --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesMonto">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvMontoTotal" runat="server" ErrorMessage="Es necesario indicar el monto total del documento." ControlToValidate="txtMontoTotal" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>

                            <%-- Observación Documento --%>
                            <div class="formulario__contenedor-input formulario__contenedor-input-sp2">
                                <asp:Label ID="lblObservacion" runat="server" CssClass="formulario__label">Observación
                                </asp:Label>
                                <asp:TextBox ID="txtObservacion" runat="server" class="formulario__input" placeholder="Compra de insumos" required="true"></asp:TextBox>
                                <%-- Validación Observación Documento --%>
                                <div class="formulario__contenedor-mensajes" runat="server" id="contenedorMensajesObservacion">
                                    <p class="formulario__mensaje">
                                        <asp:RequiredFieldValidator ID="rfvObservacion" runat="server" ErrorMessage="Es necesario indicar una observación para el documento." ControlToValidate="txtObservacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                            
                        </fieldset>
                    </div>

                    <footer class="formulario__contenedor formulario__footer">

                        <asp:LinkButton ID="btnGuardar" runat ="server" CssClass="boton boton__guardar" OnClick="btnGuardar_Click" ToolTip="Guardar Borrador de Documento">
                            <span>Guardar</span>
                            <span class="contenedor__icono">
                               <i class="fa-solid fa-floppy-disk"></i>
                            </span>  
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnModificar" runat ="server" CssClass="boton boton__modificar boton__ocultar" OnClick="btnModificar_Click" ToolTip="Modificar Documento">
                            <span>Modificar</span>
                            <span class="contenedor__icono">
                               <i class="fa-solid fa-file-pen"></i>
                            </span>  
                        </asp:LinkButton>

                        <button type="button" class="boton boton__aplicar boton__ocultar" ID="btnAplicar">
                            <span>Aplicar</span>
                            <span class="contenedor__icono">
                               <i class="fa-solid fa-file-circle-check"></i>
                            </span>  
                        </button>

                        <asp:LinkButton ID="btnEliminar" runat ="server" CssClass="boton boton__eliminar boton__ocultar" OnClick="btnEliminar_Click" ToolTip="Eliminar Documento">
                            <span>Eliminar</span>
                            <span class="contenedor__icono">
                               <i class="fa-solid fa-trash"></i>
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

        <aside class="contenedor__aside">
            <section class="aside__contenedor__titulo">
                <h3 class="aside__titulo">Documentos Pendientes de Aplicar</h3>
            </section>
            <section class="aside__contenedor__verTipoDocumento">
                <div class="contenedor__verTipoDocumento">
                    <fieldset class="aside__fieldset">
                        <legend>
                            Quiero ver:                            
                        </legend>                     
                        <button type="button" class="boton boton__opcion" id="btnFacturaFiltPend">Factura</button>
                        <button type="button"class="boton boton__opcion" id="btnPagoFiltPend">Pago</button>
                        <asp:HiddenField ID="hfFiltCategoria" runat="server" />
                    </fieldset>
                </div>
            </section>

            <section class="aside__contenedor__filtros">
                <fieldset class="aside__fieldset aside__fieldset--filtros">
                    <legend>
                        Ordenar documentos por: 
                    </legend>
                    <button type="button" class="boton boton__dis_inline boton__opcion" id="btnFiltPendReciente">Más Recientes</button>
                    <button type="button" class="boton boton__dis_inline boton__opcion" id="btnFiltPendAntiguo">Más Antiguas</button>
                    <button type="button" class="boton boton__dis_inline boton__opcion boton__opcion--active" id="btnFiltPendTodo">Todos</button>                    
                </fieldset>                
            </section>

            <section class="aside__contenedor__pendientes" id="contenedorPendientes">
                <%-- Estos item se generar por JavaScript --%>
            </section>
        </aside>
    </div>

    <script src="Scripts/cxp_Scripts/script_CreacionDocumentos.js"></script>
</asp:Content>
