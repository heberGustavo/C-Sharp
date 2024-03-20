$(document).ready(function () {

});

function ConfirmarPagamento(idPedido) {
    $('#modalConfirmarPagamento').modal();
    $('#idPedidoSelecionado').val(idPedido)
}

function ConfirmarPagamentoRecebido() {

    var idPedido = parseInt($("#idPedidoSelecionado").val());

    $.ajax({
        url: "/Sorteios/ConfirmarPagamentoRecebido/" + idPedido,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem, "success")
                    .then((okay) => {
                        window.location.reload(true);
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