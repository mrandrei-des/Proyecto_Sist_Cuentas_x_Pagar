-- PROCEDIMIENTO ALMACENADO QUE CONSULTA LA CANTIDAD DE DOCUMENTOS (FACTURAS Y PAGOS) QUE HAN SIDO AL MENOS REGISTRADOS EL DÍA DE HOY
-- EXEC sp_ConsultaCantidadDocsRegistrados_hoy
CREATE PROC sp_ConsultaCantidadDocsRegistrados_hoy
AS
BEGIN
	WITH cte_DocsIngresados AS (
		SELECT COUNT(*) as Cantidad, 1 AS CATEGORIA
		FROM Facturas
		WHERE Estado <> 6 AND FORMAT(FechaCreacion, 'yyyy-MM-dd') = FORMAT(GETDATE(), 'yyyy-MM-dd')
		UNION
		SELECT COUNT(*) as Cantidad, 2 AS CATEGORIA
		FROM Documentos_Formas_Pago
		WHERE Estado <> 6 AND FORMAT(FechaCreacion, 'yyyy-MM-dd') = FORMAT(GETDATE(), 'yyyy-MM-dd')
	)

	SELECT SUM(Cantidad) CantDocsIngresados
	FROM cte_DocsIngresados
END