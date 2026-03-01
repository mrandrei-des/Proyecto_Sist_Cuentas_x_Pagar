Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class MantenimientoProveedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceProveedores")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim modProveedor As New Models.Proveedor
        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = "", usuarioCreacion As String = "andre"

        modProveedor.TipoIdentificacion = CInt(ddlTipoIdentificacion.SelectedItem.Value)
        modProveedor.NumeroIdentificacion = txtIdentificacion.Text
        modProveedor.Nombre = txtNombre.Text
        modProveedor.Correo = txtCorreo.Text
        modProveedor.Estado = CInt(ddlEstado.SelectedItem.Value)

        If objProveedorDB.CrearProveedor(modProveedor, usuarioCreacion, errorMessage) Then
            SwalUtils.ShowSwal(Me, "¡Proveedor creado exitosamente!")
            limpiarCampos()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub limpiarCampos()
        ddlTipoIdentificacion.SelectedIndex = 0
        txtIdentificacion.Text = ""
        txtNombre.Text = ""
        txtCorreo.Text = ""
        ddlEstado.SelectedIndex = 0
    End Sub
End Class