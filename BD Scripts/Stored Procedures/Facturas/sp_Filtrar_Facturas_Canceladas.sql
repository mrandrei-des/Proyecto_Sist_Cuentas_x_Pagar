-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A LAS FACTURAS CANCELADAS EN GENERAL O POR PROVEEDOR
-- sp_Filtrar_Facturas_Canceladas 0
CREATE PROC sp_Filtrar_Facturas_Canceladas
(
@FiltID_Proveedor int,
@FiltFechaEmisionDesde date,
@FiltFechaEmisionHasta date
)
AS
BEGIN
	SELECT f.ID_Proveedor as 'IdProveedor', f.TipoFactura,f.NumeroFactura, f.Observacion, f.FechaEmision,
	f.Estado, f.Moneda, f.TipoCambio, f.Total, f.SaldoActual
	FROM Facturas f
	WHERE 1 = 1 AND f.Estado = 7 
	AND (@FiltID_Proveedor IS NULL OR f.ID_Proveedor = @FiltID_Proveedor)
	AND (@FiltFechaEmisionDesde IS NULL OR f.FechaEmision <= @FiltFechaEmisionDesde)
	AND (@FiltFechaEmisionHasta IS NULL OR f.FechaEmision >= @FiltFechaEmisionHasta)
	ORDER BY f.ID_Proveedor ASC, f.FechaEmision ASC
END