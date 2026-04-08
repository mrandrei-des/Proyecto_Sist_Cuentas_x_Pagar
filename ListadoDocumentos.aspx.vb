Imports Proyecto_Sist_Cuentas_x_Pagar.Models
Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class ListadoDocumentos
    Inherits System.Web.UI.Page

    Dim montoDocsUSD As Double, montoDocsCRC As Double, montoFactUSD As Double, montoFactCRC As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim enlace As HtmlAnchor = Master.FindControl("enlaceRegistroDocumentos")
        enlace.Style.Add("background-color", "var(--colorLetraOscuroSecundario)")
        If Not IsPostBack Then
            ' Cargar select u otros elementos
            prcLlena__ddlTipoDocumentos()
            prcLlena__ddlMonedas()
        End If

        MostrarTotales_Facturas()
        MostrarTotales_Documentos()
        MostrarMontosBalances()
    End Sub

    Private Sub MostrarTotales_Facturas()

        Dim listaMontosFacturas As New List(Of Models.MontosTotales)
        Dim objHerramientas As New Herramientas

        listaMontosFacturas = ConsultaMontosTotales_Facturas()

        If listaMontosFacturas Is Nothing Then
            SwalUtils.ShowSwalError(Me, "Atención", "No se encontró información acerca de los montos totales de las facturas.")
            Return
        End If

        For Each monto As Models.MontosTotales In listaMontosFacturas
            If monto.Moneda = "DOL" Then
                totalFactUSD.InnerText = $"{monto.Simbolo}{objHerramientas.FormatearMonto(monto.Monto)}"
                montoFactUSD = monto.Monto
            Else
                totalFactCRC.InnerText = $"{monto.Simbolo}{objHerramientas.FormatearMonto(monto.Monto)}"
                montoFactCRC = monto.Monto
            End If
        Next

    End Sub

    Private Sub MostrarTotales_Documentos()

        Dim listaMontosDocumentos As New List(Of Models.MontosTotales)
        Dim objHerramientas As New Herramientas
        listaMontosDocumentos = ConsultaMontosTotales_Documentos()

        If listaMontosDocumentos Is Nothing Then
            SwalUtils.ShowSwalError(Me, "Atención", "No se encontró información acerca de los montos totales de los documentos de pago.")
            Return
        End If

        For Each monto As Models.MontosTotales In listaMontosDocumentos
            If monto.Moneda = "DOL" Then
                totalDocUSD.InnerText = $"{monto.Simbolo}{objHerramientas.FormatearMonto(monto.Monto)}"
                montoDocsUSD = monto.Monto
            Else
                totalDocCRC.InnerText = $"{monto.Simbolo}{objHerramientas.FormatearMonto(monto.Monto)}"
                montoDocsCRC = monto.Monto
            End If
        Next

    End Sub

    Private Function ConsultaMontosTotales_Facturas() As List(Of Models.MontosTotales)
        Dim errorMessage As String = ""
        Dim objMontosDB As New MontosTotalesDB

        Return objMontosDB.ConsultarMontosTotales_Facturas(errorMessage)
    End Function

    Private Function ConsultaMontosTotales_Documentos() As List(Of Models.MontosTotales)
        Dim errorMessage As String = ""
        Dim objMontosDB As New MontosTotalesDB

        Return objMontosDB.ConsultarMontosTotales_Documentos(errorMessage)
    End Function

    Private Sub MostrarMontosBalances()
        Dim objHerramientas As New Herramientas
        Dim saldoGeneral As Double = 0

        saldoGeneral = montoFactUSD - montoDocsUSD
        balanceFactUSD.InnerText = $"${objHerramientas.FormatearMonto(montoFactUSD)}"
        balanceDocsUSD.InnerText = $"${objHerramientas.FormatearMonto(montoDocsUSD)}"
        balanceSaldoUSD.InnerText = $"${objHerramientas.FormatearMonto(saldoGeneral)}"
        If saldoGeneral > 0 Then
            balanceTextoSaldoUSD.Style.Add("color", "var(--colorLetraOscuroSecundario)")
            balanceSaldoUSD.Style.Add("color", "var(--colorLetraOscuroSecundario)")
        ElseIf saldoGeneral < 0 Then
            balanceTextoSaldoUSD.Style.Add("color", "var(--colorPrincipal)")
            balanceSaldoUSD.Style.Add("color", "var(--colorPrincipal)")
        Else
            balanceTextoSaldoUSD.Style.Add("color", "#9C9A92")
            balanceSaldoUSD.Style.Add("color", "#9C9A92")
        End If

        saldoGeneral = 0

        saldoGeneral = montoFactCRC - montoDocsCRC
        balanceFactCRC.InnerText = $"¢{objHerramientas.FormatearMonto(montoFactCRC)}"
        balanceDocsCRC.InnerText = $"¢{objHerramientas.FormatearMonto(montoDocsCRC)}"
        balanceSaldoCRC.InnerText = $"¢{objHerramientas.FormatearMonto(saldoGeneral)}"
        If saldoGeneral > 0 Then
            balanceTextoSaldoCRC.Style.Add("color", "var(--colorLetraOscuroSecundario)")
            balanceSaldoCRC.Style.Add("color", "var(--colorLetraOscuroSecundario)")
        ElseIf saldoGeneral < 0 Then
            balanceTextoSaldoCRC.Style.Add("color", "var(--colorPrincipal)")
            balanceSaldoCRC.Style.Add("color", "var(--colorPrincipal)")
        Else
            balanceTextoSaldoCRC.Style.Add("color", "#9C9A92")
            balanceSaldoCRC.Style.Add("color", "#9C9A92")
        End If

    End Sub

    Private Sub prcLlena__ddlTipoDocumentos()
        Dim listTipoDocumento As List(Of Models.TipoDocumento)
        Dim objTipoDocumentosDB As New TipoDocumentoDB
        Dim errorMessage As String = ""

        listTipoDocumento = objTipoDocumentosDB.ConsultarTipoDocumento_Todos(errorMessage)

        If listTipoDocumento Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listTipoDocumento.Count > 0 Then
            ddlTipoDocumento.Items.Clear()

            ddlTipoDocumento.Items.Add(New ListItem("Seleccione una opción", ""))

            For Each modTipoDocumento As Models.TipoDocumento In listTipoDocumento
                ddlTipoDocumento.Items.Add(New ListItem(modTipoDocumento.Descripcion.ToString(), modTipoDocumento.IdTipoDocumento))
            Next

            ddlTipoDocumento.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron tipos de documento en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

    Private Sub prcLlena__ddlMonedas()
        Dim listMonedas As List(Of Models.Moneda)
        Dim objMonedaDB As New MonedaDB
        Dim errorMessage As String = ""

        listMonedas = objMonedaDB.ConsultarMonedas(errorMessage)

        If listMonedas Is Nothing Then
            SwalUtils.ShowSwalError(Me, errorMessage)
            Return
        End If

        If listMonedas.Count > 0 Then
            ddlMonedas.Items.Clear()

            ddlMonedas.Items.Add(New ListItem("Seleccione una opción", ""))
            For Each modMoneda As Models.Moneda In listMonedas
                Dim textoMoneda As String = $"{modMoneda.Descripcion}"
                ddlMonedas.Items.Add(New ListItem(textoMoneda, modMoneda.CodigoMoneda.ToString()))
            Next

            ddlMonedas.SelectedIndex = 0
        Else
            SwalUtils.ShowSwalError(Me, "No se encontraron monedas en el sistema. Revise la configuración correspondiente.")
        End If
    End Sub

End Class