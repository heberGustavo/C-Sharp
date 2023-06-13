CREATE TABLE dbo.FuncaoFuncionario
(
	id_funcao_funcionario INT IDENTITY(1,1) NOT NULL,
	nome varchar(40) NOT NULL,
	excluido bit NOT NULL DEFAULT 0,

	constraint pk_FuncaoFuncionario primary key clustered(id_funcao_funcionario)
)
GO 



CREATE TABLE dbo.Funcionario
(
	id_funcionario INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(40) NOT NULL,
	id_funcao_funcionario int NOT NULL,
	diaria decimal(8,2) NULL,
	mensal decimal(8,2) NULL,
	data_contratacao date NULL,
	excluido bit NOT NULL DEFAULT 0,
	
	constraint pk_Funcionario primary key clustered(id_funcionario),
	constraint fk_FuncionarioFuncaoFuncionario FOREIGN KEY(id_funcao_funcionario) REFERENCES dbo.FuncaoFuncionario
)
GO
