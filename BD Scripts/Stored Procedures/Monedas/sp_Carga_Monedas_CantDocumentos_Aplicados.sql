-- CARGA TODOS LAS MONEDAS Y LA CANTIDAD DE DOCUMENTOS APLICADOS CON CADA MONEDA
CREATE PROC sp_Carga_Monedas_CantDocumentos_Aplicados
(
@FiltTipoDocumento int,
@FiltMoneda varchar(3),
@FiltFechaInicio date,
@FiltFechaFin date
)
AS
BEGIN

	WITH cte_totalFacturas as (
		SELECT Moneda, COUNT(*) as Cantidad
		FROM Facturas
		WHERE 1 = 1 AND Estado = 2
		AND (@FiltTipoDocumento IS NULL OR TipoFactura = @FiltTipoDocumento)
		AND (@FiltMoneda IS NULL OR Moneda = @FiltMoneda)
		AND (@FiltFechaInicio IS NULL OR FechaEmision >= @FiltFechaInicio)
		AND (@FiltFechaFin IS NULL OR FechaEmision <= @FiltFechaFin)
		GROUP BY Moneda
	),
	cte_totalDocumentos as (
		SELECT Moneda, COUNT(*) as Cantidad
		FROM Documentos_Formas_Pago
		WHERE 1 = 1 AND Estado = 2
		AND (@FiltTipoDocumento IS NULL OR TipoDocumento = @FiltTipoDocumento)
		AND (@FiltMoneda IS NULL OR Moneda = @FiltMoneda)
		AND (@FiltFechaInicio IS NULL OR FechaEmision >= @FiltFechaInicio)
		AND (@FiltFechaFin IS NULL OR FechaEmision <= @FiltFechaFin)
		GROUP BY Moneda
	)

	SELECT m.CodigoMoneda AS VALOR, CONCAT(m.Descripcion, ' (' , SUM(COALESCE(f.Cantidad, 0) + COALESCE(d.Cantidad, 0)), ')') as TEXTO 
	FROM Monedas m 
	LEFT JOIN cte_totalFacturas f on m.CodigoMoneda = f.Moneda
	LEFT JOIN cte_totalDocumentos d on m.CodigoMoneda = d.Moneda
	GROUP BY m.CodigoMoneda, m.Descripcion
END


