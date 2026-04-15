Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class UltimosCambiosDB
    Private db As New DbHealper

    Public Function ObtenerUltimosCambios(ByRef errorMessage) As List(Of Models.UltimosCambios)
        Try
            Dim query As String = "sp_ConsultaUltimosCambios"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaCambios As New List(Of Models.UltimosCambios)
            Dim objHerramientas As New Herramientas

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each documento As DataRow In dt.Rows
                    Dim modUltCambios As New Models.UltimosCambios()
                    With modUltCambios
                        .HaceTiempo = documento("HaceTiempo").ToString()
                        .NombreProveedor = documento("NombreProveedor").ToString()
                        .IdCategoria = Convert.ToInt32(documento("idCategoria").ToString())
                        .NumDocumento = documento("NumDoc").ToString()
                        .UsuarioAccion = documento("NombreUsuario").ToString()
                        .AccionRealizada = objHerramientas.prcDevuelveNombreAccion(.IdCategoria, documento("Accion").ToString())
                    End With

                    listaCambios.Add(modUltCambios)
                Next
            End If

            Return listaCambios
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


End Class
