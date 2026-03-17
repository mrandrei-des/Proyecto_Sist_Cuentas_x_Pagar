-- PROCEDIMIENTO ALMACENADO CREACIÓN DE FACTURAS
-- EXEC sp_Inserta_Factura_Nueva
ALTER PROC sp_Inserta_Factura_Nueva
(
@ID_Proveedor int, 
@TipoFactura int,
@NumeroFactura varchar(10),
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
	INSERT INTO Facturas (ID_Proveedor, TipoFactura, NumeroFactura, Observacion, FechaEmision, Estado, Moneda, 
	TipoCambio, Total, SaldoActual, UsuarioCreacion, FechaCreacion)
	VALUES (@ID_Proveedor, @TipoFactura, @NumeroFactura, @Observacion, @FechaEmision, @Estado, @Moneda, 
	@TipoCambio, @Total, @SaldoActual, @UsuarioCreacion, GETDATE())

	/* EJECUTA EL PROCEDIMIENTO ALMACENADO QUE INSERTA UN REGISTRO EN LA TABLA BITÁCORA DE CAMBIOS PARA LAS FACTURAS */
	-- ID_Accion = 1 es insertado. Cualquier duda revisar tabla TipoAcciones
	EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Facturas 1, @ID_Proveedor, @TipoFactura, @NumeroFactura,  'La factura ha sido registrado en el sistema.', @UsuarioCreacion
END