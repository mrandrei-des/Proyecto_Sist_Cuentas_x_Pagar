Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class ListadoProveedores
    Inherits System.Web.UI.Page

    '' INICIO EVENTOS DE LA PÁGINA
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceProveedores")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            prcLlena_ddlFiltTipoIdentificacion()
            prcLlena_ddls_Estado()
        End If
    End Sub

    Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs)
        prcLimpiarFiltros()
    End Sub

    Protected Sub gvProveedores_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim indexColIDProveedor = Convert.ToInt32(e.CommandArgument)
        Dim idProveedorAfectado = Convert.ToInt32(gvProveedores.DataKeys(indexColIDProveedor).Value)
        Dim nombreUsuarioEjecutaAccion = "andre"

        ' Guardamos el id del proveedor seleccionado en un campo oculto para usarlo posteriormente en el evento de modificación, ya que al abrir el modal se pierde la referencia del rowCommand y por ende del id del proveedor afectado, por lo que al guardarlo en un campo oculto, este valor se mantiene disponible para el evento de modificación
        hfIdProveedor.Value = idProveedorAfectado

        Select Case e.CommandName
            Case "EditarProveedor"
                prcCargaProveedorModal(idProveedorAfectado)

            Case "EliminarProveedor"
                prcEliminarProveedor(idProveedorAfectado, nombreUsuarioEjecutaAccion)
        End Select
    End Sub

    Protected Sub btnCerrarModal_Click(sender As Object, e As EventArgs)
        modalModify.Style.Remove("display")
    End Sub

    Protected Sub btnModificarProveedor_Click(sender As Object, e As EventArgs)
        Dim modProveedor As New Models.Proveedor
        Dim objProveedorDB As New ProveedorDB

        Dim errorMessage As String = ""
        Dim nombreUsuarioEjecutaAccion = "andre"

        Dim idProveedorAfectado As Integer = Convert.ToInt32(hfIdProveedor.Value)

        ' modProveedor.NumeroProveedor En el evento de rowCommand se le asigna el id del proveedor afectado, por lo que aquí ya se tiene el id del proveedor a modificar
        modProveedor.NumeroProveedor = idProveedorAfectado
        modProveedor.Nombre = txtNombre.Text
        modProveedor.Correo = txtCorreo.Text

        ' Validar porque no está tomando el valor del ddlEstado que se cargó previamente 
        modProveedor.Estado = CInt(ddlEstado.SelectedItem.Value)

        If objProveedorDB.ModificarProveedor(modProveedor, nombreUsuarioEjecutaAccion, errorMessage) Then
            prcLimpiarModal()
            modalModify.Style.Remove("display")
            SwalUtils.ShowSwal(Me, "¡Proveedor modificado exitosamente!")
            gvProveedores.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub



    '' FIN EVENTOS DE LA PÁGINA

    '' INICIO DE FUNCIONES Y MÉTODOS DE LA PÁGINA
    Private Sub prcLimpiarFiltros()
        ddlFiltTipoIdentificacion.SelectedIndex = 0
        ddlFiltEstado.SelectedIndex = 0
        txtFiltNombre.Text = ""
    End Sub

    Private Sub prcLimpiarModal()
        txtNombre.Text = ""
        txtCorreo.Text = ""
        ddlEstado.SelectedIndex = 0
    End Sub

    Private Sub prcCargaProveedorModal(idProveedorAfectado As Integer)
        Dim objProveedorBD As New ProveedorDB
        Dim modProveedor As Models.Proveedor
        Dim errorMessage As String = ""

        modProveedor = objProveedorBD.ConsultarProveedor_x_ID(idProveedorAfectado, errorMessage)
        If modProveedor IsNot Nothing Then
            Dim proveedor As String = "" & modProveedor.NumeroIdentificacion & " - " & modProveedor.Nombre
            txtNombre.Text = modProveedor.Nombre
            txtCorreo.Text = modProveedor.Correo
            ddlEstado.SelectedValue = modProveedor.Estado

            pSubtituloModal.InnerHtml = "Proveedor: <span>" + proveedor + "<span>"
            modalModify.Style.Add("display", "flex")
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcEliminarProveedor(idProveedorAfectado As Integer, usuarioElimino As String)
        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = ""

        If objProveedorDB.EliminarProveedor(idProveedorAfectado, usuarioElimino, errorMessage) Then
            SwalUtils.ShowSwal(Me, "¡El proveedor [" + idProveedorAfectado + "] ha sido eliminado del sistema!")
            gvProveedores.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcLlena_ddls_Estado()
        Dim listEstados As List(Of Models.Estado)
        Dim objEstadoDB As New EstadoDB
        Dim errorMessage As String = ""

        listEstados = objEstadoDB.ConsultarEstados(errorMessage)
        If listEstados.Count > 0 Then
            ddlEstado.Items.Clear()
            ddlFiltEstado.Items.Clear()

            ddlEstado.Items.Add(New ListItem("Seleccione una opción", ""))
            ddlFiltEstado.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modEstado As Models.Estado In listEstados
                ddlEstado.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
                ddlFiltEstado.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
            Next

            ddlEstado.SelectedIndex = 0
            ddlFiltEstado.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcLlena_ddlFiltTipoIdentificacion()
        Dim listTipoIdentificaciones As List(Of Models.TipoIdentificaciones)
        Dim objTipoIdentificacionBD As New TipoIdentificacionesDB
        Dim errorMessage As String = ""

        listTipoIdentificaciones = objTipoIdentificacionBD.ConsultarTipoIdentificaciones(errorMessage)
        If listTipoIdentificaciones.Count > 0 Then
            ddlFiltTipoIdentificacion.Items.Clear()
            ddlFiltTipoIdentificacion.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modTipoIdentificacion As Models.TipoIdentificaciones In listTipoIdentificaciones
                ddlFiltTipoIdentificacion.Items.Add(New ListItem(modTipoIdentificacion.Descripcion.ToString(), modTipoIdentificacion.IdTipo))
            Next

            ddlFiltTipoIdentificacion.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub ddlFiltEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltEstado.SelectedIndexChanged

        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = ""
        Dim filtNombre As String = ""
        Dim filtTipoIdentificacion As Integer = 0, filtEstado As Integer = 0
        Dim dtResultados As DataTable

        filtTipoIdentificacion = prcDevuelveParametroFiltro_int(ddlFiltTipoIdentificacion.SelectedValue)
        filtNombre = prcDevuelveParametroFiltro_str(txtFiltNombre.Text)
        filtEstado = prcDevuelveParametroFiltro_int(ddlFiltEstado.SelectedValue)

        dtResultados = objProveedorDB.FiltrarProveedores(filtTipoIdentificacion, filtNombre, filtEstado, errorMessage)
        If dtResultados IsNot Nothing Then
            gvProveedores.DataSource = dtResultados
            gvProveedores.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If

    End Sub

    Private Function prcDevuelveParametroFiltro_int(valorFiltro As String) As Integer
        If valorFiltro.IsNullOrWhiteSpace() Then
            Return 0
        Else
            Return CInt(valorFiltro)
        End If
    End Function

    Private Function prcDevuelveParametroFiltro_str(valorFiltro As String) As String
        If valorFiltro.IsNullOrWhiteSpace() Then
            Return ""
        Else
            Return valorFiltro
        End If
    End Function
    '' FIN DE FUNCIONES Y MÉTODOS DE LA PÁGINA
End Class