Public Class MantRolesPermisos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceUsuarios")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            'Llenar combos
        End If
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs)
        btnAgregar.CssClass = "boton boton__guardar"
        btnModificar.CssClass = "boton boton__modificar"
        btnEliminar.CssClass = "boton boton__eliminar"
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim nombreRol As String = txtNombreRol.Text
        ' Agrega un nuevo rol
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs)
        Dim rolSeleccionado As String = hdfRolSeleccionado.Value
        Dim nombreRol As String = txtNombreRol.Text

        ' Modifica el rol seleccionado
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs)
        Dim rolSeleccionado As String = hdfRolSeleccionado.Value

        ' Elimina el rol seleccionado siempre y cuando no tenga usuarios asociados

    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        LimpiarCampos()
    End Sub

    Private Sub LimpiarCampos()
        txtNombreRol.Text = String.Empty
        btnAgregar.CssClass = "boton boton__guardar boton__ocultar"
        btnModificar.CssClass = "boton boton__modificar boton__ocultar"
        btnEliminar.CssClass = "boton boton__eliminar boton__ocultar"
    End Sub

    'Protected Sub btnGuardarCambios_Click(sender As Object, e As EventArgs)
    ' Este evento se ejecuta desde JavaScript
    'End Sub

    Protected Sub ddlRolesCreados_SelectedIndexChanged(sender As Object, e As EventArgs)
        hdfRolSeleccionado.Value = Integer.Parse(ddlRolesCreados.SelectedValue).ToString()
        Dim rolSeleccionado As String = ddlRolesCreados.SelectedValue

        ' Consultar el rol a la base de datos y llenar los campos correspondientes
    End Sub
End Class