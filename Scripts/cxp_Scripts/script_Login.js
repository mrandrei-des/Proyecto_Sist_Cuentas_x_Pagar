var reglasValidacion = {
    usuario: {
        regex: /^[\w]+$/,
        mensaje: "Solo se permiten letras."
    },
    contrasena: {
        regex: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%.*?&])[A-Za-z\d@$!%.*?&]{8,15}$/,
        mensaje: "Mínimo 8 caracteres Máximo 15. Al menos una letra mayúscula, una letra minúscula, un número y un carácter especial. (@$!%.*?&)"
    }
};

function validarCampo(nombreElemento, valorValidar) {
    var regla = reglasValidacion[nombreElemento];
    if (!regla) return true;
    return regla.regex.test(valorValidar)
}

document.getElementById('txtUsuario').addEventListener('blur', function () {
    if (!validarCampo('usuario', this.value)) {
        mostrarMensajeError(this, 'usuario');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('txtContrasenna').addEventListener('blur', function () {
    if (!validarCampo('contrasena', this.value)) {
        mostrarMensajeError(this, 'contrasena');
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