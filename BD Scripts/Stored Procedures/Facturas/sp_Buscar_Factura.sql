-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A LA FACTURA INDICADA QUE SE ENCUENTRE EN UN ESTADO DIFERENTE A ELIMINADO
-- sp_Buscar_Factura 0
Alter PROC sp_Buscar_Factura
(
@ID_Proveedor int, 
@TipoFactura int,
@NumeroFactura varchar(10)
)
AS
BEGIN
	SELECT f.ID_Proveedor as 'IdProveedor', f.TipoFactura,f.NumeroFactura, f.Observacion, f.FechaEmision,
	f.Estado, f.Moneda, m.Simbolo, f.TipoCambio, f.Total, f.SaldoActual
	FROM Facturas f
	JOIN Monedas m on f.Moneda = m.CodigoMoneda
	WHERE f.ID_Proveedor = @ID_Proveedor and f.TipoFactura = @TipoFactura and f.NumeroFactura = @NumeroFactura and f.Estado <> 6 
END
