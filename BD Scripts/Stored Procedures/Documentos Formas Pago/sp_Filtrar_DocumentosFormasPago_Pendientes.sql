-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A LOS DOCUMENTOS DE FORMA DE PAGO QUE NO HAYAN SIDO APLICADOS SINO, SOLO GUARDADOS EN GENERAL O POR PROVEEDOR
-- exec  sp_Filtrar_DocumentosFormasPago_Pendientes NULL, NULL, NULL, 'TODAS'
alter PROC sp_Filtrar_DocumentosFormasPago_Pendientes
(
@FiltID_Proveedor int,
@FiltFechaEmisionDesde date,
@FiltFechaEmisionHasta date,
@OrderByCondition varchar(10)
)
AS
BEGIN
	SELECT d.ID_Proveedor as 'IdProveedor', p.Nombre as NombreProveedor, d.TipoDocumento,
	d.NumeroDocumento, d.Observacion, d.FechaEmision, FORMAT(d.FechaEmision, 'dd/MM/yyyy') as FechaFormateada,
	d.Estado, d.Moneda, m.Simbolo as SimboloMoneda, d.TipoCambio, d.Total, d.SaldoActual
	FROM Documentos_Formas_Pago d
	JOIN Proveedores p ON d.ID_Proveedor = p.ID_Proveedor
	JOIN Monedas m ON d.Moneda = m.CodigoMoneda
	WHERE 1 = 1 AND d.Estado = 1
	AND (@FiltID_Proveedor IS NULL OR d.ID_Proveedor = @FiltID_Proveedor)
	AND (@FiltFechaEmisionDesde IS NULL OR d.FechaEmision <= @FiltFechaEmisionDesde)
	AND (@FiltFechaEmisionHasta IS NULL OR d.FechaEmision >= @FiltFechaEmisionHasta)
	ORDER BY 
		CASE @OrderByCondition 
			WHEN 'TODAS' THEN d.ID_Proveedor END ASC,
		CASE @OrderByCondition
			WHEN 'ANTIGUAS' THEN d.FechaEmision END ASC,
		CASE @OrderByCondition
			WHEN 'RECIENTES' THEN d.FechaEmision END DESC
END