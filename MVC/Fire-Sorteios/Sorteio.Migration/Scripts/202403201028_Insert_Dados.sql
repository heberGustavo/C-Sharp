INSERT INTO dbo.StatusPedido (nome) 
					  VALUES ('Pendente'),
					         ('Pago'),
					         ('test'),
					         ('Cancelado')

INSERT INTO dbo.TipoFormaDePagamento (nome, [status])
							VALUES ('Deposito', 0),
								   ('Pix', 0)