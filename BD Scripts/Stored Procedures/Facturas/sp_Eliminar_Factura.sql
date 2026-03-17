-- PROCEDIMIENTO ALMACENADO ELIMINACIÓN DE FACTURAS
-- EXEC sp_Eliminar_Proveedor 0, ''
CREATE PROC sp_Eliminar_Factura
(
@ID_Proveedor int, 
@TipoFactura int,
@NumeroFactura varchar(10),
@UsuarioElimino varchar(25)
)
AS
BEGIN
	/* HACE UNA ELIMINACIÓN LÓGICA DE LA FACTURA */
	BEGIN
		UPDATE Facturas
		SET Estado = 6
		WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura and NumeroFactura = @NumeroFactura
		-- Estado = 6 quiere decir eliminado. Cualquier duda revisar tabla Estados
	END

	BEGIN
		-- INSERTAR UN REGISTRO DE BITÁCORA DE LA ELIMINACIÓN DE LA FACTURA
		-- ID_Accion = 3 es eliminado. Cualquier duda revisar tabla TipoAcciones
		EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Facturas 3, @ID_Proveedor, @TipoFactura, @NumeroFactura,  'La factura ha sido eliminada.', @UsuarioElimino
	END
END