Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UsuarioLoggeado") IsNot Nothing Then

                Dim idRol As Integer = Convert.ToInt32(Session("RolUsuarioLoggeado").ToString())
                'Consultar los permisos y habilitar o deshabilitar opciones
            End If
        End If
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Response.Redirect("Login.aspx", False)
    End Sub
End Class