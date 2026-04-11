Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class MonedaDB
    Private db As New DbHealper
    Public Function ConsultarMonedas(ByRef errorMessage) As List(Of Models.Moneda)
        Try
            Dim query As String = "sp_Carga_Monedas_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaMonedas As New List(Of Models.Moneda)()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim moneda As New Models.Moneda() With {
                        .CodigoMoneda = dt.Rows(x)("VALOR").ToString(),
                        .Descripcion = dt.Rows(x)("TEXTO").ToString(),
                        .Simbolo = dt.Rows(x)("Simbolo").ToString()
                    }
                    listaMonedas.Add(moneda)
                Next
            End If
            Return listaMonedas
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultaCantTotalDocs_x_Monedas(filtTipoDocumento As String, filtMoneda As String, filtFechaInicio As String, filtFechaFin As String, ByRef errorMessage As String) As List(Of Models.Moneda)
        Try
            Dim query As String = "sp_Carga_Monedas_CantDocumentos_Aplicados"

            ' Se agregan los parámetros del procedimiento almacenado a una lista de SqlParameter
            Dim parameters As New List(Of SqlParameter)

            If filtTipoDocumento.IsNullOrWhiteSpace() Then
                parameters.Add(New SqlParameter("@FiltTipoDocumento", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltTipoDocumento", Convert.ToInt32(filtTipoDocumento)))
            End If

            If filtMoneda.IsNullOrWhiteSpace() Then
                parameters.Add(New SqlParameter("@FiltMoneda", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltMoneda", filtMoneda))
            End If

            If filtFechaInicio.IsNullOrWhiteSpace() Then
                parameters.Add(New SqlParameter("@FiltFechaInicio", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltFechaInicio", Date.Parse(filtFechaInicio)))
            End If

            If filtFechaFin.IsNullOrWhiteSpace() Then
                parameters.Add(New SqlParameter("@FiltFechaFin", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltFechaFin", Date.Parse(filtFechaFin)))
            End If

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaMonedas As New List(Of Models.Moneda)()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim moneda As New Models.Moneda() With {
                        .CodigoMoneda = dt.Rows(x)("VALOR").ToString(),
                        .Descripcion = dt.Rows(x)("TEXTO").ToString()
                    }
                    listaMonedas.Add(moneda)
                Next
            End If
            Return listaMonedas
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
