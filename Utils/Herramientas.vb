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

    Public Function FormatearMonto(montoFormatear As Double) As String
        If montoFormatear = 0 Then
            Return "0,00"
        End If
        Return Format(montoFormatear, "#,###,###,###.00")
    End Function

    Public Function prcDevuelveDiaSemanaTexto(dia As DayOfWeek) As String
        Select Case dia

            Case 0
                Return "Domingo"

            Case 1
                Return "Lunes"

            Case 2
                Return "Martes"

            Case 3
                Return "Miércoles"

            Case 4
                Return "Jueves"

            Case 5
                Return "Viernes"

            Case Else
                Return "Sábado"
        End Select
    End Function

    Public Function prcDevuelveMesAnnoTexto(mes As Integer) As String
        Select Case mes
            Case 1
                Return "Enero"

            Case 2
                Return "Febrero"

            Case 3
                Return "Marzo"

            Case 4
                Return "Abril"

            Case 5
                Return "Mayo"

            Case 6
                Return "Junio"

            Case 7
                Return "Julio"

            Case 8
                Return "Agosto"

            Case 9
                Return "Setiembre"

            Case 10
                Return "Octubre"

            Case 11
                Return "Noviembre"

            Case Else
                Return "Diciembre"
        End Select
    End Function

    Public Function prcDevuelveNombreAccion(idCategoria As Integer, accion As String) As String
        Dim accionRealizada As String

        If idCategoria = 1 Then
            Select Case accion
                Case "INSERTAR"
                    accionRealizada = "registrada"
                Case "APLICAR"
                    accionRealizada = "aplicada"
                Case "ELIMINAR"
                    accionRealizada = "eliminada"
                Case Else
                    accionRealizada = "modificada"
            End Select
        Else
            Select Case accion
                Case "INSERTAR"
                    accionRealizada = "registrado"
                Case "APLICAR"
                    accionRealizada = "aplicado"
                Case "ELIMINAR"
                    accionRealizada = "eliminado"
                Case Else
                    accionRealizada = "modificado"
            End Select
        End If

        Return accionRealizada
    End Function
End Class


