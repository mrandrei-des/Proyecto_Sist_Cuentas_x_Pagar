-- PROCEDIMIENTO ALMACENADO QUE VALIDA LA EXISTENCIA DEL USUARIO Y CONTRASEÑA
-- EXEC sp_IniciarSesion '', ''
CREATE PROC sp_IniciarSesion
(
@Username varchar(25),
@PassContra varchar(125)
)
AS
BEGIN
	SELECT u.NombreUsuario, u.Nombre, u.Apellido1, u.Apellido2, u.CorreoElectronico, r.ID_Rol as Rol
	FROM Usuarios u
	JOIN Roles_x_Usuario r on u.NombreUsuario = r.Usuario
	WHERE u.NombreUsuario = @Username and u.Contrasenna = @PassContra and u.Estado = 4
END