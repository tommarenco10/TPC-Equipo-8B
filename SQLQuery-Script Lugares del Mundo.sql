USE Gestion_Clubes;
GO
INSERT INTO Pais (Nombre)
VALUES 
('Argentina'), 
('Brasil'), 
('Uruguay'), 
('Colombia'),
('España');



-- Provincias de Argentina
INSERT INTO Provincia (IdPais, Nombre)
VALUES 
((SELECT IdPais FROM Pais WHERE Nombre = 'Argentina'), 'Buenos Aires'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Argentina'), 'Córdoba'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Argentina'), 'Santa Fe'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Argentina'), 'Mendoza');

-- Ciudades de Argentina
INSERT INTO Ciudad (IdProvincia, Nombre)
VALUES 
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Buenos Aires'), 'La Plata'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Buenos Aires'), 'Mar del Plata'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Córdoba'), 'Córdoba'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Santa Fe'), 'Rosario'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Mendoza'), 'Mendoza');



-- Provincias de Brasil
INSERT INTO Provincia (IdPais, Nombre)
VALUES 
((SELECT IdPais FROM Pais WHERE Nombre = 'Brasil'), 'São Paulo'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Brasil'), 'Río de Janeiro'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Brasil'), 'Minas Gerais'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Brasil'), 'Bahía');

-- Ciudades de Brasil
INSERT INTO Ciudad (IdProvincia, Nombre)
VALUES 
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'São Paulo'), 'São Paulo'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Río de Janeiro'), 'Río de Janeiro'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Minas Gerais'), 'Belo Horizonte'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Bahía'), 'Salvador');




-- Provincias de Uruguay
INSERT INTO Provincia (IdPais, Nombre)
VALUES 
((SELECT IdPais FROM Pais WHERE Nombre = 'Uruguay'), 'Montevideo'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Uruguay'), 'Canelones'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Uruguay'), 'Maldonado');

-- Ciudades de Uruguay
INSERT INTO Ciudad (IdProvincia, Nombre)
VALUES 
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Montevideo'), 'Montevideo'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Canelones'), 'Ciudad de la Costa'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Maldonado'), 'Punta del Este');



-- Provincias de Colombia
INSERT INTO Provincia (IdPais, Nombre)
VALUES 
((SELECT IdPais FROM Pais WHERE Nombre = 'Colombia'), 'Cundinamarca'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Colombia'), 'Antioquia'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Colombia'), 'Valle del Cauca'),
((SELECT IdPais FROM Pais WHERE Nombre = 'Colombia'), 'Bolívar');

-- Ciudades de Colombia
INSERT INTO Ciudad (IdProvincia, Nombre)
VALUES 
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Cundinamarca'), 'Bogotá'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Antioquia'), 'Medellín'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Valle del Cauca'), 'Cali'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Bolívar'), 'Cartagena');


-- Provincias de España
INSERT INTO Provincia (IdPais, Nombre)
VALUES 
((SELECT IdPais FROM Pais WHERE Nombre = 'España'), 'Madrid'),
((SELECT IdPais FROM Pais WHERE Nombre = 'España'), 'Barcelona'),
((SELECT IdPais FROM Pais WHERE Nombre = 'España'), 'Sevilla'),
((SELECT IdPais FROM Pais WHERE Nombre = 'España'), 'Valencia'),
((SELECT IdPais FROM Pais WHERE Nombre = 'España'), 'Bilbao');

-- Ciudades de España
INSERT INTO Ciudad (IdProvincia, Nombre)
VALUES 
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Madrid'), 'Madrid'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Barcelona'), 'Barcelona'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Sevilla'), 'Sevilla'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Valencia'), 'Valencia'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Bilbao'), 'Bilbao');


-- Más ciudades en las provincias de España
INSERT INTO Ciudad (IdProvincia, Nombre)
VALUES 
-- Provincia: Madrid
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Madrid'), 'Alcalá de Henares'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Madrid'), 'Getafe'),

-- Provincia: Barcelona
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Barcelona'), 'Hospitalet de Llobregat'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Barcelona'), 'Badalona'),

-- Provincia: Sevilla
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Sevilla'), 'Dos Hermanas'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Sevilla'), 'Alcalá de Guadaíra'),

-- Provincia: Valencia
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Valencia'), 'Torrent'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Valencia'), 'Gandía'),

-- Provincia: Bilbao
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Bilbao'), 'Barakaldo'),
((SELECT IdProvincia FROM Provincia WHERE Nombre = 'Bilbao'), 'Getxo');





