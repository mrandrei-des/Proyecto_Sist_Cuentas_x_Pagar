Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class MantenimientoProveedor
    Inherits System.Web.UI.Page

    Private Const IDENTIFICADOR As String = "CREAR_PROVEEDORES"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not UsuarioPuedeContinuar() Then
            Session.Clear()
            Response.Redirect("Login.aspx", False)
        End If

        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceProveedores")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")

        If Not IsPostBack Then
            prcLlena_ddlTipoIdentificacion()
            prcLlena_ddlEstado()
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

    Private Sub LimpiarCampos()
        ddlTipoIdentificacion.SelectedIndex = 0
        txtIdentificacion.Text = ""
        txtNombre.Text = ""
        txtCorreo.Text = ""
        ddlEstado.SelectedIndex = 0
    End Sub

    Private Sub prcLlena_ddlEstado()
        Dim listEstados As List(Of Models.Estado)
        Dim objEstadoDB As New EstadoDB
        Dim errorMessage As String = ""

        listEstados = objEstadoDB.ConsultarEstados(errorMessage)

        If listEstados Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listEstados.Count > 0 Then
            ddlEstado.Items.Clear()
            ddlEstado.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modEstado As Models.Estado In listEstados
                ddlEstado.Items.Add(New ListItem(modEstado.Descripcion.ToString(), modEstado.IdEstado))
            Next

            ddlEstado.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron estados en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcLlena_ddlTipoIdentificacion()
        Dim listTipoIdentificaciones As List(Of Models.TipoIdentificaciones)
        Dim objTipoIdentificacionBD As New TipoIdentificacionesDB
        Dim errorMessage As String = ""

        listTipoIdentificaciones = objTipoIdentificacionBD.ConsultarTipoIdentificaciones(errorMessage)

        If listTipoIdentificaciones Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listTipoIdentificaciones.Count > 0 Then
            ddlTipoIdentificacion.Items.Clear()
            ddlTipoIdentificacion.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modTipoIdentificacion As Models.TipoIdentificaciones In listTipoIdentificaciones
                ddlTipoIdentificacion.Items.Add(New ListItem(modTipoIdentificacion.Descripcion.ToString(), modTipoIdentificacion.IdTipo))
            Next

            ddlTipoIdentificacion.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron tipos de identificación en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim modProveedor As New Models.Proveedor
        Dim objProveedorDB As New ProveedorDB
        Dim errorMessage As String = "", usuarioCreacion As String = Session("UsuarioLoggeado")
        Dim tipoIdentificacion As String = ddlTipoIdentificacion.SelectedItem.Value
        Dim numeroIdentificacion As String = txtIdentificacion.Text, nombre As String = txtNombre.Text, correo As String = txtCorreo.Text, estado As String = ddlEstado.SelectedItem.Value

        If (Not ValidarDatos(tipoIdentificacion, numeroIdentificacion, nombre, correo, estado)) Then
            SwalUtils.ShowSwalError(Me, "Atención", "Los datos ingresados no son válidos. Por favor revisar")
            Return
        End If

        modProveedor = objProveedorDB.ConsultarExistenciaProveedor(tipoIdentificacion, numeroIdentificacion, errorMessage)

        If modProveedor Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If modProveedor.NumeroIdentificacion Is Nothing Then
            modProveedor = New Models.Proveedor With {
                .TipoIdentificacion = CInt(tipoIdentificacion),
                .NumeroIdentificacion = numeroIdentificacion,
                .Nombre = nombre,
                .Correo = correo,
                .Estado = CInt(estado)
            }

            If objProveedorDB.CrearProveedor(modProveedor, usuarioCreacion, errorMessage) Then
                SwalUtils.ShowSwal(Me, "¡Proveedor creado exitosamente!")
                LimpiarCampos()
            Else
                SwalUtils.ShowSwalError(Me, errorMessage)
            End If
        Else
            SwalUtils.ShowSwalError(Me, "Ya existe un proveedor con ese tipo y número de identificación.")
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        LimpiarCampos()
    End Sub

    Private Function ValidarDatos(tipoIdentificacion As String, numeroIdentificacion As String, nombre As String, correo As String, estado As String) As Boolean
        Dim objHerramienta As New Herramientas
        Dim objListaReglas As New ListaReglas()
        Dim modRegla As ValidacionRegex
        Dim respuestaValidacion As Boolean = True

        If Not objHerramienta.ValidarNumeroEntero(tipoIdentificacion, False) Then
            contenedorMensajesTipoIdentificacion.InnerHtml = $"<p class='formulario__mensaje'>Debe seleccionar una opción de tipo identificación.</p>"
            contenedorMensajesTipoIdentificacion.Style.Add("display", "block")
            respuestaValidacion = False
        Else
            contenedorMensajesTipoIdentificacion.Style.Remove("display")
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("identificacion")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, numeroIdentificacion) Then
                contenedorMensajesIdentificacion.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesIdentificacion.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesIdentificacion.Style.Remove("display")
            End If
        End If

        modRegla = objListaReglas.ObtenerReglaPorCampo("nombreProveedor")
        If Not modRegla Is Nothing Then
            If Not objListaReglas.ValidarCampo(modRegla, nombre) Then
                contenedorMensajesNombre.InnerHtml = $"<p class='formulario__mensaje'>{modRegla.Mensaje}</p>"
                contenedorMensajesNombre.Style.Add("display", "block")
                respuestaValidacion = False
            Else
                contenedorMensajesNombre.Style.Remove("display")
            End If
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

        Return respuestaValidacion
    End Function
End Class