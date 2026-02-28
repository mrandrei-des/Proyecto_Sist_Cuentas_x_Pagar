Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class ProveedorDB
    Private db As New DbHealper

    'Función que tiene el query para agregar un proveedor nuevo
    Public Function CrearProveedor(ByVal objProveedor As Models.Proveedor, ByVal usuarioInserta As String, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "EXEC sp_Inserta_Proveedor_Nuevo @Nombre, @TipoIdentificacion, @Identificacion, @CorreoElectronico, @Estado, @UsuarioCreacion "
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@Nombre", objProveedor.Nombre},
                {"@TipoIdentificacion", objProveedor.TipoIdentificacion},
                {"@Identificacion", objProveedor.NumeroIdentificacion},
                {"@CorreoElectronico", objProveedor.Correo},
                {"@Estado", objProveedor.Estado},
                {"@UsuarioCreacion", usuarioInserta}
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function

    'Función que tiene el query para eliminar un usuario
    Public Function EliminarProveedor(idProveedorAfectado As Integer, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Dim query As String = "EXEC sp_Eliminar_Proveedor @ID_Proveedor, @UsuarioElimino "
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@ID_Proveedor", idProveedorAfectado},
            {"@UsuarioElimino", usuarioElimino}
        }
        Return db.ExecuteNonQuery(query, parameters, errorMessage)
    End Function

    Public Function ModificarProveedor(ByVal objProveedor As Models.Proveedor, ByVal usuarioModifico As String, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "EXEC sp_Modificar_Proveedor @ID_ProveedorAfectado, @Nombre, @TipoIdentificacion, @Identificacion, @CorreoElectronico, @Estado, @UsuarioModifico "
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@ID_ProveedorAfectado", objProveedor.NumeroProveedor},
                {"@Nombre", objProveedor.Nombre},
                {"@TipoIdentificacion", objProveedor.TipoIdentificacion},
                {"@Identificacion", objProveedor.NumeroIdentificacion},
                {"@CorreoElectronico", objProveedor.Correo},
                {"@Estado", objProveedor.Estado},
                {"@UsuarioModifico", usuarioModifico}
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function
End Class
