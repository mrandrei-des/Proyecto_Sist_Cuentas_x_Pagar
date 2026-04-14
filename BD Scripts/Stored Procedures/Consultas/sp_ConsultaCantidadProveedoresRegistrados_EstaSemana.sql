-- PROCEDIMIENTO ALMACENADO QUE CONSULTA POR LA CANTIDAD DE PROVEEDORES REGISTRADOS EN EL RANGO DE FECHAS INDICADO
-- EXEC sp_ConsultaCantidadProveedoresRegistrados_EstaSemana '20260316', '20260322'
CREATE PROC sp_ConsultaCantidadProveedoresRegistrados_EstaSemana
(
@FechaDiaInicial datetime,
@FechaDiaFinal datetime
)
AS
BEGIN
	SELECT COUNT(*) AS CANTIDAD
	FROM Proveedores
	WHERE Estado <> 6 
	AND FORMAT(FechaCreacion, 'yyyy-MM-dd') >= FORMAT(@FechaDiaInicial, 'yyyy-MM-dd') 
	AND FORMAT(FechaCreacion, 'yyyy-MM-dd') < FORMAT(DATEADD(DAY, 1, @FechaDiaFinal), 'yyyy-MM-dd') 
END