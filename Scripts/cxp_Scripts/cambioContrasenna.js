document.getElementById('MainContent_btnCambioContrasenna').addEventListener('click', function (e) {
    e.preventDefault()
    let txtNuevaPassword = document.getElementById('MainContent_txtContrasenna');
    let txtConfirmPassword = document.getElementById('MainContent_txtConfirmarContrasenna');

    if (txtNuevaPassword.value !== txtConfirmPassword.value) {
        txtNuevaPassword.classList.add('formulario__input--invalid');
        txtConfirmPassword.classList.add('formulario__input--invalid');
    } else {
        txtNuevaPassword.classList.remove('formulario__input--invalid');
        txtConfirmPassword.classList.remove('formulario__input--invalid');
    }
})