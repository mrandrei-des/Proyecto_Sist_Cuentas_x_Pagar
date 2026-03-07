-- PROCEDIMIENTO ALMACENADO FILTRA ENTRE LOS USUARIOS
-- EXEC sp_Filtrar_Usuarios 1, NULL, NULL
CREATE PROC sp_Filtrar_Usuarios
(
@FiltRol int, 
@FiltEnNombre varchar(30), 
@FiltEstado int
)
AS
BEGIN
	SELECT u.NombreUsuario as Usuario, TRIM(CONCAT(u.Nombre, ' ', u.Apellido1, ' ', u.Apellido2)) as 'NombreCompleto', 
	u.CorreoElectronico as Correo, e.Descripcion as Estado, ro.Descripcion as Rol
	FROM Usuarios u
	JOIN Estados e on u.Estado = e.ID_Estado
	JOIN Roles_x_Usuario ru on u.NombreUsuario = ru.Usuario
	JOIN Roles ro on ru.ID_Rol = ro.ID_Rol
	WHERE 1 = 1
	AND (@FiltEnNombre IS NULL OR TRIM(CONCAT(u.Nombre, ' ', u.Apellido1, ' ', u.Apellido2)) like '%' + LOWER(@FiltEnNombre) + '%')
	AND (@FiltEstado IS NULL OR u.Estado = @FiltEstado)
	AND (@FiltRol IS NULL OR ru.ID_Rol = @FiltRol)
	AND u.PermiteVer = 'S'
	ORDER BY FechaHoraCreacion ASC
END