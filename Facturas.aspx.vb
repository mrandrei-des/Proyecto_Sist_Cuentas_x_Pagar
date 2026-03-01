Public Class Facturas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceDocumentos")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
    End Sub

End Class