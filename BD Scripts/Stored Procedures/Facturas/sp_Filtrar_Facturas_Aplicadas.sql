-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A LAS FACTURAS APLICADAS EN GENERAL O POR PROVEEDOR
-- EXEC sp_Filtrar_Facturas_Aplicadas 1, NULL, NULL, NULL
alter PROC sp_Filtrar_Facturas_Aplicadas
( 
@FiltTipoDocumento int,
@FiltMoneda varchar(3),
@FiltFechaInicio date,
@FiltFechaFin date
)
AS
BEGIN
	--SELECT f.ID_Proveedor as 'IdProveedor', f.TipoFactura,f.NumeroFactura, f.Observacion, f.FechaEmision,
	--f.Estado, f.Moneda, f.TipoCambio, f.Total, f.SaldoActual
	--FROM Facturas f
	--WHERE 1 = 1 AND f.Estado = 2 
	--AND (@FiltID_Proveedor IS NULL OR f.ID_Proveedor = @FiltID_Proveedor)
	--AND (@FiltFechaEmisionDesde IS NULL OR f.FechaEmision <= @FiltFechaEmisionDesde)
	--AND (@FiltFechaEmisionHasta IS NULL OR f.FechaEmision >= @FiltFechaEmisionHasta)
	--ORDER BY f.ID_Proveedor ASC, f.FechaEmision ASC

	SELECT c.ID_Categoria as idCategoria, f.ID_Proveedor as idProveedor, f.TipoFactura as TipoDoc, f.NumeroFactura as NumDoc, m.Simbolo, f.SaldoActual as Monto, p.Nombre as NombreProveedor, f.FechaEmision
	FROM Facturas f 
	JOIN TipoDocumentos c on f.TipoFactura = c.ID_TipoDocumento
	JOIN Proveedores p on f.ID_Proveedor = p.ID_Proveedor
	JOIN Monedas m on f.Moneda = m.CodigoMoneda
	WHERE 1 = 1 AND f.Estado = 2
	AND (@FiltTipoDocumento IS NULL OR f.TipoFactura = @FiltTipoDocumento)
	AND (@FiltMoneda IS NULL OR f.Moneda = @FiltMoneda)
	AND (@FiltFechaInicio IS NULL OR f.FechaEmision >= @FiltFechaInicio)
	AND (@FiltFechaFin IS NULL OR f.FechaEmision <= @FiltFechaFin)
	-- AND f.FechaEmision >= '2026-03-04' -- FECHA INICIO, DE LA FECHA HACIA EL PRESENTE
	-- AND f.FechaEmision <= '2026-03-05' -- FECHA FIN, DE LA FECHA HACIA ATRÁS
	ORDER BY f.FechaEmision ASC
END