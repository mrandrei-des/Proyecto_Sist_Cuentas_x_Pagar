Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class WebForm1
    Inherits System.Web.UI.Page
    Private dbUsuario As New UsuarioDB

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub prcLimpiarFiltros()
        txtFiltNombre.Text = ""
        ddlFiltEstado.SelectedIndex = 0
        ddlFiltRoles.SelectedIndex = 0
    End Sub

    Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs)
        prcLimpiarFiltros()
    End Sub

    Private Sub prcEliminarUsuario(nombreUsuarioAfectado As String, nombreUsuarioElimino As String)
        Dim errorMessage As String = ""

        If dbUsuario.EliminarUsuario(nombreUsuarioAfectado, nombreUsuarioElimino, errorMessage) Then
            SwalUtils.ShowSwal(Me, "¡El usuario [" + nombreUsuarioAfectado + "] ha sido eliminado del sistema!")
            gvUsuarios.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcModificarUsuario(nombreUsuarioAfectado As String)
        ' Aquí se debe ir a consultar los datos del usuario a la base de datos para poder cargarlos en pantalla y a la hora de ver el modal se despliegue con los datos del usuario
        txtNombre.Text = "Andre"
        txtApellidoUno.Text = "Mesén"
        txtApellidoDos.Text = "Romero"
        txtCorreoUsuario.Text = "amense@gmaolc.com"
        ddlEstadoUsuario.SelectedValue = 3
        ddlRoles.SelectedValue = 2
        modalModify.Style.Add("display", "flex")
    End Sub

    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim indexColNombreUsuario = Convert.ToInt32(e.CommandArgument)
        Dim nombreUsuarioAfectado = gvUsuarios.DataKeys(indexColNombreUsuario).Value
        Dim nombreUsuarioEjecutaAccion = "andre"

        Select Case e.CommandName
            Case "EditarUsuario"
                prcModificarUsuario(nombreUsuarioAfectado)

            Case "EliminarUsuario"
                prcEliminarUsuario(nombreUsuarioAfectado, nombreUsuarioEjecutaAccion)
        End Select

    End Sub
    Protected Sub btnCerrarModal_Click(sender As Object, e As EventArgs) Handles btnCerrarModal.Click
        modalModify.Style.Remove("display")
    End Sub

    Private Sub prcLimpiarModal()
        txtNombre.Text = ""
        txtApellidoUno.Text = ""
        txtApellidoDos.Text = ""
        txtCorreoUsuario.Text = ""
        ddlEstadoUsuario.SelectedIndex = 0
        ddlRoles.SelectedIndex = 0
    End Sub

    Protected Sub btnModificarUsuario_Click(sender As Object, e As EventArgs)
        Dim modUsuario As New Models.Usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = ""
        Dim nombreUsuarioEjecutaAccion = "andre"

        modUsuario.NombreUsuario = "" 'txtUsuario.Text
        modUsuario.Nombre = txtNombre.Text
        modUsuario.Apellido1 = txtApellidoUno.Text
        modUsuario.Apellido2 = txtApellidoDos.Text
        modUsuario.Correo = txtCorreoUsuario.Text
        modUsuario.Estado = CInt(ddlEstadoUsuario.SelectedItem.Value)
        modUsuario.Rol = CInt(ddlRoles.SelectedItem.Value)

        If objUsuarioDB.ModificarUsuario(modUsuario, nombreUsuarioEjecutaAccion, errorMessage) Then
            prcLimpiarModal()
            modalModify.Style.Remove("display")
            SwalUtils.ShowSwal(Me, "¡Usuario modificado exitosamente!")
            gvUsuarios.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub
End Class