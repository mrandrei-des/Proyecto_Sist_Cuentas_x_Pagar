-- PROCEDIMIENTO ALMACENADO QUE CONSULTA LOS PERMISOS QUE TIENE UN GRUPO ESPECÍFICO
-- EXEC sp_Consulta_Permisos_x_Grupo 1
CREATE PROC sp_Consulta_Permisos_x_Grupo
(
@ID_Grupo int
)
AS
BEGIN
	SELECT ID_Permiso as idPermiso, Titulo, Descripcion
	FROM Permisos
	WHERE ID_Grupo = @ID_Grupo
	ORDER BY ID_Permiso ASC
END