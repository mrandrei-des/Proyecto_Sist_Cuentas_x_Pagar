CREATE PROC sp_Aplicar_Factura
(
@ID_Proveedor int, 
@TipoFactura int, 
@NumeroFactura varchar(10),
@UsuarioAplico varchar(25)
)
AS
BEGIN
	UPDATE Facturas
	SET Estado = 2	
	WHERE ID_Proveedor = @ID_Proveedor and	TipoFactura = @TipoFactura and NumeroFactura = @NumeroFactura

	-- INSERTAR UN REGISTRO DE BITÁCORA DEL UPDATE AL DOCUMENTO DE FORMA DE PAGO
	-- ID_Accion = 2 es modificado. Cualquier duda revisar tabla TipoAcciones
	EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Facturas 2, @ID_Proveedor, @TipoFactura, @NumeroFactura,  'La factura ha sido aplicada.', @UsuarioAplico
END