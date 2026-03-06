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
        Dim errorMessage As String = "", usuarioCreacion As String = ""
        Dim nombreUsuario As String = txtUsuario.Text.Trim()

        modUsuario = objUsuarioDB.ConsultarUsuario_x_Username(nombreUsuario, errorMessage)
        If modUsuario Is Nothing Then ' Si el resultado de la consulta es nulo, significa que no existe un usuario con ese nombre de usuario, por lo tanto se puede crear
            modUsuario.NombreUsuario = txtUsuario.Text
            modUsuario.Contrasenna = txtContrasenna.Text
            modUsuario.Nombre = txtNombre.Text
            modUsuario.Apellido1 = txtApellidoUno.Text
            modUsuario.Apellido2 = txtApellidoDos.Text
            modUsuario.Correo = txtCorreoUsuario.Text
            modUsuario.Estado = CInt(ddlEstadoUsuario.SelectedItem.Value)
            modUsuario.Rol = CInt(ddlRoles.SelectedItem.Value)

            If objUsuarioDB.CrearUsuario(modUsuario, usuarioCreacion, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Usuario creado exitosamente!")
                limpiarCampos()
            Else
                SwalUtils.ShowSwalError(Me, errorMessage)
            End If
        Else
            SwalUtils.ShowSwalError(Me, "Ya existe un usuario con ese nombre de usuario, por favor ingrese otro.")
        End If
    End Sub

    Private Sub limpiarCampos()
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
        If listEstados.Count > 0 Then
            ddlEstadoUsuario.Items.Clear()
            ddlEstadoUsuario.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modEstado As Models.Estado In listEstados
                ddlEstadoUsuario.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
            Next

            ddlEstadoUsuario.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcLlena_ddlRoles()
        Dim listRoles As List(Of Models.Rol)
        Dim objRolDB As New RolDB
        Dim errorMessage As String = ""

        listRoles = objRolDB.ConsultarRoles(errorMessage)
        If listRoles.Count > 0 Then
            ddlRoles.Items.Clear()
            ddlRoles.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modRol As Models.Rol In listRoles
                ddlRoles.Items.Add(New ListItem(modRol.Descripcion.ToString(), modRol.IdRol))
            Next

            ddlRoles.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub
End Class