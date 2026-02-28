-- PROCEDIMIENTO ALMACENADO ACTUALIZACIÓN DEL USUARIO
-- EXEC sp_Modificar_Usuario 'andre', '123456789', 'admin', 'Admin', '', 'admin@sist_cxp.com', 4, 1, 'andre'
CREATE PROC sp_Modificar_Usuario
(
@UsuarioAfectado varchar(25),
@Contrasenna varchar(125),
@Nombre varchar(30),
@Apellido1 varchar(30),
@Apellido2 varchar(30),
@CorreoElectronico varchar(30),
@Estado int,
@Rol int,
@UsuarioModifico varchar(25)
)
AS
BEGIN
	Declare @DescripcionAccion varchar(250) = 'Al usuario se le modificó: ', @cantCambios int = 0

	/* VERIFICA QUÉ FUE LO QUE CAMBIÓ A NIVEL DE DATOS DEL USUARIO */
	IF @Contrasenna <> (SELECT Contrasenna FROM Usuarios WHERE NombreUsuario = @UsuarioAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Contraseña, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Nombre <> (SELECT Nombre FROM Usuarios WHERE NombreUsuario = @UsuarioAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Nombre, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Apellido1 <> (SELECT Apellido1 FROM Usuarios WHERE NombreUsuario = @UsuarioAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Primer Apellido, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Apellido2 <> (SELECT Apellido2 FROM Usuarios WHERE NombreUsuario = @UsuarioAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Segundo Apellido, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @CorreoElectronico <> (SELECT CorreoElectronico FROM Usuarios WHERE NombreUsuario = @UsuarioAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Correo Electrónico, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Estado <> (SELECT Estado FROM Usuarios WHERE NombreUsuario = @UsuarioAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Estado, '
			SET @cantCambios = @cantCambios + 1
		END

	/* HACE UN UPDATE AL USUARIO SI DETECTÓ CAMBIOS*/
	BEGIN
		IF @cantCambios > 0
			BEGIN
				UPDATE Usuarios
				SET Contrasenna = @Contrasenna, Nombre = @Nombre, Apellido1 = @Apellido1,
				Apellido2 = @Apellido2, CorreoElectronico = @CorreoElectronico, Estado = @Estado
				WHERE NombreUsuario = @UsuarioAfectado
			END
	END

	BEGIN
		/* VERIFICA SI EL ROL CAMBIO */
		IF @Rol <> (SELECT ID_Rol FROM Roles_x_Usuario WHERE Usuario = @UsuarioAfectado)
			BEGIN
				SET @DescripcionAccion = @DescripcionAccion + 'Rol, '
				SET @cantCambios = @cantCambios + 1

				UPDATE Roles_x_Usuario
				SET ID_Rol = @Rol, Estado = @Estado
				WHERE Usuario = @UsuarioAfectado
			END

		/* SOLO SI REALMENTE HUBIERON CAMBIOS (DATOS DEL USUARIO Y/O ROL) INSERTA EL REGISTRO DE BITÁCOTA CON EL DETALLE DE LOS CAMBIOS */
		IF @cantCambios > 0
			BEGIN
				-- INSERTAR UN REGISTRO DE BITÁCORA DEL UPDATE AL USUARIO SI REALMENTE SE HIZO ALGÚN CAMBIO
				-- ID_Accion = 2 es modificado. Cualquier duda revisar tabla TipoAcciones
				SET @DescripcionAccion = SUBSTRING(@DescripcionAccion, 1, (LEN(@DescripcionAccion) - 1))
				EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Usuarios 2, @UsuarioAfectado, @DescripcionAccion, @UsuarioModifico
			END
	END
END