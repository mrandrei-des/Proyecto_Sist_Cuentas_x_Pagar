<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ListadoDocumentos.aspx.vb" Inherits="Proyecto_Sist_Cuentas_x_Pagar.ListadoDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HojasEstilos" runat="server">
    <link href="styles/styles_listDocumentos.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main__wrapper main__wrapper__flex">
        <main class="page__main page__main__list__docs" >
            <article class="container__resumen__movimientos">
                <div class="resumen__container__titulo">
                    <h2>Resumen de movimientos</h2>
                    <p>Visión general de los documentos incluidos en el sistema</p>
                </div>
                <div class="resumen__container__cards">
                    <div class="resumen__card">
                        <h3 class="resumen__card__title">Total por pagar <span class="card__icon"><i class="fa-solid fa-wallet"></i></span></h3>
                        <span class="card__moneda">Dólares</span>
                        <span class="card__monto" runat="server" id="totalFactUSD"></span>
                        <span class="card__moneda">Colones</span>
                        <span class="card__monto" runat="server" id="totalFactCRC"></span>
                    </div>
                    <div class="resumen__card">
                        <h3 class="resumen__card__title">Total de pagos <span class="card__icon"><i class="fa-solid fa-money-bills"></i></span></h3>
                        <span class="card__moneda">Dólares</span>
                        <span class="card__monto" runat="server" id="totalDocUSD"></span>
                        <span class="card__moneda">Colones</span>
                        <span class="card__monto" runat="server" id="totalDocCRC"></span>                        
                    </div>
                    <div class="resumen__card">
                        <h3 class="resumen__card__title">Balance Dólares <span class="card__icon"><i class="fa-solid fa-scale-unbalanced"></i></span></h3>
                        <p>Aplicado <span><i class="fa-solid fa-circle-check"></i></span></p>

                        <div class="card__container__item">
                            <span class="card__item__text">Facturas</span>
                            <span class="card__item" runat="server" id="balanceFactUSD"></span>
                        </div>

                        <div class="card__container__item">
                            <span class="card__item__text">Pagos</span>
                            <span class="card__item" runat="server" id="balanceDocsUSD"></span> 
                        </div>

                        <div class="card__container__item">
                            <span class="card__item__text item__saldo" runat="server" id="balanceTextoSaldoUSD">Saldo</span>
                            <span class="card__item item__saldo saldo__monto" runat="server" id="balanceSaldoUSD"></span> 
                        </div>
                    </div>
                    <div class="resumen__card">
                        <h3 class="resumen__card__title">Balance Colones <span class="card__icon"><i class="fa-solid fa-scale-unbalanced-flip"></i></span></h3>
                        <p>Aplicado <span><i class="fa-solid fa-circle-check"></i></span></p>

                        <div class="card__container__item">
                            <span class="card__item">Facturas</span>
                            <span class="card__item" runat="server" id="balanceFactCRC"></span>
                        </div>

                        <div class="card__container__item">
                            <span class="card__item">Pagos</span>
                            <span class="card__item" runat="server" id="balanceDocsCRC"></span> 
                        </div>

                        <div class="card__container__item">
                            <span class="card__item item__saldo" runat="server" id="balanceTextoSaldoCRC">Saldo</span>
                            <span class="card__item item__saldo saldo__monto" runat="server" id="balanceSaldoCRC"></span> 
                        </div>                        
                    </div>
                </div>
            </article>

            <section class="container__filtros">
                <div class="resumen__container__titulo">
                    <h2>Búsqueda de movimientos</h2>
                    <p>Filtros para los documentos aplicados</p>
                </div>
                <div class="resumen__container__filtros">

                    <div class="resumen__container__filter">
                        <button type="button" class="filter__button" id="btnFiltroTipoDoc">
                            <span><i class="fa-solid fa-shapes"></i></span> Tipo Documento
                        </button>
                    </div>

                    <div class="resumen__container__filter">
                        <button type="button" class="filter__button" id="btnFiltroMoneda">
                            <span><i class="fa-solid fa-colon-sign"></i></span> Moneda
                        </button>
                    </div>

                    <div class="resumen__container__filter">
                        <button type="button" class="filter__button" id="btnFiltroFechas">
                            <span><i class="fa-regular fa-calendar"></i></span> Rango de Fechas
                        </button>                     
                    </div>

                    <div class="resumen__container__filter">
                        <button type="button" class="restart__filter__button" id="btnFiltroReset">Limpiar Filtros</button>
                    </div>
                </div>

                <div class="resumen__container__filtros__inputs resumen__container__filtros__inputs--hidden" id="contenedorFiltrosInput">
                    <div class="resumen__container__input resumen__container__input--hidden" id="contenedorFiltroTipoDoc">
                        <label for="ddlTipoDocumento" class="formulario__label"><span><i class="fa-solid fa-shapes"></i></span> Tipo Documento</label>
                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="formulario__input">
                        </asp:DropDownList>                        
                    </div>

                    <div class="resumen__container__input resumen__container__input--hidden" id="contenedorFiltroMoneda">
                        <label for="ddlMonedas" class="formulario__label"><span><i class="fa-solid fa-colon-sign"></i></span> Moneda</label>
                        <asp:DropDownList ID="ddlMonedas" runat="server" CssClass="formulario__input">
                        </asp:DropDownList>
                    </div>

                    <div class="resumen__container__input resumen__container__input--hidden" id="contenedorFiltroFecha">
                        <div class="filters__fecha">
                            <div class="container__filter__fecha">
                                <label for="txtFechaInicio" class="formulario__label">Fecha Inicio</label>  
                                <asp:TextBox ID="txtFechaInicio" TextMode="Date" runat="server" CssClass="formulario__input"></asp:TextBox>
                            </div>

                            <div class="container__filter__fecha">
                                <label for="txtFechaFin" class="formulario__label">Fecha Fin</label>
                                <asp:TextBox ID="txtFechaFin" TextMode="Date" runat="server" CssClass="formulario__input"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section class="container__docs">
                <div class="resumen__container__titulo">
                    <h2>Documentos del sistema</h2>
                    <p>Listado de documentos aplicados en el sistema</p>
                </div>
                <div class="resumen__container__listado">
                    <div class="resumen__container__docs" id="resumenContainerDocs">                        
                    </div>

                    <div class="resumen__container__info__doc resumen__container__info__doc__hidden" id="containerInfoDoc">
                        <div class="info__doc__container__numDoc">
                            <p id="docTitle"></p>
                        </div>
                        
                        <div class="info__doc__container__monto">
                            <p>Monto Total</p>
                            <span class="info__doc__monto"><strong id="docMonto"></strong></span>
                        </div>

                        <div class="info__doc__container__details">
                            <div class="container__details">
                                <p class="details__title">Proveedor</p>
                                <p class="details__info" id="docNombreProveedor"></p>
                            </div>
                            <div class="container__details">
                                <p class="details__title">Tipo</p>
                                <p class="details__info" id="docTipoDoc"></p>
                            </div>
                            <div class="container__details">
                                <p class="details__title">Fecha documento</p>
                                <p class="details__info" id="docFecha"></p>
                            </div>                                                        
                            <div class="container__details">
                                <p class="details__title">Moneda</p>
                                <p class="details__info" id="docMoneda"></p>
                            </div>                            
                        </div>
                    </div>

                </div>
            </section>
        </main>
    </div>

    <script src="Scripts/cxp_Scripts/script_ListadoDocumentos.js"></script>
</asp:Content>
