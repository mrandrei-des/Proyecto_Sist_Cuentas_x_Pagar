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

    Protected Sub gvUsuarios_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        e.Cancel = True
        Dim errorMessage As String = "", nombreUsuarioElimino As String = "andre"

        Dim nombreUsuarioAfecto As String = gvUsuarios.DataKeys(e.RowIndex).Value

        If dbUsuario.EliminarUsuario(nombreUsuarioAfecto, nombreUsuarioElimino, errorMessage) Then
            SwalUtils.ShowSwal(Me, "¡El usuario [" + nombreUsuarioAfecto + "] ha sido eliminado del sistema!")
            gvUsuarios.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Protected Sub gvUsuarios_RowEditing(sender As Object, e As GridViewEditEventArgs)
        e.Cancel = True
        Dim estUsuario As New Usuario
        Dim errorMessage As String = "", nombreUsuarioModifico As String = "andre"

        Dim nombreUsuarioAfecto As String = gvUsuarios.DataKeys(e.NewEditIndex).Value
        'estUsuario = dbUsuario.ConsultarUsuario(nombreUsuarioAfecto, errorMessage)
    End Sub
End Class