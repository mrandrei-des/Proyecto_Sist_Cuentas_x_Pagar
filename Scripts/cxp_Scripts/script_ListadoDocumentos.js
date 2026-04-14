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

document.addEventListener('DOMContentLoaded', function () {
    filtTipoDocumento = '', filtMoneda = '', filtFechaInicio = '', filtFechaFin = ''
    consultarDocumentosFiltros()
});

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
    document.getElementById('containerInfoDoc').classList.add('resumen__container__info__doc__hidden')

    moverSelectOption(selTipoDoc, '')
    moverSelectOption(selMoneda, '')
    inpFechaInicio.value = ''
    inpFechaFin.value = ''

    filtTipoDocumento = '', filtMoneda = '', filtFechaInicio = '', filtFechaFin = ''
    consultarOptionsFiltros()
    consultarDocumentosFiltros()
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

selTipoDoc.addEventListener('change', function () {
    filtTipoDocumento = selTipoDoc.value
    consultarDocumentosFiltros()
    consultarOptionsFiltros('tipos')
});

selMoneda.addEventListener('change', () => {
    filtMoneda = selMoneda.value
    consultarDocumentosFiltros()
    consultarOptionsFiltros('monedas')
});

inpFechaInicio.addEventListener('change', () => {
    filtFechaInicio = inpFechaInicio.value
    consultarDocumentosFiltros()
    consultarOptionsFiltros('')
});

inpFechaFin.addEventListener('change', () => {
    filtFechaFin = inpFechaFin.value
    consultarDocumentosFiltros()
    consultarOptionsFiltros('')
});

function consultarDocumentosFiltros() {
    
    var filtros = {
        filtTipoDoc: filtTipoDocumento,
        filtMoneda: filtMoneda,
        filtFechaInicio: filtFechaInicio,
        filtFechaFin: filtFechaFin
    }

    fetch(API_ENDPOINT + 'ConsultaReporteDocumentosAplicados', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify(filtros)
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                renderizarDocumentos(data.lista)

            } else {
                mostrarAlerta(data.mensaje, '', 'warning', 'Ok')
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            mostrarAlerta('Ocurrió un problema', 'Error de conexión con el servidor [001]', 'error', 'Ok')
        });
}

