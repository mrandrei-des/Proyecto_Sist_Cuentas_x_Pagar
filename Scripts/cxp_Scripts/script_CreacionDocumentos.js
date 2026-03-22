var API_ENDPOINT = 'http://localhost:59338/WebMethods_CxP/'

document.getElementById('MainContent_txtProveedor').addEventListener('input', function () {
    var query = this.value.trim();
    if (query.length < 2) {
        cerrarSugerencias();
        return;
    }
    BuscarProveedores(query);
});

function BuscarProveedores(query) {
    fetch(API_ENDPOINT +'BuscarProveedor', {        
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
            alert('Error de conexión con el servidor');
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

document.addEventListener('DOMContentLoaded', function () {
    cargarDocumentosPendientes(1);
    document.getElementById('btnFacturaFiltPend').classList.add('boton__opcion--active');
});

document.getElementById('btnFacturaFiltPend').addEventListener('click', function () {
    cargarDocumentosPendientes(1);
    this.classList.add('boton__opcion--active');
    document.getElementById('btnPagoFiltPend').classList.remove('boton__opcion--active');
});

document.getElementById('btnPagoFiltPend').addEventListener('click', function () {
    cargarDocumentosPendientes(2);
    this.classList.add('boton__opcion--active');
    document.getElementById('btnFacturaFiltPend').classList.remove('boton__opcion--active');
});

function cargarDocumentosPendientes(idCategoria) {
    obtenerPendientes(idCategoria);    
}

function obtenerPendientes(idCategoria) {
    fetch(API_ENDPOINT + 'ObtenerPendientes', {
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
            desplegarPendientes(data.lista);
        } else {
            alert(data.mensaje)
        }
    })
    .catch(function (err) {
        console.error('Ha ocurrido un error al ejecutar la petición: ', err)
        alert('Error de conexión con el servidor');
    });
}

function desplegarPendientes(listaDocsPendientes) {
    var contenedorPendientes = document.getElementById('contenedorPendientes');
    var idCategoria = document.getElementById('MainContent_hfNumProveedor').value

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

        itemInfo.className = 'aside__item__info'
        itemActions.className = 'aside__item__actions'

        itemInfo.innerHTML = '<p class="aside__info">' + documento.numDocumento + ' - ' + documento.nombreProveedor + ' - ' + documento.fecha + ' - ' + documento.simboloMoneda + documento.montoTotal + '</p >'

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
    console.log(documento);
    console.log(key);

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
            alert('Error de conexión con el servidor');
        });
}
function llenarCamposDocumentoCargado(documento) {
    // Falta cargar en pantalla lo que viene en el objeto
    console.log(documento)

    //Actualizar, llenar, bloquear y mostrar elementos
    MainContent_btnFiltFacturaForm
    MainContent_btnFiltPagoForm

    MainContent_hfCategoria

    MainContent_ddlTipoDocumento
    MainContent_hfNumProveedor

    MainContent_txtProveedor
    MainContent_txtNumDocumento
    MainContent_txtFechaEmision
    MainContent_ddlMoneda
    MainContent_txtMontoTotal
    MainContent_txtObservacion
    document.getElementById('btnFacturaFiltPend').setAttribute('diabled', 'true');
    MainContent_btnGuardar
    MainContent_btnModificar
    MainContent_btnAplicar
}

//DocumentoExisteYEstaPendiente
