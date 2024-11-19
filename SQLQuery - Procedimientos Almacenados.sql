USE Gestion_Clubes

GO

CREATE OR ALTER PROCEDURE Agregar_Jugador 
    @Nombre VARCHAR(30),
    @Apellido VARCHAR(30),
    @FechaNacimiento DATE,
    @Pais VARCHAR(20),
    @Provincia VARCHAR(30),
    @Ciudad VARCHAR(30),
    @Email VARCHAR(30),
    @Altura TINYINT,
    @Peso DECIMAL,
    @Posicion VARCHAR(15),
    @IdCategoria TINYINT,
    @IdEstadoJugador TINYINT,
    @UrlImagen VARCHAR(300),
    @DNI VARCHAR(20) -- Agregar parámetro DNI
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO persona (Nombre, Apellido, FechaNacimiento, Pais, Provincia, Ciudad, Email, UrlImagen, DNI)
            VALUES (@Nombre, @Apellido, @FechaNacimiento, @Pais, @Provincia, @Ciudad, @Email, @UrlImagen, @DNI);
            
            DECLARE @IdPersona BIGINT;
            SET @IdPersona = SCOPE_IDENTITY();
            
            INSERT INTO jugador (IdPersona, Altura, Peso, Posicion, IdCategoria, IdEstadoJugador)
            VALUES (@IdPersona, @Altura, @Peso, @Posicion, @IdCategoria, @IdEstadoJugador);
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        RAISERROR('Error en los parámetros', 16, 10);
    END CATCH
END
GO

-- Procedimiento para eliminar jugador
CREATE OR ALTER PROCEDURE Eliminar_Jugador 
    @IdJugador BIGINT
AS
BEGIN
    DELETE FROM jugador WHERE IdJugador = @IdJugador;
END
GO

GO

CREATE OR ALTER PROCEDURE Eliminar_Socio
    @IdSocio INT
AS
BEGIN
    DELETE FROM Socio
    WHERE IdSocio = @IdSocio;
END;

GO

create or alter procedure FiltroAvanzado 
@NombreCategoria varchar(30),
@NombreEstado varchar(30)
as
begin
	select j.IdJugador ,p.Nombre, p.Apellido, p.FechaNacimiento, p.pais, p.provincia, p.ciudad, p.Email,
		j.Altura, j.peso, j.posicion, c.IdCategoria, c.nombre as NombreCategoria, ej.IdEstadoJugador ,ej.nombre as EstadoJugador 
from persona p
inner join jugador j on j.IdPersona = p.IdPersona
inner join Categoria c on c.IdCategoria = j.Idcategoria
inner join EstadoJugador ej on ej.IdEstadoJugador = j.IdEstadoJugador
where c.nombre like @NombreCategoria and ej.nombre like @NombreEstado
end

GO

create or alter procedure Listar_Jugador
as
begin
select j.IdJugador ,p.Nombre, p.Apellido, p.FechaNacimiento, p.pais, p.provincia, p.ciudad, p.Email, j.Altura, j.peso, j.posicion, c.IdCategoria, c.nombre as NombreCategoria, ej.IdEstadoJugador ,ej.nombre as EstadoJugador, p.UrlImagen
from persona p
inner join jugador j on j.IdPersona = p.IdPersona
inner join Categoria c on c.IdCategoria = j.Idcategoria
inner join EstadoJugador ej on ej.IdEstadoJugador = j.IdEstadoJugador
end

GO

create or alter procedure Modificar_Jugador 
@IdJugador bigint, 
@Nombre varchar(30),
@Apellido varchar(30),
@FechaNacimiento date,
@Pais varchar(20),
@Provincia varchar(30),
@Ciudad varchar(30),
@Email varchar(30),
@Altura tinyint,
@Peso decimal,
@Posicion varchar(15),
@IdCategoria tinyint,
@IdEstadoJugador tinyint,
@UrlImagen varchar(300),
@DNI varchar(20)  -- Agregar parámetro DNI
as
begin 
    begin try
        begin transaction
            update persona 
            set Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, pais = @Pais, provincia = @Provincia, ciudad = @Ciudad, Email = @Email, UrlImagen = @UrlImagen, DNI = @DNI  -- Actualizar DNI
            where IdPersona = @IdJugador

            update jugador 
            set Altura = @Altura, peso = @Peso, posicion = @Posicion, Idcategoria = @IdCategoria, IdEstadoJugador = @IdEstadoJugador
            where IdJugador = @IdJugador
        commit transaction
    end try
    begin catch
        rollback transaction
        raiserror('error en los parametros', 16,10)
    end catch
end


GO

CREATE or alter PROCEDURE Actualizar_Entrenador
    @IdEntrenador INT,
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50),
    @Rol NVARCHAR(50),
	@Email varchar(30),
	@FechaContratacion date,
	@IdCategoria tinyint
AS
BEGIN
	begin try
		begin transaction
			DECLARE @IdPersona bigint
			
			SELECT @IdPersona = IdPersona FROM Entrenador WHERE IdEntrandor = @IdEntrenador;
			
			UPDATE Persona
			SET Nombre = @Nombre,
			    Apellido = @Apellido,
			    FechaNacimiento = @FechaNacimiento,
				pais = @Pais,
				provincia = @Provincia,
				ciudad = @Ciudad,
				Email = @Email
			WHERE IdPersona = @IdPersona;
			
			UPDATE Entrenador
			SET Rol = @Rol,
				FechaContratacion = @FechaContratacion,
				IdCategoria = @IdCategoria
			WHERE IdEntrandor = @IdEntrenador;
		commit transaction
	end try
	begin catch
		rollback transaction
		raiserror('error en los parametros', 16, 10)
	end catch
