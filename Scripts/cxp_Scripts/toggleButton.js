const chkInput = document.getElementById('MainContent_chkInput')
const btnChkInput = document.getElementById('MainContent_btnChkInput')
const lblchkInput = document.getElementById('MainContent_lblEstado')

lblchkInput.addEventListener('click', (e) => {
    e.preventDefault()

    // chkInput.checked = chkInput.checked ? false : true
    // Si está marcado y presiono el botón, entonces lo desmarca
    if (chkInput.checked) {
        chkInput.checked = false
        btnChkInput.classList.remove('btnCircle__active')
    } else {
        chkInput.checked = true
        btnChkInput.classList.toggle('btnCircle__active')
    }
})
