$(document).ready(function () {

});

function AbrirModalEditarVencedorSorteio() {
    $('#modalFinalizarSorteio').modal();
}

function AtualizarFinalizarSorteio() {

    if (VerificarCamposObrigatorios()) {

        var dadosJson = {
            id_vencedor_sorteio: parseInt($('#idVencedorSorteioSelecionado').val()),
            id_sorteio: parseInt($('#idSorteioSelecionado').val()),
            data_sorteio: $('#data_sorteio').val().trim(),
            numero_sorteado: parseInt($('#numero_sorteado').val().trim()),
            id_usuario: parseInt($('#select_nome_cliente').val()),
        };

        $.ajax({
            url: "/Sorteios/EditarFinalizarSorteio/",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(dadosJson),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success")
                        .then((okay) => {
                            $('#modalFinalizarSorteio').modal('hide');
                            AtualizarDataDaBolhaDoSorteio();
                        });
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

}

function AtualizarDataDaBolhaDoSorteio() {
    var novoGanhador = $('#select_nome_cliente option:selected').text();

    $('.bolha_vencedor .data-sorteio').text(ConverterParaDataBr('data_sorteio'));
    $('.bolha_vencedor .nome-ganhador').text(novoGanhador);

}