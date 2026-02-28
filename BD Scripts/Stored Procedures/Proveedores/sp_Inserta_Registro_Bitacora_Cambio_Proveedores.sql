-- PROCEDIMIENTO ALMACENADO GUARDA UN REGISTRO POR CADA CAMBIO QUE SE HAGA A LOS PROVEEDORES
CREATE PROC sp_Inserta_Registro_Bitacora_Cambio_Proveedores
(
@ID_Accion int,
@ID_ProveedorAfectado int,
@DescripcionAccion varchar(150),
@UsuarioRealizoAccion varchar(25)
)
AS
BEGIN
	DECLARE @TipoIdentificacion int, @Identificacion varchar(30)

	SELECT @TipoIdentificacion = TipoIdentificacion, @Identificacion = Identificacion
	FROM Proveedores
	WHERE ID_Proveedor = @ID_ProveedorAfectado

	INSERT INTO Bitacora_Cambios_Proveedores(ID_Accion, TipoIdentificacion, Identificacion, ID_ProveedorAfectado, DescripcionAccion, UsuarioRealizoAccion, FechaHoraAccion)
	VALUES(@ID_Accion, @TipoIdentificacion, @Identificacion, @ID_ProveedorAfectado, @DescripcionAccion, @UsuarioRealizoAccion, GETDATE())
END
