Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class UsuarioDB
    Private db As New DbHealper

    'Función que tiene el script para agregar un usuario nuevo
    Public Function CrearUsuario(ByVal objUsuario As Models.Usuario, ByVal usuarioCreacion As String, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "sp_Inserta_Usuario_Nuevo"

            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@Usuario", objUsuario.NombreUsuario),
                New SqlParameter("@Contrasenna", objUsuario.Contrasenna),
                New SqlParameter("@Nombre", objUsuario.Nombre),
                New SqlParameter("@Apellido1", objUsuario.Apellido1),
                New SqlParameter("@Apellido2", objUsuario.Apellido2),
                New SqlParameter("@CorreoElectronico", objUsuario.Correo),
                New SqlParameter("@Estado", objUsuario.Estado),
                New SqlParameter("@Rol", objUsuario.Rol),
                New SqlParameter("@UsuarioCreacion", usuarioCreacion)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function

    'Función que tiene el query para eliminar un usuario
    Public Function EliminarUsuario(usuarioAfectado As String, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Dim query As String = "sp_Eliminar_Usuario"

        Dim parameters As New List(Of SqlParameter) From {
            New SqlParameter("@UsuarioAfectado", usuarioAfectado),
            New SqlParameter("@UsuarioElimino", usuarioElimino)
        }

        Return db.ExecuteNonQuery(query, parameters, errorMessage)
    End Function

    'Función que tiene el query para modificar un usuario
    Public Function ModificarUsuario(ByVal objUsuario As Models.Usuario, ByVal usuarioModifico As String, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "sp_Modificar_Usuario"

            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@UsuarioAfectado", objUsuario.NombreUsuario),
                New SqlParameter("@Nombre", objUsuario.Nombre),
                New SqlParameter("@Apellido1", objUsuario.Apellido1),
                New SqlParameter("@Apellido2", objUsuario.Apellido2),
                New SqlParameter("@CorreoElectronico", objUsuario.Correo),
                New SqlParameter("@Estado", objUsuario.Estado),
                New SqlParameter("@Rol", objUsuario.Rol),
                New SqlParameter("@UsuarioModifico", usuarioModifico)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function

    'Función que tiene el query para que el usuario pueda modificar su propia contraseña
    Public Function CambioContrasenna(ByVal objUsuario As Models.Usuario, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "sp_Cambio_Contrasenna"

            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@NombreUsuario", objUsuario.NombreUsuario),
                New SqlParameter("@ContrasennaNueva", objUsuario.Contrasenna)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function


    ' Cargar el usuario indicado según el nombre de usuario que se le pase a la función
    Public Function ConsultarUsuario_x_Username(ByVal nombreUsuario As String, errorMessage As String) As Models.Usuario
        Dim query As String = "sp_Buscar_Usuario"

        ' Se agregan los parámetros del procedimiento almacenado a una lista de SqlParameter
        Dim parameters As New List(Of SqlParameter) From {
             New SqlParameter("@UsuarioConsultado", nombreUsuario)
        }

        Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            Dim modUsuario As New Models.Usuario With {
                .Nombre = row("Nombre").ToString(),
                .Apellido1 = row("Apellido1").ToString(),
                .Apellido2 = row("Apellido1").ToString(),
                .Correo = row("CorreoElectronico").ToString(),
                .Estado = Convert.ToInt32(row("Estado")),
                .Rol = Convert.ToInt32(row("Rol"))
            }
            Return modUsuario
        End If
        Return Nothing
    End Function
End Class


