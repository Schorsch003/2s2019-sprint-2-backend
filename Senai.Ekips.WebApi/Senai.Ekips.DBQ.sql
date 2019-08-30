CREATE DATABASE T_Ekpis

Use T_Ekpis

BEGIN -- DDL

Create Table Permissoes(
	IdPermissao Int Primary Key Identity,
	Nome Varchar(40) not null unique
);

Create Table Usuarios(
	IdUsuario Int Primary Key Identity,
	Email Varchar(50) not null unique,
	Senha Varchar(50) not null,
	IdPermissao Int Foreign Key References Permissoes (IdPermissao)
);

Drop Table Usuarios

Create Table Departamentos (
	IdDepartamento Int Primary Key Identity,
	Nome Varchar(40)
);

Create Table Cargos (
	IdCargo Int Primary Key Identity,
	Nome Varchar(40)
)

Alter Table Cargos Add Ativo Bit Not Null Default(0)

Create Table Funcionarios(
	IdFuncionario Int Primary Key Identity,
	Nome Varchar(50) not null,
	Cpf Varchar(14) not null Unique,
	Salario Money not null,
	IdDepartamento Int Foreign Key References Departamentos (IdDepartamento),
	IdCargo Int Foreign Key References Cargos (IdCargo),
	IdUsuario Int Foreign Key References Usuarios (IdUsuario)
);

END

BEGIN -- DML

Select * From Permissoes
Insert into Permissoes Values ('ADMINISTRADOR'),('COMUM ')

Select * From Usuarios
Insert into Usuarios (Email,Senha,IdPermissao) Values ('gabriel@email.com','123456789',1)

Select * From Departamentos
Insert into Departamentos Values ('Desenvolvimento')

Select * From Cargos
Insert into Cargos Values ('Desenvolvedor',1)

Select * From Funcionarios
Insert into Funcionarios (Nome,Cpf,Salario,IdDepartamento,IdCargo) Values ('Gabriel Schorsch','495.493.038-37','3500.00',1,1)

END 

BEGIN --DQL
	Select * From Permissoes
	Select * From Usuarios
	Select * From Departamentos
	Select * From Cargos
	Select * From Funcionarios
END
