-- PROCEDIMIENTO ALMACENADO CREACIÓN DE ROLES
-- EXEC sp_Inserta_Rol_Nuevo 0, '', 0, ''
CREATE PROC sp_Inserta_Rol_Nuevo
(
@ID_Rol int, 
@Descripcion varchar(50), 
@Estado int,
@UsuarioCreacion varchar(25)
)
AS
BEGIN
	INSERT INTO Roles (ID_Rol, Descripcion, Estado, UsuarioCreacion, FechaCreacion)
	VALUES (@ID_Rol, @Descripcion, @Estado, @UsuarioCreacion, GETDATE())
END
