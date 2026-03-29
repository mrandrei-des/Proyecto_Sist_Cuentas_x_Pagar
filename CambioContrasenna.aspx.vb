Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CambioContrasenna
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnCambioContrasenna_Click(sender As Object, e As EventArgs)
        Dim encryptor As New Simple3Des("def_.phas7401{}pinna??¿")
        Dim nuevaContrasenna As String = txtContrasenna.Text.Trim()
        Dim confirmarContrasenna As String = txtConfirmarContrasenna.Text.Trim()

        ' Valida que los datos estén correctos, para el momento de hacer la conversión
        If Not ValidarDatos(nuevaContrasenna, confirmarContrasenna) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        confirmarContrasenna = encryptor.EncryptData(txtConfirmarContrasenna.Text.Trim())

        Dim modUsuario As New Models.Usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = ""
        modUsuario.NombreUsuario = "andre" 'Session("NombreUsuario")
        modUsuario.Contrasenna = confirmarContrasenna

        If objUsuarioDB.CambioContrasenna(modUsuario, errorMessage) Then
            SwalUtils.ShowSwal(Me, "Contraseña cambiada exitosamente.")
        Else
            SwalUtils.ShowSwalError(Me, "Error al cambiar la contraseña: " & errorMessage)
        End If
    End Sub

    Private Function ValidarDatos(nuevaContrasenna As String, confirmarContrasenna As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True
        contenedorMensajesContrasenna.InnerHtml = ""
        contenedorMensajesConfirmContrasenna.InnerHtml = ""


        modRegla = objListaReglas.ObtenerReglaPorCampo("contrasena")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, nuevaContrasenna) Then
                contenedorMensajesContrasenna.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesContrasenna.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesContrasenna.Style.Remove("display")
            End If
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("contrasena")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, confirmarContrasenna) Then
                contenedorMensajesConfirmContrasenna.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesConfirmContrasenna.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesConfirmContrasenna.Style.Remove("display")
            End If
        End If

        If nuevaContrasenna <> confirmarContrasenna Then
            contenedorMensajesConfirmContrasenna.InnerHtml += $"<p class='formulario__mensaje'>Las contraseñas no coinciden.</p>"
            contenedorMensajesConfirmContrasenna.Style.Add("display", "block")
            respuestaValidacion = False
        End If

        Return respuestaValidacion
    End Function
End Class