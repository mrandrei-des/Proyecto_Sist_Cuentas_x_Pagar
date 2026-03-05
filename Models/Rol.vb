Namespace Models
    Public Class Rol
        Private _idRol As Integer
        Private _descripcion As String
        Private _estado As Integer

        Public Sub New()
        End Sub

        Public Sub New(idRol As Integer, descripcion As String, estado As Integer)
            _idRol = idRol
            _descripcion = descripcion
            _estado = estado
        End Sub

        Public Property IdRol As Integer
            Get
                Return _idRol
            End Get
            Set(value As Integer)
                _idRol = value
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
