-- PROCEDIMIENTO ALMACENADO GUARDA UN REGISTRO POR CADA CAMBIO QUE SE HAGA A LOS USUARIOS
CREATE PROC sp_Inserta_Registro_Bitacora_Cambio_Usuarios
(
@ID_Accion int,
@UsuarioAfectado varchar(25),
@DescripcionAccion varchar(150),
@UsuarioRealizoAccion varchar(25)
)
AS
BEGIN
	INSERT INTO Bitacora_Cambios_Usuarios (ID_Accion, UsuarioAfectado, DescripcionAccion, UsuarioRealizoAccion, FechaHoraAccion)
	VALUES(@ID_Accion, @UsuarioAfectado, @DescripcionAccion, @UsuarioRealizoAccion, GETDATE())
END