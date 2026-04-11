Imports System.Security.Policy
Imports Proyecto_Sist_Cuentas_x_Pagar.Models

Public Class ConsultaReporteDocumentosAplicados
    Inherits System.Web.UI.Page

    Public Class BusquedaRequest
        Public Property filtTipoDoc As String
        Public Property filtMoneda As String
        Public Property filtFechaInicio As String
        Public Property filtFechaFin As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Se lee el POST recibido
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        ' Deserializás el JSON
        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)

        Dim filtTipoDoc As String, filtMoneda As String, filtFechaInicio As String, filtFechaFin As String, errorMessage As String = ""

        filtTipoDoc = datos.filtTipoDoc
        filtMoneda = datos.filtMoneda
        filtFechaInicio = datos.filtFechaInicio
        filtFechaFin = datos.filtFechaFin

        Dim listaDocumentos = ObtenerDocumentos(filtTipoDoc, filtMoneda, filtFechaInicio, filtFechaFin, errorMessage)

        Dim respuesta As Object

        If listaDocumentos.Count > 0 Then
            respuesta = New With {
                .estado = True,
                .lista = listaDocumentos,
                .mensaje = ""
            }
        Else
            respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron documentos aplicados."
            }
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub

    Private Function ObtenerDocumentos(filtroTipoDocumento As String, filtroMoneda As String, filtroFechaInicio As String, filtroFechaFin As String, ByRef errorMessage As String) As List(Of Models.DocumentoAplicado)
        Dim objDocAplicado As New DocumentoAplicadoDB
        Dim listDocsAplicados As New List(Of Models.DocumentoAplicado)

        listDocsAplicados = objDocAplicado.FiltrarDocumentosAplicados(filtroTipoDocumento, filtroMoneda, filtroFechaInicio, filtroFechaFin, errorMessage)

        If listDocsAplicados Is Nothing Then
            Return New List(Of Models.DocumentoAplicado)
        End If

        Return listDocsAplicados
    End Function

End Class