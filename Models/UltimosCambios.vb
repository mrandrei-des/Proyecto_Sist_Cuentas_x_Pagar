Namespace Models
    Public Class UltimosCambios
        Private _haceTiempo As String
        Private _nombreProveedor As String
        Private _idCategoria As Integer
        Private _numDocumento As String
        Private _usuarioAccion As String
        Private _accionRealizada As String

        Public Sub New()
        End Sub

        Public Property HaceTiempo As String
            Get
                Return _haceTiempo
            End Get
            Set(value As String)
                _haceTiempo = value
            End Set
        End Property

        Public Property NombreProveedor As String
            Get
                Return _nombreProveedor
            End Get
            Set(value As String)
                _nombreProveedor = value
            End Set
        End Property

        Public Property IdCategoria As Integer
            Get
                Return _idCategoria
            End Get
            Set(value As Integer)
                _idCategoria = value
            End Set
        End Property

        Public Property NumDocumento As String
            Get
                Return _numDocumento
            End Get
            Set(value As String)
                _numDocumento = value
            End Set
        End Property

        Public Property UsuarioAccion As String
            Get
                Return _usuarioAccion
            End Get
            Set(value As String)
                _usuarioAccion = value
            End Set
        End Property

        Public Property AccionRealizada As String
            Get
                Return _accionRealizada
            End Get
            Set(value As String)
                _accionRealizada = value
            End Set
        End Property
    End Class
End Namespace
