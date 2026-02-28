/* INSERTAR VALORES PARA TABLAS INICIALES */

/*	Tipo Identificaciones:

	INSERT INTO TipoIdentificaciones (ID_Tipo, Descripcion)
	VALUES (1, 'Cédula Física'), (2, 'Cédula Jurídica'), (3, 'Pasaporte'), (4, 'DIMEX')
*/

/*	Estados:

	INSERT INTO Estados (ID_Estado, Descripcion)
	VALUES (1, 'Pendiente'), (2, 'Aplicado'), (3, 'Inactivo'), (4, 'Activo'), (5, 'Temporal'), (6, 'Eliminado')
*/
 
/*	Tipo de Documentos:

	INSERT INTO TipoDocumentos (Tipo_Documento, Descripcion)
	VALUES ('FACR', 'Factura de Crédito'), ('FACO', 'Factura de Contado'), ('EFEC', 'Efectivo'), ('SINP', 'SinpeMovil'), ('TRAN', 'Transferencia'), ('CHEQ', 'Cheque')
*/

/*	Monedas:

	INSERT INTO Monedas (CodigoMoneda, Descripcion, Simbolo)
	VALUES ('COL', 'Colones', '₡'), ('DOL', 'Dólares', '$'), ('EUR', 'Euro', '€')
*/

/*	Monedas:

	INSERT INTO TipoAcciones (ID_Accion, Accion, Descripcion)
	VALUES (1, 'INSERTAR', 'Ha insertado al menos un nuevo registro'), (2, 'ACTUALIZAR', 'Ha actualizado al menos un registro'), (3, 'ELIMINAR', 'Ha eliminado al menos un registro'),
	(4, 'ACTIVAR', 'Ha pasado un registro de Inactivo a Activo'), (5, 'INACTIVAR', 'Ha pasado un registro de Activo a Inactivo'), (6, 'CONSULTAR', 'Ha consultado por al menos un registro')
*/

/*	Usuario admin propio
	INSERT INTO Usuarios (NombreUsuario, Contrasenna, Nombre, Apellido1, Apellido2, CorreoElectronico, Estado)
	VALUES ('andre', '123456789', 'Admin', 'Admin', '', 'andre_admin@sist_cxp.com', 4)
*/

/* Roles básicos
	INSERT INTO Roles (ID_Rol, Descripcion, Estado, UsuarioCreacion, FechaCreacion)
	VALUES (1, 'Administrador', 4, 'andre', GETDATE()),
		   (2, 'Digitador Facturas', 4, 'andre', GETDATE()),
		   (3, 'Digitador Documentos Pago', 4, 'andre', GETDATE()),
		   (4, 'Asociador Pagos', 4, 'andre', GETDATE()),
		   (5, 'Reportes', 4, 'andre', GETDATE()),
		   (6, 'Creador Proveedores', 4, 'andre', GETDATE())
*/
