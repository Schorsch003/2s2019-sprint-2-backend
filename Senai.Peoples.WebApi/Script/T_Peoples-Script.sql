CREATE DATABASE T_Peoples;

Use T_Peoples

BEGIN -- DDL

Create table Funcionarios(
	IdFuncionario Int Primary Key Identity,
	Nome Varchar(20) not null,
	Sobrenome Varchar(40) not null
);

END

BEGIN -- DML
	Insert into Funcionarios (Nome,Sobrenome) Values ('Catarina','Strada'),
													 ('Tadeu','Vitelli')


END

BEGIN -- DQL

	Select * From Funcionarios

END