Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CambioContrasenna
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnCambioContrasenna_Click(sender As Object, e As EventArgs)
        Dim nuevaContrasenna As String = txtContrasenna.Text
        Dim confirmarContrasenna As String = txtConfirmarContrasenna.Text

        contenedorMensajesConfirmContrasenna.InnerHtml = ""
        If nuevaContrasenna = confirmarContrasenna Then
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
        Else
            'SwalUtils.ShowSwalError(Me, "Las contraseñas no coinciden. Por favor, inténtalo de nuevo.")
            contenedorMensajesConfirmContrasenna.InnerHtml &= "<p class=""formulario__mensaje"">Las contraseñas no coinciden.</p>"
            txtContrasenna.Style.Add("border-botom-color", "#f00")
            txtConfirmarContrasenna.Style.Add("border-botom-color", "#f00")
        End If
    End Sub
End Class