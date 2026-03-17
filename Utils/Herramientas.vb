Imports System.Security.Permissions
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

    Public Function ValidarNumeroDecimales(numeroValidar As String, Optional permiteNegativos As Boolean = False) As Boolean
        If Not IsNumeric(numeroValidar) Then
            Return False
        End If

        If Not permiteNegativos Then
            If Convert.ToDouble(numeroValidar) < 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Public Function ValidarNumeroEntero(numeroValidar As String, Optional permiteNegativos As Boolean = False) As Boolean
        If Not IsNumeric(numeroValidar) Then
            Return False
        End If

        If Not permiteNegativos Then
            If Convert.ToInt32(numeroValidar) < 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Public Function ValidarCadena(cadenaValidar As String) As Boolean
        If cadenaValidar.IsNullOrWhiteSpace() Then
            Return False
        End If

        Return True
    End Function

    Public Function ValidarFecha(fechaValidar As String) As Boolean
        If Not IsDate(fechaValidar) Then
            Return False
        End If

        Return True
    End Function
End Class


