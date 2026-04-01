Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class RolDB
    Private db As New DbHealper

    Public Function ConsultarRoles(ByRef errorMessage) As List(Of Models.Rol)
        Try
            Dim query As String = "sp_Cargar_Roles_Usuario_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)
            Dim listaRoles As New List(Of Models.Rol)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim rol As New Models.Rol() With {
                        .IdRol = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString(),
                        .Estado = Convert.ToInt32(dt.Rows(x)("Estado").ToString())
                    }
                    listaRoles.Add(rol)
                Next
            End If
            Return listaRoles
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarRoles_x_ID(idRol As Integer, ByRef errorMessage As String) As Models.Rol
        Try
            Dim query As String = "sp_Consulta_Rol_x_ID"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Rol", idRol)
            }

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)
            Dim modRol As New Models.Rol()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                With modRol
                    .IdRol = Convert.ToInt32(dt.Rows(0)("IDRol").ToString())
                    .Descripcion = dt.Rows(0)("Descripcion").ToString()
                    .Estado = Convert.ToInt32(dt.Rows(0)("Estado").ToString())
                End With
            End If
            Return modRol
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function InsertaRol(modRol As Models.Rol, usuarioCreacion As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Inserta_Rol_Nuevo"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@Descripcion", modRol.Descripcion),
                 New SqlParameter("@Estado", modRol.Estado),
                 New SqlParameter("@UsuarioCreacion", usuarioCreacion)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarRoles_x_Nombre(nombreRol As String, ByRef errorMessage As String) As List(Of Models.Rol)
        Try
            Dim query As String = "sp_Consulta_Rol_x_Nombre"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@DescripcionRol", nombreRol)
            }
            Dim listaRoles As New List(Of Models.Rol)()
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim rol As New Models.Rol() With {
                        .IdRol = Convert.ToInt32(dt.Rows(x)("IDRol").ToString()),
                        .Descripcion = dt.Rows(x)("Descripcion").ToString(),
                        .Estado = Convert.ToInt32(dt.Rows(x)("Estado").ToString())
                    }
                    listaRoles.Add(rol)
                Next
            End If
            Return listaRoles
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ModificaRol(modRol As Models.Rol, usuarioModifico As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Modifica_Rol"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Rol", modRol.IdRol),
                 New SqlParameter("@Descripcion", modRol.Descripcion),
                 New SqlParameter("@Estado", modRol.Estado),
                 New SqlParameter("@UsuarioModificacion", usuarioModifico)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarRoles_x_ID_Usuarios(idRol As Integer, ByRef errorMessage As String) As List(Of Models.Usuario)
        Try
            Dim query As String = "sp_Consulta_Rol_x_ID_Usuario"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Rol", idRol)
            }

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)
            Dim listaUsuariosConRol As New List(Of Models.Usuario)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim usuario As New Models.Usuario With {
                        .NombreUsuario = dt.Rows(x)("Usuario").ToString(),
                        .Nombre = dt.Rows(x)("Nombre").ToString(),
                        .Apellido1 = dt.Rows(x)("Apellido1").ToString(),
                        .Apellido2 = dt.Rows(x)("Apellido2").ToString(),
                        .Correo = dt.Rows(x)("CorreoElectronico").ToString()
                    }
                    listaUsuariosConRol.Add(usuario)
                Next x
            End If
            Return listaUsuariosConRol
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function EliminaRol(idRol As Integer, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Elimina_Rol"
            Dim parameters As New List(Of SqlParameter) From {
                 New SqlParameter("@ID_Rol", idRol),
                 New SqlParameter("@UsuarioElimino", usuarioElimino)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
