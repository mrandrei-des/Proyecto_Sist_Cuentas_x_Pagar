Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceHome")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        CargarContadoresMetricas()
    End Sub

    Private Sub CargarContadoresMetricas()

        Dim fechaInicialSemanaActual As DateTime, fechaFinalSemanaActual As DateTime
        Dim fecha As DateTime
        fecha = DateTime.Now

        If fecha.DayOfWeek = DayOfWeek.Sunday Then
            fechaInicialSemanaActual = fecha.AddDays(-6)
        Else
            fechaInicialSemanaActual = fecha.AddDays(-fecha.DayOfWeek + 1)
        End If

        If fecha.DayOfWeek = DayOfWeek.Sunday Then
            fechaFinalSemanaActual = fecha
        Else
            fechaFinalSemanaActual = fecha.AddDays(7 - fecha.DayOfWeek)
        End If

        Dim fechaLunes As DateTime = DateTime.Now.AddDays(-DateTime.Now.DayOfWeek + 1)
    End Sub

End Class