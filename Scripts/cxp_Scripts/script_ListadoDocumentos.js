const API_ENDPOINT = 'http://localhost:59338/WebMethods_CxP/'

const botonFiltroTipoDoc = document.getElementById('btnFiltroTipoDoc')
const botonFiltroMoneda = document.getElementById('btnFiltroMoneda')
const botonFiltroFechas = document.getElementById('btnFiltroFechas')
const botonFiltroReset = document.getElementById('btnFiltroReset')

const selTipoDoc = document.getElementById('MainContent_ddlTipoDocumento')
const selMoneda = document.getElementById('MainContent_ddlMonedas')
const inpFechaInicio = document.getElementById('MainContent_txtFechaInicio')
const inpFechaFin = document.getElementById('MainContent_txtFechaFin')

var filtTipoDocumento = '';
var filtMoneda = '';
var filtFechaInicio = '';
var filtFechaFin = '';

botonFiltroTipoDoc.addEventListener('click', () => {
    moverSelectOption(selTipoDoc, '')
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

    moverSelectOption(selTipoDoc, '')
    moverSelectOption(selMoneda, '')
    inpFechaInicio.value = ''
    inpFechaFin.value = ''

    filtTipoDocumento = '', filtMoneda = '', filtFechaInicio = '', filtFechaFin = ''
})

function moverSelectOption(selectElement, valorBuscar) {
    for (let i = 0; i < selectElement.options.length; i++) {
        if (selectElement.options[i].value == valorBuscar) {
            selectElement.options[i].selected = true;
            break;
        }
    }
}

function mostrarAlerta(titulo, mensaje, icon, textoBoton, tipo) {
    Swal.fire({
        title: titulo,
        text: mensaje,
        icon: icon,
        confirmButtonText: textoBoton
    });
}

//mostrarAlerta('Atención', 'Mensaje de proceso fallido', 'error', 'Ok', false)
//mostrarAlerta('Atención', 'Mensaje de proceso requiere cuidado', 'warning', 'Ok', false)

selTipoDoc.addEventListener('blur', function () {
    filtTipoDocumento = selTipoDoc.value
    consultarDocumentosFiltros()
});

selMoneda.addEventListener('blur', () => {
    filtMoneda = selMoneda.value
    consultarDocumentosFiltros()
});

inpFechaInicio.addEventListener('input', () => {
    filtFechaInicio = inpFechaInicio.value
    consultarDocumentosFiltros()
});

inpFechaFin.addEventListener('input', () => {
    filtFechaFin = inpFechaFin.value
    consultarDocumentosFiltros()
});

function consultarDocumentosFiltros() {

    var filtros = {
        filtTipoDoc: filtTipoDocumento,
        filtMoneda: filtMoneda,
        filtFechaInicio: filtFechaInicio,
        filtFechaFin: filtFechaFin
    }

    fetch(API_ENDPOINT + '', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: filtros
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                renderizarDocumentos(data.lista)

            } else {
                mostrarAlerta('Ocurrió un problema al consultar los documentos', data.mensaje, 'warning', 'Ok')
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            mostrarAlerta('Ocurrió un problema', 'Error de conexión con el servidor [001]', 'error', 'Ok')
        });
}

function renderizarDocumentos(listaDocumentos) {
    const contResumenDocs = document.getElementById('resumenContainerDocs')

    contResumenDocs.innerHTML = ''

    listaDocumentos.foreach(function (documento) {
        var divResumenDoc = document.createElement('div')
        divResumenDoc.className = 'resumen__doc'
        divResumenDoc.innerHTML =`
        <div class="doc__info">
            <span>${documento.numDocumento}</span>
            <span>${documento.monto}</span>
        </div>
        <p class="doc__name__proveedor">${documento.proveedor}</p>`

        contResumenDocs.appendChild(divResumenDoc)
    });
}

/*
select tipo doc= 
select monedas = 

input fecha inicio= 
input fecha fin= 

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