/* INSERTAR VALORES PARA TABLAS INICIALES */

/*	Tipo Identificaciones:

	INSERT INTO TipoIdentificaciones (ID_Tipo, Descripcion)
	VALUES (1, 'Cédula Física'), (2, 'Cédula Jurídica'), (3, 'Pasaporte'), (4, 'DIMEX')
*/

/*	Estados:

	INSERT INTO Estados (ID_Estado, Descripcion)
	VALUES (1, 'Pendiente'), (2, 'Aplicado'), (3, 'Inactivo'), (4, 'Activo'), (5, 'Temporal'), (6, 'Eliminado'), (7, 'Cancelado')
*/

/* CategoriaDocumentos:

	INSERT INTO CategoriaDocumentos (Descripcion)
	VALUES ('Facturas'), ('Documentos de Pago')
*/
 
/*	Tipo de Documentos:

	INSERT INTO TipoDocumentos (Descripcion, IdCategoria)
	VALUES ('Factura de Crédito', 1), ('Factura de Contado', 1), ('Efectivo', 2), ('SinpeMovil', 2), ('Transferencia', 2), ('Cheque', 2)
*/

/*	Monedas:

	INSERT INTO Monedas (CodigoMoneda, Descripcion, Simbolo)
	VALUES ('COL', 'Colones', '₡'), ('DOL', 'Dólares', '$'), ('EUR', 'Euro', '€')
*/

/*	Monedas:

	INSERT INTO TipoAcciones (ID_Accion, Accion, Descripcion)
	VALUES (1, 'INSERTAR', 'Ha insertado al menos un nuevo registro'), (2, 'ACTUALIZAR', 'Ha actualizado al menos un registro'), (3, 'ELIMINAR', 'Ha eliminado al menos un registro'),
	(4, 'ACTIVAR', 'Ha pasado un registro de Inactivo a Activo'), (5, 'INACTIVAR', 'Ha pasado un registro de Activo a Inactivo'), (6, 'CONSULTAR', 'Ha consultado por al menos un registro'), (7, 'APLICAR', 'Ha aplicado el documento.')
*/

/*	Usuario admin propio
	INSERT INTO Usuarios (NombreUsuario, Contrasenna, Nombre, Apellido1, Apellido2, CorreoElectronico, Estado)
	VALUES ('andre', '123456789', 'Admin', 'Admin', '', 'andre_admin@sist_cxp.com', 4)
*/

/* Roles básicos
	INSERT INTO Roles (ID_Rol, Descripcion, Estado, UsuarioCreacion, FechaCreacion)
	VALUES (1, 'Administrador', 4, 'andre', GETDATE()),
		   (2, 'Digitador Facturas', 4, 'andre', GETDATE()),
		   (5, 'Reportes', 4, 'andre', GETDATE()),
		   (6, 'Creador Proveedores', 4, 'andre', GETDATE())
*/

/* Grupos de Permisos
	INSERT INTO GruposPermisos(Descripcion, Estado)
	VALUES ('Usuarios', 4), ('Proveedores', 4), ('Documentos', 4), ('Reportes', 4)
*/

/* Permisos
	INSERT INTO Permisos (Titulo, Descripcion, Estado, ID_Grupo, UsuarioCreacion, FechaCreacion, Identificador)
	VALUES ('Creación de usuarios', 'Permite que el usuario pueda crear usuarios en el sistema', 4, 1, 'andre', GETDATE(), 'CREAR_USUARIOS'), ('Listado y Mantenimiento de usuarios', 'Permite que el usuario pueda ver darles mantenimiento a todos los usuarios registrados en el sistema', 4, 1, 'andre', GETDATE(), 'LIST_MANT_USUARIOS'), ('Cambio Contraseña', 'Permite que el usuario pueda realizar su propio cambio de contraseña', 4, 1, 'andre', GETDATE(), 'CAMBIO_CONTRASENNA'),
	('Creación de proveedores', 'El usuario podrá registrar nuevos proveedores', 4, 2, 'andre', GETDATE(), 'CREAR_PROVEEDORES'), ('Listado de proveedores', 'Permite que el usuario pueda ver y darles mantenimiento a todos los proveedores registrados', 4, 2, 'andre', GETDATE(), 'LIST_MANT_PROVEEDORES'),
	('Creación, Mantenimiento y Aplicación de documentos', 'Permitir que el usuario pueda registrar facturas y documentos de pago además, pueda darles mantenimiento y aplicarlos para dejarlos en firme', 4, 3, 'andre', GETDATE(), 'CREAR_MANT_DOCUMENTOS'), 
	('Listado de documentos', 'Le permite al usuario ver los documentos que se encuentran aplicados en el sistema', 4, 3, 'andre', GETDATE(), 'LIST_DOCUMENTOS'), 
	('Creación de Roles y Configuración de Permisos', 'Permite que el usuario pueda crear y darle mantenimiento a los roles del sistema además, le permite cambiar los permisos que cada rol tiene configurado', 4, 1, 'andre', GETDATE(), 'CREAR_MANT_ROLES'), 
	('Reportes sobre documentos', 'Permite que el usuario pueda generar reportes y consultas sobre los documentos registrados', 4, 4, 'andre', GETDATE(), 'REPO_DOCUMENTOS'),
	('Reportes sobre proveedores', 'Permitir que el usuario pueda generar reportes sobre los proveedores', 4, 4, 'andre', GETDATE(), 'REPO_PROVEEDORES'),
	('Reportes sobre usuarios', 'Permite que el usuario pueda generar reportes y consultas sobre los usuarios del sistema', 4, 4, 'andre', GETDATE(), 'REPO_USUARIOS')
*/

/* Permisos por ID
	INSERT INTO Permisos_x_Rol (ID_Rol, ID_Permiso, UsuarioCreacion, FechaCreacion)
	VALUES (1, 1, 'andre', GETDATE()), (1, 2, 'andre', GETDATE()), (1, 4, 'andre', GETDATE()),(1, 7, 'andre', GETDATE()),
	(1, 11, 'andre', GETDATE())
*/