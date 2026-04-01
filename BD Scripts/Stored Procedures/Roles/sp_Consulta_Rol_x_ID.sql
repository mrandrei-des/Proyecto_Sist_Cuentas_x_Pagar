-- CONSULTA EL ROL POR MEDIO DEL ID QUE SE LE PASE
CREATE PROC sp_Consulta_Rol_x_ID
(
@ID_Rol int
)
AS
BEGIN
	SELECT ID_Rol as IDRol, Descripcion, Estado
	FROM Roles
	WHERE Estado IN (4) and ID_Rol = @ID_Rol
END