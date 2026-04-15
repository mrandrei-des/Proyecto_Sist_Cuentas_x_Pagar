Namespace Models
    Public Class DocumentosPendientes
        Private _idCategoria As Integer
        Private _nombreProveedor As String
        Private _numDocumento As String
        Private _simboloMoneda As String
        Private _totalDocumento As Double
        Private _totalDocumentoFormateado As String
        Private _cantDias As Integer

        Public Sub New()
        End Sub

        Public Property IdCategoria As Integer
            Get
                Return _idCategoria
            End Get
            Set(value As Integer)
                _idCategoria = value
            End Set
        End Property

        Public Property NombreProveedor As String
            Get
                Return _nombreProveedor
            End Get
            Set(value As String)
                _nombreProveedor = value
            End Set
        End Property

        Public Property NumDocumento As String
            Get
                Return _numDocumento
            End Get
            Set(value As String)
                _numDocumento = value
            End Set
        End Property

        Public Property SimboloMoneda As String
            Get
                Return _simboloMoneda
            End Get
            Set(value As String)
                _simboloMoneda = value
            End Set
        End Property

        Public Property TotalDocumento As Double
            Get
                Return _totalDocumento
            End Get
            Set(value As Double)
                _totalDocumento = value
            End Set
        End Property

        Public Property TotalDocumentoFormateado As String
            Get
                Return _totalDocumentoFormateado
            End Get
            Set(value As String)
                _totalDocumentoFormateado = value
            End Set
        End Property

        Public Property CantDias As Integer
            Get
                Return _cantDias
            End Get
            Set(value As Integer)
                _cantDias = value
            End Set
        End Property
    End Class
End Namespace
