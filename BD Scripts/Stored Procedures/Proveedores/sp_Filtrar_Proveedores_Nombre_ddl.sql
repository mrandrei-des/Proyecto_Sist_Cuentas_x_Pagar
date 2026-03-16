-- PROCEDIMIENTO ALMACENADO CREACIÓN DE USUARIOS
-- EXEC sp_Filtrar_Proveedores_Nombre_ddl 'mar'
CREATE PROC sp_Filtrar_Proveedores_Nombre_ddl
( 
@FiltEnNombre varchar(50)
)
AS
BEGIN
	SELECT p.ID_Proveedor as 'idProveedor', p.Nombre
	FROM Proveedores p
	WHERE LOWER(p.Nombre) like '%' + LOWER(@FiltEnNombre) + '%' AND p.Estado = 4 AND p.PermiteVer = 'S'
	ORDER BY p.ID_Proveedor ASC
END
