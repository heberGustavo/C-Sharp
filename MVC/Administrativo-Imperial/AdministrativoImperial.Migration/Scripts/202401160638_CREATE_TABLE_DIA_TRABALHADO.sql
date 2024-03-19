﻿IF NOT EXISTS(SELECT * FROM SYS.tables WHERE NAME = 'TB_DIA_TRABALHADO')
BEGIN
	CREATE TABLE dbo.TB_DIA_TRABALHADO
	(
		  DitId INT IDENTITY(1,1) NOT NULL
		, DitData DATETIME NOT NULL
		, FunId INT NOT NULL
		, ObrId INT NOT NULL
	
		, CONSTRAINT PK_TB_DIA_TRABALHADO_DIT_ID PRIMARY KEY(DitId)
		, CONSTRAINT FK_TB_DIA_TRABALHADO_TB_FUNCIONARIO_FUN_ID FOREIGN KEY(FunId) REFERENCES TB_FUNCIONARIO
		, CONSTRAINT FK_TB_DIA_TRABALHADO_TB_OBRA_OBR_ID FOREIGN KEY(ObrId) REFERENCES TB_OBRA
	)
END
GO