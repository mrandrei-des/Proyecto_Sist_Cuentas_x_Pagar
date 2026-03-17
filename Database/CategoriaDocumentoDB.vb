Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CategoriaDocumentoDB
    Private db As New DbHealper

    Public Function ConsultarCategoriaDocumento(ByRef errorMessage) As List(Of Models.CategoriaDocumento)
        Try
            Dim query As String = "sp_Cargar_Categoria_Documento_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim listaCategoriaDocumentos As New List(Of Models.CategoriaDocumento)()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim estado As New Models.CategoriaDocumento() With {
                        .IdCategoria = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString()
                    }
                    listaCategoriaDocumentos.Add(estado)
                Next
            End If

            Return listaCategoriaDocumentos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
