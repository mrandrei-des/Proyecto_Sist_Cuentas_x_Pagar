Public Class ObtenerPendientes
    Inherits System.Web.UI.Page

    ' Las propiedades deben corresponder en cantidad y nombres a las que requiere el web method
    Public Class BusquedaRequest
        Public Property categoriaDocumento As Integer
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Se lee el POST recibido
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        ' Deserializás el JSON
        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)

        Dim idCategoriaDocumento As Integer = datos.categoriaDocumento
        Dim respuesta As Object, listaPendientes As Object
        Dim errorMessage As String = ""

        If idCategoriaDocumento = 1 Then
            Dim objFactura As New FacturaDB
            listaPendientes = objFactura.FiltrarFacturasPendientes(0, "", "", errorMessage)
        Else
            Dim objDocumentoPago As New DocumentoPagoDB
            listaPendientes = objDocumentoPago.FiltrarDocumentosPendientes(0, "", "", errorMessage)
        End If

        If listaPendientes Is Nothing Then
            respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron documentos pendientes."
            }
        Else
            If listaPendientes.Count > 0 Then
                respuesta = New With {
                .estado = True,
                .lista = listaPendientes,
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

    Private Function ObtenerProveedores(datoBuscar As String) As List(Of Models.Proveedor)
        Dim listProveedores As List(Of Models.Proveedor)
        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = ""

        listProveedores = objProveedorDB.FiltrarProveedores_Nombre(datoBuscar, errorMessage)
        Return listProveedores
    End Function

End Class