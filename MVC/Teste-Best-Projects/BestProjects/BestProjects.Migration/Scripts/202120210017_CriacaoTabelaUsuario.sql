CREATE TABLE Usuario 
(
    id_usuario INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(30) NOT NULL,
	telefone VARCHAR(30) NOT NULL,
	celular VARCHAR(30) NULL,

	CONSTRAINT pk_Usuario PRIMARY KEY CLUSTERED(id_usuario)
);

GO;