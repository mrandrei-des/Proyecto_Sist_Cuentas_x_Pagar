Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class MantenimientoProveedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceProveedores")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            prcLlena_ddlTipoIdentificacion()
            prcLlena_ddlEstado()
        End If
    End Sub

    Private Sub limpiarCampos()
        ddlTipoIdentificacion.SelectedIndex = 0
        txtIdentificacion.Text = ""
        txtNombre.Text = ""
        txtCorreo.Text = ""
        ddlEstado.SelectedIndex = 0
    End Sub

    Private Sub prcLlena_ddlEstado()
        Dim listEstados As List(Of Models.Estado)
        Dim objEstadoDB As New EstadoDB
        Dim errorMessage As String = ""

        listEstados = objEstadoDB.ConsultarEstados(errorMessage)

        If listEstados Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listEstados.Count > 0 Then
            ddlEstado.Items.Clear()
            ddlEstado.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modEstado As Models.Estado In listEstados
                ddlEstado.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
            Next

            ddlEstado.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron estados en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcLlena_ddlTipoIdentificacion()
        Dim listTipoIdentificaciones As List(Of Models.TipoIdentificaciones)
        Dim objTipoIdentificacionBD As New TipoIdentificacionesDB
        Dim errorMessage As String = ""

        listTipoIdentificaciones = objTipoIdentificacionBD.ConsultarTipoIdentificaciones(errorMessage)

        If listTipoIdentificaciones Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listTipoIdentificaciones.Count > 0 Then
            ddlTipoIdentificacion.Items.Clear()
            ddlTipoIdentificacion.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modTipoIdentificacion As Models.TipoIdentificaciones In listTipoIdentificaciones
                ddlTipoIdentificacion.Items.Add(New ListItem(modTipoIdentificacion.Descripcion.ToString(), modTipoIdentificacion.IdTipo))
            Next

            ddlTipoIdentificacion.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron tipos de identificación en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim modProveedor As New Models.Proveedor
        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = "", usuarioCreacion As String = "andre"
        Dim tipoIdentificacion As Integer = CInt(ddlTipoIdentificacion.SelectedItem.Value)
        Dim numeroIdentificacion As String = txtIdentificacion.Text

        modProveedor = objProveedorDB.BuscarProveedor_x_Identificacion(tipoIdentificacion, numeroIdentificacion, errorMessage)

        If modProveedor Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If modProveedor Is New Models.Proveedor Then
            modProveedor = New Models.Proveedor With {
                .TipoIdentificacion = CInt(ddlTipoIdentificacion.SelectedItem.Value),
                .NumeroIdentificacion = txtIdentificacion.Text,
                .Nombre = txtNombre.Text,
                .Correo = txtCorreo.Text,
                .Estado = CInt(ddlEstado.SelectedItem.Value)
            }

            If objProveedorDB.CrearProveedor(modProveedor, usuarioCreacion, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Proveedor creado exitosamente!")
                limpiarCampos()
            Else
                SwalUtils.ShowSwalError(Me, errorMessage)
            End If
        Else
            SwalUtils.ShowSwalError(Me, "Ya existe un proveedor con ese tipo y número de identificación.")
        End If
    End Sub
End Class