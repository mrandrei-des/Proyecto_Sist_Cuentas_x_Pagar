-- PROCEDIMIENTO ALMACENADO ACTUALIZACIÓN DEL PROVEEDOR
-- EXEC sp_Modificar_Factura 
CREATE PROC sp_Modificar_Factura
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
@UsuarioModifico varchar(25)
)
AS
BEGIN
	Declare @DescripcionAccion varchar(250) = 'A la factura se le modificó: ', @cantCambios int = 0

	/* VERIFICA QUÉ FUE LO QUE CAMBIÓ A NIVEL DE DATOS DE LA FACTURA */
	IF @Observacion <> (SELECT Observacion FROM Facturas 
					   WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura 
					   and NumeroFactura = @NumeroFactura
					   )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Observación, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @FechaEmision <> (SELECT FechaEmision FROM Facturas 
						WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura 
						and NumeroFactura = @NumeroFactura
						)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Fecha de emisión, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Estado <> (SELECT Estado FROM Facturas 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura 
				  and NumeroFactura = @NumeroFactura
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Estado, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Moneda <> (SELECT Moneda FROM Facturas 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura 
				  and NumeroFactura = @NumeroFactura
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Moneda, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @TipoCambio <> (SELECT TipoCambio FROM Facturas 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura 
				  and NumeroFactura = @NumeroFactura
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Tipo de Cambio, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Total <> (SELECT Total FROM Facturas 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura 
				  and NumeroFactura = @NumeroFactura
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Monto Total, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @SaldoActual <> (SELECT SaldoActual FROM Facturas 
				  WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura 
				  and NumeroFactura = @NumeroFactura
				  )
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Saldo Actual, '
			SET @cantCambios = @cantCambios + 1
		END

	/* HACE UN UPDATE A LA FACTURA SI DETECTÓ CAMBIOS */
	BEGIN
		IF @cantCambios > 0
			BEGIN
				UPDATE Facturas
				SET Observacion = @Observacion, FechaEmision = @FechaEmision, Estado = @Estado,
				Moneda = @Moneda, TipoCambio = @TipoCambio, Total = @Total, SaldoActual = @SaldoActual
				WHERE ID_Proveedor = @ID_Proveedor and TipoFactura = @TipoFactura and NumeroFactura = @NumeroFactura
			END

			/* SOLO SI REALMENTE HUBIERON CAMBIOS (DATOS DE LA FACTURA) INSERTA EL REGISTRO DE BITÁCOTA CON EL DETALLE DE LOS CAMBIOS */
			BEGIN
				-- INSERTAR UN REGISTRO DE BITÁCORA DEL UPDATE A LA FACTURA SI REALMENTE SE HIZO ALGÚN CAMBIO
				-- ID_Accion = 2 es modificado. Cualquier duda revisar tabla TipoAcciones
				SET @DescripcionAccion = SUBSTRING(@DescripcionAccion, 1, (LEN(@DescripcionAccion) - 1))
				EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Facturas 2, @ID_Proveedor, @TipoFactura, @NumeroFactura,  @DescripcionAccion, @UsuarioModifico
			END
	END
END