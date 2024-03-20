$(document).ready(function () {

});

function CadastrarCategoria() {
    var idCategoriaSelecionada = $('#idCategoriaSorteioSelecionado').val();

    //Cadastrar
    if (IsNullOrEmpty(idCategoriaSelecionada)) {

        if (!IsNullOrEmpty($('#nomeCategoria').val().trim())) {

            var dadosJson = {
                nome: $('#nomeCategoria').val().trim()
            }

            $.ajax({
                url: "/CategoriaSorteio/CriarCategoriaSorteio",
                type: "POST",
                contentType: 'application/json; charset=UTF-8',
                dataType: "json",
                data: JSON.stringify(dadosJson),
                success: function (response) {
                    if (!response.erro) {
                        swal("Sucesso", response.mensagem, "success");
                        ObterTodosCategoriaSorteioAtivo();
                        FecharModalNovaCategoriaLimparCampos();
                    }
                    else {
                        swal("Opss", response.mensagem, "error");
                    }
                },
                error: function (response) {
                    swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                    console.log(response);
                }
            });

        }
        else {
            swal('Atenção', 'Informe o nome da categoria', 'error');
        }
    }

    //Editar
    else {

        if (!IsNullOrEmpty($('#nomeCategoria').val().trim())) {

            var dadosJson = {
                id_categoria_sorteio: parseInt(idCategoriaSelecionada),
                nome: $('#nomeCategoria').val().trim()
            }

            $.ajax({
                url: "/CategoriaSorteio/EditarCategoriaSorteio/",
                type: "POST",
                contentType: 'application/json; charset=UTF-8',
                dataType: "json",
                data: JSON.stringify(dadosJson),
                success: function (response) {
                    if (!response.erro) {
                        swal("Sucesso", response.mensagem, "success");
                        ObterTodosCategoriaSorteioAtivo();
                        FecharModalNovaCategoriaLimparCampos();
                    }
                    else {
                        swal("Opss", response.mensagem, "error");
                    }
                },
                error: function (response) {
                    swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                    console.log(response);
                }
            });

        }
        else {
            swal('Atenção', 'Informe o nome da categoria', 'error');
        }

    }

}