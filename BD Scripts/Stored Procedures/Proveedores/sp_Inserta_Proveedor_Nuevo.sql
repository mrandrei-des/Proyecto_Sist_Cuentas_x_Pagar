-- PROCEDIMIENTO ALMACENADO CREACIÓN DE PROVEEDORES
-- EXEC sp_Inserta_Proveedor_Nuevo '', 0, '', '', 0, ''
CREATE PROC sp_Inserta_Proveedor_Nuevo
(
@Nombre varchar(30),
@TipoIdentificacion int,
@Identificacion varchar(30),
@CorreoElectronico varchar(30),
@Estado int,
@UsuarioCreacion varchar(25)
)
AS
BEGIN
	/* INSERTA EL PROVEEDOR */
	BEGIN
		INSERT INTO Proveedores(Nombre, TipoIdentificacion, Identificacion, CorreoElectronico, Estado, UsuarioCreacion, FechaCreacion)
		VALUES (@Nombre, @TipoIdentificacion, @Identificacion, @CorreoElectronico, @Estado, @UsuarioCreacion, GETDATE())
	END
	
	BEGIN
		-- AL SER AUTOINCREMENTABLE LA COLUMNA DE ID_PROVEEDOR, SE DEBE CONSULTAR CON CUÁL FUE EL ID QUE SE INSERTÓ
		DECLARE @ID_Proveedor int
		SET @ID_Proveedor = (SELECT TOP 1 ID_Proveedor FROM Proveedores ORDER BY ID_Proveedor DESC)
		/*EJECUTA EL PROCEDIMIENTO ALMACENADO QUE INSERTA UN REGISTRO EN LA TABLA BITÁCORA DE CAMBIOS PARA LOS PROVEEDORES*/
		-- ID_Accion = 1 es insertado. Cualquier duda revisar tabla TipoAcciones
		EXECUTE sp_Inserta_Registro_Bitacora_Cambio_Proveedores 1, @ID_Proveedor, 'El proveedor ha sido registrado en el sistema.', @UsuarioCreacion
	END
END