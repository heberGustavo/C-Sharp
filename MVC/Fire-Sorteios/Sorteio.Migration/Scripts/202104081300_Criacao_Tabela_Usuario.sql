CREATE TABLE Usuario
(
	id_usuario INT IDENTITY(1,1) NOT NULL,
	nome VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT NULL,
	senha VARCHAR(255) NOT NULL,
	celular VARCHAR(20) NOT NULL,
	cpf VARCHAR(20) NOT NULL,
	url_imagem VARCHAR(255),
	id_tipo_usuario INT NOT NULL,

	CONSTRAINT pk_Usuario PRIMARY KEY CLUSTERED (id_usuario),
	CONSTRAINT fk_UsuarioTipoUsuario FOREIGN KEY (id_tipo_usuario) REFERENCES dbo.TipoUsuario
);