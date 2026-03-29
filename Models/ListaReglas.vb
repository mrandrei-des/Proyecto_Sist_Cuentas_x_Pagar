Public Class ListaReglas
    Private _listaReglas As List(Of ValidacionRegex)

    Public Sub New()
        CrearListaReglas()
    End Sub

    Public Property ListaReglas As List(Of ValidacionRegex)
        Get
            Return _listaReglas
        End Get
        Set(value As List(Of ValidacionRegex))
            _listaReglas = value
        End Set
    End Property

    Private Sub CrearListaReglas()

        Dim lista As New List(Of ValidacionRegex)()
        lista.Add(New ValidacionRegex("nombreProveedor", New Regex("^[\w\s]+$"), "Solo se permiten letras, números y espacios."))
        lista.Add(New ValidacionRegex("numDocumento", New Regex("^[0-9]+[-]?[0-9]+$"), "Solo se permiten números y un guión medio."))
        lista.Add(New ValidacionRegex("montoDocumento", New Regex("^[0-9]+[,]?[0-9]{1,3}$"), "Ingrese un monto válido con 3 decimales máximo."))
        lista.Add(New ValidacionRegex("observacion", New Regex("^[\wáéíóúÁÉÍÓÚ.\s]+$"), "Solo se permite números, letras, punto y espacios."))
        lista.Add(New ValidacionRegex("nombres", New Regex("^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$"), "Solo se permiten letras y espacios."))
        lista.Add(New ValidacionRegex("identificacion", New Regex("^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ]+$"), "Solo se permiten letras y números."))
        lista.Add(New ValidacionRegex("usuario", New Regex("^[\w]+$"), "Solo se permiten letras."))
        lista.Add(New ValidacionRegex("enteroDDL", New Regex("^\d+$"), "Debe seleccionar una opción válida."))
        lista.Add(New ValidacionRegex("correo", New Regex("^[\w._-]+@[\w.]+.[a-zA-Z]{2,4}$"), "El dato ingresado no es un correo válido. (correo@correo.com)"))
        lista.Add(New ValidacionRegex("contrasena", New Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%.*?&])[A-Za-z\d@$!%.*?&]{8,15}$"), "Mínimo 8 caracteres Máximo 15. Al menos una letra mayúscula, una letra minúscula, un número y un carácter especial. (@$!%.*?&)"))
        ListaReglas = lista
    End Sub

    Public Function ObtenerReglaPorCampo(nombreCampo As String) As ValidacionRegex
        For Each regla As ValidacionRegex In ListaReglas
            If regla.NombreCampo.Equals(nombreCampo) Then
                Return regla
            End If
        Next
        Return Nothing
    End Function

    Public Function ValidarCampo(modRegla As ValidacionRegex, valorValidar As String) As Boolean
        Return modRegla.Regla.IsMatch(valorValidar)
    End Function
End Class
