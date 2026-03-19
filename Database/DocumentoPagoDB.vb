Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class DocumentoPagoDB
    Private db As New DbHealper

    'Función que tiene el query para agregar un nuevo documento de pago
    Public Function CrearDocumentoPago(objDocumentoPago As Models.DocumentoPago, usuarioInserta As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Inserta_DocumentoFormaPago_Nuevo"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objDocumentoPago.IdProveedor),
                New SqlParameter("@TipoDocumento", objDocumentoPago.TipoDocumento),
                New SqlParameter("@NumeroDocumento", objDocumentoPago.NumeroDocumento),
                New SqlParameter("@Observacion", objDocumentoPago.Observacion),
                New SqlParameter("@FechaEmision", objDocumentoPago.FechaEmision),
                New SqlParameter("@Estado", objDocumentoPago.Estado),
                New SqlParameter("@Moneda", objDocumentoPago.Moneda),
                New SqlParameter("@TipoCambio", 500),
                New SqlParameter("@Total", objDocumentoPago.Total),
                New SqlParameter("@SaldoActual", objDocumentoPago.SaldoActual),
                New SqlParameter("@UsuarioCreacion", usuarioInserta)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ModificarDocumentoPago(objDocumentoPago As Models.DocumentoPago, usuarioModifica As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Modificar_DocumentoFormaPago"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objDocumentoPago.IdProveedor),
                New SqlParameter("@TipoDocumento", objDocumentoPago.TipoDocumento),
                New SqlParameter("@NumeroDocumento", objDocumentoPago.NumeroDocumento),
                New SqlParameter("@Observacion", objDocumentoPago.Observacion),
                New SqlParameter("@FechaEmision", objDocumentoPago.FechaEmision),
                New SqlParameter("@Estado", objDocumentoPago.Estado),
                New SqlParameter("@Moneda", objDocumentoPago.Moneda),
                New SqlParameter("@TipoCambio", objDocumentoPago.TipoCambio),
                New SqlParameter("@Total", objDocumentoPago.Total),
                New SqlParameter("@SaldoActual", objDocumentoPago.SaldoActual),
                New SqlParameter("@UsuarioModifico", usuarioModifica)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Función que elimina el documento de pago indicado
    Public Function EliminarDocumentoPago(objDocumentoPago As Models.DocumentoPago, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Eliminar_DocumentoFormaPago"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objDocumentoPago.IdProveedor),
                New SqlParameter("@TipoDocumento", objDocumentoPago.TipoDocumento),
                New SqlParameter("@NumeroDocumento", objDocumentoPago.NumeroDocumento),
                New SqlParameter("@UsuarioElimino", usuarioElimino)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConsultarExistenciaDocumentoPago(objDocumentoPago As Models.DocumentoPago, errorMessage As String) As Models.DocumentoPago
        Try
            Dim query As String = "sp_Consulta_Existencia_DocumentoFormaPago"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Proveedor", objDocumentoPago.IdProveedor),
                 New SqlParameter("@TipoDocumento", objDocumentoPago.TipoDocumento),
                 New SqlParameter("@NumeroDocumento", objDocumentoPago.NumeroDocumento)
            }
            Dim modDocumentoPago As New Models.DocumentoPago
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                With modDocumentoPago
                    .IdProveedor = Convert.ToInt32(row("IdProveedor"))
                    .TipoDocumento = row("TipoDocumento").ToString()
                    .NumeroDocumento = row("NumeroDocumento").ToString()
                    .Observacion = row("Observacion").ToString()
                    .FechaEmision = Convert.ToDateTime(row("FechaEmision"))
                    .Estado = Convert.ToInt32(row("Estado"))
                    .Moneda = row("Moneda").ToString()
                    .TipoCambio = Convert.ToDouble(row("TipoCambio").ToString())
                    .Total = Convert.ToDouble(row("Total").ToString())
                    .SaldoActual = Convert.ToDouble(row("SaldoActual").ToString())
                End With
            End If
            Return modDocumentoPago
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function BuscarDocumentoPago_x_Numero(objDocumentoPago As Models.DocumentoPago, errorMessage As String) As Models.DocumentoPago
        Try
            Dim query As String = "sp_Buscar_DocumentoFormaPago"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Proveedor", objDocumentoPago.IdProveedor),
                 New SqlParameter("@TipoDocumento", objDocumentoPago.TipoDocumento),
                 New SqlParameter("@NumeroDocumento", objDocumentoPago.NumeroDocumento)
            }
            Dim modDocumentoPago As New Models.DocumentoPago
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                With modDocumentoPago
                    .IdProveedor = Convert.ToInt32(row("IdProveedor"))
                    .TipoDocumento = row("TipoDocumento").ToString()
                    .NumeroDocumento = row("NumeroDocumento").ToString()
                    .Observacion = row("Observacion").ToString()
                    .FechaEmision = Convert.ToDateTime(row("FechaEmision"))
                    .Estado = Convert.ToInt32(row("Estado"))
                    .Moneda = row("Moneda").ToString()
                    .TipoCambio = Convert.ToDouble(row("TipoCambio").ToString())
                    .Total = Convert.ToDouble(row("Total").ToString())
                    .SaldoActual = Convert.ToDouble(row("SaldoActual").ToString())
                End With
            End If
            Return modDocumentoPago
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AplicarDocumentoPago(objDocumentoPago As Models.DocumentoPago, usuarioAplica As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Aplicar_DocumentoFormaPago"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objDocumentoPago.IdProveedor),
                New SqlParameter("@TipoDocumento", objDocumentoPago.TipoDocumento),
                New SqlParameter("@NumeroDocumento", objDocumentoPago.NumeroDocumento),
                New SqlParameter("@UsuarioAplico", usuarioAplica)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
