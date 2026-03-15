-- CARGA LOS TIPOS DE DOCUMENTOS. ESTO SE USA PARA LLENAR LOS SELECT/DROPDOWNLIST
CREATE PROC sp_Cargar_Tipo_Documento_ddl
(
@ID_Categoria int
)
AS
BEGIN
	SELECT ID_TipoDocumento AS VALOR, Descripcion as TEXTO, ID_Categoria as IdCategoria
	FROM TipoDocumentos
	WHERE ID_Categoria = @ID_Categoria
	ORDER BY VALOR ASC
END