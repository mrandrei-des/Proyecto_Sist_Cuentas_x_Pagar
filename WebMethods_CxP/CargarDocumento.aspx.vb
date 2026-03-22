Public Class CargarDocumento
    Inherits System.Web.UI.Page

    ' Las propiedades deben corresponder en cantidad y nombres a las que requiere el web method
    Public Class BusquedaRequest
        Public Property idCategoriaDoc As Integer
        Public Property idproveedor As Integer
        Public Property tipoDoc As Integer
        Public Property numDocumento As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Se lee el POST recibido
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        ' Deserializás el JSON
        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)

        Dim idCategoriaDocumento As Integer = datos.idCategoriaDoc
        Dim numProveedor As Integer = datos.idproveedor
        Dim tipoDocumento As Integer = datos.tipoDoc
        Dim numDocumento As String = datos.numDocumento

        Dim respuesta As Object, datosDocumento As Object
        Dim errorMessage As String = ""

        If idCategoriaDocumento = 1 Then
            datosDocumento = obtenerInformacionFactura(numProveedor, tipoDocumento, numDocumento, errorMessage)
        Else
            datosDocumento = obtenerInformacionDocumentoPago(numProveedor, tipoDocumento, numDocumento, errorMessage)
        End If

        If datosDocumento Is Nothing Then
            respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron documentos pendientes."
            }
        Else
            If datosDocumento.idProveedor > 0 AndAlso datosDocumento.Moneda IsNot Nothing Then
                respuesta = New With {
                .estado = True,
                .lista = datosDocumento,
                .mensaje = ""
            }
            Else
                respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron documentos pendientes."
            }
            End If
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub

    Private Function obtenerInformacionFactura(numProveedor As Integer, idTipoDocumento As Integer, numDocumento As String, ByRef errorMessage As String) As Models.Factura
        Dim objFactura As New FacturaDB
        Dim modFactura As New Models.Factura
        With modFactura
            .IdProveedor = numProveedor
            .TipoFactura = idTipoDocumento
            .NumeroFactura = numDocumento
        End With

        Return objFactura.BuscarFactura_x_Numero(modFactura, errorMessage)
    End Function

    Private Function obtenerInformacionDocumentoPago(numProveedor As Integer, idTipoDocumento As Integer, numDocumento As String, ByRef errorMessage As String) As Models.DocumentoPago
        Dim objDocumentoPago As New DocumentoPagoDB
        Dim modDocumentoPago As New Models.DocumentoPago
        With modDocumentoPago
            .IdProveedor = numProveedor
            .TipoDocumento = idTipoDocumento
            .NumeroDocumento = numDocumento
        End With

        Return objDocumentoPago.BuscarDocumentoPago_x_Numero(modDocumentoPago, errorMessage)
    End Function

End Class