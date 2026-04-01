-- PROCEDIMIENTO ALMACENADO QUE CONSULTA LOS PERMISOS QUE TIENE ASIGNADO UN ROL ESPECÍFICO
-- EXEC sp_Consulta_Permisos_x_Grupo 1
CREATE PROC sp_Consulta_Permisos_x_Roles_x_Rol
(
@ID_Rol int
)
AS
BEGIN
	SELECT ID_Rol as idRol, ID_Permiso as idPermiso
	FROM Permisos_x_Rol
	WHERE ID_Rol = @ID_Rol
	ORDER BY ID_Permiso ASC
END