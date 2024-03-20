$(document).ready(function () {

});

function ConfirmarExclusao(id, nomeCategoria) {
    $('#idCategoriaExclusaoSelecionada').val('');
    $('#nomeCategoriaExclusao').text('');

    $('#idCategoriaExclusaoSelecionada').val(id);
    $('#nomeCategoriaExclusao').text(nomeCategoria);

    $('#modalFinalizarSorteio').modal();
}

function ExcluirCategoria() {

    var id = $('#idCategoriaExclusaoSelecionada').val();

    $.ajax({
        url: "/CategoriaSorteio/ExcluirCategoriaSorteio/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem, "success");
                ObterTodosCategoriaSorteioAtivo();
                FecharModalExcluirCategoriaLimparCampos();
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