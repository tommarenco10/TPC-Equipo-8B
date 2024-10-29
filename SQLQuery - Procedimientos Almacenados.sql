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
