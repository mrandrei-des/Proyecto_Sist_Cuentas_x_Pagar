Namespace Models
    Public Class CategoriaDocumento
        Private _idCategoria As Integer
        Private _descripcion As String

        Public Sub New()
        End Sub
        Public Sub New(idCategoria As Integer, descripcion As String)
            _idCategoria = idCategoria
            _descripcion = descripcion
        End Sub

        Public Property IdCategoria As Integer
            Get
                Return _idCategoria
            End Get
            Set(value As Integer)
                _idCategoria = value
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
    End Class
End Namespace
