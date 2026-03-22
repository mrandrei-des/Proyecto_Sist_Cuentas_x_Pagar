alter PROC sp_Carga_Monedas_ddl
AS
BEGIN
	SELECT CodigoMoneda as VALOR, CodigoMoneda + ' ' + Simbolo as TEXTO, Simbolo
	FROM Monedas
	ORDER BY CodigoMoneda ASC
END