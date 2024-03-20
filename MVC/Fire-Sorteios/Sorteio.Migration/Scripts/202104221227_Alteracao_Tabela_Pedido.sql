ALTER TABLE Pedido ADD id_sorteio INT NULL
CONSTRAINT fk_PedidoSorteio FOREIGN KEY (id_sorteio) REFERENCES dbo.Sorteio GO;