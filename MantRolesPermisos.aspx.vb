Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class MantRolesPermisos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceUsuarios")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            contenedorSectionConfig.Style.Add("display", "none")
            prcLlena_ddlRoles()
            hdfRolSeleccionado.Value = 0
        End If
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs)
        txtNombreRol.Text = String.Empty
        hdfRolSeleccionado.Value = 0
        contenedorSectionConfig.Style.Add("display", "none")
        contenedorMensajesNombreRolNuevo.InnerHtml = ""
        mostrarBotonesRolNuevo()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim nombreRol As String = txtNombreRol.Text, errorMessage As String = "", usuarioCreacion As String = "andre"

        If (Not ValidarDatos(nombreRol)) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        ' Verificar que el nuevo nombre no exista ya en el sistema
        Dim listRoles As List(Of Models.Rol)
        Dim objRol As New RolDB()

        listRoles = objRol.ConsultarRoles_x_Nombre(nombreRol, errorMessage)

        If listRoles.Count > 0 Then
            SwalUtils.ShowSwalError(Me, "Atención", "Se han encontrado coincidencias. Por favor cambiar el nombre del rol para evitar malentendido.")
            Return
        End If

        Dim modRol As New Models.Rol() With {
            .Descripcion = nombreRol,
            .Estado = 4
        }

        If (objRol.InsertaRol(modRol, usuarioCreacion, errorMessage)) Then
            SwalUtils.ShowSwal(Me, $"¡Rol registrado exitosamente!", "El rol ya se encuentra listo para configurarle sus permisos.")
            LimpiarCampos()
        Else
            SwalUtils.ShowSwalError(Me, "Atención", $"Ha ocurrido un problema a la hora de registrar el nuevo rol. [{errorMessage}]")
        End If

    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs)
        Dim rolSeleccionado As Integer = Convert.ToInt32(hdfRolSeleccionado.Value)
        Dim nombreRol As String = txtNombreRol.Text, errorMessage As String = "", usuarioModificacion As String = "andre"

        If (Not ValidarDatos(nombreRol)) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        ' Verificar si el rol se encuentra en el sistema
        Dim modRol As New Models.Rol
        Dim objRol As New RolDB()

        modRol = objRol.ConsultarRoles_x_ID(rolSeleccionado, errorMessage)

        If modRol.Descripcion Is Nothing Then
            SwalUtils.ShowSwalError(Me, "Atención", "No se encontró en el sistema el rol seleccionado.")
            Return
        End If

        modRol = New Models.Rol() With {
            .IdRol = rolSeleccionado,
            .Descripcion = nombreRol,
            .Estado = 4
        }

        If (objRol.ModificaRol(modRol, usuarioModificacion, errorMessage)) Then
            SwalUtils.ShowSwal(Me, $"¡Rol modificado exitosamente!", "El rol ha sido actualizado.")
            LimpiarCampos()
        Else
            SwalUtils.ShowSwalError(Me, "Atención", $"Ha ocurrido un problema a la hora de modificar el rol. [{errorMessage}]")
        End If
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs)
        Dim rolSeleccionado As Integer = Convert.ToInt32(hdfRolSeleccionado.Value)
        Dim errorMessage As String = "", usuarioEliminacion As String = "andre"
        ' Elimina el rol seleccionado siempre y cuando no tenga usuarios asociados

        ' Verificar si el rol se encuentra en el sistema
        Dim listUsuarios As List(Of Models.Usuario)
        Dim objRol As New RolDB()
        Dim modRol As New Models.Rol

        modRol = objRol.ConsultarRoles_x_ID(rolSeleccionado, errorMessage)

        If modRol.Descripcion Is Nothing Then
            SwalUtils.ShowSwalError(Me, "Atención", "No se puede eliminar el Rol seleccionado debido a que se encontró en el sistema.")
            Return
        End If

        listUsuarios = objRol.ConsultarRoles_x_ID_Usuarios(rolSeleccionado, errorMessage)

        If listUsuarios Is Nothing Then
            SwalUtils.ShowSwalError(Me, "Atención", "No se puede eliminar el Rol seleccionado debido a que se no se pudo verificar si existen usuarios con este Rol asignado.")
            Return
        End If

        If listUsuarios.Count > 0 Then
            SwalUtils.ShowSwalError(Me, "Atención", "No se puede eliminar el Rol debido a que se encontraron múltiples usuarios que tienen habilitado este rol.")
            Return
        End If


        If (objRol.EliminaRol(rolSeleccionado, usuarioEliminacion, errorMessage)) Then
            SwalUtils.ShowSwal(Me, $"¡Rol eliminado exitosamente!", "El rol ya no se encuentra en el sistema.")
            LimpiarCampos()
        Else
            SwalUtils.ShowSwalError(Me, "Atención", $"Ha ocurrido un problema a la hora de eliminar el rol. [{errorMessage}]")
        End If

    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        LimpiarCampos()
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
            ddlRolesCreados.Items.Clear()
            ddlRolesCreados.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modRol As Models.Rol In listRoles
                ddlRolesCreados.Items.Add(New ListItem(modRol.Descripcion.ToString(), modRol.IdRol))
            Next

            ddlRolesCreados.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron roles en el sistema. Revise la configuración.")
        End If
    End Sub

    Private Sub LimpiarCampos()
        txtNombreRol.Text = String.Empty
        ocultarBotonesRoles()
        prcLlena_ddlRoles()
        hdfRolSeleccionado.Value = 0
        contenedorSectionConfig.Style.Add("display", "none")
    End Sub

    Protected Sub ddlRolesCreados_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim rolSeleccionado As String = ddlRolesCreados.SelectedValue

        If Not String.IsNullOrEmpty(rolSeleccionado) Then
            hdfRolSeleccionado.Value = Convert.ToInt32(ddlRolesCreados.SelectedValue)
            Dim idRolSeleccionado As Integer = Convert.ToInt32(ddlRolesCreados.SelectedValue)
            Dim errorMessage As String = ""

            Dim modRol As New Models.Rol()
            Dim objRolDB As New RolDB()

            modRol = objRolDB.ConsultarRoles_x_ID(idRolSeleccionado, errorMessage)

            If modRol Is Nothing Then
                SwalUtils.ShowSwalError(Me, $"No se logró encontrar información sobre el rol seleccionado. [{errorMessage}]")
                Return
            End If

            txtNombreRol.Text = modRol.Descripcion
            mostrarBotonesRolSeleccionado()
            contenedorSectionConfig.Style.Add("display", "grid")
        Else
            LimpiarCampos()
        End If
    End Sub

    Private Sub mostrarBotonesRolSeleccionado()
        btnAgregar.CssClass = "boton boton__guardar boton__ocultar"
        btnModificar.CssClass = "boton boton__modificar"
        btnEliminar.CssClass = "boton boton__eliminar"
    End Sub

    Private Sub ocultarBotonesRoles()
        btnAgregar.CssClass = "boton boton__guardar boton__ocultar"
        btnModificar.CssClass = "boton boton__modificar boton__ocultar"
        btnEliminar.CssClass = "boton boton__eliminar boton__ocultar"
    End Sub

    Private Sub mostrarBotonesRolNuevo()
        btnAgregar.CssClass = "boton boton__guardar"
        btnModificar.CssClass = "boton boton__modificar boton__ocultar"
        btnEliminar.CssClass = "boton boton__eliminar boton__ocultar"
    End Sub

    Private Function ValidarDatos(nombreRol As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True
        contenedorMensajesNombreRolNuevo.InnerHtml = String.Empty

        modRegla = objListaReglas.ObtenerReglaPorCampo("nombreProveedor")
        If modRegla IsNot Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, nombreRol) Then
                contenedorMensajesNombreRolNuevo.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesNombreRolNuevo.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesNombreRolNuevo.Style.Remove("display")
            End If
        End If

        Return respuestaValidacion

    End Function
End Class