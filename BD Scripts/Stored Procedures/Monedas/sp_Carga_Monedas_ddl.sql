alter PROC sp_Carga_Monedas_ddl
AS
BEGIN
	SELECT CodigoMoneda as VALOR, Descripcion as TEXTO, Simbolo
	FROM Monedas
END
