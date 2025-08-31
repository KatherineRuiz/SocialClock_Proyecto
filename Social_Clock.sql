CREATE DATABASE Social_Clock
GO
USE Social_Clock;
GO


Create table Rol (
idRol int identity (1,1) primary key,
nombreRol varchar (25) not null
);
Go

Create table Proyecto (
idProyecto int identity (1,1) primary key,
nombreProyecto varchar (40) not null,
estadoProyecto bit default 0 not null
);
Go

Create table Especialidad (
idEspecialidad int identity (1,1) primary key,
nombreEspecialidad varchar (40) not null 
);
Go

Create table NivelAcademico (
idNivelAcademico int identity (1,1) primary key,
nombreNivel varchar (30) not null
);
Go


Create table Seccion(
idSeccion int identity (1,1) primary key,
nombreSeccion varchar(20) not null
);
Go

-- Aqui terminan las tablas padre --

Create table Esp_Niv_Sec(
idEsp_Niv_Sec int identity (1,1) primary key,
id_Especialidad int not null,
id_NivelAcademico int not null,
id_Seccion int not null
constraint fkEspecialidad foreign key (id_Especialidad)
references Especialidad (idEspecialidad) on delete cascade,
constraint fkSeccion foreign key (id_Seccion)
references Seccion (idSeccion) on delete cascade,
constraint fkNivelAcademico foreign key (id_NivelAcademico)
references NivelAcademico (idNivelAcademico) on delete cascade
);
Go

Create table Usuario (
idUsuario int identity (1,1) primary key,
nombreUsuario varchar (30) unique not null,
clave varchar (10) unique not null,
estadoUsuario bit default 0 not null,
id_Rol int not null,
constraint fkRol foreign key (id_Rol)
references Rol(idRol) on delete cascade,
);
Go


create table Evento (
idEvento int identity (1,1) primary key,
nombreEvento varchar(40) not null,
descripcionEvento varchar(200) not null,
fechaEvento date not null,
fechaHoraPublicacion datetime not null,
idUsuario int not null,
constraint fkUsuario foreign key (idUsuario)
references Usuario(idUsuario) on delete cascade,
);
Go

Create table Estudiante (
idEstudiante int identity (1,1) primary key,
nombreEstudiante varchar (50) not null,
carnet varchar(8) unique not null,
nie varchar(8) default 0,
estadoEstudiante bit default 0 not null,
id_Proyecto int not null,
id_EspNivSec int not null,
constraint fkProyecto foreign key (id_Proyecto)
references Proyecto(idProyecto) on delete cascade,
constraint fkEspNivSec foreign key (id_EspNivSec)
references Esp_Niv_Sec(idEsp_Niv_Sec) on delete cascade,
);
Go

Create table BitacoraSocial (
idBitacora int identity (1,1) primary key,
registroHoras int not null,
descripcion nvarchar(100) not null,
fechaBitacora date not null,
idEstudiante int not null,
constraint fkEstudiante foreign key(idEstudiante)
references Estudiante(idEstudiante) on delete cascade,
);
Go

--Insersiones
insert into Rol values
('Administrador'),
('Colaborador');

insert into Proyecto values
('Stant Cultural',0),
('Psicología',0);

insert into Proyecto values
('OPV asistencia',0),
('Vida comunitaria',0),
('Mural de enfermería',0),
('Tutor de inglés',0),
('Tutor de software',0),
('Tutor de arquitectura',0),
('Orquesta ITR',0),
('Comunicaciones',0),
('Desafío matemático',0),
('Evangelización',0),
('Gestion de mobiliario',0),
('Logística',0),
('Infraestructura tecnológica',0),
('Seguridad y emergencia',0),
('Protocolo',0),
('Cultural',0),
('Técnico científico',0),
('Seleccion deportiva',0),
('Coreografía RICO',0),
('Apoyo en psicología',0),
('Teatro logística',0),
('Escuela prof.Sandor',0),
('Teletón',0),
('Externo',0);

