-- PROCEDIMIENTO ALMACENADO ELIMINACIÓN DE DOCUMENTOS DE FORMA DE PAGO
-- EXEC sp_Eliminar_DocumentoFormaPago 0, ''
CREATE PROC sp_Eliminar_DocumentoFormaPago
(
@ID_Proveedor int, 
@TipoDocumento int,
@NumeroDocumento varchar(10),
@UsuarioElimino varchar(25)
)
AS
BEGIN
	/* HACE UNA ELIMINACIÓN LÓGICA DEL DOCUMENTO */
	BEGIN
		UPDATE Documentos_Formas_Pago
		SET Estado = 6
		WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento and NumeroDocumento = @NumeroDocumento
		-- Estado = 6 quiere decir eliminado. Cualquier duda revisar tabla Estados
	END

	BEGIN
		-- INSERTAR UN REGISTRO DE BITÁCORA DE LA ELIMINACIÓN DEL DOCUMENTO
		-- ID_Accion = 3 es eliminado. Cualquier duda revisar tabla TipoAcciones
		EXECUTE sp_Inserta_Registro_Bitacora_Cambio_DocumentosFormasPago 3, @ID_Proveedor, @TipoDocumento, @NumeroDocumento,  'El documento ha sido eliminado.', @UsuarioElimino
	END
END