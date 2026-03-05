Namespace Models
    Public Class TipoIdentificaciones
        Private _idTipo As Integer
        Private _descripcion As String

        Public Sub New()
        End Sub

        Public Sub New(idTipo As Integer, descripcion As String)
            _idTipo = idTipo
            _descripcion = descripcion
        End Sub

        Public Property IdTipo As Integer
            Get
                Return _idTipo
            End Get
            Set(value As Integer)
                _idTipo = value
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
