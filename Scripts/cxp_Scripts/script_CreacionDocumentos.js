var API_ENDPOINT = 'http://localhost:59338/WebMethods_CxP/'
var reglasValidacion = {
    nombreProveedor: {
        regex: /^[\wáéíóúÁÉÍÓÚ\s]+$/,
        mensaje: "Solo se permiten letras y números."
    },
    numDocumento: {
        regex: /^[0-9]+[-]?[0-9]+$/,
        mensaje: "Solo se permiten números y un guión medio."
    },
    montoDocumento: {
        regex: /^[0-9]+[,]?[0-9]{1,3}$/,
        mensaje: "Ingrese un monto válido con 3 decimales máximo."
    },
    observacion: {
        regex: /^[\wáéíóúÁÉÍÓÚ.\s]+$/,
        mensaje: "Solo se permite números, letras, punto y espacios."
    }
};

function validarCampo(nombreElemento, valorValidar) {
    var regla = reglasValidacion[nombreElemento];
    if (!regla) return true;
    return regla.regex.test(valorValidar)
}

document.getElementById('MainContent_txtProveedor').addEventListener('blur', function () {
    if (!validarCampo('nombreProveedor', this.value)) {
        mostrarMensajeError(this, 'nombreProveedor');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtNumDocumento').addEventListener('blur', function () {
    if (!validarCampo('numDocumento', this.value)) {
        mostrarMensajeError(this, 'numDocumento');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtObservacion').addEventListener('blur', function () {
    if (!validarCampo('observacion', this.value)) {
        mostrarMensajeError(this, 'observacion');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtMontoTotal').addEventListener('blur', function () {
    if (!validarCampo('montoDocumento', this.value)) {
        mostrarMensajeError(this, 'montoDocumento');
    } else {
        limpiarMensajesError(this);
    }
});

function mostrarMensajeError(elementoValidado, nombreElemento) {
    var contenedorMensajes = elementoValidado.parentElement.querySelector('.formulario__contenedor-mensajes');
    contenedorMensajes.innerHTML = '';

    var itemMensaje = document.createElement('p');
    itemMensaje.className = 'formulario__mensaje';
    itemMensaje.innerHTML = reglasValidacion[nombreElemento].mensaje;

    contenedorMensajes.appendChild(itemMensaje);
    contenedorMensajes.style.display = 'block';
}

function limpiarMensajesError(elementoValidado) {
    var contenedorMensajes = elementoValidado.parentElement.querySelector('.formulario__contenedor-mensajes');
    contenedorMensajes.innerHTML = '';
}

// Eventos de la página
document.getElementById('MainContent_txtProveedor').addEventListener('input', function () {
    var query = this.value.trim();
    if (query.length < 2) {
        cerrarSugerencias();
        return;
    }
    BuscarProveedores(query);
});

document.addEventListener('DOMContentLoaded', function () {
    cargarDocumentosPendientes(1);
    document.getElementById('btnFacturaFiltPend').classList.add('boton__opcion--active');
});

document.getElementById('btnFacturaFiltPend').addEventListener('click', function () {
    cargarDocumentosPendientes(1);
    this.classList.add('boton__opcion--active');
    document.getElementById('btnPagoFiltPend').classList.remove('boton__opcion--active');
    document.getElementById('MainContent_hfFiltCategoria').value = 1
});

document.getElementById('btnPagoFiltPend').addEventListener('click', function () {
    cargarDocumentosPendientes(2);
    this.classList.add('boton__opcion--active');
    document.getElementById('btnFacturaFiltPend').classList.remove('boton__opcion--active');
    document.getElementById('MainContent_hfFiltCategoria').value = 2
});

document.getElementById('btnFiltPendTodo').addEventListener('click', function () {
    var idCategoria = document.getElementById('MainContent_hfFiltCategoria').value
    this.classList.add('boton__opcion--active');
    document.getElementById('btnFiltPendReciente').classList.remove('boton__opcion--active');
    document.getElementById('btnFiltPendAntiguo').classList.remove('boton__opcion--active');
    cargarDocumentosPendientes(idCategoria);
});

document.getElementById('btnFiltPendReciente').addEventListener('click', function () {
    var idCategoria = document.getElementById('MainContent_hfFiltCategoria').value
    this.classList.add('boton__opcion--active');
    document.getElementById('btnFiltPendTodo').classList.remove('boton__opcion--active');
    document.getElementById('btnFiltPendAntiguo').classList.remove('boton__opcion--active');
    cargarDocumentosPendientes(idCategoria);
});

document.getElementById('btnFiltPendAntiguo').addEventListener('click', function () {
    var idCategoria = document.getElementById('MainContent_hfFiltCategoria').value
    this.classList.add('boton__opcion--active');
    document.getElementById('btnFiltPendTodo').classList.remove('boton__opcion--active');
    document.getElementById('btnFiltPendReciente').classList.remove('boton__opcion--active');
    cargarDocumentosPendientes(idCategoria);
});

document.getElementById('btnAplicar').addEventListener('click', function () {
    document.getElementById('MainContent_contenedor__dialogConfirm').style.display = 'block';
    document.getElementById('dialogConfirm').style.animationPlayState = 'running';
});

document.getElementById('btnCancelarAplicacion').addEventListener('click', function () {
    document.getElementById('MainContent_contenedor__dialogConfirm').style.display = 'none';
});

// Funciones para ejecutar en los eventos
function BuscarProveedores(query) {
    fetch(API_ENDPOINT + 'BuscarProveedor', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify({ datoBuscar: query })
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                mostrarSugerencias(data.lista);
            } else {
                alert(data.mensaje)
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            alert('Error de conexión con el servidor [001]');
        });
}

function mostrarSugerencias(lista) {
    var dd = document.getElementById('ddProveedores');
    dd.innerHTML = '';
    lista.forEach(function (proveedor) {
        var item = document.createElement('div');
        item.className = 'contenedor__sugerencias__item';
        item.innerHTML = '<strong>' + proveedor.Nombre + '</strong><br><span>Proveedor: ' + proveedor.NumeroProveedor + '</span>';
        item.onclick = function () { seleccionarProveedor(proveedor.NumeroProveedor, proveedor.Nombre); };
        dd.appendChild(item);
    });
    dd.style.display = 'flex';
}

function seleccionarProveedor(id, nombre) {
    document.getElementById('MainContent_txtProveedor').value = nombre;
    document.getElementById('MainContent_hfNumProveedor').value = id;
    cerrarSugerencias();
}

function cerrarSugerencias() {
    document.getElementById('ddProveedores').style.display = 'none';
}

function obtenerFiltroOrder() {
    // Validar cual de los 3 botones tiene la clase active y retornar el orderByClause correspondiente
    var btnTodas = document.getElementById('btnFiltPendTodo');
    var btnReciente = document.getElementById('btnFiltPendReciente');
    var btnAntiguas = document.getElementById('btnFiltPendAntiguo');

    if (btnTodas.classList.contains('boton__opcion--active')) return 'TODAS';
    if (btnReciente.classList.contains('boton__opcion--active')) return 'RECIENTES';
    if (btnAntiguas.classList.contains('boton__opcion--active')) return 'ANTIGUAS';
}

function cargarDocumentosPendientes(idCategoria) {
    obtenerPendientes(idCategoria);
}

function obtenerPendientes(idCategoria) {
    var orderByClause = obtenerFiltroOrder();

    fetch(API_ENDPOINT + 'ObtenerPendientes', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify({
            categoriaDocumento: idCategoria,
            orderByClause: orderByClause
           })
    })
    .then(function (r) { return r.json(); })
    .then(function (data) {
        if (data.estado) {
            desplegarPendientes(data.lista);
        } else {
            alert(data.mensaje)
        }
    })
    .catch(function (err) {
        console.error('Ha ocurrido un error al ejecutar la petición: ', err)
        alert('Error de conexión con el servidor [002]');
    });
}

function desplegarPendientes(listaDocsPendientes) {
    var contenedorPendientes = document.getElementById('contenedorPendientes');
    var idCategoria = document.getElementById('MainContent_hfFiltCategoria').value

    contenedorPendientes.innerHTML = '';

    listaDocsPendientes.forEach(function (documento) {
        var divItem = document.createElement('div');
        var itemInfo = document.createElement('div');
        var itemActions = document.createElement('div');

        divItem.className = 'aside__pendientes__item'
        var key = JSON.stringify({
            idCategoriaDoc: documento.idCategoriaDoc,
            idproveedor: documento.idProveedor,
            tipoDoc: documento.tipoDocumento,
            numDocumento: documento.numDocumento
        });

        var tipoDocumento = idCategoria == 1 ? 'FAC - ' : 'PAG - ';

        itemInfo.className = 'aside__item__info'
        itemActions.className = 'aside__item__actions'

        itemInfo.innerHTML = '<p class="aside__info">' + tipoDocumento + documento.numDocumento + ' - ' + documento.nombreProveedor + ' - ' + documento.fecha + ' - ' + documento.simboloMoneda + documento.montoTotal + '</p >'

        itemActions.innerHTML = `<button type="button" class="boton boton__opcion boton__opcion--active" id="btnCargaDocPend">Cargar</button>`
        //onclick="cargarDocumento('${key}')" al key.replace(/"/g, '&quot;')
        divItem.appendChild(itemInfo)
        divItem.appendChild(itemActions)

        divItem.querySelector('#btnCargaDocPend').addEventListener('click', function () {
            cargarDocumento(key);
        });

        contenedorPendientes.appendChild(divItem);
    });
}

function cargarDocumento(documento) {
    var key = JSON.parse(documento)

    fetch(API_ENDPOINT + 'CargarDocumento', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: documento
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                llenarCamposDocumentoCargado(data.lista);
            } else {
                alert(data.mensaje)
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            alert('Error de conexión con el servidor [003]');
        });
}

