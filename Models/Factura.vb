Namespace Models
    Public Class Factura
        Private _idProveedor As Integer
        Private _tipoFactura As Integer
        Private _numeroFactura As String
        Private _observacion As String
        Private _fechaEmision As Date
        Private _estado As Integer
        Private _moneda As String
        Private _tipoCambio As Double
        Private _total As Double
        Private _saldoActual As Double

        Public Sub New()
        End Sub

        Public Sub New(idProveedor As Integer, tipoFactura As Integer, numeroFactura As String, observacion As String, fechaEmision As Date, estado As Integer, moneda As String, tipoCambio As Double, total As Double, saldoActual As Double)
            Me.IdProveedor = idProveedor
            Me.TipoFactura = tipoFactura
            Me.NumeroFactura = numeroFactura
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

        Public Property TipoFactura As Integer
            Get
                Return _tipoFactura
            End Get
            Set(value As Integer)
                _tipoFactura = value
            End Set
        End Property

        Public Property NumeroFactura As String
            Get
                Return _numeroFactura
            End Get
            Set(value As String)
                _numeroFactura = value
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