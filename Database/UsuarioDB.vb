Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class UsuarioDB
    Private db As New DbHealper

    'Función que tiene el script para agregar un usuario nuevo
    Public Function CrearUsuario(ByVal objUsuario As Models.Usuario, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "EXEC sp_Inserta_Usuario_Nuevo @Usuario, @Contrasenna, @Nombre, @Apellido1, @Apellido2, @CorreoElectronico, @Estado, @Rol, @UsuarioCreacion "
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@Usuario", objUsuario.NombreUsuario},
                {"@Contrasenna", objUsuario.Constrasenna},
                {"@Nombre", objUsuario.Nombre},
                {"@Apellido1", objUsuario.Apellido1},
                {"@Apellido2", objUsuario.Apellido2},
                {"@CorreoElectronico", objUsuario.Correo},
                {"@Estado", objUsuario.Estado},
                {"@Rol", objUsuario.Rol},
                {"@UsuarioCreacion", "andre"}
            }
            'Se debe buscar la opcion para que el usuario creación sea el usuario que está logueado y no uno quemado
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function

    'Función que tiene el query para eliminar un usuario
    Public Function EliminarUsuario(usuarioAfectado As String, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Dim query As String = "EXEC sp_Eliminar_Usuario @UsuarioAfectado, @UsuarioElimino "
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@UsuarioAfectado", usuarioAfectado},
            {"@UsuarioElimino", usuarioElimino}
        }
        'Se debe buscar la opcion para que el usuario creación sea el usuario que está logueado y no uno quemado
        Return db.ExecuteNonQuery(query, parameters, errorMessage)
    End Function

    Public Function ModificarUsuario(ByVal objUsuario As Models.Usuario, ByVal usuarioEjecutaAccion As String, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = ""
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@Usuario", objUsuario.NombreUsuario},
                {"@Contrasenna", objUsuario.Constrasenna},
                {"@Nombre", objUsuario.Nombre},
                {"@Apellido1", objUsuario.Apellido1},
                {"@Apellido2", objUsuario.Apellido2},
                {"@CorreoElectronico", objUsuario.Correo},
                {"@Estado", objUsuario.Estado},
                {"@Rol", objUsuario.Rol},
                {"@UsuarioCreacion", usuarioEjecutaAccion}
            }
            'Se debe buscar la opcion para que el usuario creación sea el usuario que está logueado y no uno quemado
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function
End Class
