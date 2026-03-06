-- PROCEDIMIENTO ALMACENADO QUE CONSULTA AL USUARIO INDICADO INDEPENDIENTEMENTE DE SU ESTADO
-- sp_Buscar_Usuario ''
CREATE PROC sp_Buscar_Usuario
(
	@UsuarioConsultado varchar(25)
)
AS
BEGIN
	SELECT u.*, r.ID_Rol, r.Estado as EstadoRol 
	FROM Usuarios u
	JOIN Roles_x_Usuario r on u.NombreUsuario = r.Usuario
	WHERE u.NombreUsuario = @UsuarioConsultado
END