END;

GO

create or alter procedure Listar_Entrenador
as
begin
	select e.IdEntrandor ,p.Apellido, p.Nombre, p.Email, p.FechaNacimiento, p.pais, p.provincia, p.ciudad, e.Rol, e.FechaContratacion, e.IdCategoria ,p.UrlImagen  from persona p
	inner join entrenador e on e.IdPersona = p.IdPersona
end

GO

CREATE or alter PROCEDURE Actualizar_Socio
    @IdSocio INT,
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50),
	@Email varchar(30),
	@UrlImagen varchar (300)
AS
BEGIN
	begin try
		begin transaction
			DECLARE @IdPersona INT, @IdLugarNacimiento INT;
			
			SELECT @IdPersona = IdPersona FROM Socio WHERE IdSocio = @IdSocio;
   
			UPDATE Persona
			SET Nombre = @Nombre,
			    Apellido = @Apellido,
			    FechaNacimiento = @FechaNacimiento,
				pais = @Pais,
				provincia = @Provincia,
				ciudad = @Ciudad,
				Email = @Email,
				UrlImagen = @UrlImagen
			WHERE IdPersona = @IdPersona;
		commit transaction
	end try
	begin catch
		rollback transaction
		raiserror ('error en los parametros', 16,10)
	end catch
END;

GO

CREATE or alter PROCEDURE Agregar_Entrenador
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50),
    @Rol NVARCHAR(50),
	@Email varchar(30),
	@FechaContratacion date,
	@IdCategoria tinyint
AS
BEGIN
	begin try
		begin transaction
			declare @IdPersona bigint
			
			INSERT INTO Persona (Nombre, Apellido, FechaNacimiento, pais, provincia, ciudad, Email)
			VALUES (@Nombre, @Apellido, @FechaNacimiento, @Pais, @Provincia, @Ciudad, @Email);
			SET @IdPersona = SCOPE_IDENTITY();
			
			INSERT INTO Entrenador (IdPersona, Rol, FechaContratacion , IdCategoria)
			VALUES (@IdPersona, @Rol, @FechaContratacion, @IdCategoria);
		commit transaction
	end try
	begin catch
		rollback transaction
		raiserror('error en los parametros', 16, 10)
	end catch
END;

GO

CREATE or alter PROCEDURE Agregar_Socio
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50),
	@Email varchar(30),
	@UrlImagen varchar(300)
AS
BEGIN
	begin try
		begin transaction
			declare @IdPersona bigint

			INSERT INTO Persona (Nombre, Apellido, FechaNacimiento, pais, provincia, ciudad, Email, UrlImagen)
			VALUES (@Nombre, @Apellido, @FechaNacimiento, @Pais, @Provincia, @Ciudad, @Email, @UrlImagen);
			SET @IdPersona = SCOPE_IDENTITY();
			
			INSERT INTO Socio (IdPersona)
			VALUES (@IdPersona);
		commit transaction
	end try
	begin catch
		rollback transaction
		raiserror('error en los parametros', 16, 10)
	end catch
END;

GO

CREATE OR ALTER PROCEDURE Eliminar_Entrenador
    @IdEntrenador INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 
            
            DECLARE @IdPersona BIGINT;

            -- Corrige el nombre de la columna de IdEntrandor a IdEntrenador
            SELECT @IdPersona = IdPersona FROM Entrenador WHERE IdEntrandor = @IdEntrenador;

            DELETE FROM Entrenador WHERE IdEntrandor = @IdEntrenador;

            DELETE FROM Persona WHERE IdPersona = @IdPersona;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        RAISERROR('Error en los parámetros', 16, 10);
    END CATCH
END;

GO

CREATE OR ALTER PROCEDURE sp_AgregarPersona
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @FechaNacimiento DATE,
    @DNI NVARCHAR(20), 
    @Email NVARCHAR(100),
    @UrlImagen NVARCHAR(300),
    @Pais NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Ciudad NVARCHAR(50),
    @IdPersona INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Persona (Nombre, Apellido, FechaNacimiento, DNI, Email, UrlImagen, Pais, Provincia, Ciudad)
    VALUES (@Nombres, @Apellidos, @FechaNacimiento, @DNI, @Email, @UrlImagen, @Pais, @Provincia, @Ciudad);

    SET @IdPersona = SCOPE_IDENTITY();
END;

GO

CREATE PROCEDURE ComprobarUsuarioExistente
    @NombreUsuario NVARCHAR(50),
    @Dni NVARCHAR(20),
    @Email NVARCHAR(100),
    @Resultado INT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Usuario WHERE Nombre = @NombreUsuario)
    BEGIN
        PRINT 'Nombre de usuario ya registrado.'
        SET @Resultado = 1
        RETURN
    END

    IF EXISTS (SELECT 1 FROM persona WHERE DNI = @Dni)
    BEGIN
        PRINT 'DNI ya registrado.'
        SET @Resultado = 2
        RETURN
    END

    IF EXISTS (SELECT 1 FROM Usuario WHERE Email = @Email)
    BEGIN
        PRINT 'Correo electrónico ya registrado.'
        SET @Resultado = 3
        RETURN
    END

    SET @Resultado = 0 -- 0: Todos los datos están disponibles
END







