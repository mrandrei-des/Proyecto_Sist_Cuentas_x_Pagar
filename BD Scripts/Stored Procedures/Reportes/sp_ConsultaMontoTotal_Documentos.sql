-- CONSULTA EL MONTO TOTAL DEL SALDO DE LOS DOCUMENTOS QUE SE ENCUENTRAN APLICADOS
CREATE PROC sp_ConsultaMontoTotal_Documentos
AS
BEGIN
	WITH cte_docsAplicados as
	(
		SELECT d.Moneda, SUM(SaldoActual) AS MontoDocs
		FROM Documentos_Formas_Pago d	
		WHERE  d.Estado = 2
		GROUP BY d.Moneda
	)

	SELECT m.CodigoMoneda as Moneda, m.Simbolo, CASE WHEN d.MontoDocs IS NULL THEN 0 ELSE d.MontoDocs END AS Pend_Pago_Docs
	FROM Monedas m
	LEFT JOIN cte_docsAplicados d on m.CodigoMoneda = d.Moneda
END


