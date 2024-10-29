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
@IdEstadoJugador tinyint
as
begin
insert into persona values (@Nombre, @Apellido, @FechaNacimiento, @Pais, @Provincia, @Ciudad,@Email)
declare @IdPersona bigint
set @IdPersona = SCOPE_IDENTITY()
insert into jugador values (@IdPersona, @Altura, @Peso, @Posicion, @IdCategoria, @IdEstadoJugador)
end

create or alter procedure Listar_Jugador
as
begin
select p.Nombre, p.Apellido, p.FechaNacimiento, p.pais, p.provincia, p.ciudad, p.Email,
		j.Altura, j.peso, j.posicion, c.nombre as NombreCategoria, ej.nombre as EstadoJugador 
from persona p
inner join jugador j on j.IdPersona = p.IdPersona
inner join Categoria c on c.IdCategoria = j.Idcategoria
inner join EstadoJugador ej on ej.IdEstadoJugador = j.IdEstadoJugador
end

CREATE OR ALTER PROCEDURE Listar_JugadorPorCategoria (@IdCategoria tinyint) AS
BEGIN
SELECT p.Nombre, p.Apellido, p.FechaNacimiento, p.pais, p.provincia, p.ciudad, p.Email,
		j.Altura, j.peso, j.posicion, c.nombre as NombreCategoria, ej.nombre as EstadoJugador 
FROM persona p
INNER JOIN jugador j ON j.IdPersona = p.IdPersona
INNER JOIN Categoria c ON c.IdCategoria = j.Idcategoria
INNER JOIN EstadoJugador ej ON ej.IdEstadoJugador = j.IdEstadoJugador
WHERE J.Idcategoria = @IdCategoria
END




REATE PROCEDURE Agregar_Entrenador
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



CREATE PROCEDURE Eliminar_Entrenador
    @IdEntrenador INT
AS
BEGIN
    DELETE FROM Entrenador
    WHERE IdEntrenador = @IdEntrenador;
END;






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