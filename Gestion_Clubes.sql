create database Gestion_Clubes

use Gestion_Clubes

create table Categoria(
	IdCategoria tinyint primary key identity(1,1),
	nombre varchar (30) not null,
)

create table paises(
	IdPais tinyint primary key identity (1,1),
	Nombre Varchar (30) not null,
)

create table persona(
	IdPersona bigint primary key identity(1,1),
	Nombre varchar(30) not null,
	Apellido varchar(30) not null,
	FechaNacimiento date not null,
	LugarNacimiento varchar(50) not null,
	Email varchar (30) not null,
)

create table EstadoJugador(
	IdEstadoJugador tinyint primary key identity (1,1),
	nombre varchar(30) not null,
)

create table jugador(
	IdJugador int primary key identity(1,1),
	IdPersona bigint foreign key references persona(idpersona),
	Altura tinyint not null check(altura > 150),
	peso decimal not null check (peso > 60),
	posicion varchar (15) not null,
	Idcategoria tinyint foreign key references Categoria (IdCategoria),
	IdEstadoJugador tinyint foreign key references EstadoJugador (IdEstadojugador),
)

create table nacionalidad(
	IdPais tinyint foreign key references paises(IdPais),
	IdPersona bigint foreign key references persona (IdPersona),
	primary key (Idpais, Idpersona),
)

create table entrenador(
	IdEntrandor int primary key identity (1,1),
	IdPersona bigint foreign key references persona (IdPersona),
	Rol varchar (30) not null,
)

create table socio (
	IdSocio bigint primary key identity(1,1),
	IdPersona bigint foreign key references persona (IdPersona),
)

create table EstadoEntrenamiento(
	IdEstadoEntrenamiento tinyint primary key identity(1,1),
	nombre varchar(30) not null,
)

create table entrenamiento(
	IdEntrenamiento bigint primary key identity (1,1),
	FechaHora datetime not null,
	Duracion time not null,
	IdCategoria tinyint foreign key references Categoria(IdCategoria),
	Descripcion varchar(100) not null,
	Observaciones varchar(100),
	IdEstadoEntrenamiento tinyint foreign key references EstadoEntrenamiento(IdEstadoEntrenamiento),
)

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

create table TipoIncidencia(
	IdTipoIncidencia tinyint primary key identity (1,1),
	nombre varchar(30) not null,
)

create table incidencia (
	IdIncidencia int primary key identity (1,1),
	IdJugador int foreign key references jugador (idjugador),
	Descripcion varchar(100) not null,
	FechaRegistro date not null, 
	FechaResolucion date,
	IdTipoIncidencia tinyint foreign key references  tipoIncidencia(IdTipoIncidencia),
	Estado bit not null,
)

create table TipoUsuario(
	IdTipoUsuario tinyint primary key identity(1,1),
	nombre varchar (30) not null,
)

create table Usuario (
	IdUsuario bigint primary key identity(1,1),
	Nombre varchar(50) not null,
	Contraseña varchar(50) not null,
	Email varchar (50) not null,
	IdTipoUsuario tinyint foreign key references TipoUsuario(IdTipoUsuario),
)

create table Notificacion(
	IdNotificacion int primary key identity (1,1),
	IdUsuario bigint foreign key references Usuario (IdUsuario),
	Mensaje varchar (300) not null,
	FechaEnvio date not null,
)

Select J.IdJugador, P.Nombre, P.Apellido, J.Altura, J.Peso, J.Posicion From Jugador J Left Join Persona P on J.IdPersona = P.IdPersona



