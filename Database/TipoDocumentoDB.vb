Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class TipoDocumentoDB
    Private db As New DbHealper

    Public Function ConsultarTipoDocumento(idCategoria As Integer, ByRef errorMessage As String) As List(Of Models.TipoDocumento)

        Dim query As String = "sp_Cargar_Tipo_Documento_ddl"
        Dim parameters As New List(Of SqlParameter) From {
             New SqlParameter("@ID_Categoria", idCategoria)
        }

        Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim listaTipoDocumentos As New List(Of Models.TipoDocumento)()

            For x As Integer = 0 To dt.Rows.Count - 1
                Dim tipoDocumento As New Models.TipoDocumento() With {
                    .IdTipoDocumento = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                    .Descripcion = dt.Rows(x)("Texto").ToString(),
                    .IdCategoria = Convert.ToInt32(dt.Rows(x)("IdCategoria").ToString())
                }
                listaTipoDocumentos.Add(tipoDocumento)
            Next

            Return listaTipoDocumentos
        End If
        Return Nothing
    End Function
End Class