insert into Especialidad values
('Desarrollo de software'),
('Diseño gráfico'),
('Electromecánica'),
('Arquitectura'),
('Administrativo contable'),
('Electrónica'),
('Energías renovables'),
('Mantenimiento Automotriz');

insert into Seccion values 
('A-1'),
('A-2'),
('A-3'),
('A-4'),
('A-5'),
('B-1'),
('B-2'),
('B-3'),
('B-4');

insert into NivelAcademico values
('Primer año'),
('Segundo año'),
('Tercer año');

insert into Esp_Niv_Sec values 
(1,1,1),
(1,1,2);

--Para software
insert into Esp_Niv_Sec values 
(1,1,3),
(1,1,4),
(1,1,5),
(1,1,6),
(1,1,7),
(1,1,8),
(1,1,9),
(1,2,1),
(1,2,2),
(1,2,3),
(1,2,4),
(1,2,6),
(1,2,7),
(1,2,8),
(1,2,9),
(1,3,1),
(1,3,2),
(1,3,3),
(1,3,4),
(1,3,6),
(1,3,7),
(1,3,8),
(1,3,9);

--Para Diseño Grafico
insert into Esp_Niv_Sec values 
(2,1,1),
(2,1,2),
(2,1,3),
(2,1,4),
(2,1,5),
(2,1,6),
(2,1,7),
(2,1,8),
(2,1,9),
(2,2,1),
(2,2,2),
(2,2,3),
(2,2,4),
(2,2,6),
(2,2,7),
(2,2,8),
(2,2,9),
(2,3,1),
(2,3,2),
(2,3,3),
(2,3,4),
(2,3,6),
(2,3,7),
(2,3,8),
(2,3,9);

--Para EMCA
insert into Esp_Niv_Sec values 
(3,1,1),
(3,1,2),
(3,1,3),
(3,1,4),
(3,1,5),
(3,1,6),
(3,1,7),
(3,1,8),
(3,1,9),
(3,2,1),
(3,2,2),
(3,2,3),
(3,2,4),
(3,2,6),
(3,2,7),
(3,2,8),
(3,2,9),
(3,3,1),
(3,3,2),
(3,3,3),
(3,3,4),
(3,3,6),
(3,3,7),
(3,3,8),
(3,3,9);

--Para Arquitectura
insert into Esp_Niv_Sec values 
(4,1,1),
(4,1,2),
(4,1,3),
(4,1,4),
(4,1,5),
(4,1,6),
(4,1,7),
(4,1,8),
(4,1,9),
(4,2,1),
(4,2,2),
(4,2,3),
(4,2,4),
(4,2,6),
(4,2,7),
(4,2,8),
(4,2,9),
(4,3,1),
(4,3,2),
(4,3,3),
(4,3,4),
(4,3,6),
(4,3,7),
(4,3,8),
(4,3,9);

--Para Administracion Contable
insert into Esp_Niv_Sec values 
(5,1,1),
(5,1,2),
(5,1,3),
(5,1,4),
(5,1,5),
(5,1,6),
(5,1,7),
(5,1,8),
(5,1,9),
(5,2,1),
(5,2,2),
(5,2,3),
(5,2,4),
(5,2,6),
(5,2,7),
(5,2,8),
(5,2,9),
(5,3,1),
(5,3,2),
(5,3,3),
(5,3,4),
(5,3,6),
(5,3,7),
(5,3,8),
(5,3,9);

--Para ECA
insert into Esp_Niv_Sec values 
(6,1,1),
(6,1,2),
(6,1,3),
(6,1,4),
(6,1,5),
(6,1,6),
(6,1,7),
(6,1,8),
(6,1,9),
(6,2,1),
(6,2,2),
(6,2,3),
(6,2,4),
(6,2,6),
(6,2,7),
(6,2,8),
(6,2,9),
(6,3,1),
(6,3,2),
(6,3,3),
(6,3,4),
(6,3,6),
(6,3,7),
(6,3,8),
(6,3,9);

