Namespace Models

    Public Class Estado
        Private _idEstado As Integer
        Private _descripcion As String

        Public Sub New()
        End Sub

        Public Sub New(idEstado As Integer, descripcion As String)
            _idEstado = idEstado
            _descripcion = descripcion
        End Sub

        Public Property IdEstado As Integer
            Get
                Return _idEstado
            End Get
            Set(value As Integer)
                _idEstado = value
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
