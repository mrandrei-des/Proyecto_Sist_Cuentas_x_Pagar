const API_ENDPOINT = 'http://localhost:59338/WebMethods_CxP/'

const botonFiltroTipoDoc = document.getElementById('btnFiltroTipoDoc')
const botonFiltroMoneda = document.getElementById('btnFiltroMoneda')
const botonFiltroFechas = document.getElementById('btnFiltroFechas')
const botonFiltroReset = document.getElementById('btnFiltroReset')

botonFiltroTipoDoc.addEventListener('click', () => {
    document.getElementById('contenedorFiltrosInput').classList.remove('resumen__container__filtros__inputs--hidden')

    document.getElementById('contenedorFiltroTipoDoc').classList.remove('resumen__container__input--hidden')
});

botonFiltroMoneda.addEventListener('click', () => {
    document.getElementById('contenedorFiltrosInput').classList.remove('resumen__container__filtros__inputs--hidden')

    document.getElementById('contenedorFiltroMoneda').classList.remove('resumen__container__input--hidden')
});

botonFiltroFechas.addEventListener('click', () => {
    document.getElementById('contenedorFiltrosInput').classList.remove('resumen__container__filtros__inputs--hidden')

    document.getElementById('contenedorFiltroFecha').classList.remove('resumen__container__input--hidden')
});

botonFiltroReset.addEventListener('click', () => {
    document.getElementById('contenedorFiltrosInput').classList.add('resumen__container__filtros__inputs--hidden')

    document.getElementById('contenedorFiltroTipoDoc').classList.add('resumen__container__input--hidden')

    document.getElementById('contenedorFiltroMoneda').classList.add('resumen__container__input--hidden')

    document.getElementById('contenedorFiltroFecha').classList.add('resumen__container__input--hidden')
})




/*
select tipo doc= MainContent_ddlTipoDocumento
select monedas = MainContent_ddlMonedas

input fecha inicio= MainContent_txtFechaInicio
input fecha fin= MainContent_txtFechaFin

doc seleccionado


contenedor de docs = resumenContainerDocs
doc cargado = resumenDoc
resumen__doc--active

<div class="resumen__doc">
    <div class="doc__info">
        <span>FAC-001</span>
        <span>$120,00</span>
    </div>
    <p class="doc__name__proveedor">Productora Dos Pinos</p>
</div>

-- DOCUMENTO SELECCIONADO
docTitle párrafo del número de documento y observación
docMonto strong del monto del documento
docNombreProveedor párrafo del nombre del proveedor
docTipoDoc párrafo del tipo de documento
docFecha párrafo de la fecha del documento
docMoneda párrado de la moenda del documento


*/