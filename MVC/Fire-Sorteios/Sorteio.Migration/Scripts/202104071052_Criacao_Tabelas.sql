CREATE TABLE StatusPedido
(
	id_status_pedido INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(30) NOT NULL,

	CONSTRAINT pk_StatusPedido PRIMARY KEY CLUSTERED(id_status_pedido)
);

CREATE TABLE CategoriaSorteio
(
	id_categoria_sorteio INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(30) NOT NULL,

	CONSTRAINT pk_CategoriaSorteio PRIMARY KEY CLUSTERED(id_categoria_sorteio)
);

CREATE TABLE GaleriaFotos
(
	id_galeria_fotos INT IDENTITY(1,1) NOT NULL,
	url_imagem VARCHAR(255) NOT NULL,

	CONSTRAINT pk_GaleriaFotos PRIMARY KEY CLUSTERED(id_galeria_fotos)
);