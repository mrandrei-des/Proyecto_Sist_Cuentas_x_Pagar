-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A LOS DOCUMENTOS DE FORMA DE PAGO QUE NO TENGAN SALDO ACTUAL EN GENERAL O POR PROVEEDOR
-- sp_Filtrar_DocumentosFormasPago_SinSaldo 0
CREATE PROC sp_Filtrar_DocumentosFormasPago_SinSaldo
(
@FiltID_Proveedor int,
@FiltFechaEmisionDesde date,
@FiltFechaEmisionHasta date
)
AS
BEGIN
	SELECT d.ID_Proveedor as 'IdProveedor', d.TipoDocumento,d.NumeroDocumento, d.Observacion, d.FechaEmision,
	d.Estado, d.Moneda, d.TipoCambio, d.Total, d.SaldoActual
	FROM Documentos_Formas_Pago d
	WHERE 1 = 1 AND d.Estado = 7
	AND (@FiltID_Proveedor IS NULL OR d.ID_Proveedor = @FiltID_Proveedor)
	AND (@FiltFechaEmisionDesde IS NULL OR d.FechaEmision <= @FiltFechaEmisionDesde)
	AND (@FiltFechaEmisionHasta IS NULL OR d.FechaEmision >= @FiltFechaEmisionHasta)
	ORDER BY d.ID_Proveedor ASC, d.FechaEmision ASC
END