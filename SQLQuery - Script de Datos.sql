use Gestion_Clubes
go

SELECT * FROM Usuario;

SET DATEFORMAT 'DMY'

INSERT INTO Categoria
VALUES ('Primera'), ('Reserva'), ('Juvenil')

INSERT INTO EstadoJugador
VALUES ('Disponible'), ('Lesionado'), ('Suspendido'), ('Citado a la Selecci�n')

INSERT INTO EstadoEntrenamiento
VALUES ('Programado'), ('En Curso'), ('Finalizado'), ('Cancelado'), ('Desconocido')

INSERT INTO persona (Nombre, Apellido, FechaNacimiento, pais, provincia, ciudad, email)
VALUES
('Lionel', 'Messi', '24-06-1987', 'Argentina', 'Santa Fe', 'Rosario', 'messi@gmail.com'),
('Emiliano', 'Martinez', '02-09-1992', 'Argentina', 'Buenos Aires', 'Mar del Plata', 'miraquetecomo@gmail.com'),
('�ngel', 'Di Mar�a', '14-02-1988', 'Argentina', 'Santa Fe', 'Rosario', 'fideo@gmail.com'),
('Federico', 'Mancuello', '26-03-1989', 'Argentina', 'Santa Fe', 'Reconquista', 'mancu@gmail.com'),
('Alejandro', 'Garnacho', '01-07-2004', 'Espa�a', 'Madrid', 'Madrid', 'bichito@siuuu.com'),
('Tomas', 'Parmo', '08-01-2008', 'Argentina', 'Buenos Aires', 'Capital Federal', 'tomiparmo@gmail.com')

INSERT INTO jugador
VALUES
(1, 170, 72, 'Delantero', 1, 1),
(2, 195, 88, 'Portero', 1, 3),
(3, 178, 75, 'Delantero', 1, 2),
(4, 177, 75, 'Mediocampista', 2, 1),
(5, 180, 80, 'Delantero', 3, 1),
(6, 171, 70, 'Mediocampista', 3, 4)

INSERT INTO entrenamiento
VALUES
('2024-11-12', '2:00:00', '1', 'Entrenamiento Táctico', '', '1'),
('2024-11-12', '2:00:00', '2', 'Entrenamiento Táctico', '', '1'),
('2024-11-12', '2:00:00', '3', 'Entrenamiento Táctico', '', '2')


