Imports Antlr.Runtime.Tree
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CreacionDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceRegistroDocumentos")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
        If Not IsPostBack Then
            prcLlena_ddls()
            btnModificar.CssClass = "boton boton__modificar boton__ocultar"
            btnAplicar.CssClass = "boton boton__aplicar boton__ocultar"
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
        btnGuardar.CssClass = "boton boton__guardar"
        btnModificar.CssClass = "boton boton__modificar boton__ocultar"
        btnAplicar.CssClass = "boton boton__aplicar boton__ocultar"
        ddlCategoria.Enabled = True
        ddlTipoDocumento.Enabled = True
        txtFiltProveedor.Enabled = True
        ddlProveedor.Enabled = True
        txtNumDocumento.Enabled = True
    End Sub

    Protected Sub txtNumDocumento_TextChanged(sender As Object, e As EventArgs)
        ' Variables y validaciones para ver si el documento existe o si alguien lo eliminó
        Dim idCategoriaDocumento As String = ddlCategoria.SelectedValue, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        Dim idProveedor As String = ddlProveedor.SelectedValue
        Dim numDocumento As String = txtNumDocumento.Text.ToString().Trim()
        Dim errorMessage As String = ""

        If TieneDatos_IdentificadoresDocumento(idCategoriaDocumento, idTipoDocumento, idProveedor, numDocumento) Then
            If Not ValidaDatosExistencia(idCategoriaDocumento, idProveedor, idTipoDocumento, numDocumento) Then
                SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
                Return
            End If

            Dim nombreDocumento As String = IIf(Convert.ToInt32(idCategoriaDocumento) = 1, "la factura", "el documento de pago")

            Try
                Dim respuestaExistencia As Boolean = ExisteDocumento(Convert.ToInt32(idCategoriaDocumento), Convert.ToInt32(idProveedor), Convert.ToInt32(idTipoDocumento), numDocumento, errorMessage)

                If respuestaExistencia Then
                    SwalUtils.ShowSwalMessage(Me, "Consulta", $"Ya existe {nombreDocumento} con el número {numDocumento} para este proveedor.", "warning")
                    txtNumDocumento.Focus()
                End If

                'Si no la encontró no hay problema, puede guardarla

            Catch ex As Exception
                SwalUtils.ShowSwalError(Me, "Atención", $"No se logró encontrar {nombreDocumento}. {errorMessage}.")
                Return
            End Try
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim idCategoriaDocumento As String = ddlCategoria.SelectedValue, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        Dim idProveedor As String = ddlProveedor.SelectedValue
        Dim numDocumento As String = txtNumDocumento.Text.ToString()

        'Valida que el documento no haya sido registrado previamente, aquí revisa hasta dentro de los que fueron eliminados
        If Not ContinuarProcesoGuardado(idCategoriaDocumento, idTipoDocumento, idProveedor, numDocumento) Then
            Return
        End If

        ' Variables para las validaciones y proceso de guardado
        Dim observacion As String = txtAreaObservacion.Value.ToString()
        Dim fechaEmision As String = txtFechaEmision.Text
        Dim moneda As String = ddlMoneda.SelectedValue.ToString(), montoTotal As String = txtMontoTotal.Text, saldoActual = txtMontoTotal.Text

        ' Valida que los datos estén correctos, para el momento de hacer la conversión
        If Not ValidarDatos(idCategoriaDocumento, idProveedor, idTipoDocumento, numDocumento, observacion, fechaEmision, moneda, montoTotal, saldoActual) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        Dim categoriaDocumento As Integer = Convert.ToInt32(idCategoriaDocumento)
        Dim usuarioInserta As String = "andre", errorMessage As String = "", nombreDocumento As String = ""
        Dim respuestaCreacion As Boolean

        If categoriaDocumento = 1 Then ' Se está guardando una factura
            Dim modFactura As New Models.Factura
            Dim objFactura As New FacturaDB
            nombreDocumento = "Factura guardada"

            With modFactura
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoFactura = Convert.ToInt32(idTipoDocumento)
                .NumeroFactura = numDocumento.Trim()
                .Observacion = observacion.Trim()
                .FechaEmision = fechaEmision
                .Estado = 1
                .Moneda = moneda.Trim()
                .TipoCambio = 500
                .Total = Convert.ToDouble(montoTotal)
                .SaldoActual = Convert.ToDouble(saldoActual)
            End With

            respuestaCreacion = objFactura.CrearFactura(modFactura, usuarioInserta, errorMessage)

        Else ' Se está guardando un documento de pago
            Dim modDocumentoPago As New Models.DocumentoPago
            Dim objDocumentoPagoDB As New DocumentoPagoDB
            nombreDocumento = "Documento de pago guardado"

            With modDocumentoPago
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoDocumento = Convert.ToInt32(idTipoDocumento)
                .NumeroDocumento = numDocumento.Trim()
                .Observacion = observacion.Trim()
                .FechaEmision = fechaEmision
                .Estado = 1
                .Moneda = moneda.Trim()
                .TipoCambio = 500
                .Total = Convert.ToDouble(montoTotal)
                .SaldoActual = Convert.ToDouble(saldoActual)
            End With

            respuestaCreacion = objDocumentoPagoDB.CrearDocumentoPago(modDocumentoPago, usuarioInserta, errorMessage)
        End If

        If respuestaCreacion Then
            SwalUtils.ShowSwal(Me, $"¡{nombreDocumento} exitosamente!", "El documento está listo para ser aplicado.")

            btnGuardar.CssClass = "boton boton__guardar boton__ocultar"
            btnModificar.CssClass = "boton boton__modificar"
            btnAplicar.CssClass = "boton boton__aplicar"
            ddlCategoria.Enabled = False
            ddlTipoDocumento.Enabled = False
            txtFiltProveedor.Enabled = False
            ddlProveedor.Enabled = False
            txtNumDocumento.Enabled = False
        Else
            nombreDocumento = IIf(idCategoriaDocumento = 1, "la factura", "el documento de pago")
            SwalUtils.ShowSwalError(Me, "Atención", $"No se logró guardar {nombreDocumento}. {errorMessage}")
        End If
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs)
        'Se trabaja con el documento que está en pantalla, el cual para este punto ya debe estar validado que NO haya sido eliminado

        ' Variables y validaciones para ver si el documento existe o si alguien lo eliminó
        Dim idCategoriaDocumento As String = ddlCategoria.SelectedValue, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        Dim idProveedor As String = ddlProveedor.SelectedValue
        Dim numDocumento As String = txtNumDocumento.Text.ToString().Trim()

        Dim objHerramienta As New Herramientas

        ' Si los datos para buscar el documento no son válidos, el proceso hasta aquí llega
        If Not objHerramienta.ValidarNumeroEntero(idCategoriaDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idTipoDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idProveedor, False) Or Not objHerramienta.ValidarCadena(numDocumento) Then
            SwalUtils.ShowSwal(Me, "Atención", "Los datos del documento no son válidos. Por favor revisar.", "warning")
            Return
        End If

        ' Si los datos son válidos, procede a buscar el documento/factura
        ' Agregar un botón de búsqueda de documentos pendientes de aplicar para que pueda seleccionar uno y cargarlo en pantalla

        Dim idCategoriaDoc As Integer = Convert.ToInt32(ddlCategoria.SelectedValue)
        Dim errorMessage As String = ""
        Dim observacion As String = txtAreaObservacion.Value.ToString()
        Dim fechaEmision As String = txtFechaEmision.Text
        Dim moneda As String = ddlMoneda.SelectedValue.ToString(), montoTotal As String = txtMontoTotal.Text, saldoActual = txtMontoTotal.Text
        Dim usuarioModifico As String = "andre"

        'Validar los datos
        If Not ValidarDatos(idCategoriaDocumento, idProveedor, idTipoDocumento, numDocumento, observacion, fechaEmision, moneda, montoTotal, saldoActual) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        If idCategoriaDoc = 1 Then ' Se quiere modificar una factura
            Dim objFactura As New FacturaDB
            Dim modFactura As New Models.Factura

            With modFactura
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoFactura = Convert.ToInt32(idTipoDocumento)
                .NumeroFactura = numDocumento
            End With

            modFactura = objFactura.BuscarFactura_x_Numero(modFactura, errorMessage)

            If modFactura Is Nothing Then ' No encontró el documento
                SwalUtils.ShowSwal(Me, "Atención", "La factura que desea modificar no se encontró en el sistema. Por favor revisar.", "warning")
                Return
            End If

            ' Sí encontró la factura, continua con la revisión
            ' Revisa si el estado es aplicado, si lo es, hasta aquí llega el proceso
            If modFactura.Estado = 2 Then ' La factura ya ha sido aplicada
                SwalUtils.ShowSwal(Me, "Atención", "La factura que desea modificar ya ha sido aplicado en el sistema.", "info")
                Return
            End If

            'Puede modificar el documento
            modFactura = New Models.Factura
            errorMessage = ""

            With modFactura
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoFactura = Convert.ToInt32(idTipoDocumento)
                .NumeroFactura = numDocumento.Trim()
                .Observacion = observacion.Trim()
                .FechaEmision = fechaEmision
                .Estado = 1
                .Moneda = moneda.Trim()
                .TipoCambio = 500
                .Total = Convert.ToDouble(montoTotal)
                .SaldoActual = Convert.ToDouble(saldoActual)
            End With

            If objFactura.ModificarFactura(modFactura, usuarioModifico, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Factura modificada exitosamente!", "La factura ya está lista para ser aplicada.")
                ddlCategoria.Enabled = False
                ddlTipoDocumento.Enabled = False
                txtFiltProveedor.Enabled = False
                ddlProveedor.Enabled = False
                txtNumDocumento.Enabled = False
            Else
                SwalUtils.ShowSwalError(Me, "Atención", $"No se logró modificar la factura. {errorMessage}")
            End If

        Else
            Dim objDocumentoPago As New DocumentoPagoDB
            Dim modDocumentoPago As New Models.DocumentoPago

            With modDocumentoPago
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoDocumento = Convert.ToInt32(idTipoDocumento)
                .NumeroDocumento = numDocumento
            End With

            modDocumentoPago = objDocumentoPago.BuscarDocumentoPago_x_Numero(modDocumentoPago, errorMessage)

            If modDocumentoPago Is Nothing Then ' No encontró el documento
                SwalUtils.ShowSwal(Me, "Atención", "El documento que desea modificar no se encontró en el sistema. Por favor revisar.", "warning")
                Return
            End If

            ' Sí encontró el documento, continua con la revisión
            ' Revisa si el estado es aplicado, si lo es, hasta aquí llega el proceso
            If modDocumentoPago.Estado = 2 Then ' El documento ya ha sido aplicado
                SwalUtils.ShowSwal(Me, "Atención", "El documento que desea modificar ya ha sido aplicado en el sistema.", "info")
                Return
            End If

            'Puede modificar el documento
            modDocumentoPago = New Models.DocumentoPago
            errorMessage = ""
            With modDocumentoPago
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoDocumento = Convert.ToInt32(idTipoDocumento)
                .NumeroDocumento = numDocumento.Trim()
                .Observacion = observacion.Trim()
                .FechaEmision = fechaEmision
                .Estado = 1
                .Moneda = moneda.Trim()
                .TipoCambio = 500
                .Total = Convert.ToDouble(montoTotal)
                .SaldoActual = Convert.ToDouble(saldoActual)
            End With

            If objDocumentoPago.ModificarDocumentoPago(modDocumentoPago, usuarioModifico, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Documento de pago modificado exitosamente!", "El documento está listo para ser aplicado.")
                ddlCategoria.Enabled = False
                ddlTipoDocumento.Enabled = False
                txtFiltProveedor.Enabled = False
                ddlProveedor.Enabled = False
                txtNumDocumento.Enabled = False
            Else
                SwalUtils.ShowSwalError(Me, "Atención", $"No se logró modificar el documento de pago. {errorMessage}")
            End If
        End If
    End Sub

    Protected Sub btnAplicar_Click(sender As Object, e As EventArgs)
        'Lo que hace es hacer un update a los datos y al final cambia el estado de la factura para que aparezca para ser cancelada


    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        prcLimpiarCampos()
    End Sub

    Private Function TieneDatos_IdentificadoresDocumento(idCategoriaDocumento As String, idTipoDocumento As String, idProveedor As String, numDocumento As String) As Boolean
        Dim objHerramientas As New Herramientas
        If objHerramientas.ValidarCadena(idCategoriaDocumento) And objHerramientas.ValidarCadena(idTipoDocumento) And objHerramientas.ValidarCadena(idProveedor) And objHerramientas.ValidarCadena(numDocumento) Then
            Return True
        End If
        Return False
    End Function

    Private Function ValidaDatosExistencia(idCategoriaDocumento As String, idProveedor As String, idTipoDocumento As String, numDocumento As String) As Boolean
        Dim objHerramienta As New Herramientas

        If Not objHerramienta.ValidarNumeroEntero(idCategoriaDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idTipoDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idProveedor, False) Or Not objHerramienta.ValidarCadena(numDocumento) Then
            Return False
        End If

        Return True

    End Function

    Private Function ExisteDocumento(idCategoriaDocumento As Integer, idProveedor As Integer, idTipoDocumento As Integer, numDocumento As String, ByRef errorMessage As String) As Boolean
        Dim modDocumento As Object

        If idCategoriaDocumento = 1 Then ' Revisa si existe la factura
            Dim modFactura As New Models.Factura
            Dim objFactura As New FacturaDB

            With modFactura
                .IdProveedor = idProveedor
                .TipoFactura = idTipoDocumento
                .NumeroFactura = numDocumento
            End With

            modDocumento = objFactura.ConsultarExistenciaFactura(modFactura, errorMessage)
        Else ' Revisa si existe el documento de pago
            Dim modDocumentoPago As New Models.DocumentoPago
            Dim objDocumentoPago As New DocumentoPagoDB

            With modDocumentoPago
                .IdProveedor = idProveedor
                .TipoDocumento = idTipoDocumento
                .NumeroDocumento = numDocumento
            End With

            modDocumento = objDocumentoPago.ConsultarExistenciaDocumentoPago(modDocumentoPago, errorMessage)
        End If

        If modDocumento Is Nothing Then
            Throw New Exception($"Error al consultar la existencia del documento de categoría [{idCategoriaDocumento}]")
        End If

        If modDocumento.Estado = 0 AndAlso modDocumento.Observacion Is Nothing Then
            Return False ' No existe el documento
        Else
            Return True ' Sí existe el documento
        End If
    End Function
    Private Function ConsultaDocumentoPago(idProveedor As Integer, idTipoDocumento As Integer, numDocumento As String, ByRef errorMessage As String) As Models.DocumentoPago
        Dim modDocumentoPago As New Models.DocumentoPago
        Dim objDocumentoPago As New DocumentoPagoDB

        With modDocumentoPago
            .IdProveedor = idProveedor
            .TipoDocumento = idTipoDocumento
            .NumeroDocumento = numDocumento
        End With

        Return objDocumentoPago.BuscarDocumentoPago_x_Numero(modDocumentoPago, errorMessage)
    End Function

    Private Function ContinuarProcesoGuardado(idCategoriaDocumento As String, idTipoDocumento As String, idProveedor As String, numDocumento As String) As Boolean
        Dim errorMessage As String = ""

        ' Valida los datos para la búsqueda
        If Not ValidaDatosExistencia(idCategoriaDocumento, idProveedor, idTipoDocumento, numDocumento) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return False
        End If

        Dim nombreDocumento As String = IIf(Convert.ToInt32(idCategoriaDocumento) = 1, "la factura", "el documento de pago")

        Try
            Dim respuestaExistencia As Boolean = ExisteDocumento(Convert.ToInt32(idCategoriaDocumento), Convert.ToInt32(idProveedor), Convert.ToInt32(idTipoDocumento), numDocumento, errorMessage)

            If respuestaExistencia Then
                SwalUtils.ShowSwalMessage(Me, "Consulta", $"Ya existe {nombreDocumento} con el número {numDocumento} para este proveedor.", "warning")
                Return False
            End If

            'Si no la encontró no hay problema, puede guardarla
            Return True

        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Atención", $"No se logró encontrar {nombreDocumento}. {errorMessage}.")
            Return False
        End Try

    End Function

    Private Function ValidarDatos(idCategoriaDocumento As String, idProveedor As String, tipoDocumento As String, numDocumento As String, observacion As String, fechaEmision As String, moneda As String, montoTotal As String, saldoActual As String) As Boolean
        Dim objHerramienta As New Herramientas

        If Not objHerramienta.ValidarNumeroEntero(idCategoriaDocumento, False) Then
            Return False
        End If

        If Not objHerramienta.ValidarNumeroEntero(idProveedor, False) Then
            Return False
        End If

        If Not objHerramienta.ValidarNumeroEntero(tipoDocumento, False) Then
            Return False
        End If

        If Not objHerramienta.ValidarCadena(numDocumento) Then
            Return False
        End If

        If Not objHerramienta.ValidarCadena(observacion) Then
            Return False
        End If

        If Not objHerramienta.ValidarFecha(fechaEmision) Then
            Return False
        End If

        If Not objHerramienta.ValidarCadena(moneda) Then
            Return False
        End If

        If Not objHerramienta.ValidarNumeroDecimales(montoTotal, False) Then
            Return False
        End If

        If Not objHerramienta.ValidarNumeroDecimales(saldoActual, False) Then
            Return False
        End If

        Return True
    End Function
End Class