
-- PROCEDIMIENTO ALMACENADO CREACIÓN DE USUARIOS
-- EXEC sp_Inserta_Usuario_Nuevo '', '', '', '', '', '', 0, 0, ''
CREATE PROC sp_Inserta_Usuario_Nuevo
(
@Usuario varchar(25), 
@Contrasenna varchar(125),
@Nombre varchar(30),
@Apellido1 varchar(30),
@Apellido2 varchar(30),
@CorreoElectronico varchar(30),
@Estado int,
@Rol int,
@UsuarioCreacion varchar(25)
)
AS
BEGIN
	/* INSERTA EL USUARIO*/
	BEGIN
		INSERT INTO Usuarios (NombreUsuario, Contrasenna, Nombre, Apellido1, Apellido2, CorreoElectronico, Estado, FechaHoraCreacion)
		VALUES (@Usuario, @Contrasenna, @Nombre, @Apellido1, @Apellido2, @CorreoElectronico, @Estado, GETDATE())
	END
	
	BEGIN
		/*EJECUTA EL PROCEDIMIENTO ALMACENADO QUE INSERTA UN REGISTRO EN LA TABLA BITÁCORA DE CAMBIOS PARA LOS USUARIOS*/
		-- ID_Accion = 1 es insertado. Cualquier duda revisar tabla TipoAcciones
		EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Usuarios 1, @Usuario, 'El usuario ha sido registrado en el sistema.', @UsuarioCreacion
	END
	
	/* INSERTA LA RELACIÓN DEL USUARIO Y EL ROL*/
	BEGIN
		INSERT INTO Roles_x_Usuario(Usuario, ID_Rol, Estado, UsuarioCreacion, FechaCreacion)
		VALUES (@Usuario, @Rol, @Estado, @UsuarioCreacion, GETDATE())
	END
END
