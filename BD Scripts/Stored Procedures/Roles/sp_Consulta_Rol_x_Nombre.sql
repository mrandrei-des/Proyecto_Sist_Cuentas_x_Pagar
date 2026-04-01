-- CONSULTA EL ROL POR MEDIO DEL NOMBRE QUE SE LE PASA, BUSCA COINCIDENCIAS
CREATE PROC sp_Consulta_Rol_x_Nombre
(
@DescripcionRol varchar(50)
)
AS
BEGIN
	SELECT ID_Rol as IDRol, Descripcion, Estado
	FROM Roles
	WHERE Estado IN (4) and LOWER(Descripcion) LIKE '%' + @DescripcionRol + '%' 
END