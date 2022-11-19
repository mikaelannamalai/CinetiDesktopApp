
USE MASTER
GO

DROP DATABASE IF EXISTS MVVMLoginDb
GO

CREATE DATABASE MVVMLoginDb
GO

use MVVMLoginDb
go
create table [User]
(
	Id UNIQUEIDENTIFIER primary Key default NEWID(),
	Username nvarchar (50) unique not null,
	[Password] nvarchar (100) not null,
	[Name]nvarchar (50) not null,
	LastName nvarchar (50) not null,
	Email  nvarchar (100) unique not null
)
go
insert into [User] values (NEWID(), 'admin', 'admin','RJ Code','Advance', 'rjcode@gmail.com')
insert into [User] values (NEWID(), 'jazz', 'qwerty','Jazzlyn Yarely','Sebhant', 'jazz.m@gmail.com')
insert into [User] values (NEWID(), 'keni', 'asdfg','Kenisha Aira','Montero', 'keni.m@gmail.com')
insert into [User] values (NEWID(), 'tanya', 'tanya','Tanya','Sevchencka', 't.sev@gmail.com')
insert into [User] values (NEWID(), 'chitao', 'cheeto','Chi-Tao','Li', 'chitao@gmail.com')
go

create table [Client]
(
	Id UNIQUEIDENTIFIER primary Key default NEWID(),
	Name nvarchar (150) not null,
	Language nvarchar (50) not null,
	DOB DATE,
	email nvarchar (50),
	address  nvarchar (300),
	phone NVARCHAR(20)


)
go
insert into [Client] values (NEWID(), 'Adin Ashby', 'English','1988-11-01','aa@adinashby.com', '1234 De la Montagne, Montreal QC','514 885 9585')
insert into [Client] values (NEWID(), 'Jaina Sheth', 'French','1998-02-01','js@vanier.ca', '5-6687 Rue Mcgill, Montreal QC','514 555 0375')
insert into [Client] values (NEWID(), 'Liam Peukert', 'English','2001-06-15','lpeuk@vanier.ca', '55 Avenue de Pins, Montreal QC','514 205 1287')
insert into [Client] values (NEWID(), 'Sakku Rama', 'English','1982-11-02','sakkurama@vanier.ca', '254 Ave Dollard, Laval QC','514 205 1222')
insert into [Client] values (NEWID(), 'Talla Rao', 'English','1960-06-15','tallar@vanier.ca', '154 1re Ave, Montreal QC','514 558 6687')
insert into [Client] values (NEWID(), 'Mohammed Mehdi', 'English','1965-08-15','mehdi@vanier.ca', '7788 blvd Acadie, Montreal QC','514 685 2217')
go










select *from [User]