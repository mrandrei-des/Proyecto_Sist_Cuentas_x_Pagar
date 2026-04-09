Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class DocumentoAplicadoDB
    Private db As New DbHealper
    Public Function FiltrarDocumentosAplicados(filtTipoDocumento As String, filtMoneda As String, filtFechaInicio As String, filtFechaFin As String, ByRef errorMessage As String) As List(Of Models.DocumentoAplicado)
        Try

            Dim query As String = "sp_Filtrar_DocumentosAplicados"

            If Not filtTipoDocumento.IsNullOrWhiteSpace() Then
                If IsNumeric(filtTipoDocumento) Then
                    Dim objTipoDocDB As New TipoDocumentoDB
                    Dim idCategoria As Integer = objTipoDocDB.ConsultarIDCategoria_x_TipoDocumento(Convert.ToInt32(filtTipoDocumento), errorMessage)

                    If idCategoria = 1 Then
                        query = "sp_Filtrar_Facturas_Aplicadas"
                    Else
                        query = "sp_Filtrar_DocumentosFormasPago_Aplicadas"
                    End If
                End If
            End If

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

            Dim listaDocAplicados As New List(Of Models.DocumentoAplicado)
            Dim objHerramientas As New Herramientas
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each documento As DataRow In dt.Rows

                    Dim modDocAplicado As New Models.DocumentoAplicado() With {
                        .IdCategoria = Convert.ToInt32(documento("idCategoria").ToString()),
                        .IdProveedor = Convert.ToInt32(documento("idProveedor").ToString()),
                        .IdTipoDoc = Convert.ToInt32(documento("TipoDoc").ToString()),
                        .NumDocumento = documento("NumDoc").ToString(),
                        .Simbolo = documento("Simbolo").ToString(),
                        .Monto = $"{ .Simbolo}{objHerramientas.FormatearMonto(Double.Parse(documento("Monto").ToString()))}",
                        .NombreProveedor = documento("NombreProveedor").ToString()
                    }

                    listaDocAplicados.Add(modDocAplicado)
                Next
            End If

            Return listaDocAplicados
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
