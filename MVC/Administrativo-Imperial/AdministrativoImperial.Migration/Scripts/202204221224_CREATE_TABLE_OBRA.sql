CREATE TABLE dbo.Obra
(
	id_obra INT IDENTITY(1,1) NOT NULL,
	apelido VARCHAR(40) NOT NULL,
	data_inicio date NOT NULL,
	data_fim date NULL DEFAULT NULL,
	endereco VARCHAR(100) NULL,
	orcamento decimal(12,2) NULL,
	excluido bit NOT NULL DEFAULT 0,
	
	constraint pk_Obra primary key clustered(id_obra)
)
GO