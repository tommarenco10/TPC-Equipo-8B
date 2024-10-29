use Gestion_Clubes
go
SET DATEFORMAT 'DMY'

INSERT INTO Categoria
VALUES ('Primera'), ('Reserva'), ('Juvenil')

INSERT INTO EstadoJugador
VALUES ('Disponible'), ('Lesionado'), ('Suspendido'), ('Citado a la Selecci�n')

INSERT INTO EstadoEntrenamiento
VALUES ('Programado'), ('En Curso'), ('Finalizado'), ('Cancelado'), ('Desconocido')

INSERT INTO persona
VALUES
('Lionel', 'Messi', '24-06-1987', 'Argentina', 'Santa Fe', 'Rosario', 'messi@gmail.com'),
('Emiliano', 'Martinez', '02-09-1992', 'Argentina', 'Buenos Aires', 'Mar del Plata', 'miraquetecomo@gmail.com'),
('�ngel', 'Di Mar�a', '14-02-1988', 'Argentina', 'Santa Fe', 'Rosario', 'fideo@gmail.com'),
('Federico', 'Mancuello', '26-03-1989', 'Argentina', 'Santa Fe', 'Reconquista', 'mancu@gmail.com'),
('Alejandro', 'Garnacho', '01-07-2004', 'Espa�a', 'Madrid', 'Madrid', 'bichito@siuuu.com'),
('Tomas', 'Parmo', '08-01-2008', 'Argentina', 'Buenos Aires', 'Capital Federal', 'tomiparmo@gmail.com')

SELECT * FROM persona

INSERT INTO jugador
VALUES
(2, 170, 72, 'Delantero', 1, 1),
(3, 195, 88, 'Portero', 1, 3),
(4, 178, 75, 'Delantero', 1, 2),
(5, 177, 75, 'Mediocampista', 2, 1),
(6, 180, 80, 'Delantero', 3, 1),
(7, 171, 70, 'Mediocampista', 3, 4)

SELECT * FROM jugador

select * from EstadoJugador

SELECT J.IdJugador, P.Nombre, P.Apellido, J.Altura, J.Peso, J.Posicion, c.IdCategoria,
	c.nombre AS NombreCategoria, ej.IdEstadoJugador, ej.nombre AS EstadoJugador
FROM Jugador J
LEFT JOIN Persona P ON J.IdPersona = P.IdPersona
INNER JOIN Categoria c ON c.IdCategoria = j.Idcategoria
INNER JOIN EstadoJugador ej ON ej.IdEstadoJugador = j.IdEstadoJugador

SELECT * FROM entrenamiento