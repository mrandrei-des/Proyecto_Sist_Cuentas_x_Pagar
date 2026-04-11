-- CARGA TODOS LOS TIPOS DE DOCUMENTOS.
-- EXEC sp_Cargar_Tipo_Documentos NULL, 'COL', NULL, '2026-03-01'
ALTER PROC sp_Cargar_Tipo_Documentos
(
@FiltTipoDocumento int,
@FiltMoneda varchar(3),
@FiltFechaInicio date,
@FiltFechaFin date
)
AS
BEGIN
	WITH cte_cantDocumentos as 
	(
		SELECT TipoFactura as idTipoDoc, COUNT(*) as Cantidad
		FROM Facturas
		WHERE 1 = 1 AND Estado = 2
		AND (@FiltTipoDocumento IS NULL OR TipoFactura = @FiltTipoDocumento)
		AND (@FiltMoneda IS NULL OR Moneda = @FiltMoneda)
		AND (@FiltFechaInicio IS NULL OR FechaEmision >= @FiltFechaInicio)
		AND (@FiltFechaFin IS NULL OR FechaEmision <= @FiltFechaFin)
		GROUP BY TipoFactura

		UNION

		SELECT TipoDocumento as idTipoDoc, COUNT(*) as Cantidad
		FROM Documentos_Formas_Pago
		WHERE 1 = 1 AND Estado = 2
		AND (@FiltTipoDocumento IS NULL OR TipoDocumento = @FiltTipoDocumento)
		AND (@FiltMoneda IS NULL OR Moneda = @FiltMoneda)
		AND (@FiltFechaInicio IS NULL OR FechaEmision >= @FiltFechaInicio)
		AND (@FiltFechaFin IS NULL OR FechaEmision <= @FiltFechaFin)
		GROUP BY TipoDocumento
	)

	SELECT t.ID_TipoDocumento AS VALOR, CONCAT(Descripcion, ' (' , CASE WHEN d.Cantidad IS NULL THEN 0 ELSE d.Cantidad END, ')') as TEXTO
	FROM TipoDocumentos t
	LEFT JOIN cte_cantDocumentos d on t.ID_TipoDocumento = d.idTipoDoc
	ORDER BY T.ID_TipoDocumento ASC
END