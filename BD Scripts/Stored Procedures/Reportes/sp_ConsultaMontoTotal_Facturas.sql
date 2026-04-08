-- CONSULTA EL MONTO TOTAL DEL SALDO DE LAS FACTURAS QUE SE ENCUENTRAN APLICADAS
CREATE PROC sp_ConsultaMontoTotal_Facturas
AS
BEGIN
	WITH cte_factAplicadas as
	(
		SELECT f.Moneda, SUM(SaldoActual) AS MontoFact
		FROM Facturas f	
		WHERE  f.Estado = 2
		GROUP BY f.Moneda
	)

	SELECT m.CodigoMoneda as Moneda, m.Simbolo, CASE WHEN f.MontoFact IS NULL THEN 0 ELSE f.MontoFact END AS Pend_Pago_Fact
	FROM Monedas m
	LEFT JOIN cte_factAplicadas f on m.CodigoMoneda = f.Moneda
END

