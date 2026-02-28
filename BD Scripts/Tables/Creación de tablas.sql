/* CREACIÓN DE TABLAS */

-- Tipo Identificaciones
CREATE TABLE TipoIdentificaciones
(
ID_Tipo int primary key,
Descripcion varchar(50) not null
)

-- Estados
CREATE TABLE Estados
(
ID_Estado int primary key,
Descripcion varchar(50) not null
)

-- Tipo Documentos
CREATE TABLE TipoDocumentos
(
Tipo_Documento varchar(4) primary key,
Descripcion varchar(50) not null
)
/*
FACR
FACO
EFEC
SINP
TRAN
CHEQ
*/

-- Monedas
CREATE TABLE Monedas
(
CodigoMoneda varchar(3) primary key,
Descripcion varchar(50) not null,
Simbolo char(1) not null
)
/*
COL - ₡
DOL - $
EUR - €
*/

-- Tipo Acciones
CREATE TABLE TipoAcciones
(
ID_Accion int primary key,
Accion varchar(15) not null,
Descripcion varchar(50) not null
)
/*
1 - INSERTAR
2 - ACTUALIZAR
3 - ELIMINAR
4 - ACTIVAR
5 - INACTIVAR
6 - CONSULTAR
*/

-- Usuarios
CREATE TABLE Usuarios
(
NombreUsuario varchar(25) primary key,
Contrasenna varchar(125) not null,
Nombre varchar(30) not null,
Apellido1 varchar(30) not null,
Apellido2 varchar(30) null default '',
CorreoElectronico varchar(30) not null,
Estado int not null,
FechaHoraCreacion datetime not null default GETDATE(),
FOREIGN KEY (Estado) References Estados(ID_Estado)
)

CREATE TABLE Bitacora_Cambios_Usuarios
(
ID_Cambio int identity(1,1) PRIMARY KEY,
ID_Accion int not null,
UsuarioAfectado varchar(25) not null,
DescripcionAccion varchar(150) not null,
UsuarioRealizoAccion varchar(25) not null,
FechaHoraAccion datetime not null default GETDATE(),
FOREIGN KEY (ID_Accion) REFERENCES TipoAcciones (ID_Accion),
FOREIGN KEY (UsuarioAfectado) REFERENCES Usuarios (NombreUsuario),
FOREIGN KEY (UsuarioRealizoAccion) REFERENCES Usuarios (NombreUsuario)
)

-- Tipo de Cambio
CREATE TABLE TipoCambios
(
CodigoMoneda varchar(3) not null,
Fecha date not null,
Colones float not null,
UsuarioRegistro varchar(25) not null
PRIMARY KEY (CodigoMoneda, Fecha),
FOREIGN KEY (CodigoMoneda) References Monedas(CodigoMoneda),
FOREIGN KEY (UsuarioRegistro) References Usuarios(NombreUsuario)
)

-- Roles
CREATE TABLE Roles
(
ID_Rol int primary key,
Descripcion varchar(50) not null,
Estado int not null,
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
FOREIGN KEY (Estado) References Estados(ID_Estado),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)

-- Roles por Usuario
CREATE TABLE Roles_x_Usuario
(
Usuario varchar(25) not null,
ID_Rol int not null,
Estado int not null,
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
PRIMARY KEY (Usuario, ID_Rol),
FOREIGN KEY (Usuario) References Usuarios(NombreUsuario),
FOREIGN KEY (ID_Rol) References Roles(ID_Rol),
FOREIGN KEY (Estado) References Estados(ID_Estado),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)

-- Permisos
CREATE TABLE Permisos
(
ID_Permiso int primary key,
Descripcion varchar(50) not null,
Estado int not null,
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
FOREIGN KEY (Estado) References Estados(ID_Estado),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)

-- Permisos por Rol
CREATE TABLE Permisos_x_Rol
(
ID_Rol int not null,
ID_Permiso int not null,
Estado int not null,
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
PRIMARY KEY (ID_Rol, ID_Permiso),
FOREIGN KEY (ID_Rol) References Roles(ID_Rol),
FOREIGN KEY (ID_Permiso) References Permisos(ID_Permiso),
FOREIGN KEY (Estado) References Estados(ID_Estado),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)

