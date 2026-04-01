-- PROCEDIMIENTO ALMACENADO PARA GUARDAR UN PERMISO ASIGNADO A UN ROL
-- sp_Inserta_Permiso_x_Rol 0, 0, ''
ALTER PROC sp_Inserta_Permiso_x_Rol
(
@ID_Rol int,
@ID_GrupoPermiso int,
@ID_Permiso int,
@UsuarioInserta varchar(25)
)
AS
BEGIN
	INSERT INTO Permisos_x_Rol (ID_Rol, ID_GrupoPermiso, ID_Permiso, UsuarioCreacion, FechaCreacion)
	VALUES (@ID_Rol, @ID_GrupoPermiso, @ID_Permiso, @UsuarioInserta, GETDATE())
END