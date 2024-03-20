ALTER TABLE dbo.Usuario DROP COLUMN cpf GO
ALTER TABLE dbo.Usuario DROP COLUMN url_imagem GO
ALTER TABLE dbo.Usuario DROP COLUMN cep GO
ALTER TABLE dbo.Usuario DROP COLUMN logadouro GO
ALTER TABLE dbo.Usuario DROP COLUMN numero GO
ALTER TABLE dbo.Usuario DROP COLUMN bairro GO
ALTER TABLE dbo.Usuario DROP COLUMN complemento GO
ALTER TABLE dbo.Usuario DROP COLUMN estado GO
ALTER TABLE dbo.Usuario DROP COLUMN cidade GO
ALTER TABLE dbo.Usuario DROP COLUMN referencia GO
ALTER TABLE dbo.Usuario DROP COLUMN data_de_nascimento GO
ALTER TABLE dbo.Usuario ALTER COLUMN email varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL GO
ALTER TABLE dbo.Usuario ALTER COLUMN senha varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL GO