-- PROVEEDORES
CREATE TABLE Proveedores
(
TipoIdentificacion int not null,
Identificacion varchar(30) not null,
ID_Proveedor int identity(1,1),
Nombre varchar(50) not null,
CorreoElectronico varchar(30) not null,
Estado int not null,
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
PRIMARY KEY (TipoIdentificacion, Identificacion),
FOREIGN KEY (TipoIdentificacion) References TipoIdentificaciones(ID_Tipo),
FOREIGN KEY (Estado) References Estados(ID_Estado),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)

-- BITÁCORA DE PROVEEDORES
CREATE TABLE Bitacora_Cambios_Proveedores
(
ID_Cambio int identity(1,1) PRIMARY KEY,
ID_Accion int not null,
ID_ProveedorAfectado int not null,
TipoIdentificacion int not null,
Identificacion varchar(30) not null,
DescripcionAccion varchar(150) not null,
UsuarioRealizoAccion varchar(25) not null,
FechaHoraAccion datetime not null default GETDATE(),
FOREIGN KEY (ID_Accion) REFERENCES TipoAcciones (ID_Accion),
FOREIGN KEY (TipoIdentificacion, Identificacion) REFERENCES Proveedores (TipoIdentificacion, Identificacion),
FOREIGN KEY (UsuarioRealizoAccion) REFERENCES Usuarios (NombreUsuario)
)

-- Facturas
CREATE DROP TABLE Facturas
(
ID_Proveedor int not null,
TipoFactura varchar(4) not null,
NumeroFactura varchar(10) not null,
Observacion varchar(100) not null,
FechaEmision date not null,
Estado int not null,
Moneda varchar(3) not null,
TipoCambio float not null default 500,
Total float not null,
SaldoActual float not null, 
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
PRIMARY KEY (ID_Proveedor, TipoFactura, NumeroFactura),

FOREIGN KEY (ID_Proveedor) References Proveedores(ID_Proveedor),
FOREIGN KEY (TipoFactura) References TipoDocumentos(Tipo_Documento),
FOREIGN KEY (Estado) References Estados(ID_Estado),
FOREIGN KEY (Moneda) References Monedas(CodigoMoneda),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)

-- Documentos Formas de Pago
CREATE DROP TABLE DocumentosFormasPago
(
ID_Proveedor int not null,
TipoDocumento varchar(4) not null,
NumeroDocumento varchar(10) not null,
Observacion varchar(100) not null,
FechaEmision date not null,
Estado int not null,
Moneda varchar(3) not null,
TipoCambio float not null default 500,
Total float not null,
SaldoActual float not null, 
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
PRIMARY KEY (ID_Proveedor, TipoDocumento, NumeroDocumento),
FOREIGN KEY (ID_Proveedor) References Proveedores(ID_Proveedor),
FOREIGN KEY (TipoDocumento) References TipoDocumentos(Tipo_Documento),
FOREIGN KEY (Estado) References Estados(ID_Estado),
FOREIGN KEY (Moneda) References Monedas(CodigoMoneda),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)

-- Documentos Formas de Pago
CREATE DROP TABLE Pagos_a_Facturas
(
ID_Proveedor int not null,
TipoFactura varchar(4) not null,
NumeroFactura varchar(10) not null,
TipoDocumento varchar(4) not null,
NumeroDocumento varchar(10) not null,
MontoPagado float not null,
Observacion varchar(100) not null,
FechaPago date not null,
Moneda varchar(3) not null,
TipoCambio float not null default 500,
UsuarioCreacion varchar(25) not null,
FechaCreacion datetime not null default GETDATE(),
PRIMARY KEY (ID_Proveedor, TipoFactura, NumeroFactura, TipoDocumento, NumeroDocumento),
FOREIGN KEY (ID_Proveedor) References Proveedores(ID_Proveedor),

FOREIGN KEY (TipoFactura) References TipoDocumentos(Tipo_Documento),
FOREIGN KEY (ID_Proveedor, TipoFactura, NumeroFactura) References Facturas(ID_Proveedor, TipoFactura, NumeroFactura),
FOREIGN KEY (TipoDocumento) References TipoDocumentos(Tipo_Documento),
FOREIGN KEY (ID_Proveedor, TipoDocumento, NumeroDocumento) References DocumentosFormasPago(ID_Proveedor, TipoDocumento, NumeroDocumento),
FOREIGN KEY (Moneda) References Monedas(CodigoMoneda),
FOREIGN KEY (UsuarioCreacion) References Usuarios(NombreUsuario)
)