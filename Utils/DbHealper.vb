Imports System.Data.SqlClient

Namespace Utils
    Public Class DbHealper
        Private connectionString As String = ConfigurationManager.ConnectionStrings("Sist_Cuentas_x_PagarConnectionString").ConnectionString

        'Método para crear y abrir una conexión a la base de datos
        Public Function GetConnection() As SqlConnection
            Dim conn As New SqlConnection(connectionString)
            Try
                conn.Open()
            Catch ex As Exception
                ' Si la conexión no se realiza, libera los recursos para evitar conexiones fantasmas/fallidas
                conn.Dispose()
                Throw New Exception("Error al abrir la conexión: " + ex.Message)
            End Try
            Return conn
        End Function

        'Método para ejecutar un comando SQL (INSERT, UPDATE, DELETE)
        Public Function ExecuteNonQuery(query As String, parameters As Dictionary(Of String, Object), ByRef errorMessage As String) As Boolean
            If String.IsNullOrWhiteSpace(query) Then
                Throw New ArgumentException("La consulta no puede estar vacía.")
            End If

            'Se crea una variable connection de la clase DBHelper por medio de la función GetConnection()
            Using conn As SqlConnection = GetConnection()
                'Se crea una variable de SqlCommand con el query a ejecutar y la conexión donde debe ejecutarlo
                Using cmd As New SqlCommand(query, conn)
                    If parameters IsNot Nothing Then
                        ' Agrega cada uno de los parámetros que contenga el diccionario en el parameters del SQL Command
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, param.Value)
                        Next
                    End If
                    Try
                        cmd.ExecuteNonQuery()
                        Return True
                    Catch ex As Exception
                        errorMessage = "Error al ejecutar la consulta: [" & ex.Message & "]"
                        Return False
                    End Try
                End Using
            End Using
        End Function

    End Class
End Namespace