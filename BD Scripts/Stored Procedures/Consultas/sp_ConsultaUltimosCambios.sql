-- PROCEDIMIENTO ALMACENADO QUE CONSULTA POR LOS ÚLTIMOS 5 MOVIMIENTOS DE DOCUMENTOS QUE SE REALIZARON
-- EXEC sp_ConsultaUltimosCambios 
alter PROC sp_ConsultaUltimosCambios
AS
BEGIN
	WITH cte_Ultimos_Cambios AS (
		SELECT a.ID_Proveedor AS idProveedor, a.TipoFactura as TipoDoc,	NumeroFactura as NumDoc, 
		UsuarioRealizoAccion as Usuario, a.ID_Accion as idAccion, FechaHoraAccion
		FROM Bitacora_Cambios_Facturas a

		UNION
		SELECT a.ID_Proveedor AS idProveedor, a.TipoDocumento AS TipoDoc, a.NumeroDocumento as NumDoc, 
		UsuarioRealizoAccion as Usuario, a.ID_Accion as idAccion, FechaHoraAccion
		FROM Bitacora_Cambios_Documentos_Formas_Pago a
	)

	SELECT TOP 5
	CASE WHEN DATEDIFF(MONTH, FechaHoraAccion, GETDATE()) > 0 THEN
		CONCAT('Hace ', DATEDIFF(MONTH, FechaHoraAccion, GETDATE()), ' meses')
	ELSE
		CASE WHEN DATEDIFF(DAY, FechaHoraAccion, GETDATE()) > 0 THEN
			CONCAT('Hace ', DATEDIFF(DAY, FechaHoraAccion, GETDATE()), ' días')
		ELSE
			CASE WHEN DATEDIFF(HOUR, FechaHoraAccion, GETDATE()) > 0 THEN
				CONCAT('Hace ', DATEDIFF(HOUR, FechaHoraAccion, GETDATE()), ' horas')
			ELSE
				CASE WHEN DATEDIFF(MINUTE, FechaHoraAccion, GETDATE()) > 0 THEN
					CONCAT('Hace ', DATEDIFF(MINUTE, FechaHoraAccion, GETDATE()), ' minutos')
				END
			END
		END
	END AS HaceTiempo, b.Nombre as NombreProveedor, 
	c.ID_Categoria as idCategoria, a.NumDoc as NumDoc, 
	CONCAT(d.Nombre, ' ', d.Apellido1) AS NombreUsuario,
	e.Accion
	FROM cte_Ultimos_Cambios a
	JOIN Proveedores b on a.idProveedor = b.ID_Proveedor
	JOIN TipoDocumentos c on a.TipoDoc = c.ID_TipoDocumento
	JOIN Usuarios d on a.Usuario = d.NombreUsuario
	JOIN TipoAcciones e on a.idAccion = e.ID_Accion
	ORDER BY a.FechaHoraAccion DESC	
END