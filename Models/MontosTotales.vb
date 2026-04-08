Namespace Models
    Public Class MontosTotales
        Private _moneda As String
        Private _monto As Double
        Private _simbolo As String

        Public Sub New()
        End Sub

        Public Sub New(moneda As String, monto As Double, simbolo As String)
            Me.Moneda = moneda
            Me.Monto = monto
            Me.Simbolo = simbolo
        End Sub

        Public Property Moneda As String
            Get
                Return _moneda
            End Get
            Set(value As String)
                _moneda = value
            End Set
        End Property

        Public Property Monto As Double
            Get
                Return _monto
            End Get
            Set(value As Double)
                _monto = value
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

