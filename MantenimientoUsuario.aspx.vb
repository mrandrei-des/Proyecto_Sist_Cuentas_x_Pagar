Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class Usuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceUsuarios")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            prcLlena_ddlEstados()
            prcLlena_ddlRoles()
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim modUsuario As New Models.Usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = "", usuarioCreacion As String = "andre"
        Dim nombreUsuario As String = txtUsuario.Text.Trim(), contrasenna As String = txtContrasenna.Text.Trim(), nombre As String = txtNombre.Text.Trim()
        Dim apellido1 As String = txtApellidoUno.Text.Trim(), apellido2 As String = txtApellidoDos.Text.Trim(), correo As String = txtCorreoUsuario.Text.Trim(), estado As String = ddlEstadoUsuario.SelectedItem.Value, rol As String = ddlRoles.SelectedItem.Value

        If (Not ValidarDatos(nombreUsuario, contrasenna, nombre, apellido1, apellido2, correo, estado, rol)) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        modUsuario = objUsuarioDB.ConsultarUsuario_x_Username(nombreUsuario, errorMessage)

            If modUsuario Is Nothing Then
                SwalUtils.ShowSwalError(Me, errorMessage)
                Return
            End If

        If modUsuario.NombreUsuario Is Nothing Then ' Si el resultado de la consulta vuelve vacío, significa que no existe un usuario con ese nombre de usuario, por lo tanto se puede crear
            Dim encryptor As New Simple3Des("def_.phas7401{}pinna??¿")
            Dim contrasennaHash As String = encryptor.EncryptData(contrasenna.Trim())

            modUsuario = New Models.Usuario
            modUsuario.NombreUsuario = nombreUsuario
            modUsuario.Contrasenna = contrasennaHash
            modUsuario.Nombre = nombre
            modUsuario.Apellido1 = apellido1
            modUsuario.Apellido2 = apellido2
            modUsuario.Correo = correo
            modUsuario.Estado = CInt(estado)
            modUsuario.Rol = CInt(rol)

            If objUsuarioDB.CrearUsuario(modUsuario, usuarioCreacion, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Usuario creado exitosamente!")
                LimpiarCampos()
            Else
                SwalUtils.ShowSwalError(Me, errorMessage)
            End If
        Else
            SwalUtils.ShowSwalError(Me, "Ya existe un usuario con ese nombre de usuario, por favor ingrese otro.")
        End If
    End Sub

    Private Function ValidarDatos(userName As String, contrasenna As String, nombre As String, apellido1 As String, apellido2 As String, correo As String, estado As String, rol As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True

        modRegla = objListaReglas.ObtenerReglaPorCampo("usuario")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, userName) Then
                contenedorMensajesNombreUsuario.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesNombreUsuario.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesNombreUsuario.Style.Remove("display")
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

        modRegla = objListaReglas.ObtenerReglaPorCampo("nombres")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, nombre) Then
                contenedorMensajesNombre.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesNombre.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesNombre.Style.Remove("display")
            End If
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("nombres")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, apellido1) Then
                contenedorMensajesApellido1.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesApellido1.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesApellido1.Style.Remove("display")
            End If
        End If

        If Not String.IsNullOrWhiteSpace(apellido2) Then
            modRegla = objListaReglas.ObtenerReglaPorCampo("nombres")
            If Not modRegla Is Nothing Then
                If Not objListaReglas.ValidarCampo(modRegla, apellido2) Then
                    contenedorMensajesApellido2.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                    contenedorMensajesApellido2.Style.Add("display", "block")
                    respuestaValidacion = False
                Else
                    contenedorMensajesApellido2.Style.Remove("display")
                End If
            End If
        Else
            contenedorMensajesApellido2.InnerHtml = ""
        End If


        modRegla = objListaReglas.ObtenerReglaPorCampo("correo")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, correo) Then
                contenedorMensajesCorreo.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesCorreo.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesCorreo.Style.Remove("display")
            End If
        End If

        If Not objHerramienta.ValidarNumeroEntero(estado, False) Then
            contenedorMensajesEstado.InnerHtml = $"<p class='formulario__mensaje'>Debe seleccionar una opción de estado.</p>"
            contenedorMensajesEstado.Style.Add("display", "block")
            respuestaValidacion = False
        Else
            contenedorMensajesEstado.Style.Remove("display")
        End If

        If Not objHerramienta.ValidarNumeroEntero(rol, False) Then
            contenedorMensajesRol.InnerHtml = $"<p class='formulario__mensaje'>Debe seleccionar una opción de rol.</p>"
            contenedorMensajesRol.Style.Add("display", "block")
            respuestaValidacion = False
        Else
            contenedorMensajesRol.Style.Remove("display")
        End If

        Return respuestaValidacion
    End Function

    Private Sub LimpiarCampos()
        txtUsuario.Text = ""
        txtContrasenna.Text = ""
        txtNombre.Text = ""
        txtApellidoUno.Text = ""
        txtApellidoDos.Text = ""
        txtCorreoUsuario.Text = ""
        ddlEstadoUsuario.SelectedIndex = 0
        ddlRoles.SelectedIndex = 0
    End Sub

    Private Sub prcLlena_ddlEstados()
        Dim listEstados As List(Of Models.Estado)
        Dim objEstadoDB As New EstadoDB
        Dim errorMessage As String = ""

        listEstados = objEstadoDB.ConsultarEstados(errorMessage)

        If listEstados Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listEstados.Count > 0 Then
            ddlEstadoUsuario.Items.Clear()
            ddlEstadoUsuario.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modEstado As Models.Estado In listEstados
                ddlEstadoUsuario.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
            Next

            ddlEstadoUsuario.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron estados en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcLlena_ddlRoles()
        Dim listRoles As List(Of Models.Rol)
        Dim objRolDB As New RolDB
        Dim errorMessage As String = ""

        listRoles = objRolDB.ConsultarRoles(errorMessage)

        If listRoles Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listRoles.Count > 0 Then
            ddlRoles.Items.Clear()
            ddlRoles.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modRol As Models.Rol In listRoles
                ddlRoles.Items.Add(New ListItem(modRol.Descripcion.ToString(), modRol.IdRol))
            Next

            ddlRoles.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron roles en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        LimpiarCampos()
    End Sub
End Class