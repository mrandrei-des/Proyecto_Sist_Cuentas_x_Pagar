Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CreacionDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceRegistroDocumentos")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
        If Not IsPostBack Then
            prcLlena_ddls()
        End If
    End Sub

    Private Sub prcLlena_ddls()
        prcLlena__ddlCategoria()
        Dim idCategoria As Integer = Convert.ToInt32(ddlCategoria.SelectedValue)
        prcLlena__ddlTipoDocumento(idCategoria)
        prcLlena__ddlProveedores()
        prcLlena_ddlMoneda()
    End Sub

    Private Sub prcLlena__ddlCategoria()
        Dim listCatDocumentos As List(Of Models.CategoriaDocumento)
        Dim objCatDocumentosDB As New CategoriaDocumentoDB
        Dim errorMessage As String = ""

        listCatDocumentos = objCatDocumentosDB.ConsultarCategoriaDocumento(errorMessage)

        If listCatDocumentos Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listCatDocumentos.Count > 0 Then
            ddlCategoria.Items.Clear()

            For Each modCatDocumento As Models.CategoriaDocumento In listCatDocumentos
                ddlCategoria.Items.Add(New ListItem(modCatDocumento.Descripcion.ToString(), modCatDocumento.IdCategoria))
            Next

            ddlCategoria.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se han encontrado Categorías de Documentos en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcLlena__ddlProveedores(Optional filtNombre As String = "")
        Dim listProveedores As List(Of Models.Proveedor)
        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = ""

        listProveedores = objProveedorDB.FiltrarProveedores_Nombre(filtNombre, errorMessage)
        If listProveedores Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listProveedores.Count > 0 Then
            ddlProveedor.Items.Clear()

            For Each modProveedor As Models.Proveedor In listProveedores
                ddlProveedor.Items.Add(New ListItem($"{modProveedor.NumeroProveedor} - {modProveedor.Nombre.ToString()}", modProveedor.NumeroProveedor))
            Next

            ddlProveedor.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalMessage(Me, "Consulta", "No se encontraron proveedores con los filtros utilizados.", "")
        End If
    End Sub

    Private Sub prcLlena__ddlTipoDocumento(idCategoria As Integer)
        Dim listTipoDocumento As List(Of Models.TipoDocumento)
        Dim objTipoDocumentosDB As New TipoDocumentoDB
        Dim errorMessage As String = ""

        listTipoDocumento = objTipoDocumentosDB.ConsultarTipoDocumento(idCategoria, errorMessage)

        If listTipoDocumento Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listTipoDocumento.Count > 0 Then
            ddlTipoDocumento.Items.Clear()

            ddlTipoDocumento.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modTipoDocumento As Models.TipoDocumento In listTipoDocumento
                ddlTipoDocumento.Items.Add(New ListItem(modTipoDocumento.Descripcion.ToString(), modTipoDocumento.IdCategoria))
            Next

            ddlTipoDocumento.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron tipos de documento en el sistema. Revise la configuración correspondiente.")
        End If

    End Sub

    Private Sub prcLlena_ddlMoneda()
        Dim listMonedas As List(Of Models.Moneda)
        Dim objMonedaDB As New MonedaDB
        Dim errorMessage As String = ""

        listMonedas = objMonedaDB.ConsultarMonedas(errorMessage)

        If listMonedas Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listMonedas.Count > 0 Then
            ddlMoneda.Items.Clear()
            ddlMoneda.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modMoneda As Models.Moneda In listMonedas
                Dim textoMoneda As String = $"{modMoneda.Descripcion} - {modMoneda.Simbolo}"
                ddlMoneda.Items.Add(New ListItem(textoMoneda, modMoneda.CodigoMoneda.ToString()))
            Next

            ddlMoneda.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron monedas en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim idCategoria As Integer = Convert.ToInt32(ddlCategoria.SelectedValue)
        Dim strCategoria As String = ddlCategoria.SelectedItem.Text
        prcLlena__ddlTipoDocumento(idCategoria)

        prcCambiaTextoEtiquetas(strCategoria.ToLower())
    End Sub

    Protected Sub txtFiltProveedor_TextChanged(sender As Object, e As EventArgs)
        Dim filtNombre As String = txtFiltProveedor.Text.Trim()
        prcLlena__ddlProveedores(filtNombre)
    End Sub

    Private Sub prcCambiaTextoEtiquetas(tipoDocumento As String)
        lblTipoDocumento.Text = $"Tipo de {tipoDocumento}:"
        lblNumeroDocumento.Text = $"Número de {tipoDocumento}:"
        lblFechaEmision.Text = $"Fecha de emisión {tipoDocumento}:"
        lblMonto.Text = $"Monto total {tipoDocumento}:"
    End Sub

    Private Sub prcLimpiarCampos()
        prcLlena_ddls()
        txtFiltProveedor.Text = ""
        txtNumDocumento.Text = ""
        txtFechaEmision.Text = ""
        txtAreaObservacion.InnerText = ""
        txtMontoTotal.Text = ""

        'btnChkInput.CssClass = "contenedor__btnCircle"
    End Sub

    Private Function ExisteDocumento(ByRef errorMessage) As Boolean

        If ddlCategoria.SelectedValue.IsNullOrWhiteSpace() Or ddlTipoDocumento.SelectedValue.IsNullOrWhiteSpace() Or ddlProveedor.SelectedValue.IsNullOrWhiteSpace() Or txtNumDocumento.Text.IsNullOrWhiteSpace() Then
            Return False
        End If

        Dim categoriaDocumento As Integer = Convert.ToInt32(ddlCategoria.SelectedValue)
        Dim tipoDocumento As Integer = Convert.ToInt32(ddlTipoDocumento.SelectedValue)
        Dim idProveedor As Integer = Convert.ToInt32(ddlProveedor.SelectedValue)
        Dim numeroDocumento As String = txtNumDocumento.Text

        If categoriaDocumento = 1 Then ' Revisa si existe la factura
            Dim modFactura As New Models.Factura
            Dim objFactura As New FacturaDB

            With modFactura
                .IdProveedor = idProveedor
                .TipoFactura = tipoDocumento
                .NumeroFactura = numeroDocumento
            End With

            modFactura = objFactura.ConsultarExistenciaFactura(modFactura, errorMessage)

            If modFactura Is Nothing Then
                Return True ' No se pudo completar el proceso
            End If

            If modFactura.NumeroFactura Is Nothing Then
                Return False ' No existe la factura
            Else
                Return True ' Sí existe la factura
            End If
        Else ' Revisa si existe el documento de pago
            Return True
        End If
    End Function

    Private Function ContinuarProcesoGuardado() As Boolean
        Dim errorMessage As String = ""
        Dim respuestaExistencia = ExisteDocumento(errorMessage)

        If respuestaExistencia And errorMessage <> "" Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return False
        End If

        If respuestaExistencia Then
            SwalUtils.ShowSwalMessage(Me, "Consulta", "Ya existe una factura de este tipo para el proveedor.", "warning")
            Return False
        End If

        Return True
    End Function

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        prcLimpiarCampos()
    End Sub

    Protected Sub btnAplicar_Click(sender As Object, e As EventArgs)
        'Lo que hace es hacer un update a los datos y al final cambia el estado de la factura para que aparezca para ser cancelada
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        'Lo que hace es hacer un insert con el estato pendiente
        If Not ContinuarProcesoGuardado() Then
            Return
        End If

        Dim categoriaDocumento As Integer = Convert.ToInt32(ddlCategoria.SelectedValue)
        Dim usuarioInserta As String = "andre", errorMessage As String = ""

        If categoriaDocumento = 1 Then ' Se está guardando una factura
            Dim modFactura As New Models.Factura
            Dim objFactura As New FacturaDB

            With modFactura
                .IdProveedor = Convert.ToInt32(ddlProveedor.SelectedValue)
                .TipoFactura = Convert.ToInt32(ddlTipoDocumento.SelectedValue)
                .NumeroFactura = txtNumDocumento.Text.Trim()
                .Observacion = txtAreaObservacion.Value.ToString().Trim()
                .FechaEmision = txtFechaEmision.Text
                .Estado = 1
                .Moneda = ddlMoneda.SelectedValue.ToString()
                .TipoCambio = 500
                .Total = Convert.ToDouble(txtMontoTotal.Text)
                .SaldoActual = Convert.ToDouble(txtMontoTotal.Text)
            End With

            If objFactura.CrearFactura(modFactura, usuarioInserta, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Factura guardada exitosamente!", "El documento está listo para ser aplicado.")
                ' Ocultar el botón de guardar y habilitar el de modificar

            Else ' Se está guardando un documento de pago
                SwalUtils.ShowSwalError(Me, "Atención", $"No se logró guardar la factura. {errorMessage}")
            End If
        End If

    End Sub

    Protected Sub txtNumDocumento_TextChanged(sender As Object, e As EventArgs)

        'If ExisteDocumento() Then
        '    ' Tomar el modelo del documento y valorar el estado, para ver si puede modificarlo, aplicarlo o si está eliminado
        '    SwalUtils.ShowSwalError(Me, "Ya existe una factura de este tipo para el proveedor.")
        'End If
    End Sub
End Class