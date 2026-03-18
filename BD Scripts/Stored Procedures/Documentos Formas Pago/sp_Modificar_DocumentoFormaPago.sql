-- PROCEDIMIENTO ALMACENADO ACTUALIZACIÓN DEL DOCUMENTO DE FORMA DE PAGO INDICADO
-- EXEC sp_Modificar_DocumentoFormaPago 
ALTER PROC sp_Modificar_DocumentoFormaPago
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
@UsuarioModifico varchar(25)
)
AS
BEGIN
	Declare @DescripcionAccion varchar(250) = 'Al documento de forma de pago se le modificó: ', @cantCambios int = 0

	/* VERIFICA QUÉ FUE LO QUE CAMBIÓ A NIVEL DE DATOS DEL DOCUMENTO DE FORMA DE PAGO */
	IF @Observacion <> (SELECT Observacion FROM Documentos_Formas_Pago 
					   WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento 
					   and NumeroDocumento = @NumeroDocumento
					   )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Observación, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @FechaEmision <> (SELECT FechaEmision FROM Documentos_Formas_Pago 
						WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento 
						and NumeroDocumento = @NumeroDocumento
						)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Fecha de emisión, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Estado <> (SELECT Estado FROM Documentos_Formas_Pago 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento 
				  and NumeroDocumento = @NumeroDocumento
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Estado, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Moneda <> (SELECT Moneda FROM Documentos_Formas_Pago 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento 
				  and NumeroDocumento = @NumeroDocumento
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Moneda, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @TipoCambio <> (SELECT TipoCambio FROM Documentos_Formas_Pago 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento 
				  and NumeroDocumento = @NumeroDocumento
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Tipo de Cambio, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Total <> (SELECT Total FROM Documentos_Formas_Pago 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento 
				  and NumeroDocumento = @NumeroDocumento
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Monto Total, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @SaldoActual <> (SELECT SaldoActual FROM Documentos_Formas_Pago 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento 
				  and NumeroDocumento = @NumeroDocumento
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Saldo Actual, '
			SET @cantCambios = @cantCambios + 1
		END

	/* HACE UN UPDATE A LA FACTURA SI DETECTÓ CAMBIOS */
	BEGIN
		IF @cantCambios > 0
			BEGIN
				UPDATE Documentos_Formas_Pago
				SET Observacion = @Observacion, FechaEmision = @FechaEmision, Estado = @Estado,
				Moneda = @Moneda, TipoCambio = @TipoCambio, Total = @Total, SaldoActual = @SaldoActual
				WHERE ID_Proveedor = @ID_Proveedor and TipoDocumento = @TipoDocumento and NumeroDocumento = @NumeroDocumento
			END

			/* SOLO SI REALMENTE HUBIERON CAMBIOS (DATOS DEL DOCUMENTO DE FORMA DE PAGO) INSERTA EL REGISTRO DE BITÁCOTA CON EL DETALLE DE LOS CAMBIOS */
			BEGIN
				-- INSERTAR UN REGISTRO DE BITÁCORA DEL UPDATE AL DOCUMENTO DE FORMA DE PAGO SI REALMENTE SE HIZO ALGÚN CAMBIO
				-- ID_Accion = 2 es modificado. Cualquier duda revisar tabla TipoAcciones
				SET @DescripcionAccion = SUBSTRING(@DescripcionAccion, 1, (LEN(@DescripcionAccion) - 1))
				EXECUTE sp_Inserta_Registro_Bitacora_Cambio_DocumentosFormasPago 2, @ID_Proveedor, @TipoDocumento, @NumeroDocumento,  @DescripcionAccion, @UsuarioModifico
			END
	END
END