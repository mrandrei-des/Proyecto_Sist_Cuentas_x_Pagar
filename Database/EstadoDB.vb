Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class EstadoDB
    Private db As New DbHealper

    Public Function ConsultarEstados(ByRef errorMessage) As List(Of Models.Estado)
        Try
            Dim query As String = "sp_Cargar_Estados_Usuarios_Proveedores_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim listaEstados As New List(Of Models.Estado)()

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim estado As New Models.Estado() With {
                        .IdEstado = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString()
                    }
                    listaEstados.Add(estado)
                Next

                Return listaEstados
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
