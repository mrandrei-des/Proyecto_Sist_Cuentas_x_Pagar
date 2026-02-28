
-- PROCEDIMIENTO ALMACENADO ELIMINACIÓN DE PROVEEDORES
-- EXEC sp_Eliminar_Proveedor 0, ''
CREATE PROC sp_Eliminar_Proveedor
(
@ID_Proveedor int, 
@UsuarioElimino varchar(25)
)
AS
BEGIN
	/* HACE UNA ELIMINACIÓN LÓGICA DEL PROVEEDOR*/
	BEGIN
		UPDATE Proveedores
		SET Estado = 6
		WHERE ID_Proveedor = @ID_Proveedor
		-- Estado = 6 quiere decir eliminado. Cualquier duda revisar tabla Estados
	END

	BEGIN
		-- INSERTAR UN REGISTRO DE BITÁCORA DE LA ELIMINACIÓN DEL PROVEEDOR
		-- ID_Accion = 3 es eliminado. Cualquier duda revisar tabla TipoAcciones
		EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Proveedores 3, @ID_Proveedor, 'El proveedor ha sido eliminado.', @UsuarioElimino
	END
END