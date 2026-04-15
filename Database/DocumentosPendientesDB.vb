Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class DocumentosPendientesDB
    Private db As New DbHealper

    Public Function ObtenerDocumentosPendientesAntiguos(ByRef errorMessage) As List(Of Models.DocumentosPendientes)
        Try
            Dim query As String = "sp_ConsultaDocumentosPendientes_Antiguos"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaDocsPendientes As New List(Of Models.DocumentosPendientes)
            Dim objHerramientas As New Herramientas

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each documento As DataRow In dt.Rows

                    Dim modDocsPendientes As New Models.DocumentosPendientes()
                    With modDocsPendientes
                        .IdCategoria = Convert.ToInt32(documento("idCategoria").ToString())
                        .NombreProveedor = documento("NombreProveedor").ToString()
                        .NumDocumento = documento("NumDoc").ToString()
                        .SimboloMoneda = documento("Simbolo").ToString()
                        .TotalDocumento = Convert.ToDouble(documento("Total").ToString())
                        .TotalDocumentoFormateado = objHerramientas.FormatearMonto(.TotalDocumento)
                        .CantDias = Convert.ToInt32(documento("CantDias").ToString())
                    End With

                    listaDocsPendientes.Add(modDocsPendientes)
                Next
            End If

            Return listaDocsPendientes
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
