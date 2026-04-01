-- PROCEDIMIENTO ALMACENADO MODIFICACIÓN DE ROLES
-- EXEC sp_Elimina_Rol 0
CREATE PROC sp_Elimina_Rol
(
@ID_Rol int,
@UsuarioElimino varchar(25)
)
AS
BEGIN
	UPDATE Roles
	SET Estado = 6
	WHERE ID_Rol = @ID_Rol
END
