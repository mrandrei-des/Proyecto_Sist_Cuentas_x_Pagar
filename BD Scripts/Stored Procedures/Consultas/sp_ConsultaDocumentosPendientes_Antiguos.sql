-- PROCEDIMIENTO ALMACENADO QUE CONSULTA POR LOS DOCUMENTOS PENDIENTES Y LOS ORDENA DE MÁS ANTIGUOS A MÁS RECIENTES
-- EXEC sp_ConsultaDocumentosPendientes_Antiguos
ALTER PROC sp_ConsultaDocumentosPendientes_Antiguos
AS
BEGIN
	WITH cte_documentosPendientes AS (
		SELECT ID_PROVEEDOR as idProveedor,  TipoFactura as TipoDoc, NumeroFactura as NumDoc, Moneda, Total, FechaCreacion
		FROM Facturas
		WHERE Estado = 1
			UNION
		SELECT ID_PROVEEDOR as idProveedor,  TipoDocumento as TipoDoc, NumeroDocumento as NumDoc, Moneda, Total, FechaCreacion
		FROM Documentos_Formas_Pago
		WHERE Estado = 1
	)

	SELECT b.Nombre as NombreProveedor, c.ID_Categoria as idCategoria, a.NumDoc, d.Simbolo, a.Total, 
	DATEDIFF(DAY, a.FechaCreacion, GETDATE()) AS CANTDIAS
	FROM cte_documentosPendientes a
	JOIN Proveedores b on a.idProveedor = b.ID_Proveedor
	JOIN TipoDocumentos c on a.TipoDoc = c.ID_TipoDocumento
	JOIN Monedas d on a.Moneda = d.CodigoMoneda
	ORDER BY a.FechaCreacion ASC
END