function obtenerTiposDocumento(idCategoria, tipoDocumento) {
    fetch(API_ENDPOINT + 'ObtenerTiposDocumento', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify({ categoriaDocumento: idCategoria })
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                renderizarOptionsTipoDocumento(data.lista, tipoDocumento);
            } else {
                alert(data.mensaje)
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            alert('Error de conexión con el servidor [004]');
        });
}

function renderizarOptionsTipoDocumento(listaTipoDocumento, tipoDocumentoSelected) {
    var selectElement = document.getElementById('MainContent_ddlTipoDocumento')

    selectElement.innerHTML = ''
    listaTipoDocumento.forEach(function (tipoDocumento) {
        var optionElement = document.createElement('option')
        optionElement.setAttribute('value', tipoDocumento.IdTipoDocumento)
        optionElement.innerHTML = tipoDocumento.Descripcion

        if (tipoDocumento.IdTipoDocumento == parseInt(tipoDocumentoSelected)) optionElement.setAttribute('selected', 'true');

        selectElement.appendChild(optionElement)
    })
}

function llenarCamposDocumentoCargado(documento) {

    //Actualizar, llenar, bloquear y mostrar elementos
    document.getElementById('MainContent_hfCategoria').value = documento.CategoriaDoc
    // Llenar
    obtenerTiposDocumento(documento.CategoriaDoc, documento.TipoDocumento);
    document.getElementById('MainContent_hfTipoDocumento').value = parseInt(documento.TipoDocumento)
   
    //if (document.getElementById('MainContent_hfCategoria').value != documento.CategoriaDoc) {
    //}

    if (documento.CategoriaDoc == 1) {
        document.getElementById('MainContent_btnFiltFacturaForm').classList.remove('boton__opcion--active')
        document.getElementById('MainContent_btnFiltFacturaForm').classList.add('boton__opcion--active')
        document.getElementById('MainContent_btnFiltPagoForm').classList.remove('boton__opcion--active')
    } else {
        document.getElementById('MainContent_btnFiltPagoForm').classList.remove('boton__opcion--active')
        document.getElementById('MainContent_btnFiltPagoForm').classList.add('boton__opcion--active')
        document.getElementById('MainContent_btnFiltFacturaForm').classList.remove('boton__opcion--active')
    }

    document.getElementById('MainContent_txtProveedor').value = documento.NombreProveedor
    document.getElementById('MainContent_hfNumProveedor').value = documento.NumProveedor
    document.getElementById('MainContent_txtNumDocumento').value = documento.NumDocumento
    document.getElementById('MainContent_txtFechaEmision').value = documento.FechaDoc
    document.getElementById('MainContent_txtMontoTotal').value = documento.MontoTotal //MontoTotalFormateado
    document.getElementById('MainContent_txtObservacion').value = documento.Observacion

    moverSelectOption(document.getElementById('MainContent_ddlMoneda'), documento.MonedaDoc)

    document.getElementById('MainContent_btnGuardar').classList.add('boton__ocultar')
    document.getElementById('MainContent_btnModificar').classList.remove('boton__ocultar')
    document.getElementById('MainContent_btnEliminar').classList.remove('boton__ocultar')
    document.getElementById('btnAplicar').classList.remove('boton__ocultar')

    document.getElementById('MainContent_contenedorMensajesTipoDoc').innerHTML = ''
    document.getElementById('MainContent_contenedorMensajesProveedor').innerHTML = ''
    document.getElementById('MainContent_contenedorMensajesNumDoc').innerHTML = ''
    document.getElementById('MainContent_contenedorMensajesFecha').innerHTML = ''
    document.getElementById('MainContent_contenedorMensajesMoneda').innerHTML = ''
    document.getElementById('MainContent_contenedorMensajesMonto').innerHTML = ''
    document.getElementById('MainContent_contenedorMensajesObservacion').innerHTML = ''
}

function moverSelectOption(selectElement, valorBuscar) {
    for (let i = 0; i < selectElement.options.length; i++) {
        if (selectElement.options[i].value == valorBuscar) {
            selectElement.options[i].selected = true;
            break;
        }
    }
}