function consultarOptionsFiltros(origen) {

    var filtros = {
        filtTipoDoc: filtTipoDocumento,
        filtMoneda: filtMoneda,
        filtFechaInicio: filtFechaInicio,
        filtFechaFin: filtFechaFin
    }

    fetch(API_ENDPOINT + 'ConsultarFiltrosListDocumentos', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify(filtros)
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {

                if (origen == 'monedas' && filtMoneda != '') {
                    renderizarSelectTipoDoc(data.listaTipoDocs)
                } else if (origen == 'tipos' && filtTipoDocumento != '') {
                    renderizarSelectMonedas(data.listaMonedas)
                } else {
                    renderizarSelectTipoDoc(data.listaTipoDocs)
                    renderizarSelectMonedas(data.listaMonedas)
                }

            } else {
                mostrarAlerta(data.mensaje, '', 'warning', 'Ok')
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

    listaDocumentos.forEach(documentoAplicado => {
        var divResumenDoc = document.createElement('div')
        var tipoDocumento = documentoAplicado.IdCategoria == 1 ? 'FAC - ' : 'PAG - ';
        divResumenDoc.className = 'resumen__doc'
        divResumenDoc.setAttribute('id', 'resumenDoc')
        divResumenDoc.innerHTML = `
        <div class="doc__info">
            <span>${tipoDocumento + documentoAplicado.NumDocumento}</span>
            <span>${documentoAplicado.Monto}</span>
        </div>
        <p class="doc__name__proveedor">${documentoAplicado.NombreProveedor}</p>`

        divResumenDoc.addEventListener('click', function () {
            quitarClaseActivaResumenDoc()
            this.classList.add('resumen__doc--active');            
            cargarInformacionDocumento(documentoAplicado.IdCategoria, documentoAplicado.IdProveedor, documentoAplicado.IdTipoDoc, documentoAplicado.NumDocumento)
        })

        contResumenDocs.appendChild(divResumenDoc);        
    });
}

function renderizarSelectMonedas(listaMonedas) {
    //selMoneda.innerHTML = ''

    //var optElement = document.createElement('option')
    //optElement.setAttribute('value', '')
    //optElement.innerText = 'Seleccione una opción'
    //selMoneda.appendChild(optElement);

    //listaMonedas.forEach(moneda => {
    //    var optElement = document.createElement('option')

    //    optElement.setAttribute('value', moneda.CodigoMoneda)
    //    optElement.innerText = moneda.Descripcion

    //    selMoneda.appendChild(optElement);
    //});

    listaMonedas.forEach(moneda => {
        cambiarTextOption(selMoneda, moneda.CodigoMoneda, moneda.Descripcion)
    });
}

function renderizarSelectTipoDoc(listaTipoDoc) {
    //selTipoDoc.innerHTML = ''

    //var optElement = document.createElement('option')
    //optElement.setAttribute('value', '')
    //optElement.innerText = 'Seleccione una opción'
    //selMoneda.appendChild(optElement);

    //listaTipoDoc.forEach(tipo => {
    //    var optElement = document.createElement('option')

    //    optElement.setAttribute('value', tipo.IdTipoDocumento)
    //    optElement.innerText = tipo.Descripcion

    //    selTipoDoc.appendChild(optElement);
    //});

    listaTipoDoc.forEach(tipo => {
        cambiarTextOption(selTipoDoc, tipo.IdTipoDocumento, tipo.Descripcion)
    });   
}

function quitarClaseActivaResumenDoc() {
    var listaDocumentos = document.querySelectorAll('#resumenDoc');
    listaDocumentos.forEach(documentoAplicado => {
        documentoAplicado.classList.remove('resumen__doc--active')
    });
}

function cargarInformacionDocumento(idCategoria, idProveedor, idTipoDocumento, numDocumento) {
    var documentoSeleccionado = {
        idCategoriaDoc: idCategoria,
        idProveedor: idProveedor,
        tipoDoc: idTipoDocumento,
        numDocumento: numDocumento
    }

    fetch(API_ENDPOINT + 'BuscarDocumentoAplicado', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify(documentoSeleccionado)
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                cargarDocumento(data.documento)

            } else {
                mostrarAlerta(data.mensaje, '', 'warning', 'Ok')
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            mostrarAlerta('Ocurrió un problema', 'Error de conexión con el servidor [001]', 'error', 'Ok')
        });
}

function cargarDocumento(documentoSeleccionado) {
    var tipoDocumento = documentoSeleccionado.IDCategoria == 1 ? 'FAC - ' : 'PAG - ';

    const containerInfo = document.getElementById('containerInfoDoc')
    const pTitulo = document.getElementById('docTitle')
    const stMonto = document.getElementById('docMonto')
    const pNombreProveedor = document.getElementById('docNombreProveedor')
    const pTipoDoc = document.getElementById('docTipoDoc')
    const pFecha = document.getElementById('docFecha')
    const pMoneda = document.getElementById('docMoneda')

    pTitulo.innerText = `${tipoDocumento}${documentoSeleccionado.NumDocumento} - ${documentoSeleccionado.Observacion}`
    stMonto.innerText = documentoSeleccionado.MontoTotal
    pNombreProveedor.innerText = documentoSeleccionado.NombreProveedor
    pTipoDoc.innerText = documentoSeleccionado.TipoDocumento
    pFecha.innerText = documentoSeleccionado.FechaDoc
    pMoneda.innerText = documentoSeleccionado.MonedaDoc

    containerInfo.classList.remove('resumen__container__info__doc__hidden')
}

function cambiarTextOption(selectElement, valorBuscar, valorNuevo) {
    for (let i = 0; i < selectElement.options.length; i++) {
        if (selectElement.options[i].value == valorBuscar) {
            selectElement.options[i].innerText = valorNuevo;
            break;
        }
    }
}