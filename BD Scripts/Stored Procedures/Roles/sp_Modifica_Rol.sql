-- PROCEDIMIENTO ALMACENADO MODIFICACIÓN DE ROLES
-- EXEC sp_Modifica_Rol 0, '', 0, ''
CREATE PROC sp_Modifica_Rol
(
@ID_Rol int,
@Descripcion varchar(50), 
@Estado int,
@UsuarioModificacion varchar(25)
)
AS
BEGIN
	UPDATE Roles
	SET Descripcion = @Descripcion
	WHERE ID_Rol = @ID_Rol
END
