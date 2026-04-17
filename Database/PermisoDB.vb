Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class PermisoDB
    Private db As New DbHealper
    Public Function ConsultarPermisos_x_idGrupo(idGrupo As Integer, ByRef errorMessage As String) As List(Of Models.Permiso)
        Try
            Dim query As String = "sp_Consulta_Permisos_x_Grupo"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Grupo", idGrupo)
            }

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            Dim listaPermisos As New List(Of Models.Permiso)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim permiso As New Models.Permiso() With {
                        .IdPermiso = Convert.ToInt32(dt.Rows(x)("idPermiso").ToString()),
                        .Titulo = dt.Rows(x)("Titulo").ToString(),
                        .Descripcion = dt.Rows(x)("Descripcion").ToString()
                    }
                    listaPermisos.Add(permiso)
                Next
            End If
            Return listaPermisos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarPermisos_x_idRol(idRol As Integer, ByRef errorMessage As String) As List(Of Integer)
        Try
            Dim query As String = "sp_Consulta_Permisos_x_Roles_x_Rol"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Rol", idRol)
            }

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            Dim listaPermisosAsignados As New List(Of Integer)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    listaPermisosAsignados.Add(Convert.ToInt32(dt.Rows(x)("idPermiso").ToString()))
                Next
            End If
            Return listaPermisosAsignados
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarIdentificadorPermisos_x_idRol(idRol As Integer, ByRef errorMessage As String) As List(Of String)
        Try
            Dim query As String = "sp_Consulta_IdentificadorPermisos_x_Rol"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Rol", idRol)
            }

            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)

            Dim listaPermisosAsignados As New List(Of String)()

            If dt Is Nothing AndAlso errorMessage <> "" Then
                Return Nothing
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    listaPermisosAsignados.Add(dt.Rows(x)("idPermiso").ToString())
                Next
            End If
            Return listaPermisosAsignados
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AsignarPermisos_x_Rol(idRol As Integer, idGrupo As Integer, listaPermisos As List(Of Integer), usuarioInserta As String, ByRef errorMessage As String) As Boolean
        Try
            If EliminarPermisos_x_Rol(idRol, idGrupo, errorMessage) Then
                Return InsertarPermisos_x_Rol(idRol, idGrupo, listaPermisos, usuarioInserta, errorMessage)
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function InsertarPermisos_x_Rol(idRol As Integer, idGrupo As Integer, listaPermisos As List(Of Integer), usuarioInserta As String, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Inserta_Permiso_x_Rol"

            For Each idPermiso As Integer In listaPermisos
                Dim parameters As New List(Of SqlParameter) From {
                    New SqlParameter("@ID_Rol", idRol),
                    New SqlParameter("@ID_GrupoPermiso", idGrupo),
                    New SqlParameter("@ID_Permiso", idPermiso),
                    New SqlParameter("@UsuarioInserta", usuarioInserta)
                }

                Dim result As Integer = db.ExecuteNonQuery(query, parameters, errorMessage)
                If result = 0 Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EliminarPermisos_x_Rol(idRol As Integer, idGrupo As Integer, ByRef errorMessage As String) As Boolean
        Try
            Dim query As String = "sp_Eliminar_Permisos_x_Rol"
            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_Rol", idRol),
                New SqlParameter("@ID_GrupoPermiso", idGrupo)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
