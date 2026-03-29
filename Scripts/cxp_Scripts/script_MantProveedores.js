var reglasValidacion = {
    identificacion: {
        regex: /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ]+$/,
        mensaje: "Solo se permiten letras y números."
    },
    nombreProveedor: {
        regex: /^[\w\s]+$/,
        mensaje: "Solo se permiten letras, números y espacios."
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

document.getElementById('MainContent_ddlTipoIdentificacion').addEventListener('change', function () {
    if (!validarCampo('entero', this.value)) {
        mostrarMensajeError(this, 'entero');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtIdentificacion').addEventListener('blur', function () {
    if (!validarCampo('identificacion', this.value)) {
        mostrarMensajeError(this, 'identificacion');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtNombre').addEventListener('blur', function () {
    if (!validarCampo('nombreProveedor', this.value)) {
        mostrarMensajeError(this, 'nombreProveedor');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtCorreo').addEventListener('blur', function () {
    if (!validarCampo('correo', this.value)) {
        mostrarMensajeError(this, 'correo');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_ddlEstado').addEventListener('change', function () {
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