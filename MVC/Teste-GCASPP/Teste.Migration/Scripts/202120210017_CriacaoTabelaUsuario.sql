﻿USE ProjetoTesteG

CREATE TABLE TB_FUNCIONARIO 
(
    id INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(30) NOT NULL,
	data_de_nascimento DATE NULL,
	salario DECIMAL(8,2) NULL,

	CONSTRAINT pk_Funcionario PRIMARY KEY CLUSTERED(id)
);

CREATE TABLE TB_FILHO 
(
    id INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(30) NOT NULL,
	data_de_nascimento DATE NULL,
	id_funcionario int,

	CONSTRAINT pk_Filho PRIMARY KEY CLUSTERED(id),
	CONSTRAINT fk_FilhoFuncionario FOREIGN KEY (id_funcionario) REFERENCES dbo.TB_FUNCIONARIO
);