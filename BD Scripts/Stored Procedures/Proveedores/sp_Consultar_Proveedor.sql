-- PROCEDIMIENTO ALMACENADO QUE CONSULTA AL PROVEEDOR INDICADO QUE SE ENCUENTRE EN UN ESTADO DIFERENTE A ELIMINADO
-- sp_Consultar_Proveedor 0
CREATE PROC sp_Consultar_Proveedor
(
@ID_Proveedor int
)
AS
BEGIN
	SELECT p.ID_Proveedor, p.Nombre, p.TipoIdentificacion, p.Identificacion, p.CorreoElectronico, p.Estado, e.Descripcion as EstadoDescripcion
	FROM Proveedores p
	JOIN Estados e on p.Estado = e.ID_Estado
	WHERE p.ID_Proveedor = @ID_Proveedor and p.Estado <> 6
END