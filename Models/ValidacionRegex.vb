Imports System.Text.RegularExpressions
Public Class ValidacionRegex
    Private _nombreCampo As String
    Private _regla As Regex
    Private _mensaje As String

    Public Sub New()
    End Sub

    Public Sub New(nombreCampo As String, regla As Regex, mensaje As String)
        Me.NombreCampo = nombreCampo
        Me.Regla = regla
        Me.Mensaje = mensaje
    End Sub

    Public Property NombreCampo As String
        Get
            Return _nombreCampo
        End Get
        Set(value As String)
            _nombreCampo = value
        End Set
    End Property

    Public Property Regla As Regex
        Get
            Return _regla
        End Get
        Set(value As Regex)
            _regla = value
        End Set
    End Property

    Public Property Mensaje As String
        Get
            Return _mensaje
        End Get
        Set(value As String)
            _mensaje = value
        End Set
    End Property
End Class
