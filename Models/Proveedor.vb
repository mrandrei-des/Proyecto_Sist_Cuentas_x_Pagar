Namespace Models
    Public Class Proveedor
        Private _numeroProveedor As Integer
        Private _tipoIdentificacion As Integer
        Private _numeroIdentificacion As String
        Private _nombre As String
        Private _correo As String
        Private _estado As Integer

        Public Sub New()
        End Sub

        Public Sub New(numeroProveedor As Integer, tipoIdentificacion As Integer, numeroIdentificacion As String, nombre As String, correo As String, estado As Integer)
            Me.NumeroProveedor = numeroProveedor
            Me.TipoIdentificacion = tipoIdentificacion
            Me.NumeroIdentificacion = numeroIdentificacion
            Me.Nombre = nombre
            Me.Correo = correo
            Me.Estado = estado
        End Sub

        Public Property NumeroProveedor As Integer
            Get
                Return _numeroProveedor
            End Get
            Set(value As Integer)
                _numeroProveedor = value
            End Set
        End Property

        Public Property TipoIdentificacion As Integer
            Get
                Return _tipoIdentificacion
            End Get
            Set(value As Integer)
                _tipoIdentificacion = value
            End Set
        End Property

        Public Property NumeroIdentificacion As String
            Get
                Return _numeroIdentificacion
            End Get
            Set(value As String)
                _numeroIdentificacion = value
            End Set
        End Property

        Public Property Nombre As String
            Get
                Return _nombre
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property

        Public Property Correo As String
            Get
                Return _correo
            End Get
            Set(value As String)
                _correo = value
            End Set
        End Property

        Public Property Estado As Integer
            Get
                Return _estado
            End Get
            Set(value As Integer)
                _estado = value
            End Set
        End Property
    End Class
End Namespace