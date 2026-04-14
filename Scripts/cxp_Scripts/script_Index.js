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
});

function consultarDocumentosPendientesAntiguos() {
    fetch(API_ENDPOINT + '', {
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
        var claseDiasAntiguo = ''

        if (documento.DiasAntiguo >= 15) {
            claseDiasAntiguo = 'doc__dias--Antiguo'

        } else if (documento.DiasAntiguo >= 10) {
            claseDiasAntiguo = 'doc__dias--Intermedio'

        } else {
            claseDiasAntiguo = 'doc__dias--Reciente'
        }

        pendienteDoc.className = 'pendienteDoc'
        pendienteDoc.innerHTML =`<div class="doc__info">
                                    <span>FAC-001</span>
                                    <span>$120,00</span>
                                </div>
                                <div class="doc__registro">
                                    <p class="doc__name__proveedor">Productora Dos Pinos</p>
                                    <span class="doc__dias ${claseDiasAntiguo}--Antiguo">45 días</span>
                                </div>
                                `
        contenedorPendientes.appendChild(pendienteDoc)
    })
}

function consultarActividadReciente() {
    fetch(API_ENDPOINT + '', {
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
                renderizarActividadReciente(data.listaAcciones)
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

    listaCambios.forEach(cambio => {
        var actividadReciente = document.createElement('div')        

        actividadReciente.className = 'actividad__cambio'
        actividadReciente.innerHTML =
        `<div class="cambio__info">
            <p class="cambio__info__documento">FAC-008 aplicada correctamente</p>
            <span class="cambio__info__dia">Hace 2h
            </span>
        </div>
        <p class="cambio__referencia">
            <span class="cambio__referencia__usuario">Carlos</span>
            <span>
                <i class="fa-solid fa-circle"></i>
            </span>
            <span>Importaciones Norte</span>
        </p>
        `
        contenedorActividades.appendChild(actividadReciente)
    })
}