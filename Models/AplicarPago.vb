Namespace Models
    Public Class AplicarPago
        Private _numeroProveedor As Integer
        Private _tipoFactura As String
        Private _numeroFactura As String
        Private _tipoDocumento As String
        Private _numeroDocumento As String
        Private _montoPagado As Double
        Private _observacion As String
        Private _fechaPago As Date
        Private _moneda As String
        Private _tipoCambio As Double

        Public Sub New()
        End Sub

        Public Sub New(numeroProveedor As Integer, tipoFactura As String, numeroFactura As String, tipoDocumento As String, numeroDocumento As String, montoPagado As Double, observacion As String, fechaPago As Date, moneda As String, tipoCambio As Double)
            Me.NumeroProveedor = numeroProveedor
            Me.TipoFactura = tipoFactura
            Me.NumeroFactura = numeroFactura
            Me.TipoDocumento = tipoDocumento
            Me.NumeroDocumento = numeroDocumento
            Me.MontoPagado = montoPagado
            Me.Observacion = observacion
            Me.FechaPago = fechaPago
            Me.Moneda = moneda
            Me.TipoCambio = tipoCambio
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

        Public Property TipoDocumento As String
            Get
                Return _tipoDocumento
            End Get
            Set(value As String)
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

        Public Property MontoPagado As Double
            Get
                Return _montoPagado
            End Get
            Set(value As Double)
                _montoPagado = value
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

        Public Property FechaPago As Date
            Get
                Return _fechaPago
            End Get
            Set(value As Date)
                _fechaPago = value
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
    End Class
End Namespace
