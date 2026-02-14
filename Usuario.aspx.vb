Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class Usuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim modUsuario As New Models.Usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = ""

        modUsuario.NombreUsuario = txtUsuario.Text
        modUsuario.Constrasenna = txtContrasenna.Text
        modUsuario.Nombre = txtNombre.Text
        modUsuario.Apellido1 = txtApellidoUno.Text
        modUsuario.Apellido2 = txtApellidoDos.Text
        modUsuario.Correo = txtCorreoUsuario.Text
        modUsuario.Estado = CInt(ddlEstadoUsuario.SelectedItem.Value)
        modUsuario.Rol = CInt(ddlRoles.SelectedItem.Value)

        If objUsuarioDB.CrearUsuario(modUsuario, errorMessage) Then
            SwalUtils.ShowSwal(Me, "¡Usuario creado exitosamente!")
            limpiarCampos()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub limpiarCampos()
        txtUsuario.Text = ""
        txtContrasenna.Text = ""
        txtNombre.Text = ""
        txtApellidoUno.Text = ""
        txtApellidoDos.Text = ""
        txtCorreoUsuario.Text = ""
        ddlEstadoUsuario.SelectedIndex = 0
        ddlRoles.SelectedIndex = 0
    End Sub
End Class