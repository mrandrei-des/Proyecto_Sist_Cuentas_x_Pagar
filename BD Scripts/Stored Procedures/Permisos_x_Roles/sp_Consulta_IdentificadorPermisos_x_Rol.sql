-- PROCEDIMIENTO ALMACENADO QUE CONSULTA LOS IDENTIFICADORES DE LOS PERMISOS QUE TIENE ASIGNADO UN ROL ESPECÍFICO
-- EXEC sp_Consulta_IdentificadorPermisos_x_Rol 1
CREATE PROC sp_Consulta_IdentificadorPermisos_x_Rol
(
@ID_Rol int
)
AS
BEGIN
	SELECT ID_Rol as idRol, b.Identificador as idPermiso
	FROM Permisos_x_Rol a
	JOIN Permisos b on a.ID_GrupoPermiso = b.ID_Grupo and  a.ID_Permiso = b.ID_Permiso
	WHERE ID_Rol = @ID_Rol
	ORDER BY a.ID_Permiso ASC
END