var reglasValidacion = {
    usuario: {
        regex: /^[\w]+$/,
        mensaje: "Solo se permiten letras."
    },
    contrasena: {
        regex: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%.*?&])[A-Za-z\d@$!%.*?&]{8,15}$/,
        mensaje: "Mínimo 8 caracteres Máximo 15. Al menos una letra mayúscula, una letra minúscula, un número y un carácter especial. (@$!%.*?&)"
    },
    nombre: {
        regex: /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/,
        mensaje: "Solo se permiten letras y espacios."
    },
    correo: {
        regex: /^[\w._-]+@[\w.]+.[a-zA-Z]{2,4}$/,
        mensaje: "El dato ingresado no es un correo válido. (correo@correo.com)"
    },
    entero: {
        regex: /^\d+$/,
        mensaje: "Debe seleccionar una opción válida."
    }
};

function validarCampo(nombreElemento, valorValidar) {
    var regla = reglasValidacion[nombreElemento];
    if (!regla) return true;
    return regla.regex.test(valorValidar)
}

document.getElementById('MainContent_txtUsuario').addEventListener('blur', function () {
    if (!validarCampo('usuario', this.value)) {
        mostrarMensajeError(this, 'usuario');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtContrasenna').addEventListener('blur', function () {
    if (!validarCampo('contrasena', this.value)) {
        mostrarMensajeError(this, 'contrasena');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtNombre').addEventListener('blur', function () {
    if (!validarCampo('nombre', this.value)) {
        mostrarMensajeError(this, 'nombre');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtApellidoUno').addEventListener('blur', function () {
    if (!validarCampo('nombre', this.value)) {
        mostrarMensajeError(this, 'nombre');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtApellidoDos').addEventListener('blur', function () {
    if (this.value.trim() != '') {
        if (!validarCampo('nombre', this.value)) {
            mostrarMensajeError(this, 'nombre');
        } else {
            limpiarMensajesError(this);
        }
    }
});

document.getElementById('MainContent_txtCorreoUsuario').addEventListener('blur', function () {
    if (!validarCampo('correo', this.value)) {
        mostrarMensajeError(this, 'correo');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_ddlEstadoUsuario').addEventListener('change', function () {    
    if (!validarCampo('entero', this.value)) {
        mostrarMensajeError(this, 'entero');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_ddlRoles').addEventListener('change', function () {    
    if (!validarCampo('entero', this.value)) {
        mostrarMensajeError(this, 'entero');
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