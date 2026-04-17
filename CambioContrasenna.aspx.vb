Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CambioContrasenna
    Inherits System.Web.UI.Page

    Private Const IDENTIFICADOR As String = "CAMBIO_CONTRASENNA"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not UsuarioPuedeContinuar() Then
            Session.Clear()
            Response.Redirect("Login.aspx", False)
        End If

        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceUsuarios")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
    End Sub

    Private Function UsuarioPuedeContinuar() As Boolean
        If Session("UsuarioLoggeado") IsNot Nothing Then
            If Session("RolUsuarioLoggeado") IsNot Nothing Then
                If Session("RolUsuarioLoggeado") = 1 Then
                    Return True
                End If

                If Session("ListaAccesos") IsNot Nothing Then
                    Dim listaAccesos As New List(Of String)
                    listaAccesos = Session("ListaAccesos")

                    Dim objRedireccion As New Redireccionamiento
                    If objRedireccion.PermisoEnLista(listaAccesos, IDENTIFICADOR) Then
                        Return True
                    Else
                        Dim permisoAcceder As String, nombrePagina As String
                        permisoAcceder = listaAccesos.Item(0)

                        nombrePagina = objRedireccion.DevuelvePaginaInicioUsuario(permisoAcceder)
                        Response.Redirect(nombrePagina, True)
                    End If
                End If
            End If
        End If

        Return False
    End Function

    Protected Sub btnCambioContrasenna_Click(sender As Object, e As EventArgs)
        Dim encryptor As New Simple3Des("def_.phas7401{}pinna??¿")
        Dim nuevaContrasenna As String = txtContrasenna.Text.Trim()
        Dim confirmarContrasenna As String = txtConfirmarContrasenna.Text.Trim()

        ' Valida que los datos estén correctos, para el momento de hacer la conversión
        If Not ValidarDatos(nuevaContrasenna, confirmarContrasenna) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        confirmarContrasenna = encryptor.EncryptData(txtConfirmarContrasenna.Text.Trim())

        Dim modUsuario As New Models.Usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = ""
        modUsuario.NombreUsuario = Session("UsuarioLoggeado")
        modUsuario.Contrasenna = confirmarContrasenna

        If objUsuarioDB.CambioContrasenna(modUsuario, errorMessage) Then
            SwalUtils.ShowSwal(Me, "Contraseña cambiada exitosamente.")
        Else
            SwalUtils.ShowSwalError(Me, "Error al cambiar la contraseña: " & errorMessage)
        End If
    End Sub

    Private Function ValidarDatos(nuevaContrasenna As String, confirmarContrasenna As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True
        contenedorMensajesContrasenna.InnerHtml = ""
        contenedorMensajesConfirmContrasenna.InnerHtml = ""


        modRegla = objListaReglas.ObtenerReglaPorCampo("contrasena")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, nuevaContrasenna) Then
                contenedorMensajesContrasenna.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesContrasenna.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesContrasenna.Style.Remove("display")
            End If
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("contrasena")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, confirmarContrasenna) Then
                contenedorMensajesConfirmContrasenna.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesConfirmContrasenna.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesConfirmContrasenna.Style.Remove("display")
            End If
        End If

        If nuevaContrasenna <> confirmarContrasenna Then
            contenedorMensajesConfirmContrasenna.InnerHtml += $"<p class='formulario__mensaje'>Las contraseñas no coinciden.</p>"
            contenedorMensajesConfirmContrasenna.Style.Add("display", "block")
            respuestaValidacion = False
        End If

        Return respuestaValidacion
    End Function
End Class