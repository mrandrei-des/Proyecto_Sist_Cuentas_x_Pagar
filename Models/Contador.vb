Namespace Models
    Public Class Contador
        Private _cantidad As Integer

        Public Sub New()
        End Sub

        Public Property Cantidad As Integer
            Get
                Return _cantidad
            End Get
            Set(value As Integer)
                _cantidad = value
            End Set
        End Property
    End Class
End Namespace
