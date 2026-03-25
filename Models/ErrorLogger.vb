Public Class ErrorLogger
    Private ReadOnly _logFilePath As String = "C:\Logs\error_log.txt"

    Public ReadOnly Property LogFilePath As String
        Get
            Return _logFilePath
        End Get
    End Property
End Class
