create database Gestion_Clubes

use Gestion_Clubes

create table Categoria(
	IdCategoria tinyint primary key identity(1,1),
	nombre varchar (30) not null,
)

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

