-- PROCEDIMIENTO ALMACENADO CAMBIO DE CONTRASEÑA
-- EXEC sp_Modificar_Usuario '', ''
CREATE PROC sp_Cambio_Contrasenna
(
@NombreUsuario varchar(25),
@ContrasennaNueva varchar(125)
)
AS
BEGIN

	UPDATE Usuarios
	SET Contrasenna = @ContrasennaNueva	
	WHERE NombreUsuario = @NombreUsuario
				
	EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Usuarios 2, @NombreUsuario, 'El usuario ha realizado el cambio de contraseña.', @NombreUsuario
END