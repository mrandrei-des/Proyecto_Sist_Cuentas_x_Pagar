-- CONSULTA LOS USUARIOS QUE TENGAN ASIGNADO UN ROL ESPECÍFICO
ALTER PROC sp_Consulta_Rol_x_ID_Usuario
(
@ID_Rol int
)
AS
BEGIN
	SELECT u.NombreUsuario as Usuario, u.Nombre, U.Apellido1, U.Apellido2, u.CorreoElectronico
	FROM Roles_x_Usuario ru
	JOIN Usuarios u on ru.Usuario = u.NombreUsuario
	WHERE ru.ID_Rol = @ID_Rol and ru.Estado IN (4)
	ORDER BY ru.FechaCreacion ASC
END