Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class MonedaDB
    Private db As New DbHealper
    Public Function ConsultarMonedas(ByRef errorMessage) As List(Of Models.Moneda)

        Dim query As String = "sp_Carga_Monedas_ddl"
        Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim listaMonedas As New List(Of Models.Moneda)()

            For x As Integer = 0 To dt.Rows.Count - 1
                Dim moneda As New Models.Moneda() With {
                    .CodigoMoneda = dt.Rows(x)("VALOR").ToString(),
                    .Descripcion = dt.Rows(x)("TEXTO").ToString(),
                    .Simbolo = dt.Rows(x)("Simbolo").ToString()
                }
                listaMonedas.Add(moneda)
            Next

            Return listaMonedas
        End If
        Return Nothing
    End Function
End Class
