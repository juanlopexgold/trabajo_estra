-- spRegistrarUsuario
CREATE PROCEDURE spRegistrarUsuario
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Usuarios (Username, PasswordHash, Email, FechaCreacion)
    VALUES (@Username, @PasswordHash, @Email, GETDATE());
END;
GO

-- spLoginUsuario
CREATE PROCEDURE spLoginUsuario
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    DECLARE @UserId INT;

    SELECT @UserId = IdUsuario
    FROM Usuarios
    WHERE Username = @Username AND PasswordHash = @PasswordHash;

    IF @UserId IS NOT NULL
    BEGIN
        UPDATE Usuarios
        SET UltimoLogin = GETDATE()
        WHERE IdUsuario = @UserId;

        SELECT @UserId AS IdUsuario;
    END
    ELSE
    BEGIN
        SELECT NULL AS IdUsuario;
    END
END;
GO

-- spGenerarToken
CREATE PROCEDURE spGenerarToken
    @UserId INT,
    @Token NVARCHAR(255),
    @FechaExpiracion DATETIME
AS
BEGIN
    INSERT INTO Tokens (IdUsuario, Token, FechaExpiracion)
    VALUES (@UserId, @Token, @FechaExpiracion);
END;
GO

-- spVerificarToken
CREATE PROCEDURE spVerificarToken
    @Token NVARCHAR(255)
AS
BEGIN
    DECLARE @UserId INT;

    SELECT @UserId = IdUsuario
    FROM Tokens
    WHERE Token = @Token AND FechaExpiracion > GETDATE();

    IF @UserId IS NOT NULL
    BEGIN
        SELECT @UserId AS IdUsuario;
    END
    ELSE
    BEGIN
        SELECT NULL AS IdUsuario;
    END
END;
GO

-- Asignar Rol a Usuario
CREATE PROCEDURE spAsignarRolAUsuario
    @IdUsuario INT,
    @IdRol INT
AS
BEGIN
    INSERT INTO UsuariosRoles (IdUsuario, IdRol)
    VALUES (@IdUsuario, @IdRol);
END;
GO

-- Obtener Permisos por Usuario
CREATE PROCEDURE spObtenerPermisosPorUsuario
    @IdUsuario INT
AS
BEGIN
    SELECT p.Nombre, p.Descripcion
    FROM Permisos p
    JOIN RolesPermisos rp ON p.IdPermiso = rp.IdPermiso
    JOIN UsuariosRoles ur ON rp.IdRol = ur.IdRol
    WHERE ur.IdUsuario = @IdUsuario;
END;
GO

-- Registrar Permiso
CREATE PROCEDURE spRegistrarPermiso
    @Nombre NVARCHAR(50),
    @Descripcion NVARCHAR(250)
AS
BEGIN
    INSERT INTO Permisos (Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);
END;
GO

-- Asignar Permiso a Rol
CREATE PROCEDURE spAsignarPermisoARol
    @IdRol INT,
    @IdPermiso INT
AS
BEGIN
    INSERT INTO RolesPermisos (IdRol, IdPermiso)
    VALUES (@IdRol, @IdPermiso);
END;
