﻿IF NOT EXISTS(SELECT * FROM SYS.tables WHERE NAME = 'TB_MATERIAL')
BEGIN
	CREATE TABLE dbo.TB_MATERIAL
	(
		  MtrId INT IDENTITY(1,1) NOT NULL
		, ObrId INT NOT NULL --FK
		, MtrNome VARCHAR(40) NOT NULL
		, MtrDescricao VARCHAR(200) NULL
		, MtrValor DECIMAL(12,2) NOT NULL
		, MtrDataCompra DATETIME NOT NULL
	
		, CONSTRAINT PK_TB_MATERIAL_MTR_ID PRIMARY KEY(MtrId)
		, CONSTRAINT FK_TB_MATERIAL_TB_OBRA_OBR_ID FOREIGN KEY(ObrId) REFERENCES TB_OBRA
	)
END
GO