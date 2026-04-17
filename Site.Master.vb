Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UsuarioLoggeado") IsNot Nothing Then

                Dim idRol As Integer = Convert.ToInt32(Session("RolUsuarioLoggeado").ToString())
                Dim listaAccesos = Session("ListaAccesos")

                Dim objHerramientas As New Herramientas
                'Consultar los permisos y habilitar o deshabilitar opciones
                'Obtener los permisos y apartir de ahí se ocultan opciones de menú


                ' En cada página se debe validar si puede acceder a dicha página, lo que se hace es recuperar la lista de accesos de la sesión, y con el identificador de cada página se debe ejecutar el Herramientas.NumeroEnLista y, dependiendo de la respuesta puede continuar o ser redireccionado hacia otra página

            End If
        End If
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Response.Redirect("Login.aspx", False)
    End Sub
End Class