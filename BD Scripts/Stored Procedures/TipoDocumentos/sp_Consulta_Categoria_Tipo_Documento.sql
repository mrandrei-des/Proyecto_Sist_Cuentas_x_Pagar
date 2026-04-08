-- CONSULTA EL ID DE CATEGORIA DEL TIPO DE DOCUMENTO QUE SE LE INDIQUE
CREATE PROC sp_Consulta_Categoria_Tipo_Documento
(
@ID_TipoDocumento int
)
AS
BEGIN
	SELECT ID_TipoDocumento AS VALOR, Descripcion as TEXTO, ID_Categoria as IdCategoria
	FROM TipoDocumentos
	WHERE ID_TipoDocumento = @ID_TipoDocumento
END