Imports System.Drawing
Imports System.Security.Policy
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class WebForm1
    Inherits System.Web.UI.Page

    '' INICIO EVENTOS DE LA PÁGINA
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceUsuarios")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            prcLlena_ddls_Estado()
            prcLlena_ddls_Roles()
        End If
    End Sub

    Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs)
        prcLimpiarFiltros()
        gvUsuarios.DataSourceID = "SqlDataSource2"
        gvUsuarios.DataBind()
    End Sub

    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim indexColNombreUsuario = Convert.ToInt32(e.CommandArgument)
        Dim nombreUsuarioAfectado = gvUsuarios.DataKeys(indexColNombreUsuario).Value
        Dim nombreUsuarioEjecutaAccion = "andre"
        hfUsuario.Value = nombreUsuarioAfectado

        Select Case e.CommandName
            Case "EditarUsuario"
                prcCargaUsuarioModal(nombreUsuarioAfectado)

            Case "EliminarUsuario"
                prcEliminarUsuario(nombreUsuarioAfectado, nombreUsuarioEjecutaAccion)
        End Select
    End Sub

    Protected Sub btnCerrarModal_Click(sender As Object, e As EventArgs) Handles btnCerrarModal.Click
        modalModify.Style.Remove("display")
    End Sub

    Protected Sub btnModificarUsuario_Click(sender As Object, e As EventArgs)
        Dim modUsuario As New Models.Usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = ""
        Dim nombreUsuarioEjecutaAccion = "andre"

        modUsuario.NombreUsuario = hfUsuario.Value
        modUsuario.Nombre = txtNombre.Text
        modUsuario.Apellido1 = txtApellidoUno.Text
        modUsuario.Apellido2 = txtApellidoDos.Text
        modUsuario.Correo = txtCorreoUsuario.Text
        modUsuario.Estado = CInt(ddlEstadoUsuario.SelectedItem.Value)
        modUsuario.Rol = CInt(ddlRoles.SelectedItem.Value)

        If objUsuarioDB.ModificarUsuario(modUsuario, nombreUsuarioEjecutaAccion, errorMessage) Then
            prcLimpiarModal()
            modalModify.Style.Remove("display")
            SwalUtils.ShowSwal(Me, "¡Usuario modificado exitosamente!")
            gvUsuarios.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Protected Sub txtFiltNombre_TextChanged(sender As Object, e As EventArgs)
        prcFiltrarUsuarios()
    End Sub

    Protected Sub ddlFiltEstado_SelectedIndexChanged(sender As Object, e As EventArgs)
        prcFiltrarUsuarios()
    End Sub

    Protected Sub ddlFiltRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        prcFiltrarUsuarios()
    End Sub

    '' FIN EVENTOS DE LA PÁGINA

    '' INICIO FUNCIONES Y MÉTODOS DE LA PÁGINA
    Private Sub prcCargaUsuarioModal(nombreUsuarioAfectado As String)
        ' Aquí se debe ir a consultar los datos del usuario a la base de datos para poder cargarlos en pantalla y a la hora de ver el modal se despliegue con los datos del usuario
        Dim objUsuarioDB As New UsuarioDB
        Dim modUsuario As Models.Usuario
        Dim errorMessage As String = ""

        modUsuario = objUsuarioDB.ConsultarUsuario_x_Username(nombreUsuarioAfectado, errorMessage)

        If modUsuario Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If modUsuario IsNot New Models.Usuario Then
            txtNombre.Text = modUsuario.Nombre
            txtApellidoUno.Text = modUsuario.Apellido1
            txtApellidoDos.Text = modUsuario.Apellido2
            txtCorreoUsuario.Text = modUsuario.Correo
            ddlEstadoUsuario.SelectedValue = modUsuario.Estado
            ddlRoles.SelectedValue = modUsuario.Rol
            pSubtituloModal.InnerHtml = "Usuario: <span>" + nombreUsuarioAfectado + "<span>"
            modalModify.Style.Add("display", "flex")
        Else
            SwalUtils.ShowSwalError(Me, "No se encontró información del usuario en el sistema.")
        End If
    End Sub

    Private Sub prcLimpiarModal()
        txtNombre.Text = ""
        txtApellidoUno.Text = ""
        txtApellidoDos.Text = ""
        txtCorreoUsuario.Text = ""
        ddlEstadoUsuario.SelectedIndex = 0
        ddlRoles.SelectedIndex = 0
    End Sub

    Private Sub prcLimpiarFiltros()
        txtFiltNombre.Text = ""
        ddlFiltEstado.SelectedIndex = 0
        ddlFiltRoles.SelectedIndex = 0
    End Sub

    Private Sub prcEliminarUsuario(nombreUsuarioAfectado As String, nombreUsuarioElimino As String)
        Dim objUsuarioDB As New UsuarioDB
        Dim errorMessage As String = ""

        If objUsuarioDB.EliminarUsuario(nombreUsuarioAfectado, nombreUsuarioElimino, errorMessage) Then
            SwalUtils.ShowSwal(Me, "¡El usuario [" + nombreUsuarioAfectado + "] ha sido eliminado del sistema!")
            gvUsuarios.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcLlena_ddls_Estado() 'Carga los dos dropdownlist de estados, el del modal y el de los filtros
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
            ddlFiltEstado.Items.Clear()

            ddlEstadoUsuario.Items.Add(New ListItem("Seleccione una opción", ""))
            ddlFiltEstado.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modEstado As Models.Estado In listEstados
                ddlEstadoUsuario.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
                ddlFiltEstado.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
            Next

            ddlEstadoUsuario.SelectedIndex = 0
            ddlFiltEstado.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron estados en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcLlena_ddls_Roles() 'Carga los dos dropdownlist de roles, el del modal y el de los filtros
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
            ddlFiltRoles.Items.Clear()

            ddlRoles.Items.Add(New ListItem("Seleccione una opción", ""))
            ddlFiltRoles.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modRol As Models.Rol In listRoles
                ddlRoles.Items.Add(New ListItem(modRol.Descripcion.ToString(), modRol.IdRol))
                ddlFiltRoles.Items.Add(New ListItem(modRol.Descripcion.ToString(), modRol.IdRol))
            Next

            ddlRoles.SelectedIndex = 0
            ddlFiltRoles.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron roles en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcFiltrarUsuarios()
        Dim objUsuarioDB As New UsuarioDB
        Dim ObjHerramientas As New Herramientas
        Dim errorMessage As String = ""
        Dim filtNombre As String = ""
        Dim filtRol As Integer = 0, filtEstado As Integer = 0
        Dim dtResultados As DataTable

        filtNombre = ObjHerramientas.prcDevuelveParametroFiltro_str(txtFiltNombre.Text)
        filtEstado = ObjHerramientas.prcDevuelveParametroFiltro_int(ddlFiltEstado.SelectedValue)
        filtRol = ObjHerramientas.prcDevuelveParametroFiltro_int(ddlFiltRoles.SelectedValue)

        dtResultados = objUsuarioDB.FiltrarUsuarios(filtNombre, filtEstado, filtRol, errorMessage)

        If dtResultados Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If dtResultados.Rows.Count > 0 Then
            gvUsuarios.DataSourceID = ""
            gvUsuarios.DataSource = dtResultados
            gvUsuarios.DataBind()
        Else
            SwalUtils.ShowSwalMessage(Me, "Consulta", "No se encontraron coincidencias con los filtros utilizados.", "")
            gvUsuarios.DataSourceID = "SqlDataSource2"
            gvUsuarios.DataBind()
        End If
    End Sub

    '' FIN FUNCIONES Y MÉTODOS DE LA PÁGINA
End Class