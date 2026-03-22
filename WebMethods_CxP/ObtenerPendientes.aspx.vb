Public Class ObtenerPendientes
    Inherits System.Web.UI.Page

    ' Las propiedades deben corresponder en cantidad y nombres a las que requiere el web method
    Public Class BusquedaRequest
        Public Property datoBuscar As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Se lee el POST recibido
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        ' Deserializás el JSON
        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)

        Dim datoBuscarProveedor As String = datos.datoBuscar
        Dim listaProveedores = ObtenerProveedores(datoBuscarProveedor)
        Dim respuesta As Object

        If listaProveedores.Count > 0 Then
            respuesta = New With {
                .estado = True,
                .lista = listaProveedores,
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

    Private Function ObtenerProveedores(datoBuscar As String) As List(Of Models.Proveedor)
        Dim listProveedores As List(Of Models.Proveedor)
        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = ""

        listProveedores = objProveedorDB.FiltrarProveedores_Nombre(datoBuscar, errorMessage)
        Return listProveedores
    End Function

End Class