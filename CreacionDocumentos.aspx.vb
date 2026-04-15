Imports System.Threading.Tasks
Imports System.Web.Services
Imports System.Web.Script.Services
Imports Antlr.Runtime.Tree
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CreacionDocumentos
    Inherits System.Web.UI.Page

    ' PENDIENTE IMPLEMENTAR UNA TABLA O ALGO PARA LISTAR LAS FACTURAS/DOCUMENTOS DE PAGO PENDIENTES

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceRegistroDocumentos")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
        If Not IsPostBack Then
            hfCategoria.Value = 1
            hfFiltCategoria.Value = 1
            prcLlena_ddls()
            btnFiltFacturaForm.CssClass = "boton boton__opcion boton__opcion--active"
            btnFiltPagoForm.CssClass = "boton boton__opcion"

            Dim strCategoria As String = "Factura"
            prcCambiaTextoEtiquetas(strCategoria.ToLower())

            btnModificar.CssClass = "boton boton__modificar boton__ocultar"
            btnEliminar.CssClass = "boton boton__eliminar boton__ocultar"
        End If
    End Sub

    Private Sub prcLlena_ddls()
        Dim idCategoria As Integer = Convert.ToInt32(hfCategoria.Value)
        prcLlena__ddlTipoDocumento(idCategoria)
        prcLlena_ddlMoneda()
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

            For Each modTipoDocumento As Models.TipoDocumento In listTipoDocumento
                ddlTipoDocumento.Items.Add(New ListItem(modTipoDocumento.Descripcion.ToString(), modTipoDocumento.IdTipoDocumento))
            Next

            ddlTipoDocumento.SelectedIndex = 0
            hfTipoDocumento.Value = ddlTipoDocumento.SelectedValue
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

            For Each modMoneda As Models.Moneda In listMonedas
                Dim textoMoneda As String = $"{modMoneda.Descripcion}"
                ddlMoneda.Items.Add(New ListItem(textoMoneda, modMoneda.CodigoMoneda.ToString()))
            Next

            ddlMoneda.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron monedas en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcCambiaTextoEtiquetas(tipoDocumento As String)
        lblTipoDocumento.Text = $"Tipo de {tipoDocumento}:"
        lblNumeroDocumento.Text = $"Número de {tipoDocumento}:"
        lblFechaEmision.Text = $"Fecha {tipoDocumento}:"
        lblMonto.Text = $"Monto total {tipoDocumento}:"
    End Sub

    Private Sub prcLimpiarCampos()
        hfCategoria.Value = 1
        prcLlena_ddls()
        btnFiltFacturaForm.CssClass = "boton boton__opcion boton__opcion--active"
        btnFiltPagoForm.CssClass = "boton boton__opcion"

        txtProveedor.Text = String.Empty
        txtNumDocumento.Text = String.Empty
        txtFechaEmision.Text = String.Empty
        txtObservacion.Text = String.Empty
        txtMontoTotal.Text = String.Empty
        btnGuardar.CssClass = "boton boton__guardar"
        btnModificar.CssClass = "boton boton__modificar boton__ocultar"
        btnEliminar.CssClass = "boton boton__eliminar boton__ocultar"

        btnFiltFacturaForm.Enabled = True
        btnFiltPagoForm.Enabled = True
        ddlTipoDocumento.Enabled = True
        txtProveedor.Enabled = True
        txtNumDocumento.Enabled = True
    End Sub

    Protected Sub txtNumDocumento_TextChanged(sender As Object, e As EventArgs)
        ' Variables y validaciones para ver si el documento existe o si alguien lo eliminó
        'Dim idCategoriaDocumento As String = ddlCategoria.SelectedValue, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        ' /***********/ ESTO CAMBIA POR LOS BOTONES DE CATEGORÍA
        Dim idCategoriaDocumento As String = hfCategoria.Value, idTipoDocumento As String = ddlTipoDocumento.SelectedValue

        Dim idProveedor As String = hfNumProveedor.Value.ToString()
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
        ' Variables para las validaciones y proceso de guardado
        Dim idCategoriaDocumento As String = hfCategoria.Value, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        Dim idProveedor As String = hfNumProveedor.Value.ToString()
        Dim numDocumento As String = txtNumDocumento.Text.ToString()
        Dim observacion As String = txtObservacion.Text
        Dim fechaEmision As String = txtFechaEmision.Text
        Dim moneda As String = ddlMoneda.SelectedValue.ToString(), montoTotal As String = txtMontoTotal.Text, saldoActual = txtMontoTotal.Text
        Dim errorMessage As String = ""

        ' Valida que los datos estén correctos, para el momento de hacer la conversión
        If Not ValidarDatos(idCategoriaDocumento, idProveedor, idTipoDocumento, numDocumento, observacion, fechaEmision, moneda, montoTotal, saldoActual) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        'Valida que el documento no haya sido registrado previamente, aquí revisa hasta dentro de los que fueron eliminados
        Dim nombreDocumento As String = IIf(Convert.ToInt32(idCategoriaDocumento) = 1, "la factura", "el documento de pago")

        If ExisteDocumento(Convert.ToInt32(idCategoriaDocumento), Convert.ToInt32(idProveedor), Convert.ToInt32(idTipoDocumento), numDocumento, errorMessage) Then
            SwalUtils.ShowSwalMessage(Me, "Consulta", $"Ya existe {nombreDocumento} con el número {numDocumento} para este proveedor.", "warning")
            Return
        End If

        Dim categoriaDocumento As Integer = Convert.ToInt32(idCategoriaDocumento)
        Dim usuarioInserta As String = Session("UsuarioLoggeado")
        errorMessage = ""
        nombreDocumento = ""
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
            prcLimpiarCampos()

            'btnGuardar.CssClass = "boton boton__guardar boton__ocultar"
            'btnAplicar.CssClass = "boton boton__aplicar"
            'btnFiltFacturaForm.Enabled = False
            'btnFiltPagoForm.Enabled = False
            'ddlTipoDocumento.Enabled = False
            'txtProveedor.Enabled = False
            'txtNumDocumento.Enabled = False
        Else
            nombreDocumento = IIf(idCategoriaDocumento = 1, "la factura", "el documento de pago")
            SwalUtils.ShowSwalError(Me, "Atención", $"No se logró guardar {nombreDocumento}. {errorMessage}")
        End If
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs)
        ' Variables y validaciones para ver si el documento existe o si alguien lo eliminó
        Dim idCategoriaDocumento As String = hfCategoria.Value, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        idTipoDocumento = hfTipoDocumento.Value
        Dim idProveedor As String = hfNumProveedor.Value.ToString()
        Dim numDocumento As String = txtNumDocumento.Text.ToString().Trim()

        Dim objHerramienta As New Herramientas

        ' Si los datos son válidos, procede a buscar el documento/factura
        Dim idCategoriaDoc As Integer = Convert.ToInt32(idCategoriaDocumento)
        Dim errorMessage As String = ""
        Dim observacion As String = txtObservacion.Text.ToString()
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
                prcLimpiarCampos()
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
                prcLimpiarCampos()
            Else
                SwalUtils.ShowSwalError(Me, "Atención", $"No se logró modificar el documento de pago. {errorMessage}")
            End If
        End If
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs)
        ' Variables y validaciones para ver si el documento existe o si alguien lo eliminó
        Dim idCategoriaDocumento As String = hfCategoria.Value, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        idTipoDocumento = hfTipoDocumento.Value
        Dim idProveedor As String = hfNumProveedor.Value.ToString()
        Dim numDocumento As String = txtNumDocumento.Text.ToString().Trim()
        Dim errorMessage As String = ""
        Dim objHerramienta As New Herramientas

        ' Si los datos para buscar el documento no son válidos, el proceso hasta aquí llega
        If Not objHerramienta.ValidarNumeroEntero(idCategoriaDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idTipoDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idProveedor, False) Or Not ValidarNumDocumento(numDocumento) Then
            SwalUtils.ShowSwal(Me, "Atención", "Los datos del documento no son válidos. Por favor revisar.", "warning")
            Return
        End If

        Dim idCategoriaDoc As Integer = Convert.ToInt32(idCategoriaDocumento)
        Dim usuarioElimino As String = "andre"

        If idCategoriaDoc = 1 Then

            Dim objFactura As New FacturaDB
            Dim modFactura As New Models.Factura

            With modFactura
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoFactura = Convert.ToInt32(idTipoDocumento)
                .NumeroFactura = numDocumento
            End With

            modFactura = objFactura.BuscarFactura_x_Numero(modFactura, errorMessage)

            If modFactura Is Nothing Then ' No encontró el documento
                SwalUtils.ShowSwal(Me, "Atención", "La factura que desea eliminar no se encontró en el sistema. Por favor revisar.", "warning")
                Return
            End If

            ' Sí encontró la factura, continua con la revisión
            ' Revisa si el estado es eliminado o aplicado, si lo es, hasta aquí llega el proceso
            If modFactura.Estado = 2 Then ' La factura ya ha sido aplicada
                SwalUtils.ShowSwal(Me, "Atención", "La factura que desea eliminar ya ha sido aplicado en el sistema.", "info")
                Return
            End If

            If modFactura.Estado = 6 Then ' La factura ya ha sido eliminada
                SwalUtils.ShowSwal(Me, "Atención", "La factura que desea eliminar ya ha sido eliminada en el sistema.", "info")
                Return
            End If

            'Puede eliminar el documento
            modFactura = New Models.Factura
            errorMessage = ""

            With modFactura
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoFactura = Convert.ToInt32(idTipoDocumento)
                .NumeroFactura = numDocumento.Trim()
            End With

            If objFactura.EliminarFactura(modFactura, usuarioElimino, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Factura eliminada exitosamente!", "La factura ya no se encuentra en el sistema.")
                prcLimpiarCampos()
            Else
                SwalUtils.ShowSwalError(Me, "Atención", $"No se logró eliminar la factura. {errorMessage}")
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
                SwalUtils.ShowSwal(Me, "Atención", "El documento de Pago que desea eliminar no se encontró en el sistema. Por favor revisar.", "warning")
                Return
            End If

            ' Sí encontró el DocumentoPago, continua con la revisión
            ' Revisa si el estado es eliminado o aplicado, si lo es, hasta aquí llega el proceso
            If modDocumentoPago.Estado = 2 Then ' El Documento Pago ya ha sido aplicado
                SwalUtils.ShowSwal(Me, "Atención", "El documento de pago que desea eliminar ya ha sido aplicado en el sistema.", "info")
                Return
            End If

            If modDocumentoPago.Estado = 6 Then ' La Documento Pago ya ha sido eliminado
                SwalUtils.ShowSwal(Me, "Atención", "El documento de pago que desea eliminar ya ha sido eliminado en el sistema.", "info")
                Return
            End If

            'Puede eliminar el documento
            modDocumentoPago = New Models.DocumentoPago
            errorMessage = ""

            With modDocumentoPago
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoDocumento = Convert.ToInt32(idTipoDocumento)
                .NumeroDocumento = numDocumento.Trim()
            End With

            If objDocumentoPago.EliminarDocumentoPago(modDocumentoPago, usuarioElimino, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Documento de pago eliminado exitosamente!", "El documento de pago ya no se encuentra en el sistema.")
                prcLimpiarCampos()
            Else
                SwalUtils.ShowSwalError(Me, "Atención", $"No se logró eliminar el documento de pago. {errorMessage}")
            End If
        End If

    End Sub

    Protected Sub btnContinuarAplicacion_Click(sender As Object, e As EventArgs)
        ' Procede a ejecutar la aplicación, pero antes oculta el modal
        contenedor__dialogConfirm.Style.Add("display", "none")

        ' Variables y validaciones para ver si el documento existe y si ya fue aplicado
        Dim idCategoriaDocumento As String = hfCategoria.Value, idTipoDocumento As String = ddlTipoDocumento.SelectedValue
        idTipoDocumento = hfTipoDocumento.Value

        Dim idProveedor As String = hfNumProveedor.Value.ToString()
        Dim numDocumento As String = txtNumDocumento.Text.ToString().Trim()

        Dim objHerramienta As New Herramientas

        ' Si los datos para buscar el documento no son válidos, el proceso hasta aquí llega
        If Not objHerramienta.ValidarNumeroEntero(idCategoriaDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idTipoDocumento, False) Or Not objHerramienta.ValidarNumeroEntero(idProveedor, False) Or Not objHerramienta.ValidarCadena(numDocumento) Then
            SwalUtils.ShowSwal(Me, "Atención", "Los datos del documento no son válidos. Por favor revisar.", "warning")
            Return
        End If

        Dim idCategoriaDoc As Integer = Convert.ToInt32(idCategoriaDocumento)
        Dim modObjeto As Object
        Dim errorMessage As String = "", nombreDocumento As String = ""

        If idCategoriaDoc = 1 Then
            Dim objFactura As New FacturaDB
            Dim modFactura As New Models.Factura
            nombreDocumento = "La factura"

            With modFactura
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoFactura = Convert.ToInt32(idTipoDocumento)
                .NumeroFactura = numDocumento
            End With

            modObjeto = objFactura.BuscarFactura_x_Numero(modFactura, errorMessage)
        Else
            Dim objDocumentoPago As New DocumentoPagoDB
            Dim modDocumentoPago As New Models.DocumentoPago
            nombreDocumento = "El documento de pago"

            With modDocumentoPago
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoDocumento = Convert.ToInt32(idTipoDocumento)
                .NumeroDocumento = numDocumento
            End With

            modObjeto = objDocumentoPago.BuscarDocumentoPago_x_Numero(modDocumentoPago, errorMessage)
        End If

        If modObjeto Is Nothing Then ' No encontró el documento
            SwalUtils.ShowSwal(Me, "Atención", $"{nombreDocumento} que desea aplicar no ha sido encontrado en el sistema. Por favor revisar.", "warning")
            Return
        End If

        ' Revisa si el estado es aplicado, si lo es, hasta aquí llega el proceso
        If modObjeto.Estado = 2 Then ' El documento ya ha sido aplicada
            SwalUtils.ShowSwal(Me, "Atención", $"{nombreDocumento} ya ha sido aplicado en el sistema.", "info")
            Return
        End If

        errorMessage = ""
        Dim usuarioAplica As String = "andre"
        Dim respuestaAplicacion As Boolean

        If idCategoriaDoc = 1 Then
            Dim objFactura As New FacturaDB
            Dim modFactura As New Models.Factura
            nombreDocumento = "Factura aplicada"
            With modFactura
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoFactura = Convert.ToInt32(idTipoDocumento)
                .NumeroFactura = numDocumento
            End With

            respuestaAplicacion = objFactura.AplicarFactura(modFactura, usuarioAplica, errorMessage)
        Else
            Dim objDocumentoPago As New DocumentoPagoDB
            Dim modDocumentoPago As New Models.DocumentoPago
            nombreDocumento = "Documento de pago aplicado"

            With modDocumentoPago
                .IdProveedor = Convert.ToInt32(idProveedor)
                .TipoDocumento = Convert.ToInt32(idTipoDocumento)
                .NumeroDocumento = numDocumento
            End With

            respuestaAplicacion = objDocumentoPago.AplicarDocumentoPago(modDocumentoPago, usuarioAplica, errorMessage)
        End If

        If respuestaAplicacion Then
            SwalUtils.ShowSwal(Me, $"¡{nombreDocumento} exitosamente!", "El documento se encuentra listo en el proveedor correspondiente.")
            prcLimpiarCampos()
        Else
            nombreDocumento = IIf(idCategoriaDoc = 1, "la factura", "el documento de pago")
            SwalUtils.ShowSwalError(Me, "Atención", $"No se logró aplicar {nombreDocumento}. {errorMessage}")
        End If
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
    Private Function ValidarDatos(idCategoriaDocumento As String, idProveedor As String, tipoDocumento As String, numDocumento As String, observacion As String, fechaEmision As String, moneda As String, montoTotal As String, saldoActual As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True

        If Not objHerramienta.ValidarNumeroEntero(idCategoriaDocumento, False) Then
            respuestaValidacion = False
        End If

        If Not objHerramienta.ValidarNumeroEntero(idProveedor, False) Then
            respuestaValidacion = False
        End If

        If Not objHerramienta.ValidarNumeroEntero(tipoDocumento, False) Then
            contenedorMensajesTipoDoc.InnerHtml = $"<p class='formulario__mensaje'>El tipo de documento no tiene un valor válido.</p>"
            contenedorMensajesTipoDoc.Style.Add("display", "block")
            respuestaValidacion = False
        Else
            contenedorMensajesTipoDoc.Style.Remove("display")
        End If

        If Not ValidarNumDocumento(numDocumento) Then
            respuestaValidacion = False
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("observacion")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, observacion) Then
                contenedorMensajesObservacion.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesObservacion.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesObservacion.Style.Remove("display")
            End If
        End If

        If Not objHerramienta.ValidarFecha(fechaEmision) Then
            contenedorMensajesFecha.InnerHtml = $"<p class='formulario__mensaje'>La fecha no tiene un valor válido.</p>"
            contenedorMensajesFecha.Style.Add("display", "block")
            respuestaValidacion = False
        Else
            contenedorMensajesFecha.Style.Remove("display")
        End If

        If Not objHerramienta.ValidarCadena(moneda) Then
            respuestaValidacion = False
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("montoDocumento")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, montoTotal) Then
                contenedorMensajesMonto.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesMonto.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesMonto.Style.Remove("display")
            End If
        End If

        Return respuestaValidacion
    End Function

    Private Function ValidarNumDocumento(numDocumento As String) As Boolean
        Dim modRegla As ValidacionRegex
        Dim objListaReglas As New ListaReglas()

        modRegla = objListaReglas.ObtenerReglaPorCampo("numDocumento")
        If Not modRegla Is Nothing Then
            If Not modRegla.Regla.IsMatch(numDocumento) Then
                contenedorMensajesNumDoc.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesNumDoc.Style.Add("display", "block")
                Return False
            Else
                contenedorMensajesNumDoc.Style.Remove("display")
            End If
        End If

        Return True
    End Function

    Protected Sub btnFiltFacturaForm_Click(sender As Object, e As EventArgs)
        prcLimpiarCampos()
        hfCategoria.Value = 1
        Dim idCategoria As Integer = Convert.ToInt32(hfCategoria.Value)
        prcLlena__ddlTipoDocumento(idCategoria)
        btnFiltFacturaForm.CssClass = "boton boton__opcion boton__opcion--active"
        btnFiltPagoForm.CssClass = "boton boton__opcion"

        Dim strCategoria As String = "Factura"
        prcCambiaTextoEtiquetas(strCategoria.ToLower())
    End Sub

    Protected Sub btnFiltPagoForm_Click(sender As Object, e As EventArgs)
        prcLimpiarCampos()
        hfCategoria.Value = 2
        Dim idCategoria As Integer = Convert.ToInt32(hfCategoria.Value)
        prcLlena__ddlTipoDocumento(idCategoria)
        btnFiltPagoForm.CssClass = "boton boton__opcion boton__opcion--active"
        btnFiltFacturaForm.CssClass = "boton boton__opcion"

        Dim strCategoria As String = "Documento de Pago"

        prcCambiaTextoEtiquetas(strCategoria.ToLower())
    End Sub

End Class