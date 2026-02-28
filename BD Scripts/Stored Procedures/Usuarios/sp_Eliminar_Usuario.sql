
-- PROCEDIMIENTO ALMACENADO CREACIÓN DE USUARIOS
-- EXEC sp_Eliminar_Usuario '', ''
CREATE PROC sp_Eliminar_Usuario
(
@UsuarioAfectado varchar(25), 
@UsuarioElimino varchar(25)
)
AS
BEGIN
	/* HACE UNA ELIMINACIÓN LÓGICA DEL USUARIO*/
	BEGIN
		UPDATE Usuarios
		SET Estado = 6
		WHERE NombreUsuario = @UsuarioAfectado
		-- Estado = 6 quiere decir eliminado. Cualquier duda revisar tabla Estados
	END

	BEGIN
		-- INSERTAR UN REGISTRO DE BITÁCORA DE LA ELIMINACIÓN DEL USUARIO
		-- ID_Accion = 3 es eliminado. Cualquier duda revisar tabla TipoAcciones
		EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Usuarios 3, @UsuarioAfectado, 'El usuario ha sido eliminado.', @UsuarioElimino
	END
END
