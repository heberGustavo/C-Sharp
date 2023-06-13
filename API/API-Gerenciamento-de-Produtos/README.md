# Gerenciamento de Produtos

Esse é um projeto Web API .NET Core, onde o objetivo do mesmo é que o cliente consiga realizar o gerenciamento de seus produtos.

Metodos inclusos no sistema:

🔴 Categoria
- Cadastro de Categorias 
- Listagem de Categorias por Id
- Listagem de Categorias por Nome e Situação
- Update de Categoria


🔴 Produto
- Cadatro de Produtos
- Listagem de Produto por Id
- Listagem de Produto por Categoria, Descrição do Produto e Situação
- Update de Produto


Como esse sistema é para rodar local, é necessario a criação de um Database com o nome "ProjetoTeste".
Caso querer alterar a CONNECTION_STRING você deve acessar o arquivo que esta localizado em: 0-Portal -> appsettings.json -> CONNECTION_STRING e CONNECTION_STRING_DEBUG

É necessario a execução dos scripts que estão localizados em: MIGRATION > Scripts. 
Obs: É de extrema importancia executar os scripts na ordem, caso contrario haverá erros no banco.

Espero que ocorra tudo perfeitamente, caso de alguma dúvida ou erro entre em contato comigo via Linkedin (link na Bio).
