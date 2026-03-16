Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CategoriaDocumentoDB
    Private db As New DbHealper

    Public Function ConsultarCategoriaDocumento(ByRef errorMessage) As List(Of Models.CategoriaDocumento)
        Try
            Dim query As String = "sp_Cargar_Categoria_Documento_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim listaCategoriaDocumentos As New List(Of Models.CategoriaDocumento)()

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim estado As New Models.CategoriaDocumento() With {
                        .IdCategoria = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString()
                    }
                    listaCategoriaDocumentos.Add(estado)
                Next

                Return listaCategoriaDocumentos
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
