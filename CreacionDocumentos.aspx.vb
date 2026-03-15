Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class CreacionDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Busca al elemento HTML que se le indique y se le dan estilos de línea
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceRegistroDocumentos")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
        If Not IsPostBack Then
            prcLlena__ddlCategoria()
            Dim idCategoria As Integer = Convert.ToInt32(ddlCategoria.SelectedValue)
            prcLlena__ddlTipoDocumento(idCategoria)
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub prcLlena__ddlCategoria()
        Dim listCatDocumentos As List(Of Models.CategoriaDocumento)
        Dim objCatDocumentosDB As New CategoriaDocumentoDB
        Dim errorMessage As String = ""

        listCatDocumentos = objCatDocumentosDB.ConsultarCategoriaDocumento(errorMessage)
        If listCatDocumentos.Count > 0 Then
            ddlCategoria.Items.Clear()

            For Each modCatDocumento As Models.CategoriaDocumento In listCatDocumentos
                ddlCategoria.Items.Add(New ListItem(modCatDocumento.Descripcion.ToString(), modCatDocumento.IdCategoria))
            Next

            ddlCategoria.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub prcLlena__ddlTipoDocumento(idCategoria As Integer)

        Dim listTipoDocumento As List(Of Models.TipoDocumento)
        Dim objTipoDocumentosDB As New TipoDocumentoDB
        Dim errorMessage As String = ""

        listTipoDocumento = objTipoDocumentosDB.ConsultarTipoDocumento(idCategoria, errorMessage)
        If listTipoDocumento.Count > 0 Then
            ddlTipoDocumento.Items.Clear()

            For Each modTipoDocumento As Models.TipoDocumento In listTipoDocumento
                ddlTipoDocumento.Items.Add(New ListItem(modTipoDocumento.Descripcion.ToString(), modTipoDocumento.IdCategoria))
            Next

            ddlTipoDocumento.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If

    End Sub

    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim idCategoria As Integer = Convert.ToInt32(ddlCategoria.SelectedValue)
        prcLlena__ddlTipoDocumento(idCategoria)

        ' Falta cambiar el label del número de documento dependiendo de la categoría seleccionada, por ejemplo, si es factura, que diga "Número de Factura", si es otro tipo de documento, que diga "Número de Documento"
        'If idCategoria = 1 Then
        '    lblNumeroDocumento.Text = "Número de Factura"
        'Else
        '    lblNumeroDocumento.Text = "Número de Documento"
        'End If
    End Sub
End Class