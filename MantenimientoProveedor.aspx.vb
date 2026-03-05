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

    Private Sub prcLlena_ddlEstado()
        Dim listEstados As List(Of Models.Estado)
        Dim objEstadoDB As New EstadoDB
        Dim errorMessage As String = ""

        listEstados = objEstadoDB.ConsultarEstados(errorMessage)
        If listEstados.Count > 0 Then
            ddlEstado.Items.Clear()
            ddlEstado.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modEstado As Models.Estado In listEstados
                ddlEstado.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
            Next

            ddlEstado.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcLlena_ddlTipoIdentificacion()
        Dim listTipoIdentificaciones As List(Of Models.TipoIdentificaciones)
        Dim objTipoIdentificacionBD As New TipoIdentificacionesDB
        Dim errorMessage As String = ""

        listTipoIdentificaciones = objTipoIdentificacionBD.ConsultarTipoIdentificaciones(errorMessage)
        If listTipoIdentificaciones.Count > 0 Then
            ddlTipoIdentificacion.Items.Clear()
            ddlTipoIdentificacion.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modTipoIdentificacion As Models.TipoIdentificaciones In listTipoIdentificaciones
                ddlTipoIdentificacion.Items.Add(New ListItem(modTipoIdentificacion.Descripcion.ToString(), modTipoIdentificacion.IdTipo))
            Next

            ddlTipoIdentificacion.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub
End Class