--Para Enerías renobables
insert into Esp_Niv_Sec values 
(7,1,1),
(7,1,2),
(7,1,3),
(7,1,4),
(7,1,5),
(7,1,6),
(7,1,7),
(7,1,8),
(7,1,9),
(7,2,1),
(7,2,2),
(7,2,3),
(7,2,4),
(7,2,6),
(7,2,7),
(7,2,8),
(7,2,9),
(7,3,1),
(7,3,2),
(7,3,3),
(7,3,4),
(7,3,6),
(7,3,7),
(7,3,8),
(7,3,9);

--Para Automotriz
insert into Esp_Niv_Sec values 
(8,1,1),
(8,1,2),
(8,1,3),
(8,1,4),
(8,1,5),
(8,1,6),
(8,1,7),
(8,1,8),
(8,1,9),
(8,2,1),
(8,2,2),
(8,2,3),
(8,2,4),
(8,2,6),
(8,2,7),
(8,2,8),
(8,2,9),
(8,3,1),
(8,3,2),
(8,3,3),
(8,3,4),
(8,3,6),
(8,3,7),
(8,3,8),
(8,3,9);


insert into Usuario values
('Ana Cecilia Ordoñez',544854,0,1),
('Mirna Espinoza Anzora',277423,0,2);

insert into Usuario values 
('Eliseo_Crisostomo',654697,0,2),
('Dina_Alfaro',674459,0,2),
('Iris_Chavez',255485,0,2),
('Roxana_Rodríguez',467145,0,2),
('Cristian_Faguada',654657,0,2),
('Jenni_Carpio',485487,0,2),
('Josue_Guinea',24586,0,2),
('Ricardo_Paz',658945,0,2),
('Rodrigo_Duenas',744464,0,2),
('Eduardo Barrera', 648547,0,2),
('Karina_Hernandez',625148,0, 1),
('Veronica_Sanchez',8456321,0,2),
('Yolanda_Canales',214585,0,2);

insert into Evento values 
('Día de la madre', 'Los estudiantes de cultural realizarán la decoración', '2025/5/5','2025/5/1 10:30:00', 1),
('Retiro de padres', 'Los estudiantes de protocolo ordenaran las sillas', '2025/7/19','2025/7/10 11:30:00', 2);

insert into Estudiante values
('Katherine Andrea Ruiz Bonilla','20250409','665464',0,1,2),
('Abraham Isaac Rodríguez Velasquez','20230129','1651653',0,2,1),
('Noe David Saravia Siliezar',20250065,'549875',0,9,1),
('Rodrigo Alejandro Tisnado corpeno',20250488,'',0,11,1);

insert into Estudiante values
('Ariela Melissa Barahona Carranza',20230449,'448559',0,12,1),
('Estefany Gabriela Garcia Mina', 20220212,'654855',0,14,2),
('Cesar Eduardo Orozco Rivas',20220056,'154258',0,3,2),
('Mateo Amilcar Gonzalez Cardoza',20240574,'',0,9,1),
('Ever Dasahev Henriquez Abarca',20250247,'543226',0,5,1),
('Gabriela Michelle Mayen Gonzales',20220423,'',0,7,1),
('Camila Celeste Fabian Echegoyen',20230746,'',0,15,1),
('Julio Wilfredo Flores Cuevas',20250155,'547856',0,11,2),
('Rudy Enrique Pineda Azucar', 20220181,'',0,6,2),
('Juan Pablo Contreras Chavez',20250248,'654867',0,12,1),
('Ryan Alexander Baxter Segovia',20230485,'',0,13,1);
insert into Estudiante values
('Ariela Melissa Barahona Carranza',20237449,'448559',0,12,1);

insert into BitacoraSocial values 
(50, 'Creación de stand cultural', '2025/8/26', 1),	
(40, 'Participación en obra de teatro','2025/4/26', 2);

delete Estudiante where idEstudiante=21

--Primer Inner Join--

