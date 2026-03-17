-- PROCEDIMIENTO ALMACENADO CREACIÓN DE DOCUMENTOS DE FORMA DE PAGO
-- EXEC sp_Inserta_DocumentoFormaPago_Nuevo
CREATE PROC sp_Inserta_DocumentoFormaPago_Nuevo
(
@ID_Proveedor int, 
@TipoDocumento int,
@NumeroDocumento varchar(10),
@Observacion varchar(100),
@FechaEmision date,
@Estado int,
@Moneda varchar(3),
@TipoCambio float,
@Total float,
@SaldoActual float,
@UsuarioCreacion varchar(25)
)
AS
BEGIN
	INSERT INTO Documentos_Formas_Pago (ID_Proveedor, TipoDocumento, NumeroDocumento, Observacion, FechaEmision, Estado, Moneda, 
	TipoCambio, Total, SaldoActual, UsuarioCreacion, FechaCreacion)
	VALUES (@ID_Proveedor, @TipoDocumento, @NumeroDocumento, @Observacion, @FechaEmision, @Estado, @Moneda, 
	@TipoCambio, @Total, @SaldoActual, @UsuarioCreacion, GETDATE())

	/* EJECUTA EL PROCEDIMIENTO ALMACENADO QUE INSERTA UN REGISTRO EN LA TABLA BITÁCORA DE CAMBIOS PARA LOS Documentos Formas Pago */
	-- ID_Accion = 1 es insertado. Cualquier duda revisar tabla TipoAcciones
	EXECUTE sp_Inserta_Registro_Bitacora_Cambio_DocumentosFormasPago 1, @ID_Proveedor, @TipoDocumento, @NumeroDocumento,  'El documento de forma de pago ha sido registrado en el sistema.', @UsuarioCreacion
END