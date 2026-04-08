Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class MontosTotalesDB
    Private db As New DbHealper

    Public Function ConsultarMontosTotales_Facturas(ByRef errorMessage) As List(Of Models.MontosTotales)
        Try
            Dim query As String = "sp_ConsultaMontoTotal_Facturas"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaMontos As New List(Of Models.MontosTotales)()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim monto As New Models.MontosTotales() With {
                        .Moneda = dt.Rows(x)("Moneda").ToString(),
                        .Monto = Convert.ToDouble(dt.Rows(x)("Pend_Pago_Fact").ToString()),
                        .Simbolo = dt.Rows(x)("Simbolo").ToString()
                    }
                    listaMontos.Add(monto)
                Next
            End If
            Return listaMontos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarMontosTotales_Documentos(ByRef errorMessage) As List(Of Models.MontosTotales)
        Try
            Dim query As String = "sp_ConsultaMontoTotal_Documentos"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaMontos As New List(Of Models.MontosTotales)()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim monto As New Models.MontosTotales() With {
                        .Moneda = dt.Rows(x)("Moneda").ToString(),
                        .Monto = Convert.ToDouble(dt.Rows(x)("Pend_Pago_Docs").ToString()),
                        .Simbolo = dt.Rows(x)("Simbolo").ToString()
                    }
                    listaMontos.Add(monto)
                Next
            End If
            Return listaMontos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
