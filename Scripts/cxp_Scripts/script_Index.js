const API_ENDPOINT = 'http://localhost:59338/WebMethods_CxP/'

function mostrarAlerta(titulo, mensaje, icon, textoBoton, tipo) {
    Swal.fire({
        title: titulo,
        text: mensaje,
        icon: icon,
        confirmButtonText: textoBoton
    });
}

document.addEventListener('DOMContentLoaded', function () {
    consultarDocumentosPendientesAntiguos()
    consultarActividadReciente()
});

function consultarDocumentosPendientesAntiguos() {
    fetch(API_ENDPOINT + 'ConsultaDocsPendientes', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: {}
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                renderizarPendientes(data.listaDocumentos)               
            } else {
                mostrarAlerta(data.mensaje, '', 'warning', 'Ok')
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            mostrarAlerta('Ocurrió un problema', 'Error de conexión con el servidor [001]', 'error', 'Ok')
        });
}

function renderizarPendientes(listaDocumentos) {
    const contenedorPendientes = document.getElementById('listadoDocumentosPendientes')

    contenedorPendientes.innerHTML = ''

    listaDocumentos.forEach(documento => {
        var pendienteDoc = document.createElement('div')
        var tipoDocumento = document.IdCategoria == 1 ? 'FAC - ' : 'PAG - ';
        var cantDias = documento.CantDias == 1 ? `1 día` : `${documento.CantDias} días`

        var claseDiasAntiguo = ''

        if (documento.CantDias >= 15) {
            claseDiasAntiguo = 'doc__dias--Antiguo'

        } else if (documento.CantDias >= 10) {
            claseDiasAntiguo = 'doc__dias--Intermedio'

        } else {
            claseDiasAntiguo = 'doc__dias--Reciente'
        }

        pendienteDoc.className = 'pendiente__doc'
        pendienteDoc.innerHTML =`<div class="doc__info">
                                    <span>${tipoDocumento} ${documento.NumDocumento}</span>
                                    <span>${documento.SimboloMoneda}${documento.TotalDocumentoFormateado}</span>
                                </div>
                                <div class="doc__registro">
                                    <p class="doc__name__proveedor">${documento.NombreProveedor}</p>
                                    <span class="doc__dias ${claseDiasAntiguo}">${cantDias}</span>
                                </div>
                                `
        contenedorPendientes.appendChild(pendienteDoc)
    })
}

function consultarActividadReciente() {
    fetch(API_ENDPOINT + 'ConsultaUltimasAcciones', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: {}
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                renderizarActividadReciente(data.lista)
            } else {
                mostrarAlerta(data.mensaje, '', 'warning', 'Ok')
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            mostrarAlerta('Ocurrió un problema', 'Error de conexión con el servidor [002]', 'error', 'Ok')
        });
}

function renderizarActividadReciente(listaCambios) {
    const contenedorActividades = document.getElementById('listadoActividadReciente')

    contenedorActividades.innerHTML = ''
    console.log(listaCambios)
    listaCambios.forEach(cambio => {
        var actividadReciente = document.createElement('div')        
        var tipoDocumento = cambio.IdCategoria == 1 ? 'FAC - ' : 'PAG - ';
        var numDocumento = tipoDocumento + cambio.NumDocumento

        actividadReciente.className = 'actividad__cambio'
        actividadReciente.innerHTML =
        `<div class="cambio__info">
            <p class="cambio__info__documento">${numDocumento} ${cambio.AccionRealizada}</p>
            <span class="cambio__info__dia">${cambio.HaceTiempo}</span>
        </div>
        <p class="cambio__referencia">
            <span class="cambio__referencia__usuario">${cambio.UsuarioAccion}</span>
            <span><i class="fa-solid fa-circle"></i></span>
            <span>${cambio.NombreProveedor}</span>
        </p>
        `
        contenedorActividades.appendChild(actividadReciente)
    })
}