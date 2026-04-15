Public Class ConsultaUltimasAcciones
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

        Dim listaAcciones As New List(Of Models.UltimosCambios)
        listaAcciones = ObtenerUltimosCambios()
        Dim respuesta As Object

        If listaAcciones IsNot Nothing Then
            If listaAcciones.Count > 0 Then
                respuesta = New With {
                .estado = True,
                .lista = listaAcciones,
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

    Private Function ObtenerUltimosCambios()
        Dim objUlimosCambiosBD As New UltimosCambiosDB
        Dim listaCambios As New List(Of Models.UltimosCambios)
        Dim errorMessage As String = ""

        listaCambios = objUlimosCambiosBD.ObtenerUltimosCambios(errorMessage)

        Return listaCambios

    End Function

End Class