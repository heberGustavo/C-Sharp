CREATE TABLE dbo.TipoUsuario
(
	id_tipo_usuario INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(30) NOT NULL,

	CONSTRAINT pk_TipoUsuario PRIMARY KEY CLUSTERED(id_tipo_usuario)
);

INSERT INTO dbo.TipoUsuario (nome) VALUES ('Administrador'),
										  ('Cliente');