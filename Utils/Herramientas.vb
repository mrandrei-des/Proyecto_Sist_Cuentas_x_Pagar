Imports Microsoft.Ajax.Utilities

Public Class Herramientas
    Public Function prcDevuelveParametroFiltro_int(valorFiltro As String) As Integer
        If valorFiltro.IsNullOrWhiteSpace() Then
            Return 0
        Else
            Return CInt(valorFiltro)
        End If
    End Function

    Public Function prcDevuelveParametroFiltro_str(valorFiltro As String) As String
        If valorFiltro.IsNullOrWhiteSpace() Then
            Return ""
        Else
            Return valorFiltro
        End If
    End Function
End Class
