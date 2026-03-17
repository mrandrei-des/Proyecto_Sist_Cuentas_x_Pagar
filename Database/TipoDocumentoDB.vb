Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class TipoDocumentoDB
    Private db As New DbHealper

    Public Function ConsultarTipoDocumento(idCategoria As Integer, ByRef errorMessage As String) As List(Of Models.TipoDocumento)
        Try
            Dim query As String = "sp_Cargar_Tipo_Documento_ddl"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Categoria", idCategoria)
            }

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)
            Dim listaTipoDocumentos As New List(Of Models.TipoDocumento)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim tipoDocumento As New Models.TipoDocumento() With {
                        .IdTipoDocumento = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString(),
                        .IdCategoria = Convert.ToInt32(dt.Rows(x)("IdCategoria").ToString())
                    }
                    listaTipoDocumentos.Add(tipoDocumento)
                Next
            End If
            Return listaTipoDocumentos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class