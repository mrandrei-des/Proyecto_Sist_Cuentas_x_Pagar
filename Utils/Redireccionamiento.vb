Public Class Redireccionamiento

    Public Function DevuelvePaginaInicioUsuario(identificadorPermiso As String) As String

        Select Case identificadorPermiso
            Case "CREAR_USUARIOS"
                Return "MantenimientoUsuario.aspx"
            Case "LIST_MANT_USUARIOS"
                Return "ListadoUsuarios.aspx"
            Case "CAMBIO_CONTRASENNA"
                Return "CambioContrasenna.aspx"
            Case "CREAR_PROVEEDORES"
                Return "MantenimientoProveedor.aspx"
            Case "LIST_MANT_PROVEEDORES"
                Return "ListadoProveedores.aspx"
            Case "CREAR_MANT_DOCUMENTOS"
                Return "CreacionDocumentos.aspx"
            Case "LIST_DOCUMENTOS"
                Return "ListadoDocumentos.aspx"
            Case "CREAR_MANT_ROLES"
                Return "MantRolesPermisos.aspx"
            Case Else
                Return "index.aspx"
        End Select
    End Function

    Public Function PermisoEnLista(listaPermisos As List(Of String), identificadorPermiso As String) As Boolean
        For Each permiso In listaPermisos
            If permiso = identificadorPermiso Then
                Return True
            End If
        Next
        Return False
    End Function
End Class
