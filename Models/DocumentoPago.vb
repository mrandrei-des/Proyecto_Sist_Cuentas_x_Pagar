Namespace Models
    Public Class DocumentoPago
        Private _idProveedor As Integer
        Private _tipoDocumento As Integer
        Private _numeroDocumento As String
        Private _observacion As String
        Private _fechaEmision As Date
        Private _estado As Integer
        Private _moneda As String
        Private _tipoCambio As Double
        Private _total As Double
        Private _saldoActual As Double

        Public Sub New()
        End Sub

        Public Sub New(idProveedor As Integer, tipoDocumento As Integer, numeroDocumento As String, observacion As String, fechaEmision As Date, estado As Integer, moneda As String, tipoCambio As Double, total As Double, saldoActual As Double)
            Me.IdProveedor = idProveedor
            Me.TipoDocumento = tipoDocumento
            Me.NumeroDocumento = numeroDocumento
            Me.Observacion = observacion
            Me.FechaEmision = fechaEmision
            Me.Estado = estado
            Me.Moneda = moneda
            Me.TipoCambio = tipoCambio
            Me.Total = total
            Me.SaldoActual = saldoActual
        End Sub

        Public Property IdProveedor As Integer
            Get
                Return _idProveedor
            End Get
            Set(value As Integer)
                _idProveedor = value
            End Set
        End Property

        Public Property TipoDocumento As Integer
            Get
                Return _tipoDocumento
            End Get
            Set(value As Integer)
                _tipoDocumento = value
            End Set
        End Property

        Public Property NumeroDocumento As String
            Get
                Return _numeroDocumento
            End Get
            Set(value As String)
                _numeroDocumento = value
            End Set
        End Property

        Public Property Observacion As String
            Get
                Return _observacion
            End Get
            Set(value As String)
                _observacion = value
            End Set
        End Property

        Public Property FechaEmision As Date
            Get
                Return _fechaEmision
            End Get
            Set(value As Date)
                _fechaEmision = value
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

        Public Property Moneda As String
            Get
                Return _moneda
            End Get
            Set(value As String)
                _moneda = value
            End Set
        End Property

        Public Property TipoCambio As Double
            Get
                Return _tipoCambio
            End Get
            Set(value As Double)
                _tipoCambio = value
            End Set
        End Property

        Public Property Total As Double
            Get
                Return _total
            End Get
            Set(value As Double)
                _total = value
            End Set
        End Property

        Public Property SaldoActual As Double
            Get
                Return _saldoActual
            End Get
            Set(value As Double)
                _saldoActual = value
            End Set
        End Property
    End Class
End Namespace
