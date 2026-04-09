-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A LOS DOCUMENTOS DE FORMA DE PAGO APLICADOS EN GENERAL O POR PROVEEDOR
-- EXEC sp_Filtrar_DocumentosFormasPago_Aplicadas NULL, 'COL', NULL, '2026-03-01'
ALTER PROC sp_Filtrar_DocumentosFormasPago_Aplicadas
(
@FiltTipoDocumento int,
@FiltMoneda varchar(3),
@FiltFechaInicio date,
@FiltFechaFin date
)
AS
BEGIN
	--SELECT d.ID_Proveedor as 'IdProveedor', d.TipoDocumento,d.NumeroDocumento, d.Observacion, d.FechaEmision,
	--d.Estado, d.Moneda, d.TipoCambio, d.Total, d.SaldoActual
	--FROM Documentos_Formas_Pago d
	--WHERE 1 = 1 AND d.Estado = 2 
	--AND (@FiltID_Proveedor IS NULL OR d.ID_Proveedor = @FiltID_Proveedor)
	--AND (@FiltFechaEmisionDesde IS NULL OR d.FechaEmision <= @FiltFechaEmisionDesde)
	--AND (@FiltFechaEmisionHasta IS NULL OR d.FechaEmision >= @FiltFechaEmisionHasta)
	--ORDER BY d.ID_Proveedor ASC, d.FechaEmision ASC
	
	SELECT c.ID_Categoria as idCategoria, d.ID_Proveedor as idProveedor, d.TipoDocumento as TipoDoc, d.NumeroDocumento as NumDoc, m.Simbolo, d.SaldoActual as Monto, p.Nombre as NombreProveedor, d.FechaEmision
	FROM Documentos_Formas_Pago d 
	JOIN TipoDocumentos c on d.TipoDocumento = c.ID_TipoDocumento
	JOIN Proveedores p on d.ID_Proveedor = p.ID_Proveedor
	JOIN Monedas m on d.Moneda = m.CodigoMoneda
	WHERE 1 = 1 AND d.Estado = 2
	AND (@FiltTipoDocumento IS NULL OR d.TipoDocumento = @FiltTipoDocumento)
	AND (@FiltMoneda IS NULL OR d.Moneda = @FiltMoneda)
	AND (@FiltFechaInicio IS NULL OR d.FechaEmision >= @FiltFechaInicio)
	AND (@FiltFechaFin IS NULL OR d.FechaEmision <= @FiltFechaFin)
	-- AND d.FechaEmision >= '2026-03-04' -- FECHA INICIO, DE LA FECHA HACIA EL PRESENTE
	-- AND d.FechaEmision <= '2026-03-05' -- FECHA FIN, DE LA FECHA HACIA ATRÁS
	ORDER BY d.FechaEmision ASC
END
