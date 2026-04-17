Imports System.Drawing
Imports System.Security.Policy
Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class WebForm1
    Inherits System.Web.UI.Page

    Private Const IDENTIFICADOR As String = "LIST_MANT_USUARIOS"

    '' INICIO EVENTOS DE LA PÁGINA
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not UsuarioPuedeContinuar() Then
            Session.Clear()
            Response.Redirect("Login.aspx", False)
        End If

        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceUsuarios")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            prcLlena_ddls_Estado()
            prcLlena_ddls_Roles()
        End If
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

    Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs)
        prcLimpiarFiltros()
        gvUsuarios.DataSourceID = "SqlDataSource2"
        gvUsuarios.DataBind()
    End Sub

    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim indexColNombreUsuario = Convert.ToInt32(e.CommandArgument)
        Dim nombreUsuarioAfectado = gvUsuarios.DataKeys(indexColNombreUsuario).Value
        Dim nombreUsuarioEjecutaAccion = Session("UsuarioLoggeado")
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
        Dim nombreUsuarioEjecutaAccion = Session("UsuarioLoggeado")
        Dim nombre As String = txtNombre.Text.Trim(), apellido1 As String = txtApellidoUno.Text.Trim(), apellido2 As String = txtApellidoDos.Text.Trim(), correo As String = txtCorreoUsuario.Text.Trim(), estado As String = ddlEstadoUsuario.SelectedItem.Value, rol As String = ddlRoles.SelectedItem.Value

        If (Not ValidarDatos(nombre, apellido1, apellido2, correo, estado, Rol)) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        modUsuario.NombreUsuario = hfUsuario.Value
        modUsuario.Nombre = nombre
        modUsuario.Apellido1 = apellido1
        modUsuario.Apellido2 = apellido2
        modUsuario.Correo = correo
        modUsuario.Estado = CInt(estado)
        modUsuario.Rol = CInt(rol)

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

        If modUsuario.NombreUsuario IsNot Nothing Then
            txtNombre.Text = modUsuario.Nombre
            txtApellidoUno.Text = modUsuario.Apellido1
            txtApellidoDos.Text = modUsuario.Apellido2
            txtCorreoUsuario.Text = modUsuario.Correo
            ddlEstadoUsuario.SelectedValue = modUsuario.Estado
            ddlRoles.SelectedValue = modUsuario.Rol
            contenedorMensajesModalNombre.InnerHtml = ""
            contenedorMensajesModalApellido1.InnerHtml = ""
            contenedorMensajesModalApellido2.InnerHtml = ""
            contenedorMensajesModalCorreo.InnerHtml = ""
            contenedorMensajesModalEstado.InnerHtml = ""
            contenedorMensajesModalRol.InnerHtml = ""

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

    Private Function ValidarDatos(nombre As String, apellido1 As String, apellido2 As String, correo As String, estado As String, rol As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True

        modRegla = objListaReglas.ObtenerReglaPorCampo("nombres")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, nombre) Then
                contenedorMensajesModalNombre.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesModalNombre.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesModalNombre.Style.Remove("display")
            End If
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("nombres")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, apellido1) Then
                contenedorMensajesModalApellido1.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesModalApellido1.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesModalApellido1.Style.Remove("display")
            End If
        End If

        If Not String.IsNullOrWhiteSpace(apellido2) Then
            modRegla = objListaReglas.ObtenerReglaPorCampo("nombres")
            If Not modRegla Is Nothing Then
                If Not objListaReglas.ValidarCampo(modRegla, apellido2) Then
                    contenedorMensajesModalApellido2.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                    contenedorMensajesModalApellido2.Style.Add("display", "block")
                    respuestaValidacion = False
                Else
                    contenedorMensajesModalApellido2.Style.Remove("display")
                End If
            End If
        Else
            contenedorMensajesModalApellido2.InnerHtml = ""
        End If


        modRegla = objListaReglas.ObtenerReglaPorCampo("correo")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, correo) Then
                contenedorMensajesModalCorreo.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesModalCorreo.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesModalCorreo.Style.Remove("display")
            End If
        End If

        If Not objHerramienta.ValidarNumeroEntero(estado, False) Then
            contenedorMensajesModalEstado.InnerHtml = $"<p class='formulario__mensaje'>Debe seleccionar una opción de estado.</p>"
            contenedorMensajesModalEstado.Style.Add("display", "block")
            respuestaValidacion = False
        Else
            contenedorMensajesModalEstado.Style.Remove("display")
        End If

        If Not objHerramienta.ValidarNumeroEntero(rol, False) Then
            contenedorMensajesModalRol.InnerHtml = $"<p class='formulario__mensaje'>Debe seleccionar una opción de rol.</p>"
            contenedorMensajesModalRol.Style.Add("display", "block")
            respuestaValidacion = False
        Else
            contenedorMensajesModalRol.Style.Remove("display")
        End If

        Return respuestaValidacion
    End Function

    '' FIN FUNCIONES Y MÉTODOS DE LA PÁGINA
End Class