-- ALTER TABLE Pedido DROP CONSTRAINT DF__Pedido__status__75A278F5 GO;
ALTER TABLE Pedido DROP CONSTRAINT fk_PedidoStatusPedido GO;
ALTER TABLE Pedido DROP COLUMN id_status_pedido GO;

ALTER TABLE Pedido ADD id_status_pedido INT NOT NULL 
CONSTRAINT fk_PedidoStatusPedidoo FOREIGN KEY (id_status_pedido) REFERENCES dbo.StatusPedido GO;