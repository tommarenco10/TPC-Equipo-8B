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
('Tomas', 'Parmo', '2008-01-08', 'Argentina', 'Buenos Aires', 'Capital Federal', 'tomiparmo@gmail.com', 'https://img.a.transfermarkt.technology/portrait/header/1225756-1720466848.png?lm=1' , '20-67890123-4'),
('Paulo', 'Dybala', '1993-11-15', 'Argentina', 'Córdoba', 'Laguna Larga', 'dybala@gmail.com', 'https://img.a.transfermarkt.technology/portrait/header/206050-1725530843.jpg?lm=1', '20-78901234-5'),
('Lautaro', 'Martínez', '1997-08-22', 'Argentina', 'Buenos Aires', 'Bahía Blanca', 'lautaro@gmail.com', 'https://img.a.transfermarkt.technology/portrait/header/406625-1695024988.jpg?lm=1', '20-89012345-6'),

-- España
('Sergio', 'Busquets', '1988-07-16', 'España', 'Barcelona', 'Sabadell', 'busquets@gmail.com', 'https://img.a.transfermarkt.technology/portrait/big/65230-1710079767.jpg?lm=1', '30-12345678-9'),
('Jordi', 'Alba', '1989-03-21', 'España', 'Barcelona', 'Hospitalet de Llobregat', 'alba@gmail.com', 'https://img.a.transfermarkt.technology/portrait/big/69751-1710080407.jpg?lm=1', '30-23456789-0'),
-- Brasil
('Neymar', 'Junior', '1992-02-05', 'Brasil', 'São Paulo', 'Mogi das Cruzes', 'neymar@gmail.com', 'https://img.a.transfermarkt.technology/portrait/big/68290-1697056482.png?lm=1', '40-34567890-1'),
('Casemiro', 'Santos', '1992-02-23', 'Brasil', 'São Paulo', 'São José dos Campos', 'casemiro@gmail.com', 'https://img.a.transfermarkt.technology/portrait/big/16306-1699018876.jpg?lm=1', '40-45678901-2'),

-- Uruguay
('Federico', 'Valverde', '1998-07-22', 'Uruguay', 'Montevideo', 'Montevideo', 'valverde@gmail.com', 'https://img.a.transfermarkt.technology/portrait/big/369081-1731018042.jpg?lm=1', '50-56789012-3'),
('Darwin', 'Nuñez', '1999-06-24', 'Uruguay', 'Artigas', 'Artigas', 'darwin@gmail.com', 'https://img.a.transfermarkt.technology/portrait/big/546543-1681827179.jpg?lm=1', '50-67890123-4');




-- Inserción de jugadores
INSERT INTO jugador (IdPersona, Altura, peso, posicion, IdCategoria, IdEstadoJugador)
VALUES
((SELECT IdPersona FROM persona WHERE Nombre = 'Lionel' AND Apellido = 'Messi'), 170, 72, 'Delantero', 1, 1),
((SELECT IdPersona FROM persona WHERE Nombre = 'Emiliano' AND Apellido = 'Martinez'), 195, 88, 'Portero', 1, 3),
((SELECT IdPersona FROM persona WHERE Nombre = 'Ángel' AND Apellido = 'Di María'), 178, 75, 'Delantero', 1, 2),
((SELECT IdPersona FROM persona WHERE Nombre = 'Federico' AND Apellido = 'Mancuello'), 177, 75, 'Mediocampista', 2, 1),
((SELECT IdPersona FROM persona WHERE Nombre = 'Alejandro' AND Apellido = 'Garnacho'), 180, 80, 'Delantero', 3, 1),
((SELECT IdPersona FROM persona WHERE Nombre = 'Tomas' AND Apellido = 'Parmo'), 171, 70, 'Mediocampista', 3, 4),

((SELECT IdPersona FROM persona WHERE Nombre = 'Paulo' AND Apellido = 'Dybala'), 177, 73, 'Delantero', 1, 1),
((SELECT IdPersona FROM persona WHERE Nombre = 'Lautaro' AND Apellido = 'Martínez'), 175, 78, 'Delantero', 1, 1),

-- España
((SELECT IdPersona FROM persona WHERE Nombre = 'Sergio' AND Apellido = 'Busquets'), 189, 76, 'Mediocampista', 2, 1),
((SELECT IdPersona FROM persona WHERE Nombre = 'Jordi' AND Apellido = 'Alba'), 170, 68, 'Defensor', 2, 2),

-- Brasil
((SELECT IdPersona FROM persona WHERE Nombre = 'Neymar' AND Apellido = 'Junior'), 175, 68, 'Delantero', 3, 1),
((SELECT IdPersona FROM persona WHERE Nombre = 'Casemiro' AND Apellido = 'Santos'), 185, 84, 'Mediocampista', 3, 1),

-- Uruguay
((SELECT IdPersona FROM persona WHERE Nombre = 'Federico' AND Apellido = 'Valverde'), 182, 78, 'Mediocampista', 3, 1),
((SELECT IdPersona FROM persona WHERE Nombre = 'Darwin' AND Apellido = 'Nuñez'), 187, 80, 'Delantero', 3, 1);



-- Inserción de entrenamientos
/*INSERT INTO entrenamiento (FechaHora, Duracion, IdCategoria, Descripcion, Observaciones, IdEstadoEntrenamiento)
VALUES
('2024-11-12 19:00:00:000', '2:00:00', 1, 'Entrenamiento Táctico', '', 1),
('2024-11-12 16:00:00:000', '1:00:00', 2, 'Entrenamiento Técnico', '', 1),
('2024-11-12 14:00:00:000', '3:00:00', 3, 'Entrenamiento Físico', '', 2);*/

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
