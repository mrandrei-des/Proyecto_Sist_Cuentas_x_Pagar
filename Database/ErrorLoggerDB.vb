Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class ErrorLoggerDB
    ' Método principal para registrar errores
    Public Sub SaveDBError(ex As Exception)

        Dim fullMessage As String = $"Message: {ex.Message}" & Environment.NewLine & $"StackTrace: {ex.StackTrace}" & Environment.NewLine &
        $"InnerException: {If(ex.InnerException IsNot Nothing, ex.InnerException.Message, "N/A")}"

        Dim severity As Integer = 16
        Dim state As Integer = 1
        Dim procedureName As String = If(ex.TargetSite IsNot Nothing, ex.TargetSite.Name, "")
        Dim lineNumber As Integer = 0

        If ex.StackTrace IsNot Nothing AndAlso ex.StackTrace.Contains(":line ") Then
            Integer.TryParse(ex.StackTrace.Split(New String() {":line "}, StringSplitOptions.None).Last().Split(" "c)(0), lineNumber)
        End If

        Try
            Dim db As New DbHealper
            Dim query As String = "sp_Inserta_ErrorLogger"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ErrorMessage", fullMessage),
                New SqlParameter("@ErrorSeverity", severity),
                New SqlParameter("@ErrorState", state),
                New SqlParameter("@ErrorLine", lineNumber),
                New SqlParameter("@ErrorProcedure", If(procedureName.IsNullOrWhiteSpace(), DBNull.Value, procedureName))
            }

            db.ExecuteNonQuery(query, parameters, "")

        Catch logEx As Exception
            ' Fallback: guardar en archivo si falla el log a la base de datos
            LogErrorToFile(fullMessage & Environment.NewLine & "Log DB Error: " & logEx.Message)
        End Try
    End Sub

    Private Sub LogErrorToFile(message As String)
        Try
            Dim modErrorLogger As New ErrorLogger()
            ' Asegurar que el directorio exista
            Dim dir As String = Path.GetDirectoryName(modErrorLogger.LogFilePath)
            If Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            File.AppendAllText(modErrorLogger.LogFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}{New String("-"c, 50)}{Environment.NewLine}")
        Catch ex As Exception
            ' Si también falla el archivo, no hay mucho más que hacer sin lanzar una excepción que rompa la app
        End Try
    End Sub
End Class
