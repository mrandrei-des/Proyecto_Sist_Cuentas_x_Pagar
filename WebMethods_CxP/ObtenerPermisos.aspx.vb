Imports Proyecto_Sist_Cuentas_x_Pagar.Models

Public Class ObtenerPermisos
    Inherits System.Web.UI.Page

    Public Class BusquedaRequest
        Public Property GrupoPadre As Integer
        Public Property RolPadre As Integer
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Se lee el POST recibido
        Dim body As String
        Using sr As New IO.StreamReader(Request.InputStream)
            body = sr.ReadToEnd()
        End Using

        ' Deserializás el JSON
        Dim datos = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BusquedaRequest)(body)
        Dim idGrupo As Integer = datos.GrupoPadre
        Dim idRol As Integer = datos.RolPadre

        Dim errorMessage As String = ""
        Dim respuesta As Object
        Dim listaPermisos As New List(Of Permiso)
        Dim listaPermisosAsignados As New List(Of Integer)()
        Dim objPermiso As New PermisoDB

        listaPermisos = objPermiso.ConsultarPermisos_x_idGrupo(idGrupo, errorMessage)
        listaPermisosAsignados = objPermiso.ConsultarPermisos_x_idRol(idRol, errorMessage)

        If listaPermisos Is Nothing Then
            respuesta = New With {
                .estado = False,
                .lista = {},
                .listaAsignados = {},
                .mensaje = "No se pudo consultar los permisos."
            }
        Else
            If listaPermisos.Count > 0 Then
                respuesta = New With {
                .estado = True,
                .lista = listaPermisos,
                .listaAsignados = listaPermisosAsignados,
                .mensaje = ""
            }
            Else
                respuesta = New With {
                .estado = False,
                .lista = {},
                .listaAsignados = {},
                .mensaje = "No se encontraron permisos en el sistema."
            }
            End If
        End If

        Response.ContentType = "application/json"
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(respuesta))
        Response.End()
    End Sub

End Class