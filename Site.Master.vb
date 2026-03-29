Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Response.Redirect("Login.aspx", False)
    End Sub
End Class