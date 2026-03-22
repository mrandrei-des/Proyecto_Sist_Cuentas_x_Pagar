Public Class ObtenerTiposDocumento
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

        Dim idCategoriaTipoDocumento As Integer = datos.categoriaDocumento
        Dim listaTiposDocumento = ConsultarTiposDocumento(idCategoriaTipoDocumento)

        Dim respuesta As Object

        If listaTiposDocumento.Count > 0 Then
            respuesta = New With {
                .estado = True,
                .lista = listaTiposDocumento,
                .mensaje = ""
            }
        Else
            respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron proveedores."
            }
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub

    Private Function ConsultarTiposDocumento(idCategoria As Integer) As List(Of Models.TipoDocumento)
        Dim listTiposDocumento As List(Of Models.TipoDocumento)
        Dim objTipoDocumentoDB As New TipoDocumentoDB
        Dim errorMessage As String = ""

        listTiposDocumento = objTipoDocumentoDB.ConsultarTipoDocumento(idCategoria, errorMessage)
        Return listTiposDocumento
    End Function

End Class