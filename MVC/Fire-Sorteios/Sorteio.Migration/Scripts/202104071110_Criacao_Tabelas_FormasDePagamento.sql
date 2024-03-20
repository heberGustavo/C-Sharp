CREATE TABLE dbo.TipoFormaDePagamento
(
	id_tipo_forma_de_pagamento INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(30),

	CONSTRAINT pk_TipoFormaDePagamento PRIMARY KEY CLUSTERED(id_tipo_forma_de_pagamento)
);

CREATE TABLE dbo.FormasDePagamento
(
	id_forma_de_pagamento INT IDENTITY(1,1) NOT NULL,
	nome_banco VARCHAR(100) NOT NULL,
	codigo_banco VARCHAR(20) NOT NULL,
	favorecido VARCHAR(200) NOT NULL,
	cpf VARCHAR(25) NOT NULL,
	agencia VARCHAR(10) NOT NULL,
	conta VARCHAR(20) NOT NULL,
	url_imagem VARCHAR(255) NOT NULL,
	id_tipo_forma_de_pagamento INT NOT NULL,

	CONSTRAINT pk_FormasDePagamento PRIMARY KEY CLUSTERED(id_forma_de_pagamento),
	CONSTRAINT FormasDePagamentoTipoFormaDePagamento FOREIGN KEY(id_tipo_forma_de_pagamento) REFERENCES dbo.TipoFormaDePagamento
);