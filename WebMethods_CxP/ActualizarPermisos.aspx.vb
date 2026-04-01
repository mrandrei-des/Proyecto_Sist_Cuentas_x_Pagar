Imports Proyecto_Sist_Cuentas_x_Pagar.Models

Public Class ActualizarPermisos
    Inherits System.Web.UI.Page

    Public Class BusquedaRequest
        Public Property idRol As Integer
        Public Property idGrupoActivo As Integer
        Public Property listaPermisos As List(Of Integer)
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Se lee el POST recibido
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        ' Deserializás el JSON
        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)
        Dim idRol As Integer = datos.idRol
        Dim idGrupo As Integer = datos.idGrupoActivo
        Dim listaPermisosAsignados As New List(Of Integer)()
        Dim usuarioInserta As String = "andre"
        listaPermisosAsignados = datos.listaPermisos

        Dim errorMessage As String = ""
        Dim respuesta As Object

        Dim objPermiso As New PermisoDB
        Dim permisosAsignadosCorrectamente As Boolean

        If listaPermisosAsignados.Count > 0 Then
            permisosAsignadosCorrectamente = objPermiso.AsignarPermisos_x_Rol(idRol, idGrupo, listaPermisosAsignados, usuarioInserta, errorMessage)
        End If

        If Not permisosAsignadosCorrectamente Then
            respuesta = New With {
                .estado = False,
                .mensaje = "No se pudieron actualizar los permisos."
            }
        Else
            respuesta = New With {
                .estado = True,
                .mensaje = ""
            }
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub

End Class