<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="index.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main__wrapper main__wrapper__flex">
        <main class="page__main page__home">
            <article class="contenedor__saludo">
                <div class="saludo__contenedor__usuario">
                    <h2 runat="server" id="titleUser"></h2>
                    <p class="usuario__fecha" runat="server" id="fechaActual"></p>
                </div>
                <div class="saludo__contenedor__sistema">
                    <p class="sistema__nombre">Sistema de Cuentas por Pagar</p>
                </div>
            </article>

            <article class="contenedor__metricas">
                <div class="metricas__box">
                    <p class="box__leyenda">Documentos ingresados</p>
                    <span class="box__contador" runat="server" id="documentosIngresados"></span>
                    <p class="box__leyenda">Hoy</p>
                </div>
                <div class="metricas__box">
                    <p class="box__leyenda">Proveedores registrados</p>
                    <span class="box__contador" runat="server" id="proveedoresRegistrados"></span>
                    <p class="box__leyenda">Esta semana</p>
                </div>
                <div class="metricas__box">
                    <p class="box__leyenda">Facturas pendientes</p>
                    <span class="box__contador" runat="server" id="facturasPendientes"></span>
                    <p class="box__leyenda">Sin aplicar</p>
                </div>
                <div class="metricas__box">
                    <p class="box__leyenda">Pagos pendientes</p>
                    <span class="box__contador" runat="server" id="pagosPendientes"></span>
                    <p class="box__leyenda">Sin aplicar</p>
                </div>
                <div class="metricas__box">
                    <p class="box__leyenda">Aplicados este mes</p>
                    <span class="box__contador" runat="server" id="aplicadosEsteMes"></span>
                    <p class="box__leyenda" runat="server" id="mesActual"></p>
                </div>
            </article>

            <article class="contenedor__movimientos">
                <div class="movimientos__pendientes">
                    <div class="movimientos__pendientes__titulo">
                        <h2>
                            <span><i class="fa-solid fa-file-circle-question"></i></span>
                            Pendientes más antiguos
                        </h2>
                        <a href="CreacionDocumentos" class="pendientes__enlace">Ver todos
                            <span>
                                <i class="fa-solid fa-arrow-right"></i>
                            </span>
                        </a>
                    </div>
                    <div class="movimientos__pendientes__listado" id="listadoDocumentosPendientes">

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias doc__dias--Antiguo">45 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias doc__dias--Intermedio">25 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias doc__dias--Reciente">7 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias">45 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias">45 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias">45 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias">45 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias">45 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias">45 días</span>
                            </div>
                        </div>

                        <div class="pendiente__doc">
                            <div class="doc__info">
                                <span>FAC-001</span>
                                <span>$120,00</span>
                            </div>
                            <div class="doc__registro">
                                <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                <span class="doc__dias">45 días</span>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="movimientos__complementos">
                    <div class="complementos__accesos">
                        <div class="complementos__accesos__titulo">
                            <h2>
                                <span><i class="fa-solid fa-paperclip"></i></span>
                                Accesos rápidos
                            </h2>
                        </div>
                        <div class="complementos__accesos__enlaces">
                            <div class="enlace__box">
                                <h3>
                                    <a href="CreacionDocumentos" class="enlace__box__link">Nueva factura
                                        <span>
                                            <i class="fa-solid fa-arrow-right"></i>
                                        </span>
                                    </a>
                                </h3>
                                <p class="enlace__box__descripcion">Registrar facturas de compras/servicios</p>
                            </div>
                            <div class="enlace__box">
                                <h3>
                                    <a href="CreacionDocumentos" class="enlace__box__link">Nuevo pago
                                        <span>
                                            <i class="fa-solid fa-arrow-right"></i>
                                        </span>
                                    </a>
                                </h3>
                                <p class="enlace__box__descripcion">Registrar documentos de pagos</p>
                            </div>
                            <div class="enlace__box">
                                <h3>
                                    <a href="ListadoDocumentos" class="enlace__box__link">Ver aplicados
                                        <span>
                                            <i class="fa-solid fa-arrow-right"></i>
                                        </span>
                                    </a>
                                </h3>
                                <p class="enlace__box__descripcion">Consultar listado</p>
                            </div>
                        </div>
                    </div>
                    <div class="complementos__actividad">
                        <div class="complementos__actividad__titulo">
                            <h2>
                                <span><i class="fa-regular fa-clock"></i></span>
                                Actividad reciente documentos
                            </h2>
                        </div>                        
                        <div class="complementos__actividad__listado" id="listadoActividadReciente">
                        </div>
                    </div>
                </div>
            </article>
        </main>
    </div>
    <script src="Scripts/cxp_Scripts/script_Index.js"></script>
</asp:Content>