Imports System.Web.WebSockets

Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceHome")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
        Dim nombreUsuario As String

        nombreUsuario = "andre"

        CargaSaludoUsuario(nombreUsuario)
        CargarContadoresMetricas()
    End Sub

    Private Sub CargaSaludoUsuario(usuarioLoggeado As String)
        Dim objHerramientas As New Herramientas
        Dim objUsuarioDB As New UsuarioDB
        Dim modUsuario As New Models.Usuario()
        Dim errorMessage As String = ""
        Dim fecha As DateTime
        Dim diaSemanaTexto As String, mesAnnoTexto As String

        fecha = DateTime.Now
        diaSemanaTexto = objHerramientas.prcDevuelveDiaSemanaTexto(fecha.DayOfWeek)
        mesAnnoTexto = objHerramientas.prcDevuelveMesAnnoTexto(fecha.Month)

        modUsuario = objUsuarioDB.ConsultarUsuario_x_Username(usuarioLoggeado, errorMessage)

        If modUsuario IsNot Nothing Then
            If modUsuario.Nombre IsNot Nothing Then
                titleUser.InnerText = $"Bienvenido, {modUsuario.Nombre}"
            Else
                titleUser.InnerText = "No se encontró información del usuario."
            End If
        End If

        fechaActual.InnerText = $"{diaSemanaTexto} {fecha.Day} de {mesAnnoTexto.ToLower()}, {fecha.Year}"
    End Sub

    Private Sub CargarContadoresMetricas()
        CargaMetricaDocsIngresados()
        CargaMetricaProveedoresRegistrados()
        CargaMetricaFacturasPend()
        CargaMetricaPagosPend()
        CargaMetricaDocsAplicados()
    End Sub

    Private Sub CargaMetricaDocsIngresados()
        Dim objContadorDB As New ContadorDB
        Dim modContador As New Models.Contador
        Dim errorMessage As String = ""

        modContador = objContadorDB.ConsultarCantDocsRegistrados_hoy(errorMessage)

        If modContador IsNot Nothing Then
            If IsNumeric(modContador.Cantidad) Then
                documentosIngresados.InnerHtml = $"<strong>{modContador.Cantidad}</strong>"
            Else
                documentosIngresados.InnerHtml = $"<strong>*ERROR*</strong>"
            End If
        End If

    End Sub

    Private Sub CargaMetricaProveedoresRegistrados()
        Dim objContadorDB As New ContadorDB
        Dim modContador As New Models.Contador()
        Dim fechaInicialSemanaActual As String, fechaFinalSemanaActual As String, errorMessage As String = ""
        Dim fecha As DateTime

        fecha = DateTime.Now

        If fecha.DayOfWeek = DayOfWeek.Sunday Then
            fechaInicialSemanaActual = fecha.AddDays(-6).ToString()
        Else
            fechaInicialSemanaActual = fecha.AddDays(-fecha.DayOfWeek + 1).ToString()
        End If

        If fecha.DayOfWeek = DayOfWeek.Sunday Then
            fechaFinalSemanaActual = fecha.ToString()
        Else
            fechaFinalSemanaActual = fecha.AddDays(7 - fecha.DayOfWeek).ToString()
        End If

        modContador = objContadorDB.ConsultarCantProveedorRegistrados_semana(fechaInicialSemanaActual, fechaFinalSemanaActual, errorMessage)

        If modContador IsNot Nothing Then
            If IsNumeric(modContador.Cantidad) Then
                proveedoresRegistrados.InnerHtml = $"<strong>{modContador.Cantidad}</strong>"
            Else
                proveedoresRegistrados.InnerHtml = $"<strong>*ERROR*</strong>"
            End If
        End If

    End Sub

    Private Sub CargaMetricaFacturasPend()
        Dim objContadorDB As New ContadorDB
        Dim modContador As New Models.Contador()
        Dim errorMessage As String = ""

        modContador = objContadorDB.ConsultarCantFacturasPendientes(errorMessage)

        If modContador IsNot Nothing Then
            If IsNumeric(modContador.Cantidad) Then
                facturasPendientes.InnerHtml = $"<strong>{modContador.Cantidad}</strong>"
            Else
                facturasPendientes.InnerHtml = $"<strong>*ERROR*</strong>"
            End If
        End If
    End Sub

    Private Sub CargaMetricaPagosPend()
        Dim objContadorDB As New ContadorDB
        Dim modContador As New Models.Contador()
        Dim errorMessage As String = ""

        modContador = objContadorDB.ConsultarCantPagosPendientes(errorMessage)

        If modContador IsNot Nothing Then
            If IsNumeric(modContador.Cantidad) Then
                pagosPendientes.InnerHtml = $"<strong>{modContador.Cantidad}</strong>"
            Else
                pagosPendientes.InnerHtml = $"<strong>*ERROR*</strong>"
            End If
        End If
    End Sub

    Private Sub CargaMetricaDocsAplicados()
        Dim objContadorDB As New ContadorDB
        Dim modContador As New Models.Contador()
        Dim objHerramientas As New Herramientas
        Dim mesAnnoTexto As String, errorMessage As String = ""
        Dim fecha As DateTime

        modContador = objContadorDB.ConsultarCantDocsAplicados_mes(errorMessage)

        If modContador IsNot Nothing Then
            If IsNumeric(modContador.Cantidad) Then
                aplicadosEsteMes.InnerHtml = $"<strong>{modContador.Cantidad}</strong>"
            Else
                aplicadosEsteMes.InnerHtml = $"<strong>*ERROR*</strong>"
            End If
        End If

        fecha = DateTime.Now
        mesAnnoTexto = objHerramientas.prcDevuelveMesAnnoTexto(fecha.Month)

        mesActual.InnerText = $"{mesAnnoTexto} {fecha.Year}"
    End Sub

End Class