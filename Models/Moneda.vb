Namespace Models
    Public Class Moneda
        Private _codigoMoneda As String
        Private _descripcion As String
        Private _simbolo As String

        Public Sub New()
        End Sub

        Public Sub New(codigoMoneda As String, descripcion As String, simbolo As String)
            Me.CodigoMoneda = codigoMoneda
            Me.Descripcion = descripcion
            Me.Simbolo = simbolo
        End Sub

        Public Property CodigoMoneda As String
            Get
                Return _codigoMoneda
            End Get
            Set(value As String)
                _codigoMoneda = value
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

        Public Property Simbolo As String
            Get
                Return _simbolo
            End Get
            Set(value As String)
                _simbolo = value
            End Set
        End Property
    End Class
End Namespace
