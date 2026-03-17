Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class UsuarioDB
    Private db As New DbHealper

    'Función que tiene el script para agregar un usuario nuevo
    Public Function CrearUsuario(ByVal objUsuario As Models.Usuario, ByVal usuarioCreacion As String, ByRef errorMessage As String) As Boolean
        Try
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
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Función que tiene el query para eliminar un usuario
    Public Function EliminarUsuario(usuarioAfectado As String, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Eliminar_Usuario"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@UsuarioAfectado", usuarioAfectado),
                New SqlParameter("@UsuarioElimino", usuarioElimino)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Función que tiene el query para modificar un usuario
    Public Function ModificarUsuario(ByVal objUsuario As Models.Usuario, ByVal usuarioModifico As String, ByRef errorMessage As String) As Boolean
        Try
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
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Función que tiene el query para que el usuario pueda modificar su propia contraseña
    Public Function CambioContrasenna(ByVal objUsuario As Models.Usuario, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Cambio_Contrasenna"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@NombreUsuario", objUsuario.NombreUsuario),
                New SqlParameter("@ContrasennaNueva", objUsuario.Contrasenna)
            }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function


    ' Cargar el usuario indicado según el nombre de usuario que se le pase a la función
    Public Function ConsultarUsuario_x_Username(nombreUsuario As String, errorMessage As String) As Models.Usuario
        Try
            Dim query As String = "sp_Buscar_Usuario"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@UsuarioConsultado", nombreUsuario)
            }
            Dim modUsuario As New Models.Usuario
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                With modUsuario
                    .Nombre = row("Nombre").ToString()
                    .Apellido1 = row("Apellido1").ToString()
                    .Apellido2 = row("Apellido1").ToString()
                    .Correo = row("CorreoElectronico").ToString()
                    .Estado = Convert.ToInt32(row("Estado"))
                    .Rol = Convert.ToInt32(row("Rol"))
                End With
            End If
            Return modUsuario
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function FiltrarUsuarios(filtNombre As String, filtEstado As Integer, filtRol As Integer, errorMessage As String) As DataTable
        Try
            Dim query As String = "sp_Filtrar_Usuarios"
            Dim parameters As New List(Of SqlParameter)

            If filtRol = 0 Then
                parameters.Add(New SqlParameter("@FiltRol", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltRol", filtRol))
            End If

            If filtNombre.IsNullOrWhiteSpace() Then
                parameters.Add(New SqlParameter("@FiltEnNombre", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltEnNombre", filtNombre))
            End If

            If filtEstado = 0 Then
                parameters.Add(New SqlParameter("@FiltEstado", DBNull.Value))
            Else
                parameters.Add(New SqlParameter("@FiltEstado", filtEstado))
            End If

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            End If

            Return New DataTable()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class