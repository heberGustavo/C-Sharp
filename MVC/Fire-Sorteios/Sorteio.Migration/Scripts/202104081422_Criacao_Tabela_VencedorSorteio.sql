CREATE TABLE VencedorSorteio
(
	id_vencedor_sorteio INT IDENTITY(1,1) NOT NULL,
	id_sorteio INT NOT NULL,
	id_usuario INT NOT NULL,
	numero_sorteado INT NOT NULL,
	data_sorteio DATE NOT NULL,

	CONSTRAINT pk_VencedorSorteio PRIMARY KEY CLUSTERED (id_vencedor_sorteio),
	CONSTRAINT fk_VencedorSorteioSorteio FOREIGN KEY (id_sorteio) REFERENCES dbo.Sorteio,
	CONSTRAINT fk_VencedorSorteioUsuario FOREIGN KEY (id_usuario) REFERENCES dbo.Usuario
);