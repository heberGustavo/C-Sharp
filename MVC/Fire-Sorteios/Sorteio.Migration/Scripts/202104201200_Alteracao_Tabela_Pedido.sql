ALTER TABLE Pedido DROP CONSTRAINT DF__Pedido__status__75A278F5 GO;
ALTER TABLE Pedido DROP COLUMN status GO;

ALTER TABLE Pedido ADD id_status_pedido INT NOT NULL 
					  CONSTRAINT fk_PedidoStatusPedido FOREIGN KEY (id_status_pedido) REFERENCES dbo.Pedido;