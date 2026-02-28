-- PROCEDIMIENTO ALMACENADO QUE CONSULTA A TODOS LOS PROVEEDOR QUE SE ENCUENTREN CON UN ESTADO DIFERENTE A ELIMINADO = 6
-- sp_Consultar_Proveedores_Registrados
CREATE PROC sp_Consultar_Proveedores_Registrados
AS
BEGIN
	SELECT p.ID_Proveedor, p.Nombre, p.TipoIdentificacion, p.Identificacion, p.CorreoElectronico, p.Estado, e.Descripcion as EstadoDescripcion
	FROM Proveedores p
	JOIN Estados e on p.Estado = e.ID_Estado
	WHERE p.Estado <> 6
	ORDER BY p.ID_Proveedor ASC
END