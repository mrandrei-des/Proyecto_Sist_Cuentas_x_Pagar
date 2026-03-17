-- PROCEDIMIENTO ALMACENADO QUE CONSULTA AL DOCUMENTO DE FORMA DE PAGO INDICADO QUE SE ENCUENTRE EN UN ESTADO DIFERENTE A ELIMINADO
-- sp_Buscar_DocumentoFormaPago 0
CREATE PROC sp_Buscar_DocumentoFormaPago
(
@ID_Proveedor int, 
@TipoDocumento int,
@NumeroDocumento varchar(10)
)
AS
BEGIN
	SELECT d.ID_Proveedor as 'IdProveedor', d.TipoDocumento,d.NumeroDocumento, d.Observacion, d.FechaEmision,
	d.Estado, d.Moneda, d.TipoCambio, d.Total, d.SaldoActual
	FROM Documentos_Formas_Pago d
	WHERE d.ID_Proveedor = @ID_Proveedor and d.TipoDocumento = @TipoDocumento and d.NumeroDocumento = @NumeroDocumento and d.Estado <> 6 
END