Select idEsp_Niv_Sec, Especialidad.nombreEspecialidad As [Especialidad], NivelAcademico.nombreNivel As [Nivel académico], Seccion.nombreSeccion As [Seccion]
from Esp_Niv_Sec
INNER JOIN
NivelAcademico on Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico
INNER JOIN
Seccion on Esp_Niv_Sec.id_Seccion = Seccion.idSeccion
INNER JOIN
Especialidad on Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad


--Segundo Inner Join--

select Usuario.idUsuario, Usuario.nombreUsuario As [Usuario], Rol.nombreRol As [Rol], clave As [Contraseña], CASE estadoUsuario
when 0 then 'ACTIVO'
when 1 then 'INACTIVO'
END As [Estado]
from Usuario
inner join
Rol On Usuario.id_Rol = Rol.idRol

--Tercer Inner Join--

select idEvento, nombreEvento As [Evento], descripcionEvento As [Descripcion], fechaEvento As [Fecha del Evento],
fechaHoraPublicacion As [Hora de publicacion], Usuario.nombreUsuario As [Usuario]
from Evento
inner join
Usuario on Evento.idUsuario = Usuario.idUsuario

-- Cuarto inner join --

select  carnet As [Carnet], nombreEstudiante As [Nombre],Especialidad.nombreEspecialidad As [Especialidad],
NivelAcademico.nombreNivel As [Nivel académico], Seccion.nombreSeccion As [Seccion], nie As [NIE], CASE estadoEstudiante
when 0 then 'ACTIVO'
when 1 then 'INACTIVO'
END As [Estado],
Proyecto.nombreProyecto As [Proyecto], BitacoraSocial.registroHoras As [No. Horas]
from Estudiante 
LEFT JOIN 
BitacoraSocial on BitacoraSocial.idEstudiante = Estudiante.idEstudiante
INNER JOIN
Proyecto on Estudiante.id_Proyecto = Proyecto.idProyecto
INNER JOIN
Esp_Niv_Sec on Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec
INNER JOIN
Especialidad on Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad
INNER JOIN 
NivelAcademico on Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico
INNER JOIN 
Seccion on Esp_Niv_Sec.id_Seccion = Seccion.idSeccion where NivelAcademico.idNivelAcademico =  1;

select  nombreProyecto As Proyecto, CASE estadoProyecto
when 0 then 'ACTIVO'
when 1 then 'INACTIVO'
END As [Estado]
from Proyecto 
 

SELECT idEsp_Niv_Sec FROM Esp_Niv_Sec WHERE id_Especialidad = 6 AND id_NivelAcademico = 3 AND id_Seccion = 7
 
 SELECT idEsp_Niv_Sec FROM Esp_Niv_Sec WHERE id_Especialidad = 1 AND id_NivelAcademico = 1 AND id_Seccion = 1;

create view DatosEstudiantesPrimero as
select  carnet As [Carnet], nombreEstudiante As [Nombre],Especialidad.nombreEspecialidad As [Especialidad],
NivelAcademico.nombreNivel As [Nivel académico], Seccion.nombreSeccion As [Seccion], nie As [NIE], CASE estadoEstudiante
when 0 then 'ACTIVO'
when 1 then 'INACTIVO'
END As [Estado],
Proyecto.nombreProyecto As [Proyecto], sum(registroHoras) As [No. Horas]
from Estudiante 
LEFT JOIN 
BitacoraSocial on BitacoraSocial.idEstudiante = Estudiante.idEstudiante
INNER JOIN
Proyecto on Estudiante.id_Proyecto = Proyecto.idProyecto
INNER JOIN
Esp_Niv_Sec on Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec
INNER JOIN
Especialidad on Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad
INNER JOIN 
NivelAcademico on Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico
INNER JOIN 
Seccion on Esp_Niv_Sec.id_Seccion = Seccion.idSeccion where NivelAcademico.idNivelAcademico = 1 group by Estudiante.idEstudiante, carnet, nombreEstudiante, 
Especialidad.nombreEspecialidad, NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;

select * from DatosEstudiantesPrimero
