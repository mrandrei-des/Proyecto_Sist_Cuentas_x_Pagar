Imports Proyecto_Sist_Cuentas_x_Pagar.Models

Public Class ObtenerGruposPermisos
    Inherits System.Web.UI.Page

    Public Class BusquedaRequest
        ' En este caso no se requieren parámetros, pero se deja la clase preparada para futuras necesidades
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Se lee el POST recibido
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        ' Deserializás el JSON
        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)

        Dim respuesta As Object, listaGruposPermisos As New List(Of GrupoPermiso)
        Dim objGrupoPermisoDB As New GrupoPermisoDB
        Dim errorMessage As String = ""

        listaGruposPermisos = objGrupoPermisoDB.ConsultarGruposPermisos(errorMessage)

        If listaGruposPermisos Is Nothing Then
            respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se pudo consultar los grupos de permisos registrados."
            }
        Else
            If listaGruposPermisos.Count > 0 Then
                respuesta = New With {
                .estado = True,
                .lista = listaGruposPermisos,
                .mensaje = ""
            }
            Else
                respuesta = New With {
                .estado = False,
                .lista = {},
                .mensaje = "No se encontraron grupos de permisos en el sistema."
            }
            End If
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub
End Class