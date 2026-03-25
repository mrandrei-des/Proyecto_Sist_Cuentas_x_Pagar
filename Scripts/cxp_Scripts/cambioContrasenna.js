document.getElementById('MainContent_btnCambioContrasenna').addEventListener('click', function (e) {
    e.preventDefault()
    let txtNuevaPassword = document.getElementById('MainContent_txtContrasenna');
    let txtConfirmPassword = document.getElementById('MainContent_txtConfirmarContrasenna');

    // NO SE APLICAN LOS ESTILOS POR UN TEMA DE ESPECIFICIDAD, SE DEBE REVISAR EL CSS PARA VER SI SE PUEDE SOLUCIONAR
    // EN EL ESTILO .formulario__input:valid, DEL ARCHIVO styles_formulario.css
    //if (txtNuevaPassword.value !== txtConfirmPassword.value) {
    //    txtNuevaPassword.classList.add('formulario__input--invalid');
    //    txtConfirmPassword.classList.add('formulario__input--invalid');
    //} else {
    //    txtNuevaPassword.classList.remove('formulario__input--invalid');
    //    txtConfirmPassword.classList.remove('formulario__input--invalid');
    //}

    // Aquí se deben aplicar las validaciones de la contraseña y confirmar constraseña, se puede utilizar expresiones regulares
})