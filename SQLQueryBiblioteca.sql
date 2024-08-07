create database BIBLIOTECAV
GO
USE BIBLIOTECA
GO
CREATE TABLE Libro1
(
IDLibro int identity (10,10) primary key,
Titulo varchar (50) not null unique,
Autor varchar(50)not null,
ISBN int default 'N/A',
AnioPublicacion int not null,
NumeroPaginas int not null
)
create table Usuario1
(
IDUsuario int identity(100,100) primary key,
DocumentoIdentidad int unique not null,
Nombre varchar (50) not null,
Apellido varchar (50)not null,
Telefono int not null
)
create table Prestamo1
(
IDPrestamo int identity (5,5) primary key,
LibroID int references Libro(IdLibro) not null,
UsuarioID int references Usuario(IDUsuario),
FechaPrestamo datetime,
FechaDevolucion datetime,
Estado varchar (50) null
)
