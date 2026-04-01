var API_ENDPOINT = 'http://localhost:59338/WebMethods_CxP/'
var reglasValidacion = {
    nombreProveedor: {
        regex: /^[\wáéíóúÁÉÍÓÚ\s]+$/,
        mensaje: "Solo se permiten letras y números."
    }
};
var opcionGrupoActivo = 1;
var rolActivo = 1;

function validarCampo(nombreElemento, valorValidar) {
    var regla = reglasValidacion[nombreElemento];
    if (!regla) return true;
    return regla.regex.test(valorValidar)
}

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

document.getElementById('MainContent_txtNombreRol').addEventListener('blur', function () {
    if (!validarCampo('nombreProveedor', this.value)) {
        mostrarMensajeError(this, 'nombreProveedor');
    } else {
        limpiarMensajesError(this);
    }

    //mostrarAlerta('Atención', 'Mensaje de proceso exitoso', 'success', 'Ok', false)
    //mostrarAlerta('Atención', 'Mensaje de proceso fallido', 'error', 'Ok', false)
    //mostrarAlerta('Atención', 'Mensaje de proceso requiere cuidado', 'warning', 'Ok', false)
});

function mostrarAlerta(titulo, mensaje, icon, textoBoton, tipo) {
    Swal.fire({
        title: titulo,
        text: mensaje,
        icon: icon,
        confirmButtonText: textoBoton
    });
}

document.addEventListener('DOMContentLoaded', function () {
    rolActivo = parseInt(document.getElementById('MainContent_hdfRolSeleccionado').value);
    console.log(rolActivo);
    document.getElementById('contenedorPermisos').innerHTML = ''
    if (rolActivo > 0) {
        cargarGruposPermisos();
        cargarPermisos(1);
    }
});

document.getElementById('btnGuardarCambios').addEventListener('click', () => {
    var checkboxes = document.querySelectorAll('#lblchkInput input[type=checkbox]')

    var permisosSeleccionados = []

    checkboxes.forEach(function (chk) {
        if (chk.checked) {
            permisosSeleccionados.push(chk.value)
        }
    })

    var permisosAsignados = {
        idRol: rolActivo,
        idGrupoActivo: opcionGrupoActivo,
        listaPermisos: permisosSeleccionados
    }

    enviarPermisosAsignados(permisosAsignados);
})

function enviarPermisosAsignados(permisosAsignados) {
    fetch(API_ENDPOINT + 'ActualizarPermisos', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify(permisosAsignados)
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                mostrarAlerta('Atención', 'Permisos actualizados correctamente.', 'success', 'Ok', false)
            } else {
                alert(data.mensaje)
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            alert('Error de conexión con el servidor [001]');
        });
}

function cargarGruposPermisos() {
    fetch(API_ENDPOINT + 'ObtenerGruposPermisos', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify({ })
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.estado) {
                renderizarGruposPermisos(data.lista);
            } else {
                alert(data.mensaje)
            }
        })
        .catch(function (err) {
            console.error('Ha ocurrido un error al ejecutar la petición: ', err)
            alert('Error de conexión con el servidor [001]');
        });
}

function renderizarGruposPermisos(listaGrupos) {
    var ulListaElement = document.getElementById('rolList')
    ulListaElement.innerHTML = ''

    listaGrupos.forEach(grupo => {
        var liElement = document.createElement('li')
        liElement.innerHTML = `<p class="rol" id="rolRegistrado">${grupo.Descripcion}</p>`

        liElement.querySelector('#rolRegistrado').addEventListener('click', function () {
            quitarClaseActivaGrupoPermiso()
            this.classList.add('rol__active');
            //El idGrupo es el id del grupo de permiso padre, apartir de este id se obtienen los permisos asignados a ese grupo
            cargarPermisos(grupo.IdGrupoPermiso);
        })

        ulListaElement.appendChild(liElement);
    });

    document.getElementById('rolRegistrado').classList.add('rol__active')
}

function quitarClaseActivaGrupoPermiso() {
    var gruposPermisos = document.querySelectorAll('#rolRegistrado');
    gruposPermisos.forEach(itemGrupo => {
        itemGrupo.classList.remove('rol__active')
    });
}

function cargarPermisos(grupoPadre) {
    opcionGrupoActivo = grupoPadre
    
    fetch(API_ENDPOINT + 'ObtenerPermisos', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify({
            GrupoPadre: grupoPadre,
            RolPadre: rolActivo
        })
    })
    .then(function (r) { return r.json(); })
    .then(function (data) {
        if (data.estado) {
            renderizarPermisos(data.lista, data.listaAsignados);
        } else {
            alert(data.mensaje)
        }
    })
    .catch(function (err) {
        console.error('Ha ocurrido un error al ejecutar la petición: ', err)
        alert('Error de conexión con el servidor [002]');
    });
}

function renderizarPermisos(listaPermisos, listaPermisosAsignados) {

    const contenedorPermisos = document.getElementById('contenedorPermisos')
    contenedorPermisos.innerHTML = ''
    var asignados = new Set(listaPermisosAsignados)

    listaPermisos.forEach(permiso => {
        var contenedorPermisoItem = document.createElement('div')
        var asignado = asignados.has(permiso.IdPermiso);

        contenedorPermisoItem.innerHTML = `
        <div class="contenedor__permiso__item"> 
            <div class="permiso__item__button"> 
                <div class="contenedor__item__button">  
                    <label id="lblchkInput" name="chkInput" class="contenedor__toggle__button"> 
                        <input type="checkbox" id="p_${permiso.IdPermiso}" value="${permiso.IdPermiso}" name="chkInput" class="input__chk__hidden" ${asignado ? 'checked' : ''}> 
                        <div class="barra"></div> 
                        <div class="circle"></div> 
                    </label>
                </div>  
            </div>   
            <div class="permiso__item__info">  
                <h3 class="permiso__item__title">${permiso.Titulo}</h3>  
                <p class="permiso__item__description">${permiso.Descripcion}</p> 
            </div> 
        </div>`
        contenedorPermisos.appendChild(contenedorPermisoItem)
    });
}