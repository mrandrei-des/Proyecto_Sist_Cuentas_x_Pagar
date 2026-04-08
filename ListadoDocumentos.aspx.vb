Public Class ListadoDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceRegistroDocumentos")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
        If Not IsPostBack Then
            ' Cargar select u otros elementos
        End If
    End Sub

End Class