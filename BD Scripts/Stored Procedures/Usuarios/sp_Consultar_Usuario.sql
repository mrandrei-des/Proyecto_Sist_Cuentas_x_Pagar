-- PROCEDIMIENTO ALMACENADO QUE CONSULTA AL USUARIO INDICADO QUE SE ENCUENTRE EN UN ESTADO DIFERENTE A ELIMINADO
-- sp_Consultar_Usuario ''
CREATE PROC sp_Consultar_Usuario
(
@UsuarioConsultado varchar(25)
)
AS
BEGIN
	SELECT u.*, r.ID_Rol, r.Estado as EstadoRol 
	FROM Usuarios u
	JOIN Roles_x_Usuario r on u.NombreUsuario = r.Usuario
	WHERE u.NombreUsuario = @UsuarioConsultado and u.Estado <> 6
END