CREATE TABLE Sorteio
(
	id_sorteio INT IDENTITY(1,1) NOT NULL,
	id_categoria_sorteio INT NOT NULL,
	nome VARCHAR(100) NOT NULL,
	edicao VARCHAR(20) NOT NULL,
	valor DECIMAL(7,2) NOT NULL,
	quantidade_numeros INT NOT NULL,
	descricao_curta VARCHAR(100) NOT NULL,
	descricao_longa VARCHAR(1000) NOT NULL,

	CONSTRAINT pk_Soteio PRIMARY KEY CLUSTERED (id_sorteio),
	CONSTRAINT fk_SorteioCategoriaSorteio FOREIGN KEY (id_categoria_sorteio) REFERENCES dbo.CategoriaSorteio
);
GO;

ALTER TABLE GaleriaFotos ADD id_sorteio INT NOT NULL
						CONSTRAINT fk_GaleriaFotosSorteio FOREIGN KEY (id_sorteio) REFERENCES dbo.Sorteio

GO;