Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class ContadorDB
    Private db As New DbHealper

    Public Function ConsultarCantDocsRegistrados_hoy(ByRef errorMessage As String) As Models.Contador
        Try
            Dim query As String = "sp_ConsultaCantidadDocsRegistrados_hoy"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim modContador As New Models.Contador()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                With modContador
                    .Cantidad = Convert.ToInt32(dt.Rows(0)("CantDocsIngresados").ToString())
                End With
            End If

            Return modContador
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarCantProveedorRegistrados_semana(fechaInicioSemana As String, fechaFinSemana As String, ByRef errorMessage As String) As Models.Contador
        Try
            Dim query As String = "sp_ConsultaCantidadProveedoresRegistrados_EstaSemana"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@FechaDiaInicial", Date.Parse(fechaInicioSemana)),
                New SqlParameter("@FechaDiaFinal", Date.Parse(fechaFinSemana))
            }

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim modContador As New Models.Contador()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                With modContador
                    .Cantidad = Convert.ToInt32(dt.Rows(0)("CANTIDAD").ToString())
                End With
            End If

            Return modContador
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarCantFacturasPendientes(ByRef errorMessage As String) As Models.Contador
        Try
            Dim query As String = "sp_ConsultaCantidadFacturasPendientes"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim modContador As New Models.Contador()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                With modContador
                    .Cantidad = Convert.ToInt32(dt.Rows(0)("CANTIDAD").ToString())
                End With
            End If

            Return modContador
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarCantPagosPendientes(ByRef errorMessage As String) As Models.Contador
        Try
            Dim query As String = "sp_ConsultaCantidadDocsPagoPendientes"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim modContador As New Models.Contador()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                With modContador
                    .Cantidad = Convert.ToInt32(dt.Rows(0)("CANTIDAD").ToString())
                End With
            End If

            Return modContador
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarCantDocsAplicados_mes(ByRef errorMessage As String) As Models.Contador
        Try
            Dim query As String = "sp_ConsultaCantidadDocsAplicados_EsteMes"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim modContador As New Models.Contador()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                With modContador
                    .Cantidad = Convert.ToInt32(dt.Rows(0)("CANTAPLICADOS").ToString())
                End With
            End If

            Return modContador
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
