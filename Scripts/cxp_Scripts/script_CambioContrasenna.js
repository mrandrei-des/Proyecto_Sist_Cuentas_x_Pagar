var reglasValidacion = {
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

document.getElementById('MainContent_txtContrasenna').addEventListener('blur', function () {
    if (!validarCampo('contrasena', this.value)) {
        mostrarMensajeError(this, 'contrasena');
    } else {
        limpiarMensajesError(this);
    }
});

document.getElementById('MainContent_txtConfirmarContrasenna').addEventListener('blur', function () {
    if (!validarCampo('contrasena', this.value)) {
        mostrarMensajeError(this, 'contrasena');
    } else {
        limpiarMensajesError(this);
    }

    var contrasena = document.getElementById('MainContent_txtContrasenna').value;
    if (contrasena != this.value) {
        var contenedorMensajes = this.parentElement.querySelector('.formulario__contenedor-mensajes');
        var itemMensaje = document.createElement('p');
        itemMensaje.className = 'formulario__mensaje';
        itemMensaje.innerHTML = "Las contraseñas no coinciden.";

        contenedorMensajes.appendChild(itemMensaje);
        contenedorMensajes.style.display = 'block';
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