Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities
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
                    .FechaEmision = CDate(row("FechaEmision"))
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
                    .FechaEmision = CDate(row("FechaEmision"))
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

    Public Function FiltrarDocumentosPendientes(filtNumProveedor As Integer, filtFechaInicio As String, filtFechaFin As String, orderByClause As String, ByRef errorMessage As String) As List(Of Object)
        Try
            Dim query As String = "sp_Filtrar_DocumentosFormasPago_Pendientes"

            ' Se agregan los parámetros del procedimiento almacenado a una lista de SqlParameter
            Dim parameters As New List(Of SqlParameter)

            If filtNumProveedor = 0 Then
                parameters.Add(New SqlParameter("@FiltID_Proveedor", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltID_Proveedor", filtNumProveedor))
            End If

            If filtFechaInicio.IsNullOrWhiteSpace() Then
                parameters.Add(New SqlParameter("@FiltFechaEmisionDesde", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltFechaEmisionDesde", Date.Parse(filtFechaInicio)))
            End If

            If filtFechaFin.IsNullOrWhiteSpace() Then
                parameters.Add(New SqlParameter("@FiltFechaEmisionHasta", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltFechaEmisionHasta", Date.Parse(filtFechaFin)))
            End If

            parameters.Add(New SqlParameter("@OrderByCondition", orderByClause))

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaDocumentos As New List(Of Object)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each documento As DataRow In dt.Rows
                    Dim modObjeto As Object = New With {
                        .idCategoriaDoc = 2,
                        .idProveedor = Convert.ToInt32(documento("IdProveedor").ToString()),
                        .nombreProveedor = documento("NombreProveedor").ToString(),
                        .tipoDocumento = documento("TipoDocumento").ToString(),
                        .numDocumento = documento("NumeroDocumento").ToString(),
                        .fecha = documento("FechaFormateada").ToString(),
                        .montoTotal = Double.Parse(documento("Total").ToString()),
                        .simboloMoneda = documento("SimboloMoneda").ToString()
                    }
                    listaDocumentos.Add(modObjeto)
                Next
            End If

            Return listaDocumentos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
