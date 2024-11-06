USE Gestion_Clubes

GO

create or alter procedure Agregar_Jugador 
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
@UrlImagen varchar(300)
as
begin
	begin try
		begin transaction
			insert into persona values (@Nombre, @Apellido, @FechaNacimiento, @Pais, @Provincia, @Ciudad,@Email, @UrlImagen)
			declare @IdPersona bigint
			set @IdPersona = SCOPE_IDENTITY()
			insert into jugador values (@IdPersona, @Altura, @Peso, @Posicion, @IdCategoria, @IdEstadoJugador)
		commit transaction
	end try
	begin catch
		rollback transaction
		raiserror('error en los parametros',16,10)
	end catch
end

GO

create or alter procedure Eliminar_Jugador 
@IdJugador bigint 
as
begin
	delete from jugador where IdJugador = @IdJugador
end

GO

CREATE PROCEDURE Eliminar_Socio
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
select j.IdJugador ,p.Nombre, p.Apellido, p.FechaNacimiento, p.pais, p.provincia, p.ciudad, p.Email,
		j.Altura, j.peso, j.posicion, c.IdCategoria, c.nombre as NombreCategoria, ej.IdEstadoJugador ,ej.nombre as EstadoJugador, p.UrlImagen
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
@UrlImagen varchar(300)
as
begin 
	begin try
		begin transaction
			update persona 
			set Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, pais = @Pais, provincia = @Provincia, ciudad = @Ciudad, Email = @Email, UrlImagen = @UrlImagen
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

CREATE PROCEDURE Eliminar_Entrenador
    @IdEntrenador INT
AS
BEGIN
	begin try
		begin transaction 
			
			declare @idpersona bigint

			select @idpersona = IdPersona from entrenador where IdEntrandor = @IdEntrenador

			DELETE FROM Entrenador
			WHERE IdEntrandor = @IdEntrenador;

			delete persona 
			where IdPersona = @idpersona

		commit transaction
	end try
	begin catch
		rollback transaction
	end catch
END;