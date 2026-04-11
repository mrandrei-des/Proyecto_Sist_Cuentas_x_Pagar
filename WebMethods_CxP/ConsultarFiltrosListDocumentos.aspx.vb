Public Class ConsultarFiltrosListDocumentos
    Inherits System.Web.UI.Page
    Public Class BusquedaRequest
        Public Property filtTipoDoc As String
        Public Property filtMoneda As String
        Public Property filtFechaInicio As String
        Public Property filtFechaFin As String
    End Class
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)

        Dim filtTipoDoc As String, filtMoneda As String, filtFechaInicio As String, filtFechaFin As String, errorMessage As String = ""

        filtTipoDoc = datos.filtTipoDoc
        filtMoneda = datos.filtMoneda
        filtFechaInicio = datos.filtFechaInicio
        filtFechaFin = datos.filtFechaFin

        Dim listaTiposDocumentos = ObtenerTipoDocSelect(filtTipoDoc, filtMoneda, filtFechaInicio, filtFechaFin, errorMessage)
        Dim listaMonedas = ObtenerMonedasSelect(filtTipoDoc, filtMoneda, filtFechaInicio, filtFechaFin, errorMessage)
        Dim respuesta As Object

        If listaTiposDocumentos.Count > 0 And listaMonedas.Count > 0 Then
            respuesta = New With {
                .estado = True,
                .listaMonedas = listaMonedas,
                .listaTipoDocs = listaTiposDocumentos,
                .mensaje = ""
            }
        Else
            respuesta = New With {
                .estado = False,
                .listaMonedas = {},
                .listaTipoDocs = {},
                .mensaje = "No se encontraron documentos aplicados con los filtros seleccionados."
            }
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub

    Private Function ObtenerMonedasSelect(filtTipoDoc As String, filtMoneda As String, filtFechaInicio As String, filtFechaFin As String, ByRef errorMessage As String) As List(Of Models.Moneda)
        Dim objMonedaDB As New MonedaDB
        Dim listMonedas As New List(Of Models.Moneda)

        listMonedas = objMonedaDB.ConsultaCantTotalDocs_x_Monedas(filtTipoDoc, filtMoneda, filtFechaInicio, filtFechaFin, errorMessage)

        If listMonedas Is Nothing Then
            Return New List(Of Models.Moneda)
        End If

        Return listMonedas
    End Function

    Private Function ObtenerTipoDocSelect(filtTipoDoc As String, filtMoneda As String, filtFechaInicio As String, filtFechaFin As String, ByRef errorMessage As String) As List(Of Models.TipoDocumento)
        Dim objTipoDocDB As New TipoDocumentoDB
        Dim listTipoDocs As New List(Of Models.TipoDocumento)

        listTipoDocs = objTipoDocDB.ConsultarTipoDocumento_Todos(filtTipoDoc, filtMoneda, filtFechaInicio, filtFechaFin, errorMessage)

        If listTipoDocs Is Nothing Then
            Return New List(Of Models.TipoDocumento)
        End If

        Return listTipoDocs
    End Function

End Class