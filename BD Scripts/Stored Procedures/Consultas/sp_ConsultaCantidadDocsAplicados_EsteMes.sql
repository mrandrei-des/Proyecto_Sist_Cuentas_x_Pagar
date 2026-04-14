-- PROCEDIMIENTO ALMACENADO QUE DEVUELVE LA CANTIDAD DE DOCUMENTOS QUE FUERON APLICADOS EL MES ACTUAL
-- EXEC sp_ConsultaCantidadDocsAplicados_EsteMes
CREATE PROC sp_ConsultaCantidadDocsAplicados_EsteMes
AS
BEGIN

	WITH cte_docsAplicados_MesActual AS (
		SELECT COUNT(*) AS CANTIDAD, 1 AS CATEGORIA 
		FROM Bitacora_Cambios_Facturas a
		WHERE ID_Accion = 7 AND YEAR(FechaHoraAccion) = YEAR(GETDATE()) AND MONTH(FechaHoraAccion) = MONTH(GETDATE())
		UNION
		SELECT COUNT(*) AS CANTIDAD, 2 AS CATEGORIA 
		FROM Bitacora_Cambios_Documentos_Formas_Pago
		WHERE ID_Accion = 7 AND YEAR(FechaHoraAccion) = YEAR(GETDATE()) AND MONTH(FechaHoraAccion) = MONTH(GETDATE())
	)

	SELECT SUM(CANTIDAD) AS CANTAPLICADOS
	FROM cte_docsAplicados_MesActual
END