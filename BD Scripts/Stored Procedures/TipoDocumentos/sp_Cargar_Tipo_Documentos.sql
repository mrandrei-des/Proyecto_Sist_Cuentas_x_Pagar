-- CARGA TODOS LOS TIPOS DE DOCUMENTOS.
ALTER PROC sp_Cargar_Tipo_Documentos
AS
BEGIN
	WITH cte_cantDocumentos as 
	(
		SELECT TipoFactura as idTipoDoc, COUNT(*) as Cantidad
		FROM Facturas
		WHERE Estado = 2
		GROUP BY TipoFactura

		UNION

		SELECT TipoDocumento as idTipoDoc, COUNT(*) as Cantidad
		FROM Documentos_Formas_Pago
		WHERE Estado = 2
		GROUP BY TipoDocumento
	)

	SELECT t.ID_TipoDocumento AS VALOR, CONCAT(Descripcion, ' (' , CASE WHEN d.Cantidad IS NULL THEN 0 ELSE d.Cantidad END, ')') as TEXTO
	FROM TipoDocumentos t
	LEFT JOIN cte_cantDocumentos d on t.ID_TipoDocumento = d.idTipoDoc
	ORDER BY T.ID_TipoDocumento ASC
END