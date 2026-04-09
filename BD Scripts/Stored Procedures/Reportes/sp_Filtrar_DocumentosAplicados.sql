-- EXEC sp_Filtrar_DocumentosAplicados NULL, NULL, NULL
ALTER PROC sp_Filtrar_DocumentosAplicados
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
	
	
	SELECT c.ID_Categoria as idCategoria, f.ID_Proveedor as idProveedor, f.TipoFactura as TipoDoc, f.NumeroFactura as NumDoc, m.Simbolo, f.SaldoActual as Monto, p.Nombre as NombreProveedor, f.FechaEmision
	FROM Facturas f 
	JOIN TipoDocumentos c on f.TipoFactura = c.ID_TipoDocumento
	JOIN Proveedores p on f.ID_Proveedor = p.ID_Proveedor
	JOIN Monedas m on f.Moneda = m.CodigoMoneda
	WHERE 1 = 1 AND f.Estado = 2
	AND (@FiltMoneda IS NULL OR f.Moneda = @FiltMoneda)
	AND (@FiltFechaInicio IS NULL OR f.FechaEmision >= @FiltFechaInicio)
	AND (@FiltFechaFin IS NULL OR f.FechaEmision <= @FiltFechaFin)
		UNION
	SELECT c.ID_Categoria as idCategoria, d.ID_Proveedor as idProveedor, d.TipoDocumento as TipoDoc, d.NumeroDocumento as NumDoc, m.Simbolo, d.SaldoActual as Monto, p.Nombre as NombreProveedor, d.FechaEmision
	FROM Documentos_Formas_Pago d 
	JOIN TipoDocumentos c on d.TipoDocumento = c.ID_TipoDocumento
	JOIN Proveedores p on d.ID_Proveedor = p.ID_Proveedor
	JOIN Monedas m on d.Moneda = m.CodigoMoneda
	WHERE 1 = 1 AND d.Estado = 2
	AND (@FiltMoneda IS NULL OR d.Moneda = @FiltMoneda)
	AND (@FiltFechaInicio IS NULL OR d.FechaEmision >= @FiltFechaInicio)
	AND (@FiltFechaFin IS NULL OR d.FechaEmision <= @FiltFechaFin)
	ORDER BY FechaEmision ASC

END