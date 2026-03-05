Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class RolDB
    Private db As New DbHealper

    Public Function ConsultarRoles(ByRef errorMessage) As List(Of Models.Rol)

        Dim query As String = "sp_Cargar_Roles_Usuario_ddl"
        Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim listaRoles As New List(Of Models.Rol)()

            For x As Integer = 0 To dt.Rows.Count - 1
                Dim rol As New Models.Rol() With {
                    .IdRol = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                    .Descripcion = dt.Rows(x)("Texto").ToString(),
                    .Estado = Convert.ToInt32(dt.Rows(x)("Estado").ToString())
                }
                listaRoles.Add(rol)
            Next

            Return listaRoles
        End If
        Return Nothing
    End Function
End Class
