Imports System.Data.SqlClient
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
                New SqlParameter("@TipoCambio", 500),
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
                    .FechaEmision = Convert.ToDateTime(row("FechaEmision"))
                    .Estado = Convert.ToInt32(row("Estado"))
                    .Moneda = row("Moneda").ToString()
                    .TipoCambio = Convert.ToDouble(row("Moneda").ToString())
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
                    .FechaEmision = Convert.ToDateTime(row("FechaEmision"))
                    .Estado = Convert.ToInt32(row("Estado"))
                    .Moneda = row("Moneda").ToString()
                    .TipoCambio = Convert.ToDouble(row("Moneda").ToString())
                    .Total = Convert.ToDouble(row("Total").ToString())
                    .SaldoActual = Convert.ToDouble(row("SaldoActual").ToString())
                End With
            End If
            Return modFactura
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class