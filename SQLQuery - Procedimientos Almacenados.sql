USE Gestion_Clubes

GO

create procedure Agregar_Jugador 
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
@IdEstadoJugador tinyint
as
begin
insert into persona values (@Nombre, @Apellido, @FechaNacimiento, @Pais, @Provincia, @Ciudad,@Email)
declare @IdPersona bigint
set @IdPersona = SCOPE_IDENTITY()
insert into jugador values (@IdPersona, @Altura, @Peso, @Posicion, @IdCategoria, @IdEstadoJugador)
end

create procedure Listar_Jugador
as
begin
select j.IdJugador ,p.Nombre, p.Apellido, p.FechaNacimiento, p.pais, p.provincia, p.ciudad, p.Email,
		j.Altura, j.peso, j.posicion, c.IdCategoria, c.nombre as NombreCategoria, ej.IdEstadoJugador ,ej.nombre as EstadoJugador 
from persona p
inner join jugador j on j.IdPersona = p.IdPersona
inner join Categoria c on c.IdCategoria = j.Idcategoria
inner join EstadoJugador ej on ej.IdEstadoJugador = j.IdEstadoJugador
end

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
@IdEstadoJugador tinyint
as
begin 
	update persona 
	set Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, pais = @Pais, provincia = @Provincia, ciudad = @Ciudad, Email = @Email
	where IdPersona = @IdJugador

	update jugador 
	set Altura = @Altura, peso = @Peso, posicion = @Posicion, Idcategoria = @IdCategoria, IdEstadoJugador = @IdEstadoJugador
	where IdJugador = @IdJugador
end

GO

create or alter procedure Eliminar_Jugador 
@IdJugador bigint 
as
begin
	delete from jugador where IdJugador = @IdJugador
end

GO

CREATE PROCEDURE Agregar_Entrenador
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50),
    @Rol NVARCHAR(50)
AS
BEGIN
    DECLARE @IdLugarNacimiento INT, @IdPersona INT;

    
    INSERT INTO LugarNacimiento (Ciudad, Provincia, Pais)
    VALUES (@Ciudad, @Provincia, @Pais);
    SET @IdLugarNacimiento = SCOPE_IDENTITY();

    
    INSERT INTO Persona (Nombre, Apellido, FechaNacimiento, IdLugarNacimiento)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @IdLugarNacimiento);
    SET @IdPersona = SCOPE_IDENTITY();

    
    INSERT INTO Entrenador (IdPersona, Rol)
    VALUES (@IdPersona, @Rol);
END;

GO

CREATE PROCEDURE Eliminar_Entrenador
    @IdEntrenador INT
AS
BEGIN
    DELETE FROM Entrenador
    WHERE IdEntrenador = @IdEntrenador;
END;

GO

CREATE PROCEDURE Actualizar_Entrenador
    @IdEntrenador INT,
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50),
    @Rol NVARCHAR(50)
AS
BEGIN
    DECLARE @IdPersona INT, @IdLugarNacimiento INT;

    
    SELECT @IdPersona = IdPersona FROM Entrenador WHERE IdEntrenador = @IdEntrenador;

    
    SELECT @IdLugarNacimiento = IdLugarNacimiento FROM Persona WHERE IdPersona = @IdPersona;
    UPDATE LugarNacimiento
    SET Ciudad = @Ciudad,
        Provincia = @Provincia,
        Pais = @Pais
    WHERE IdLugarNacimiento = @IdLugarNacimiento;

    
    UPDATE Persona
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        FechaNacimiento = @FechaNacimiento
    WHERE IdPersona = @IdPersona;

    
    UPDATE Entrenador
    SET Rol = @Rol
    WHERE IdEntrenador = @IdEntrenador;
END;

GO

CREATE PROCEDURE Agregar_Socio
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50)
AS
BEGIN
    DECLARE @IdLugarNacimiento INT, @IdPersona INT;

    -
    INSERT INTO LugarNacimiento (Ciudad, Provincia, Pais)
    VALUES (@Ciudad, @Provincia, @Pais);
    SET @IdLugarNacimiento = SCOPE_IDENTITY();

    
    INSERT INTO Persona (Nombre, Apellido, FechaNacimiento, IdLugarNacimiento)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @IdLugarNacimiento);
    SET @IdPersona = SCOPE_IDENTITY();

    
    INSERT INTO Socio (IdPersona)
    VALUES (@IdPersona);
END;

GO

CREATE PROCEDURE Actualizar_Socio
    @IdSocio INT,
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @Ciudad NVARCHAR(50),
    @Provincia NVARCHAR(50),
    @Pais NVARCHAR(50)
AS
BEGIN
    DECLARE @IdPersona INT, @IdLugarNacimiento INT;

    
    SELECT @IdPersona = IdPersona FROM Socio WHERE IdSocio = @IdSocio;

   
    SELECT @IdLugarNacimiento = IdLugarNacimiento FROM Persona WHERE IdPersona = @IdPersona;
    UPDATE LugarNacimiento
    SET Ciudad = @Ciudad,
        Provincia = @Provincia,
        Pais = @Pais
    WHERE IdLugarNacimiento = @IdLugarNacimiento;

    
    UPDATE Persona
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        FechaNacimiento = @FechaNacimiento
    WHERE IdPersona = @IdPersona;
END;

GO

CREATE PROCEDURE Eliminar_Socio
    @IdSocio INT
AS
BEGIN
    DELETE FROM Socio
    WHERE IdSocio = @IdSocio;
END;

GO