Namespace Models
    Public Class Permiso
        Private _idGrupo As Integer
        Private _idPermiso As Integer
        Private _titulo As String
        Private _descripcion As String

        Public Sub New()
        End Sub

        Public Sub New(idGrupo As Integer, idPermiso As Integer, titulo As String, descripcion As String)
            Me.IdGrupo = idGrupo
            Me.IdPermiso = idPermiso
            Me.Titulo = titulo
            Me.Descripcion = descripcion
        End Sub

        Public Property IdGrupo As Integer
            Get
                Return _idGrupo
            End Get
            Set(value As Integer)
                _idGrupo = value
            End Set
        End Property

        Public Property IdPermiso As Integer
            Get
                Return _idPermiso
            End Get
            Set(value As Integer)
                _idPermiso = value
            End Set
        End Property

        Public Property Titulo As String
            Get
                Return _titulo
            End Get
            Set(value As String)
                _titulo = value
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
    End Class
End Namespace
