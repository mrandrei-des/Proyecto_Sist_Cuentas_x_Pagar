-- PROCEDIMIENTO ALMACENADO GUARDA UN REGISTRO POR CADA CAMBIO QUE SE HAGA A LAS FACTURAS
ALTER PROC sp_Inserta_Registro_Bitacora_Cambio_Facturas
(
@ID_Accion int,
@ID_Proveedor int,
@TipoFactura int,
@NumeroFactura varchar(10),
@DescripcionAccion varchar(150),
@UsuarioRealizoAccion varchar(25)
)
AS
BEGIN
	INSERT INTO Bitacora_Cambios_Facturas(ID_Accion, ID_Proveedor, TipoFactura, NumeroFactura, DescripcionAccion, UsuarioRealizoAccion, FechaHoraAccion)
	VALUES(@ID_Accion, @ID_Proveedor, @TipoFactura, @NumeroFactura, @DescripcionAccion, @UsuarioRealizoAccion, GETDATE())
END