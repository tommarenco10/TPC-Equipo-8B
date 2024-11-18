create database Gestion_Clubes
GO
Set Dateformat 'DMY'
use Gestion_Clubes
GO

create table persona(
	IdPersona bigint primary key identity(1,1),
	Nombre varchar(30) not null,
	Apellido varchar(30) not null,
	FechaNacimiento date not null,
	pais varchar(20) not null,
	provincia varchar(30) not null,
	ciudad varchar (30) not null,
	Email varchar (30) not null,
)

GO

alter table persona
add UrlImagen varchar(300)

GO

ALTER TABLE persona
ADD DNI VARCHAR(20) NOT NULL;

create table Categoria(
	IdCategoria tinyint primary key identity(1,1),
	nombre varchar (30) not null,
)

create table EstadoJugador(
	IdEstadoJugador tinyint primary key identity (1,1),
	nombre varchar(30) not null,
)

GO

create table jugador(
	IdJugador int primary key identity(1,1),
	IdPersona bigint foreign key references persona(idpersona),
	Altura tinyint not null check(altura > 150),
	peso decimal not null check (peso > 60),
	posicion varchar (15) not null,
	Idcategoria tinyint foreign key references Categoria (IdCategoria),
	IdEstadoJugador tinyint foreign key references EstadoJugador (IdEstadojugador),
)

create table entrenador(
	IdEntrandor int primary key identity (1,1),
	IdPersona bigint foreign key references persona (IdPersona),
	Rol varchar (30) not null,
)
	
GO

alter table entrenador
	add IdCategoria tinyint foreign key references categoria(IdCategoria)
alter table entrenador
	add FechaContratacion date 

create table socio (
	IdSocio bigint primary key identity(1,1),
	IdPersona bigint foreign key references persona (IdPersona),
)

create table EstadoEntrenamiento(
	IdEstadoEntrenamiento tinyint primary key identity(1,1),
	nombre varchar(30) not null,
)

GO

create table entrenamiento(
	IdEntrenamiento bigint primary key identity (1,1),
	FechaHora datetime not null,
	Duracion time not null,
	IdCategoria tinyint foreign key references Categoria(IdCategoria),
	Descripcion varchar(100) not null,
	Observaciones varchar(100),
	IdEstadoEntrenamiento tinyint foreign key references EstadoEntrenamiento(IdEstadoEntrenamiento),
)

GO

create table Asistencia (
	IdAsistenia bigint primary key identity (1,1),
	IdEntrenamiento bigint foreign key references entrenamiento (IdEntrenamiento),
	IdJugador int foreign key references Jugador (idjugador),
	Asistio bit not null,
	Observaciones varchar (30),
)

create table Reporte(
	IdReporte bigint primary key identity (1,1),
	IdEntrenamiento bigint foreign key references entrenamiento (IdEntrenamiento),
	Descrpcion varchar (100) not null,
	Observaciones varchar (100),
)

GO

create table incidencia (
	IdIncidencia int primary key identity (1,1),
	IdJugador int foreign key references jugador (idjugador),
	IdEstadoJugador tinyint foreign key references  EstadoJugador(IdEstadoJugador),
	Descripcion varchar(100) not null,
	FechaRegistro date not null, 
	FechaResolucion date,
	Estado bit not null
)

create table TipoUsuario(
	IdTipoUsuario tinyint primary key identity(1,1),
	nombre varchar (30) not null,
)

GO

create table Usuario (
	IdUsuario bigint primary key identity(1,1),
	IdPersona bigint foreign key references persona(IdPersona),
	Nombre varchar(50) not null,
	Contraseña varchar(50) not null,
	Email varchar (50) not null,
	IdTipoUsuario tinyint foreign key references TipoUsuario(IdTipoUsuario),
)

GO

create table Notificacion(
	IdNotificacion int primary key identity (1,1),
	IdUsuario bigint foreign key references Usuario (IdUsuario),
	Mensaje varchar (300) not null,
	FechaEnvio date not null,
)



CREATE TABLE Pais (
    IdPais INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Provincia (
    IdProvincia INT PRIMARY KEY IDENTITY(1,1),
    IdPais INT FOREIGN KEY REFERENCES Pais(IdPais),
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Ciudad (
    IdCiudad INT PRIMARY KEY IDENTITY(1,1),
    IdProvincia INT FOREIGN KEY REFERENCES Provincia(IdProvincia),
    Nombre VARCHAR(100) NOT NULL
);