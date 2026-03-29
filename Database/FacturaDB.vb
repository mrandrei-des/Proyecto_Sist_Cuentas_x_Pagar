Imports System.Data.SqlClient
Imports System.Web.WebSockets
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class FacturaDB
    Private db As New DbHealper

    'Función que tiene el query para agregar una nueva factura
    Public Function CrearFactura(objFactura As Models.Factura, usuarioInserta As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Inserta_Factura_Nueva"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objFactura.IdProveedor),
                New SqlParameter("@TipoFactura", objFactura.TipoFactura),
                New SqlParameter("@NumeroFactura", objFactura.NumeroFactura),
                New SqlParameter("@Observacion", objFactura.Observacion),
                New SqlParameter("@FechaEmision", objFactura.FechaEmision),
                New SqlParameter("@Estado", objFactura.Estado),
                New SqlParameter("@Moneda", objFactura.Moneda),
                New SqlParameter("@TipoCambio", 500),
                New SqlParameter("@Total", objFactura.Total),
                New SqlParameter("@SaldoActual", objFactura.SaldoActual),
                New SqlParameter("@UsuarioCreacion", usuarioInserta)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ModificarFactura(objFactura As Models.Factura, usuarioModifica As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Modificar_Factura"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objFactura.IdProveedor),
                New SqlParameter("@TipoFactura", objFactura.TipoFactura),
                New SqlParameter("@NumeroFactura", objFactura.NumeroFactura),
                New SqlParameter("@Observacion", objFactura.Observacion),
                New SqlParameter("@FechaEmision", objFactura.FechaEmision),
                New SqlParameter("@Estado", objFactura.Estado),
                New SqlParameter("@Moneda", objFactura.Moneda),
                New SqlParameter("@TipoCambio", objFactura.TipoCambio),
                New SqlParameter("@Total", objFactura.Total),
                New SqlParameter("@SaldoActual", objFactura.SaldoActual),
                New SqlParameter("@UsuarioModifico", usuarioModifica)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Función que elimina una factura
    Public Function EliminarFactura(objFactura As Models.Factura, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Eliminar_Factura"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objFactura.IdProveedor),
                New SqlParameter("@TipoFactura", objFactura.TipoFactura),
                New SqlParameter("@NumeroFactura", objFactura.NumeroFactura),
                New SqlParameter("@UsuarioElimino", usuarioElimino)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConsultarExistenciaFactura(objFactura As Models.Factura, errorMessage As String) As Models.Factura
        Try
            Dim query As String = "sp_Consulta_Existencia_Factura"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Proveedor", objFactura.IdProveedor),
                 New SqlParameter("@TipoFactura", objFactura.TipoFactura),
                 New SqlParameter("@NumeroFactura", objFactura.NumeroFactura)
            }
            Dim modFactura As New Models.Factura
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                With modFactura
                    .IdProveedor = Convert.ToInt32(row("IdProveedor"))
                    .TipoFactura = row("TipoFactura").ToString()
                    .NumeroFactura = row("NumeroFactura").ToString()
                    .Observacion = row("Observacion").ToString()
                    .FechaEmision = CDate(row("FechaEmision"))
                    .Estado = Convert.ToInt32(row("Estado"))
                    .Moneda = row("Moneda").ToString()
                    .TipoCambio = Convert.ToDouble(row("TipoCambio").ToString())
                    .Total = Convert.ToDouble(row("Total").ToString())
                    .SaldoActual = Convert.ToDouble(row("SaldoActual").ToString())
                End With
            End If
            Return modFactura
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function BuscarFactura_x_Numero(objFactura As Models.Factura, errorMessage As String) As Models.Factura
        Try
            Dim query As String = "sp_Buscar_Factura"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Proveedor", objFactura.IdProveedor),
                 New SqlParameter("@TipoFactura", objFactura.TipoFactura),
                 New SqlParameter("@NumeroFactura", objFactura.NumeroFactura)
            }
            Dim modFactura As New Models.Factura
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                With modFactura
                    .IdProveedor = Convert.ToInt32(row("IdProveedor"))
                    .TipoFactura = row("TipoFactura").ToString()
                    .NumeroFactura = row("NumeroFactura").ToString()
                    .Observacion = row("Observacion").ToString()
                    .FechaEmision = CDate(row("FechaEmision"))
                    .Estado = Convert.ToInt32(row("Estado"))
                    .Moneda = row("Moneda").ToString()
                    .TipoCambio = Convert.ToDouble(row("TipoCambio").ToString())
                    .Total = Convert.ToDouble(row("Total"))
                    .SaldoActual = Convert.ToDouble(row("SaldoActual").ToString())
                End With
            End If
            Return modFactura
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AplicarFactura(objFactura As Models.Factura, usuarioModifica As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Aplicar_Factura"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Proveedor", objFactura.IdProveedor),
                New SqlParameter("@TipoFactura", objFactura.TipoFactura),
                New SqlParameter("@NumeroFactura", objFactura.NumeroFactura),
                New SqlParameter("@UsuarioAplico", usuarioModifica)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FiltrarFacturasPendientes(filtNumProveedor As Integer, filtFechaInicio As String, filtFechaFin As String, orderByClause As String, ByRef errorMessage As String) As List(Of Object)
        Try
            Dim query As String = "sp_Filtrar_Facturas_Pendientes"

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

            Dim listaFacturas As New List(Of Object)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each factura As DataRow In dt.Rows
                    Dim modObjeto As Object = New With {
                        .idCategoriaDoc = 1,
                        .idProveedor = Convert.ToInt32(factura("IdProveedor").ToString()),
                        .nombreProveedor = factura("NombreProveedor").ToString(),
                        .tipoDocumento = factura("TipoFactura").ToString(),
                        .numDocumento = factura("NumeroFactura").ToString(),
                        .fecha = factura("FechaFormateada").ToString(),
                        .montoTotal = Double.Parse(factura("Total").ToString()),
                        .simboloMoneda = factura("SimboloMoneda").ToString()
                    }
                    listaFacturas.Add(modObjeto)
                Next
            End If

            Return listaFacturas
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class