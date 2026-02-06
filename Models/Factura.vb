Namespace Models
    Public Class Factura
        Private _numeroProveedor As Integer
        Private _tipoFactura As String
        Private _numeroFactura As String
        Private _fechaEmision As Date
        Private _observacion As String
        Private _moneda As String
        Private _tipoCambio As Double
        Private _total As Double
        Private _saldo As Double
        Private _estado As Integer

        Public Sub New()
        End Sub

        Public Sub New(numeroProveedor As Integer, tipoFactura As String, numeroFactura As String, fechaEmision As Date, observacion As String, moneda As String, tipoCambio As Double, total As Double, saldo As Double, estado As Integer)
            Me.NumeroProveedor = numeroProveedor
            Me.TipoFactura = tipoFactura
            Me.NumeroFactura = numeroFactura
            Me.FechaEmision = fechaEmision
            Me.Observacion = observacion
            Me.Moneda = moneda
            Me.TipoCambio = tipoCambio
            Me.Total = total
            Me.Saldo = saldo
            Me.Estado = estado
        End Sub

        Public Property NumeroProveedor As Integer
            Get
                Return _numeroProveedor
            End Get
            Set(value As Integer)
                _numeroProveedor = value
            End Set
        End Property

        Public Property TipoFactura As String
            Get
                Return _tipoFactura
            End Get
            Set(value As String)
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

        Public Property FechaEmision As Date
            Get
                Return _fechaEmision
            End Get
            Set(value As Date)
                _fechaEmision = value
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

        Public Property Saldo As Double
            Get
                Return _saldo
            End Get
            Set(value As Double)
                _saldo = value
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
    End Class
End Namespace
