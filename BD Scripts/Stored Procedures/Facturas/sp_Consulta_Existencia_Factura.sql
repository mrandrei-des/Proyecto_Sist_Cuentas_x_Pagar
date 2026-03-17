-- PROCEDIMIENTO ALMACENADO QUE BUSCA A LA FACTURA INDICADA INDEPENDIENTEMENTE DE SU ESTADO
-- sp_Buscar_Factura 0, 0, ''
CREATE PROC sp_Consulta_Existencia_Factura
(
@ID_Proveedor int, 
@TipoFactura int,
@NumeroFactura varchar(10)
)
AS
BEGIN
	SELECT f.ID_Proveedor as 'IdProveedor', f.TipoFactura,f.NumeroFactura, f.Observacion, f.FechaEmision,
	f.Estado, f.Moneda, f.TipoCambio, f.Total, f.SaldoActual
	FROM Facturas f
	WHERE f.ID_Proveedor = @ID_Proveedor and f.TipoFactura = @TipoFactura and f.NumeroFactura = @NumeroFactura
END