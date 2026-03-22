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
   
});