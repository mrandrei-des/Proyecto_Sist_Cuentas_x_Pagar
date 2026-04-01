Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class GrupoPermisoDB
    Private db As New DbHealper()
    Public Function ConsultarGruposPermisos(ByRef errorMessage As String) As List(Of Models.GrupoPermiso)
        Try
            Dim query As String = "sp_Consulta_GruposPermisos"

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)
            Dim listaGruposPermisos As New List(Of Models.GrupoPermiso)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim grupo As New Models.GrupoPermiso() With {
                        .IdGrupoPermiso = Convert.ToInt32(dt.Rows(x)("IDGrupo").ToString()),
                        .Descripcion = dt.Rows(x)("Descripcion").ToString()
                    }
                    listaGruposPermisos.Add(grupo)
                Next
            End If
            Return listaGruposPermisos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
