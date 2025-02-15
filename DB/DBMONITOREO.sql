USE [master]
GO

CREATE DATABASE [DBMONITOREO]
go
USE [DBMONITOREO]
GO
/****** Object:  Table [dbo].[Estudiante]    Script Date: 09/10/2024 10:11:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiante](
	[id_estudiantes] [int] IDENTITY(1,1) NOT NULL,
	[nombre_estudiante] [varchar](100) NOT NULL,
	[fecha_llegada] [date] NOT NULL,
	[hora_llegada] [time](7) NOT NULL,
	[comentario] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_estudiantes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Estudiante] ON 

INSERT [dbo].[Estudiante] ([id_estudiantes], [nombre_estudiante], [fecha_llegada], [hora_llegada], [comentario]) VALUES (2, N'Sultan Lopez', CAST(N'2024-10-09' AS Date), CAST(N'08:45:00' AS Time), N'Llegó tarde')
INSERT [dbo].[Estudiante] ([id_estudiantes], [nombre_estudiante], [fecha_llegada], [hora_llegada], [comentario]) VALUES (3, N'Isaac Aleman', CAST(N'2024-10-10' AS Date), CAST(N'08:45:00' AS Time), N'No Llego')
SET IDENTITY_INSERT [dbo].[Estudiante] OFF
GO
/****** Object:  StoredProcedure [dbo].[EditarRegistro]    Script Date: 09/10/2024 10:11:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EditarRegistro]
    @id_estudiantes INT,
    @nombre_estudiante VARCHAR(100),
    @fecha_llegada DATE,
    @hora_llegada TIME,
    @comentario VARCHAR(255)
AS
BEGIN
    UPDATE Estudiante
    SET nombre_estudiante = @nombre_estudiante,
        fecha_llegada = @fecha_llegada,
        hora_llegada = @hora_llegada,
        comentario = @comentario
    WHERE id_estudiantes = @id_estudiantes;
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarRegistro]    Script Date: 09/10/2024 10:11:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarRegistro]
    @id_estudiantes INT
AS
BEGIN
    DELETE FROM Estudiante WHERE id_estudiantes = @id_estudiantes;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarRegistro]    Script Date: 09/10/2024 10:11:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarRegistro]
    @nombre_estudiante VARCHAR(100),
    @fecha_llegada DATE,
    @hora_llegada TIME,
    @comentario VARCHAR(255)
AS
BEGIN
    INSERT INTO Estudiante (nombre_estudiante, fecha_llegada, hora_llegada, comentario)
    VALUES (@nombre_estudiante, @fecha_llegada, @hora_llegada, @comentario);
END
GO
/****** Object:  StoredProcedure [dbo].[MostrarRegistroPorID]    Script Date: 09/10/2024 10:11:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MostrarRegistroPorID]
    @id_estudiantes INT
AS
BEGIN
    SELECT * FROM Estudiante WHERE id_estudiantes = @id_estudiantes;
END
GO
/****** Object:  StoredProcedure [dbo].[MostrarUltimos50Registros]    Script Date: 09/10/2024 10:11:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MostrarUltimos50Registros]
AS
BEGIN
    SELECT TOP 50 * FROM Estudiante
    ORDER BY id_estudiantes DESC;
END
GO
