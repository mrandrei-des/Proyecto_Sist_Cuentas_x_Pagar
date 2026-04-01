Namespace Models
    Public Class GrupoPermiso
        Private _idGrupoPermiso As Integer
        Private _descripcion As String

        Public Sub New()
        End Sub

        Public Sub New(idGrupoPermiso As Integer, descripcion As String)
            Me.IdGrupoPermiso = idGrupoPermiso
            Me.Descripcion = descripcion
        End Sub

        Public Property IdGrupoPermiso As Integer
            Get
                Return _idGrupoPermiso
            End Get
            Set(value As Integer)
                _idGrupoPermiso = value
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
