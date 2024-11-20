USE Gestion_Clubes;
GO

INSERT INTO Categoria (nombre) VALUES ('Primera'), ('Reserva'), ('Juvenil');
INSERT INTO EstadoJugador (nombre) VALUES ('Disponible'), ('Lesionado'), ('Suspendido'), ('Citado a la Selección');
INSERT INTO EstadoEntrenamiento (nombre) VALUES ('Programado'), ('Cancelado'), ('Finalizado'), ('En Curso'), ('Desconocido');

-- Inserción de personas
INSERT INTO persona (Nombre, Apellido, FechaNacimiento, pais, provincia, ciudad, email, UrlImagen, DNI)
VALUES
('Lionel', 'Messi', '1987-06-24', 'Argentina', 'Santa Fe', 'Rosario', 'messi@gmail.com', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCPcW8CnGQhOEvHKhbBzZeqlLsn9byNDBcNQ&s' , '20-12345678-9'),
('Emiliano', 'Martinez', '1992-09-02', 'Argentina', 'Buenos Aires', 'Mar del Plata', 'miraquetecomo@gmail.com', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIO-NxhnNljF9PGXcomusBUs8yctMPk4ucGQ&s' , '20-23456789-0'),
('Ángel', 'Di María', '1988-02-14', 'Argentina', 'Santa Fe', 'Rosario', 'fideo@gmail.com', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9uL8hbJfev8GPWgOVkvpPO-ZBxugFVOGdHQ&s' , '20-34567890-1'),
('Federico', 'Mancuello', '1989-03-26', 'Argentina', 'Santa Fe', 'Reconquista', 'mancu@gmail.com', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSroL7PhRSJXz3eVvnvLTPrtrccsO750xID8Q&s' , '20-45678901-2'),
('Alejandro', 'Garnacho', '2004-07-01', 'España', 'Madrid', 'Madrid', 'bichito@siuuu.com', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTEvUFtCprHvlSxCv4QUZzxKsZjpTj40I5Xw&s' , '20-56789012-3'),
('Tomas', 'Parmo', '2008-01-08', 'Argentina', 'Buenos Aires', 'Capital Federal', 'tomiparmo@gmail.com', '' , '20-67890123-4');

-- Inserción de jugadores
INSERT INTO jugador (IdPersona, Altura, peso, posicion, IdCategoria, IdEstadoJugador)
VALUES
(1, 170, 72, 'Delantero', 1, 1),
(2, 195, 88, 'Portero', 1, 3),
(3, 178, 75, 'Delantero', 1, 2),
(4, 177, 75, 'Mediocampista', 2, 1),
(5, 180, 80, 'Delantero', 3, 1),
(6, 171, 70, 'Mediocampista', 3, 4);

-- Inserción de entrenamientos
INSERT INTO entrenamiento (FechaHora, Duracion, IdCategoria, Descripcion, Observaciones, IdEstadoEntrenamiento)
VALUES
('2024-11-12', '2:00:00', 1, 'Entrenamiento Táctico', '', 1),
('2024-11-12', '2:00:00', 2, 'Entrenamiento Táctico', '', 1),
('2024-11-12', '2:00:00', 3, 'Entrenamiento Táctico', '', 2);

-- Inserción de tipo de usuario
INSERT INTO TipoUsuario (nombre)
VALUES 
('Administrador'),
('CuerpoTecnico'),
('CuerpoMedico'),
('Socio'),
('Hincha');

-- Inserción de usuarios
INSERT INTO Usuario (IdPersona, Nombre, Contraseña, Email, IdTipoUsuario) 
VALUES 
(1, 'admin', 'admin123', 'admin@example.com', 1),
(2, 'tecnico1', 'tecnico123', 'tecnico@example.com', 2),
(3, 'medico1', 'medico123', 'medico@example.com', 3),
(4, 'socio1', 'socio123', 'socio@example.com', 4),
(5, 'hincha1', 'hincha123', 'hincha@example.com', 5);

-- Inserción de paises, provincias y ciudades
INSERT INTO Pais (Nombre) VALUES ('Argentina'), ('Brasil'), ('Chile');
INSERT INTO Provincia (IdPais, Nombre) 
VALUES 
(1, 'Buenos Aires'), 
(1, 'Córdoba'), 
(2, 'São Paulo'), 
(3, 'Santiago');

INSERT INTO Ciudad (IdProvincia, Nombre) 
VALUES 
(1, 'La Plata'),
(2, 'Córdoba Capital'),
(3, 'São Paulo'),
(4, 'Santiago Centro');
