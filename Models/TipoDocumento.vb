Namespace Models
    Public Class TipoDocumento
        Private _idTipoDocumento As Integer
        Private _descripcion As String
        Private _idCategoria As Integer

        Public Sub New()
        End Sub

        Public Sub New(idTipoDocumento As Integer, descripcion As String, idCategoria As Integer)
            _idTipoDocumento = idTipoDocumento
            _descripcion = descripcion
            _idCategoria = idCategoria
        End Sub

        Public Property IdTipoDocumento As Integer
            Get
                Return _idTipoDocumento
            End Get
            Set(value As Integer)
                _idTipoDocumento = value
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
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
    End Class
End Namespace
