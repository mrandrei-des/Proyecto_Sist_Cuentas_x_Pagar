
-- PROCEDIMIENTO ALMACENADO QUE CONSULTA POR TODOS LOS USUARIO INDICADOS QUE SE ENCUENTREN EN UN ESTADO DIFERENTE A ELIMINADO
-- sp_Consultar_Usuarios_Sistema
CREATE PROC sp_Consultar_Usuarios_Sistema
AS
	BEGIN
		SELECT u.NombreUsuario as Usuario, TRIM(CONCAT(u.Nombre, ' ', u.Apellido1, ' ', u.Apellido2)) as 'NombreCompleto', u.CorreoElectronico as Correo,
		e.Descripcion as Estado, ro.Descripcion as Rol
		FROM Usuarios u
		JOIN Estados e on u.Estado = e.ID_Estado
		JOIN Roles_x_Usuario ru on u.NombreUsuario = ru.Usuario
		JOIN Roles ro on ru.ID_Rol = ro.ID_Rol
		WHERE u.Estado <> 6
		ORDER BY FechaHoraCreacion ASC
	END