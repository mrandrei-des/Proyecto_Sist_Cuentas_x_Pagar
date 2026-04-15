Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnInicioSesion_Click(sender As Object, e As EventArgs)
        Dim encryptor As New Simple3Des("def_.phas7401{}pinna??¿")
        Dim usuario As String = txtUsuario.Text.Trim()
        Dim contrasenna As String = txtContrasenna.Text.Trim()

        ' Valida que los datos estén correctos
        If Not ValidarDatos(usuario, contrasenna) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        Dim contrasennaHash = encryptor.EncryptData(contrasenna)
        Dim modUsuario As New Models.Usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = ""

        modUsuario = objUsuarioDB.Login(usuario, contrasennaHash, errorMessage)

        If modUsuario Is Nothing Then
            SwalUtils.ShowSwalError(Me, "Error", $"Ocurrió un error al intentar iniciar sesión. [{errorMessage}]")
            Return
        End If

        If modUsuario.NombreUsuario IsNot Nothing Then
            'Session("Usuario") = modUsuario
            Session("UsuarioLoggeado") = modUsuario.NombreUsuario
            Session("RolUsuarioLoggeado") = modUsuario.Rol

            ' DEPENDIENTO DEL USUARIO Y ROLES QUE TENGO LO DIRIGE A UNA PANTALLA EN ESPECIAL
            If modUsuario.Rol = 1 Then
                Response.Redirect("index.aspx", False)
            Else
                Response.Redirect("index.aspx", False)
            End If

        Else
            SwalUtils.ShowSwalError(Me, "El usuario o la contraseña no coinciden.")
        End If
    End Sub

    Private Function ValidarDatos(usuario As String, contrasenna As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True
        contenedorMensajesUsuario.InnerHtml = ""
        contenedorMensajesContrasenna.InnerHtml = ""

        modRegla = objListaReglas.ObtenerReglaPorCampo("usuario")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, usuario) Then
                contenedorMensajesUsuario.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesUsuario.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesUsuario.Style.Remove("display")
            End If
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("contrasena")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, contrasenna) Then
                contenedorMensajesContrasenna.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesContrasenna.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesContrasenna.Style.Remove("display")
            End If
        End If

        Return respuestaValidacion
    End Function
End Class