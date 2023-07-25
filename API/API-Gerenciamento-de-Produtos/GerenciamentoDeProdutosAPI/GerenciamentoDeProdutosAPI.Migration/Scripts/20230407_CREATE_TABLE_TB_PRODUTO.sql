﻿CREATE TABLE TB_PRODUTO
(
    ID INT IDENTITY(1,1) NOT NULL,
	NOME VARCHAR(30) NOT NULL,
	DESCRICAO VARCHAR(100) NOT NULL,
	PRECO DECIMAL(10,2) NOT NULL,
	SITUACAO BIT NOT NULL,
	CAT_ID INT NOT NULL,

	CONSTRAINT pk_TB_PRODUTO_ID PRIMARY KEY CLUSTERED(ID),
	CONSTRAINT FK_TB_CATEGORIA_ID FOREIGN KEY(CAT_ID) REFERENCES TB_CATEGORIA
);

GO