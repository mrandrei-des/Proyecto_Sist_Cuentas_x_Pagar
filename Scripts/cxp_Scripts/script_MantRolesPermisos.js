var listaRoles = [
    {
        id: 1,
        nombre: 'Usuarios'
    },
    {
        id: 2,
        nombre: 'Proveedores'
    },
    {
        id: 3,
        nombre: 'Documentos'
    },
    {
        id: 4,
        nombre: 'Reportes'
    }
]

var listaPermisos = [
    {
        id: 1,
        titulo: 'Registro',
        descripcion: 'Permitir crear usuarios en el sistema'
    },
    {
        id: 2,
        titulo: 'Cambio de Contraseña',
        descripcion: 'Permitir al usuario cambiar su contraseña'
    },
    {
        id: 3,
        titulo: 'Configuración de Roles',
        descripcion: 'Permitir al usuario crear y configurar roles'
    },
    {
        id: 4,
        titulo: 'Listado',
        descripcion: 'Permitir que el usuario pueda ver la lista de usuarios registrados'
    },
    {
        id: 5,
        titulo: 'Cambio de Contraseña',
        descripcion: 'Permitir al usuario cambiar su contraseña'
    }
]

var listaPermisosActivos = [2, 3]

var rolActivo = null

document.addEventListener('DOMContentLoaded', function () {
    cargarRoles();
    document.getElementById('contenedorPermisos').innerHTML = ''
});

document.getElementById('MainContent_btnGuardarCambios').addEventListener('click', () => {
    var checkboxes = document.querySelectorAll('#lblchkInput input[type=checkbox]')

    var permisosSeleccionados = []

    checkboxes.forEach(function (chk) {
        if (chk.checked) {
            permisosSeleccionados.push(chk.value)
        }
    })

    var permisosAsignados = {
        idRol: rolActivo,
        permisos: permisosSeleccionados
    }
    console.log(permisosAsignados)
})

function cargarRoles() {
    var ulListaElement = document.getElementById('rolList')
    ulListaElement.innerHTML = ''

    listaRoles.forEach(rol => {
        var liElement = document.createElement('li')

        liElement.innerHTML = '<p class="rol" id="rolRegistrado">' + rol.nombre + '</p>'

        liElement.querySelector('#rolRegistrado').addEventListener('click', function () {
            cargarPermisos(rol);
            this.classList.add('rol__active')
        })

        ulListaElement.appendChild(liElement);
    });
}

function cargarPermisos(rol) {
    rolActivo = rol.id
    var roles = document.querySelectorAll('#rolRegistrado');
    roles.forEach(itemRol => {
        itemRol.classList.remove('rol__active')
    })

    renderizarPermisos(listaPermisos);
}

function renderizarPermisos(listaPermisosRol) {

    const contenedorPermisos = document.getElementById('contenedorPermisos')

    contenedorPermisos.innerHTML = ''

    var asignados = new Set(listaPermisosActivos)

    listaPermisosRol.forEach(permiso => {
        var contenedorPermisoItem = document.createElement('div')
        var asignado = asignados.has(permiso.id);

        contenedorPermisoItem.innerHTML = `
        <div class="contenedor__permiso__item"> 
            <div class="permiso__item__button"> 
                <div class="contenedor__item__button">  
                    <label id="lblchkInput" name="chkInput" class="contenedor__toggle__button"> 
                        <input type="checkbox" id="p_${permiso.id}" value="${permiso.id}" name="chkInput" class="input__chk__hidden" ${asignado ? 'checked' : ''}> 
                        <div class="barra"></div> 
                        <div class="circle"></div> 
                    </label>
                </div>  
            </div>   
            <div class="permiso__item__info">  
                <h3 class="permiso__item__title">${permiso.titulo}</h3>  
                <p class="permiso__item__description">${permiso.descripcion}</p> 
            </div> 
        </div>`
        contenedorPermisos.appendChild(contenedorPermisoItem)
    });
}