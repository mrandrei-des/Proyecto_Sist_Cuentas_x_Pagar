-- PROCEDIMIENTO ALMACENADO GUARDA UN REGISTRO POR CADA CAMBIO QUE SE HAGA A LOS DOCUMENTOS DE FORMAS DE PAGO
CREATE PROC sp_Inserta_Registro_Bitacora_Cambio_DocumentosFormasPago
(
@ID_Accion int,
@ID_Proveedor int,
@TipoDocumento int,
@NumeroDocumento varchar(10),
@DescripcionAccion varchar(150),
@UsuarioRealizoAccion varchar(25)
)
AS
BEGIN
	INSERT INTO Bitacora_Cambios_Documentos_Formas_Pago(ID_Accion, ID_Proveedor, TipoDocumento, NumeroDocumento, DescripcionAccion, UsuarioRealizoAccion, FechaHoraAccion)
	VALUES(@ID_Accion, @ID_Proveedor, @TipoDocumento, @NumeroDocumento, @DescripcionAccion, @UsuarioRealizoAccion, GETDATE())
END