ALTER PROC sp_Aplicar_DocumentoFormaPago
(
@ID_Proveedor int, 
@TipoDocumento int, 
@NumeroDocumento varchar(10),
@UsuarioAplico varchar(25)
)
AS
BEGIN
	UPDATE Documentos_Formas_Pago
	SET Estado = 2	
	WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento and NumeroDocumento = @NumeroDocumento

	-- INSERTAR UN REGISTRO DE BITÁCORA DEL UPDATE AL DOCUMENTO DE FORMA DE PAGO
	-- ID_Accion = 2 es modificado. Cualquier duda revisar tabla TipoAcciones
	EXECUTE sp_Inserta_Registro_Bitacora_Cambio_DocumentosFormasPago 7, @ID_Proveedor, @TipoDocumento, @NumeroDocumento,  'El documento de forma de pago ha sido aplicado.', @UsuarioAplico
END
