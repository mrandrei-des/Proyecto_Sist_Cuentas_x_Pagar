Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class EstadoDB
    Private db As New DbHealper

    Public Function ConsultarEstados(ByRef errorMessage) As List(Of Models.Estado)
        Try
            Dim query As String = "sp_Cargar_Estados_Usuarios_Proveedores_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaEstados As New List(Of Models.Estado)()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim estado As New Models.Estado() With {
                        .IdEstado = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString()
                    }
                    listaEstados.Add(estado)
                Next
            End If
            Return listaEstados
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
