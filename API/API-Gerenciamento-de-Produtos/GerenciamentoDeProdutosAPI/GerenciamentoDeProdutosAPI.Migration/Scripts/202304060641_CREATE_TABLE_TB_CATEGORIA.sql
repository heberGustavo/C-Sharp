﻿CREATE TABLE TB_CATEGORIA
(
    CAT_ID INT IDENTITY(1,1) NOT NULL,
	CAT_NOME VARCHAR(30) NOT NULL,
	CAT_SITUACAO BIT NOT NULL,

	CONSTRAINT pk_TB_CATEGORIA_ID PRIMARY KEY CLUSTERED(CAT_ID)
);

GO

