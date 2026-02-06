Imports System.Data.SqlClient

Namespace Utils
    Public Class DbHealper
        Private connectionString As String = ConfigurationManager.ConnectionStrings("Sist_Cuentas_x_PagarConnectionString").ConnectionString

        Public Function GetConnection() As SqlConnection
            Return New SqlConnection(connectionString)
        End Function
    End Class
End Namespace