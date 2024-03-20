CREATE TABLE Pedido
(
	id_pedido INT IDENTITY(1,1) NOT NULL,
	id_usuario INT NOT NULL,
	data_pedido DATE NOT NULL,
	valor_total DECIMAL(7,2) NOT NULL,
	status BIT NOT NULL DEFAULT 0

	CONSTRAINT pf_Pedido PRIMARY KEY CLUSTERED (id_pedido),
	CONSTRAINT fk_PedidoUsuario FOREIGN KEY (id_usuario) REFERENCES dbo.Usuario
);