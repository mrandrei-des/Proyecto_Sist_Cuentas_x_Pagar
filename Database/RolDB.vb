Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class RolDB
    Private db As New DbHealper

    Public Function ConsultarRoles(ByRef errorMessage) As List(Of Models.Rol)
        Try
            Dim query As String = "sp_Cargar_Roles_Usuario_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)
            Dim listaRoles As New List(Of Models.Rol)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim rol As New Models.Rol() With {
                        .IdRol = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString(),
                        .Estado = Convert.ToInt32(dt.Rows(x)("Estado").ToString())
                    }
                    listaRoles.Add(rol)
                Next
            End If
            Return listaRoles
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
