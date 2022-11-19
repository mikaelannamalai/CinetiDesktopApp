
go
use MVVMLoginDb
go
drop table if exists Client
go



create table [Client]
(
	Id nVARCHAR(50),
	[Name]nvarchar (50) not null,
	[Language]nvarchar (50) not null,
	[gender]nvarchar (50) not null,
	[description]nvarchar (50) not null,
	dob Date,
	CONSTRAINT PK_CLIENT PRIMARY KEY(Id),
);

create table [Client_Email]
(
	Id nVARCHAR(50),
	Email  nvarchar (100)
	CONSTRAINT PK_EMAIL PRIMARY KEY(Id,Email),
    CONSTRAINT FK_EMAIL_01 FOREIGN KEY (Id) REFERENCES Client(Id)
);


create table [Client_Address]
(
	Id nVARCHAR(50),
	Address_tier nVARCHAR(50),
	STREET_NAME VARCHAR(50),
    STREET_NO INT,
    UNIT_NO INT,
    CITY VARCHAR(50),
    PROVINCE_STATE VARCHAR(50),
    POSTAL_CODE VARCHAR(50),
	CONSTRAINT PK_CL_AD PRIMARY KEY(Id,Address_tier),
	CONSTRAINT FK_CL_AD_01 FOREIGN KEY (Id) REFERENCES Client(Id)
);

create table [Client_Relationship]
(
	Id nVARCHAR(50),
	Relation nVARCHAR(50),
	RefId nVARCHAR(50),
	CONSTRAINT PK_CL_RL PRIMARY KEY(Id),
	CONSTRAINT FK_CL_RL_01 FOREIGN KEY (Id) REFERENCES Client(Id)
);


go