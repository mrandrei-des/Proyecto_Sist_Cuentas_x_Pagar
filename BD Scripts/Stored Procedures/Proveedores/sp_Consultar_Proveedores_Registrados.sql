-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A TODOS LOS PROVEEDOR QUE SE ENCUENTREN CON UN ESTADO DIFERENTE A ELIMINADO = 6
-- sp_Consultar_Proveedores_Registrados
CREATE PROC sp_Consultar_Proveedores_Registrados
AS
BEGIN
	SELECT p.ID_Proveedor as 'ID Proveedor', p.Nombre, p.Identificacion as 'Identificación', 
	p.CorreoElectronico as 'Correo Electrónico', e.Descripcion as Estado
	FROM Proveedores p
	JOIN Estados e on p.Estado = e.ID_Estado
	WHERE p.Estado <> 6
	ORDER BY p.ID_Proveedor ASC
END