
select * from Proveedores
order by ID_Proveedor


	DECLARE @FiltTipoIdentificacion int, @FiltEnNombre varchar(15), @FiltEstado int
	SET @FiltTipoIdentificacion = 1
	SET @FiltEnNombre = NULL
	SET @FiltEstado = NULL

	SELECT p.ID_Proveedor as 'ID Proveedor', p.Nombre, p.Identificacion as 'Identificación', 
	p.CorreoElectronico as 'Correo Electrónico', e.Descripcion as Estado
	FROM Proveedores p
	JOIN Estados e on p.Estado = e.ID_Estado
	WHERE 1 = 1
	AND (@FiltTipoIdentificacion IS NULL OR p.TipoIdentificacion = @FiltTipoIdentificacion)
	AND (@FiltEnNombre IS NULL OR LOWER(p.Nombre) like '%' + LOWER(@FiltEnNombre) + '%')
	AND (@FiltEstado IS NULL OR p.Estado = @FiltEstado)
	ORDER BY p.ID_Proveedor ASC

	CREATE PROC sp_Filtrar_Proveedores
	(
	@FiltTipoIdentificacion int, 
	@FiltEnNombre varchar(50), 
	@FiltEstado int
	)
	AS
	BEGIN
		SELECT p.ID_Proveedor as 'ID Proveedor', p.Nombre, p.Identificacion as 'Identificación', p.CorreoElectronico as 'Correo Electrónico', e.Descripcion as Estado
		FROM Proveedores p
		JOIN Estados e on p.Estado = e.ID_Estado
		WHERE 1 = 1
		AND (@FiltTipoIdentificacion IS NULL OR p.TipoIdentificacion = @FiltTipoIdentificacion)
		AND (@FiltEnNombre IS NULL OR LOWER(p.Nombre) like '%' + LOWER(@FiltEnNombre) + '%')
		AND (@FiltEstado IS NULL OR p.Estado = @FiltEstado)
		ORDER BY p.ID_Proveedor ASC
	END
