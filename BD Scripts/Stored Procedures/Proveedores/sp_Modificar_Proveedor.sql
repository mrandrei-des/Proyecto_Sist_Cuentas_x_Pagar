-- PROCEDIMIENTO ALMACENADO ACTUALIZACIÓN DEL PROVEEDOR
-- EXEC sp_Modificar_Usuario 0, '', 0, '', '', 0, 'andre'
CREATE PROC sp_Modificar_Proveedor
(
@ID_ProveedorAfectado int,
@Nombre varchar(30),
@TipoIdentificacion int,
@Identificacion varchar(30),
@CorreoElectronico varchar(30),
@Estado int,
@UsuarioModifico varchar(25)
)
AS
BEGIN
	Declare @DescripcionAccion varchar(250) = 'Al proveedor se le modificó: ', @cantCambios int = 0

	/* VERIFICA QUÉ FUE LO QUE CAMBIÓ A NIVEL DE DATOS DEL PROVEEDOR */
	IF @Nombre <> (SELECT Nombre FROM Proveedores WHERE ID_Proveedor = @ID_ProveedorAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Nombre, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @TipoIdentificacion <> (SELECT TipoIdentificacion FROM Proveedores WHERE ID_Proveedor = @ID_ProveedorAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Tipo Identificación, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Identificacion <> (SELECT Identificacion FROM Proveedores WHERE ID_Proveedor = @ID_ProveedorAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Identificación, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @CorreoElectronico <> (SELECT CorreoElectronico FROM Proveedores WHERE ID_Proveedor = @ID_ProveedorAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Correo Electrónico, '
			SET @cantCambios = @cantCambios + 1
		END

	IF @Estado <> (SELECT Estado FROM Proveedores WHERE ID_Proveedor = @ID_ProveedorAfectado)
		BEGIN
			SET @DescripcionAccion = @DescripcionAccion + 'Estado, '
			SET @cantCambios = @cantCambios + 1
		END

	/* HACE UN UPDATE AL PROVEEDOR SI DETECTÓ CAMBIOS*/
	BEGIN
		IF @cantCambios > 0
			BEGIN
				UPDATE Proveedores
				SET Nombre = @Nombre, TipoIdentificacion = @TipoIdentificacion,
				Identificacion = @Identificacion, CorreoElectronico = @CorreoElectronico, Estado = @Estado
				WHERE ID_Proveedor = @ID_ProveedorAfectado
			END

			/* SOLO SI REALMENTE HUBIERON CAMBIOS (DATOS DEL PROVEEDOR) INSERTA EL REGISTRO DE BITÁCOTA CON EL DETALLE DE LOS CAMBIOS */
			BEGIN
				-- INSERTAR UN REGISTRO DE BITÁCORA DEL UPDATE AL PROVEEDOR SI REALMENTE SE HIZO ALGÚN CAMBIO
				-- ID_Accion = 2 es modificado. Cualquier duda revisar tabla TipoAcciones
				SET @DescripcionAccion = SUBSTRING(@DescripcionAccion, 1, (LEN(@DescripcionAccion) - 1))
				EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Proveedores 2, @ID_ProveedorAfectado, @DescripcionAccion, @UsuarioModifico
			END
	END
END