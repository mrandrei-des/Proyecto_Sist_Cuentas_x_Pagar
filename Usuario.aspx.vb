Public Class Usuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim usuario As New Models.Usuario
        usuario.NombreUsuario = txtUsuario.Text
        usuario.Constrasenna = txtContrasenna.Text
        usuario.Nombre = txtNombre.Text
        usuario.Apellido1 = txtApellidoUno.Text

        usuario.Apellido2 = txtApellidoDos.Text
        usuario.Correo = txtCorreoUsuario.Text
        usuario.Estado = CInt(ddlEstadoUsuario.SelectedItem.Value)
    End Sub
End Class