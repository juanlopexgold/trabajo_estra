CREATE DATABASE web_api_auth
GO
USE
web_api_auth
GO

-- 01 tabla Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    UltimoLogin DATETIME
);
GO

-- 02 tabla Roles
CREATE TABLE Roles (
    IdRol INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(250)
);
GO

-- 03 tabla Permisos
CREATE TABLE Permisos (
    IdPermiso INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(250)
);
GO

-- 04 tabla UsuariosRoles
CREATE TABLE UsuariosRoles (
    IdUsuario INT,
    IdRol INT,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol),
    PRIMARY KEY (IdUsuario, IdRol)
);
GO

-- 05 tabla RolesPermisos
CREATE TABLE RolesPermisos (
    IdRol INT,
    IdPermiso INT,
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol),
    FOREIGN KEY (IdPermiso) REFERENCES Permisos(IdPermiso),
    PRIMARY KEY (IdRol, IdPermiso)
);

-- 06 tabla Tokens
CREATE TABLE Tokens (
    IdToken INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT,
    Token NVARCHAR(255) NOT NULL,
    FechaExpiracion DATETIME NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);
GO



-- DATOS DE PRUEBA
-- Usuarios
INSERT INTO Usuarios (Username, PasswordHash, Email, FechaCreacion) VALUES 
('admin', '$2a$12$4f0b4V.Sg5hSujW8A3zG7uO.X7KQib3CuZna9Y2UhoOaqGEXys4l6', 'admin@example.com', GETDATE()), -- Contraseña: AdminPassword
('vendor', '$2a$12$4f0b4V.Sg5hSujW8A3zG7uO.X7KQib3CuZna9Y2UhoOaqGEXys4l6', 'vendor@example.com', GETDATE()); -- Contraseña: VendorPassword
select * from Usuarios

-- Roles
INSERT INTO Roles (Nombre, Descripcion) VALUES 
('Administrador', 'Acceso a todos los endpoints'),
('Vendedor', 'Acceso a loguearse y ventas');
select * from Roles

-- Permisos
INSERT INTO Permisos (Nombre, Descripcion) VALUES 
('AccesoCompleto', 'Acceso a todos los endpoints'),
('AccesoVentas', 'Acceso a ventas');
select * from Permisos

-- UsuarioRoles
INSERT INTO UsuariosRoles (IdUsuario, IdRol) VALUES 
(1, 1), -- Admin es Administrador
(2, 2); -- Vendor es Vendedor
select * from UsuariosRoles

-- RolesPermisos
INSERT INTO RolesPermisos (IdRol, IdPermiso) VALUES 
(1, 1), -- Administrador tiene acceso completo
(1, 2), -- Administrador tiene acceso a ventas
(2, 2); -- Vendedor tiene acceso a ventas
select * from RolesPermisos

-- Tokens
INSERT INTO Tokens (IdUsuario, Token, FechaExpiracion) VALUES 
(1, 'sampleTokenForAdmin', DATEADD(day, 7, GETDATE())), -- Token de prueba para Admin
(2, 'sampleTokenForVendor', DATEADD(day, 7, GETDATE())); -- Token de prueba para Vendor
select * from Tokens