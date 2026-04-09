Namespace Models
    Public Class DocumentoAplicado
        Private _idCategoria As Integer
        Private _idTipoDoc As Integer
        Private _numDocumento As String
        Private _simbolo As String
        Private _monto As String
        Private _idProveedor As Integer
        Private _nombreProveedor As String

        Public Sub New()
        End Sub

        Public Sub New(idCategoria As Integer, idTipoDoc As Integer, numDocumento As String, simbolo As String, monto As String, idProveedor As Integer, nombreProveedor As String)
            Me.IdCategoria = idCategoria
            Me.IdTipoDoc = idTipoDoc
            Me.NumDocumento = numDocumento
            Me.Simbolo = simbolo
            Me.Monto = monto
            Me.IdProveedor = idProveedor
            Me.NombreProveedor = nombreProveedor
        End Sub

        Public Property IdCategoria As Integer
            Get
                Return _idCategoria
            End Get
            Set(value As Integer)
                _idCategoria = value
            End Set
        End Property

        Public Property IdTipoDoc As Integer
            Get
                Return _idTipoDoc
            End Get
            Set(value As Integer)
                _idTipoDoc = value
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

        Public Property Simbolo As String
            Get
                Return _simbolo
            End Get
            Set(value As String)
                _simbolo = value
            End Set
        End Property

        Public Property Monto As String
            Get
                Return _monto
            End Get
            Set(value As String)
                _monto = value
            End Set
        End Property

        Public Property IdProveedor As Integer
            Get
                Return _idProveedor
            End Get
            Set(value As Integer)
                _idProveedor = value
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
    End Class
End Namespace
