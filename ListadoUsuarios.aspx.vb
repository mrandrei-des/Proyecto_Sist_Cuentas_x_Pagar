Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class WebForm1
    Inherits System.Web.UI.Page
    Private dbUsuario As New UsuarioDB

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRestFiltros_Click(sender As Object, e As EventArgs)
        prcLimpiarFiltros()
    End Sub

    Private Sub prcLimpiarFiltros()
        txtFiltNombre.Text = ""
        ddlFiltEstado.SelectedIndex = 0
        ddlFiltRoles.SelectedIndex = 0
    End Sub

    ' Se usa este evento para que en la misma celda del grid estén los dos botones
    ' Lo que hace es, cada botón tiene su respectivo CommandName Eliminar/Modificar que además, trae el CommandArgument que es donde viene el nombre del usuario de la fila donde se hizo click al botón
    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        ' Se captura el argumento que trae cada botón, en este caso el nombre del usuario. Esto porque tanto modificar como eliminar ocupan ese dato
        Dim nombreUsuarioEjecuto As String = "andre", nombreUsuarioAfectado As String = e.CommandArgument

        ' Se revisa cuál de las 2 acciones fue la que invocó al evento
        If e.CommandName = "AccionEliminar" Then
            prcEliminarUsuario(nombreUsuarioAfectado, nombreUsuarioEjecuto)
        Else
            prcModificarUsuario(nombreUsuarioAfectado)
        End If
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

    'Protected Sub gvUsuarios_RowEditing(sender As Object, e As GridViewEditEventArgs)
    '    e.Cancel = True
    '    Dim estUsuario As New Usuario
    '    Dim errorMessage As String = "", nombreUsuarioModifico As String = "andre"

    '    Dim nombreUsuarioAfecto As String = gvUsuarios.DataKeys(e.NewEditIndex).Value
    '    'estUsuario = dbUsuario.ConsultarUsuario(nombreUsuarioAfecto, errorMessage)
    'End Sub

    Protected Sub btnCerrarModal_Click(sender As Object, e As EventArgs)
        modalModify.Style.Remove("display")
    End Sub

    Protected Sub btnModificarUsuario_Click(sender As Object, e As EventArgs)
        'Procede a ejecutar los cambios al usuario

    End Sub

End Class