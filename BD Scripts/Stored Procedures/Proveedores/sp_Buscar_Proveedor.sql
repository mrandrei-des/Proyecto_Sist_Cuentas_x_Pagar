-- PROCEDIMIENTO ALMACENADO QUE BUSCA AL PROVEEDOR INDICADO INDEPENDIENTEMENTE DE SU ESTADO
-- sp_Consultar_Proveedor 0
CREATE PROC sp_Buscar_Proveedor
(
	@TipoIdentificacion int,
	@NumeroIdentificacion varchar(30)
)
AS
BEGIN
	SELECT p.ID_Proveedor, p.Nombre, p.TipoIdentificacion, p.Identificacion, p.CorreoElectronico, p.Estado, e.Descripcion as EstadoDescripcion
	FROM Proveedores p
	JOIN Estados e on p.Estado = e.ID_Estado
	WHERE p.TipoIdentificacion = @TipoIdentificacion and p.Identificacion = @NumeroIdentificacion
END