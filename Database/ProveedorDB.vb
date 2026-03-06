Imports System.Data.SqlClient
Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class ProveedorDB
    Private db As New DbHealper

    'Función que tiene el query para agregar un proveedor nuevo
    Public Function CrearProveedor(ByVal objProveedor As Models.Proveedor, ByVal usuarioInserta As String, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "sp_Inserta_Proveedor_Nuevo"

            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@Nombre", objProveedor.Nombre),
                New SqlParameter("@TipoIdentificacion", objProveedor.TipoIdentificacion),
                New SqlParameter("@Identificacion", objProveedor.NumeroIdentificacion),
                New SqlParameter("@CorreoElectronico", objProveedor.Correo),
                New SqlParameter("@Estado", objProveedor.Estado),
                New SqlParameter("@UsuarioCreacion", usuarioInserta)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function

    'Función que tiene el query para eliminar un usuario
    Public Function EliminarProveedor(idProveedorAfectado As Integer, usuarioElimino As String, ByRef errorMessage As String) As Boolean
        Dim query As String = "sp_Eliminar_Proveedor"

        Dim parameters As New List(Of SqlParameter) From {
            New SqlParameter("@ID_Proveedor", idProveedorAfectado),
            New SqlParameter("@UsuarioElimino", usuarioElimino)
        }

        Return db.ExecuteNonQuery(query, parameters, errorMessage)
    End Function

    Public Function ModificarProveedor(ByVal objProveedor As Models.Proveedor, ByVal usuarioModifico As String, ByRef errorMessage As String) As Boolean
        Using db.GetConnection()
            Dim query As String = "sp_Modificar_Proveedor"

            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@ID_ProveedorAfectado", objProveedor.NumeroProveedor),
                New SqlParameter("@Nombre", objProveedor.Nombre),
                New SqlParameter("@CorreoElectronico", objProveedor.Correo),
                New SqlParameter("@Estado", objProveedor.Estado),
                New SqlParameter("@UsuarioModifico", usuarioModifico)
            }

            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function

    ' Cargar el proveedor indicado según el id que se le pase a la función
    Public Function ConsultarProveedor_x_ID(ByVal idProveedor As Integer, errorMessage As String) As Models.Proveedor
        Dim query As String = "sp_Consultar_Proveedor"

        ' Se agregan los parámetros del procedimiento almacenado a una lista de SqlParameter
        Dim parameters As New List(Of SqlParameter) From {
             New SqlParameter("@ID_Proveedor", idProveedor)
        }

        Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            Dim modProveedor As New Models.Proveedor With {
                .NumeroProveedor = Convert.ToInt32(row("ID_Proveedor")),
                .Nombre = row("Nombre").ToString(),
                .TipoIdentificacion = Convert.ToInt32(row("TipoIdentificacion")),
                .NumeroIdentificacion = row("Identificacion").ToString(),
                .Correo = row("CorreoElectronico").ToString(),
                .Estado = Convert.ToInt32(row("Estado"))
            }
            Return modProveedor
        End If
        Return Nothing
    End Function

    ' Busca al proveedor que coincida con el tipo y número de identificación que se le pase a la función
    Public Function BuscarProveedor_x_Identificacion(tipoIdentificacion As Integer, numeroIdentificacion As String, errorMessage As String) As Models.Proveedor
        Dim query As String = "sp_Buscar_Proveedor"

        ' Se agregan los parámetros del procedimiento almacenado a una lista de SqlParameter
        Dim parameters As New List(Of SqlParameter) From {
             New SqlParameter("@TipoIdentificacion", tipoIdentificacion),
             New SqlParameter("@NumeroIdentificacion", numeroIdentificacion)
        }

        Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True, parameters)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            Dim modProveedor As New Models.Proveedor With {
                .NumeroProveedor = Convert.ToInt32(row("ID_Proveedor")),
                .Nombre = row("Nombre").ToString(),
                .TipoIdentificacion = Convert.ToInt32(row("TipoIdentificacion")),
                .NumeroIdentificacion = row("Identificacion").ToString(),
                .Correo = row("CorreoElectronico").ToString(),
                .Estado = Convert.ToInt32(row("Estado"))
            }
            Return modProveedor
        End If
        Return Nothing
    End Function
End Class