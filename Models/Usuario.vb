Namespace Models
    Public Class Usuario
        Private _nombreUsuario As String
        Private _constrasenna As String
        Private _nombre As String
        Private _apellido1 As String
        Private _apellido2 As String
        Private _correo As String
        Private _estado As Integer

        Public Sub New()
        End Sub

        Public Sub New(nombreUsuario As String, constrasenna As String, nombre As String, apellido1 As String, apellido2 As String, correo As String, estado As Integer)
            Me.NombreUsuario = nombreUsuario
            Me.Constrasenna = constrasenna
            Me.Nombre = nombre
            Me.Apellido1 = apellido1
            Me.Apellido2 = apellido2
            Me.Correo = correo
            Me.Estado = estado
        End Sub

        Public Property NombreUsuario As String
            Get
                Return _nombreUsuario
            End Get
            Set(value As String)
                _nombreUsuario = value
            End Set
        End Property

        Public Property Constrasenna As String
            Get
                Return _constrasenna
            End Get
            Set(value As String)
                _constrasenna = value
            End Set
        End Property

        Public Property Nombre As String
            Get
                Return _nombre
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property

        Public Property Apellido1 As String
            Get
                Return _apellido1
            End Get
            Set(value As String)
                _apellido1 = value
            End Set
        End Property

        Public Property Apellido2 As String
            Get
                Return _apellido2
            End Get
            Set(value As String)
                _apellido2 = value
            End Set
        End Property

        Public Property Correo As String
            Get
                Return _correo
            End Get
            Set(value As String)
                _correo = value
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