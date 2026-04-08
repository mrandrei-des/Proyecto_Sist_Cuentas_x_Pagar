-- CARGA TODOS LOS TIPOS DE DOCUMENTOS.
ALTER PROC sp_Cargar_Tipo_Documentos
AS
BEGIN
	SELECT ID_TipoDocumento AS VALOR, Descripcion as TEXTO
	FROM TipoDocumentos
	ORDER BY ID_TipoDocumento ASC
END