Public Class ConsultaDocsPendientes
    Inherits System.Web.UI.Page

    Public Class BusquedaRequest
        Public Property datoBuscar As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        'Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)

        Dim listaDocsPendientes As New List(Of Models.DocumentosPendientes)
        listaDocsPendientes = ObtenerDocsPendientes()
        Dim respuesta As Object

        If listaDocsPendientes IsNot Nothing Then
            If listaDocsPendientes.Count > 0 Then
                respuesta = New With {
                .estado = True,
                .listaDocumentos = listaDocsPendientes,
                .mensaje = ""
            }
            Else
                respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron registros."
                }
            End If
        Else
            respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron registros."
            }
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub

    Private Function ObtenerDocsPendientes()
        Dim objDocsPendientesDB As New DocumentosPendientesDB
        Dim listaDocumentosPendientes As New List(Of Models.DocumentosPendientes)
        Dim errorMessage As String = ""

        listaDocumentosPendientes = objDocsPendientesDB.ObtenerDocumentosPendientesAntiguos(errorMessage)

        Return listaDocumentosPendientes

    End Function

End Class