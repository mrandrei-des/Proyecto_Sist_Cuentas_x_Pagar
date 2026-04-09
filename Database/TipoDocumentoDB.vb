Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities
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

    Public Function ConsultarTipoDocumento_Todos(ByRef errorMessage As String) As List(Of Models.TipoDocumento)
        Try
            Dim query As String = "sp_Cargar_Tipo_Documentos"

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)
            Dim listaTipoDocumentos As New List(Of Models.TipoDocumento)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim tipoDocumento As New Models.TipoDocumento() With {
                        .IdTipoDocumento = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString()
                    }
                    listaTipoDocumentos.Add(tipoDocumento)
                Next
            End If
            Return listaTipoDocumentos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarIDCategoria_x_TipoDocumento(idTipoDocumento As Integer, ByRef errorMessage As String) As Integer
        Try
            Dim query As String = "sp_Consulta_Categoria_Tipo_Documento"
            Dim parameters As New List(Of SqlParameter)

            parameters.Add(New SqlParameter("@ID_TipoDocumento", idTipoDocumento))

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0)("IdCategoria").ToString())
            End If

            Return -1
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function BuscaTipoDocumento_x_IDTipo(idTipoDocumento As Integer, ByRef errorMessage As String) As Models.TipoDocumento
        Try
            Dim query As String = "sp_Consulta_Categoria_Tipo_Documento"
            Dim parameters As New List(Of SqlParameter)

            parameters.Add(New SqlParameter("@ID_TipoDocumento", idTipoDocumento))

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            Dim modTipoDocumento As New Models.TipoDocumento()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                With modTipoDocumento
                    .IdCategoria = Convert.ToInt32(dt.Rows(0)("IdCategoria").ToString())
                    .IdTipoDocumento = Convert.ToInt32(dt.Rows(0)("VALOR").ToString())
                    .Descripcion = dt.Rows(0)("TEXTO").ToString()
                End With

                Return modTipoDocumento
            End If

            Return modTipoDocumento
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class