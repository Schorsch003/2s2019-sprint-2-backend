CREATE DATABASE T_Peoples;

Use T_Peoples

BEGIN -- DDL

Create table Funcionarios(
	IdFuncionario Int Primary Key Identity,
	Nome Varchar(20) not null,
	Sobrenome Varchar(40) not null
);
Alter table Funcionarios Add DataNascimento Date 
Alter table Funcionarios Alter Column DataNascimento Date Not NUll
Alter table Funcionarios Alter Column DataNascimento Date Not NUll
END

BEGIN -- DML
	Insert into Funcionarios (Nome,Sobrenome) Values ('Catarina','Strada'),
													 ('Tadeu','Vitelli')

Update Funcionarios SET DataNascimento = '0001-01-01'
END

BEGIN -- DQL

	Select * From Funcionarios

END