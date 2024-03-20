$(document).ready(function () {

});

function ConfirmarExclusao(id, nomeBanco) {
    $('#idFormaDePagamentoExclusaoSelecionada').val('');
    $('#nomeBancoExclusao').text('');

    $('#idFormaDePagamentoExclusaoSelecionada').val(id);
    $('#nomeBancoExclusao').text(nomeBanco);

    $('#modalFinalizarSorteio').modal();
}

function ExcluirFormaDePagamento() {

    var id = $('#idFormaDePagamentoExclusaoSelecionada').val();

    $.ajax({
        url: "/FormasDePagamento/ExcluirFormaDePagamento/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem, "success")
                    .then((okay) => {
                        window.location.reload(true);
                    });
                //ObterTodasFormasDePagamentoAtivo();
                //FecharModalExcluirFormaDePagamentoLimparCampos();
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