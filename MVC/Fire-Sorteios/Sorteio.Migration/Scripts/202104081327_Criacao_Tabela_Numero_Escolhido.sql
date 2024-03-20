CREATE TABLE NumeroEscolhido
(
	id_numero_escolhido INT IDENTITY(1,1) NOT NULL,
	id_pedido INT NOT NULL,
	numero INT NOT NULL,

	CONSTRAINT pk_NumeroEscolhido PRIMARY KEY CLUSTERED (id_numero_escolhido),
	CONSTRAINT fk_NumeroEscolhidoPedido FOREIGN KEY (id_pedido) REFERENCES dbo.Pedido
);