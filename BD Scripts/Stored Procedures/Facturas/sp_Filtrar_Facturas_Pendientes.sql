-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A LAS FACTURAS PENDIENTES DE APLICAR EN GENERAL O POR PROVEEDOR
-- sp_Filtrar_Facturas_Pendientes 0
alter PROC sp_Filtrar_Facturas_Pendientes
(
@FiltID_Proveedor int,
@FiltFechaEmisionDesde date,
@FiltFechaEmisionHasta date,
@OrderByCondition varchar(10)
)
AS
BEGIN
	SELECT f.ID_Proveedor as 'IdProveedor', p.Nombre as NombreProveedor, f.TipoFactura,f.NumeroFactura, 
	f.Observacion, f.FechaEmision, FORMAT(f.FechaEmision, 'dd/MM/yyyy') as FechaFormateada,
	f.Estado, f.Moneda, m.Simbolo as SimboloMoneda, f.TipoCambio, f.Total, f.SaldoActual
	FROM Facturas f
	JOIN Proveedores p ON f.ID_Proveedor = p.ID_Proveedor
	JOIN Monedas m ON f.Moneda = m.CodigoMoneda
	WHERE 1 = 1 AND f.Estado = 1 
	AND (@FiltID_Proveedor IS NULL OR f.ID_Proveedor = @FiltID_Proveedor)
	AND (@FiltFechaEmisionDesde IS NULL OR f.FechaEmision <= @FiltFechaEmisionDesde)
	AND (@FiltFechaEmisionHasta IS NULL OR f.FechaEmision >= @FiltFechaEmisionHasta)
	ORDER BY 
		CASE @OrderByCondition 
			WHEN 'TODAS' THEN f.ID_Proveedor END ASC,
		CASE @OrderByCondition
			WHEN 'ANTIGUAS' THEN f.FechaEmision END ASC,
		CASE @OrderByCondition
			WHEN 'RECIENTES' THEN f.FechaEmision END DESC
END

-- exec sp_Filtrar_Facturas_Pendientes NULL, NULL, NULL