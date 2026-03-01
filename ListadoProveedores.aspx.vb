Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class ListadoProveedores
    Inherits System.Web.UI.Page
    Dim modProveedor As New Models.Proveedor
    Dim dbProveedor As New ProveedorDB
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceProveedores")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
    End Sub

    Protected Sub btnModificarProveedor_Click(sender As Object, e As EventArgs)
        Dim errorMessage As String = ""
        Dim nombreUsuarioEjecutaAccion = "andre"
        modProveedor.NumeroProveedor = 0
        modProveedor.Nombre = txtNombre.Text
        modProveedor.Correo = txtCorreo.Text
        modProveedor.Estado = CInt(ddlEstado.SelectedItem.Value)

        If dbProveedor.ModificarProveedor(modProveedor, nombreUsuarioEjecutaAccion, errorMessage) Then
            prcLimpiarModal()
            modalModify.Style.Remove("display")
            SwalUtils.ShowSwal(Me, "¡Proveedor modificado exitosamente!")
            gvProveedores.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Protected Sub btnCerrarModal_Click(sender As Object, e As EventArgs)
        modalModify.Style.Remove("display")
    End Sub

    Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs)
        prcLimpiarFiltros()
    End Sub

    Private Sub prcLimpiarFiltros()
        ddlFiltTipoIdentificacion.SelectedIndex = 0
        ddlFiltEstado.SelectedIndex = 0
        txtFiltNombre.Text = ""
    End Sub

    Private Sub prcLimpiarModal()
        txtNombre.Text = ""
        txtCorreo.Text = ""
        ddlEstado.SelectedIndex = 4
    End Sub
    Private Sub prcModificarProveedor(idProveedorAfectado As Integer)
        ' Aquí se debe ir a consultar los datos del proveedor a la base de datos para poder cargarlos en pantalla y a la hora de ver el modal se despliegue con los datos del proveedor

        txtNombre.Text = "Andre"
        txtCorreo.Text = "andrei@corre.com"
        ddlEstado.SelectedValue = 4
        Dim identificacionProveedor As String = "3-101-253621 Dos Pinos S.A."

        pSubtituloModal.InnerHtml = "Proveedor: <span>" + identificacionProveedor + "<span>"
        modalModify.Style.Add("display", "flex")
    End Sub

    Private Sub prcEliminarProveedor(idProveedorAfectado As Integer, usuarioElimino As String)
        Dim errorMessage As String = ""

        If dbProveedor.EliminarProveedor(idProveedorAfectado, usuarioElimino, errorMessage) Then
            SwalUtils.ShowSwal(Me, "¡El proveedor [" + idProveedorAfectado + "] ha sido eliminado del sistema!")
            gvProveedores.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Protected Sub gvProveedores_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim indexColIDProveedor = Convert.ToInt32(e.CommandArgument)
        Dim idProveedorAfectado = Convert.ToInt32(gvProveedores.DataKeys(indexColIDProveedor).Value)
        Dim nombreUsuarioEjecutaAccion = "andre"

        Select Case e.CommandName
            Case "EditarProveedor"
                prcModificarProveedor(idProveedorAfectado)

            Case "EliminarProveedor"
                prcEliminarProveedor(idProveedorAfectado, nombreUsuarioEjecutaAccion)
        End Select
    End Sub
End Class