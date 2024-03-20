$(document).ready(function () {
    swal("Atenção", "A imagem é tentada ser enviada por BLOB da Azure, como não tem a conexão acontecerá erro", "error")
});

function AtualizarFormaDePagamento() {

    if (VerificarCamposObrigatorios()) {
        var dadosJson = GerarJsonCamposObrigatorios();

        dadosJson.id_forma_de_pagamento = parseInt($('#idFormaDePagamentoEscolhida').val());

        $.ajax({
            url: "/FormasDePagamento/EditarFormaDePagamento/",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(dadosJson),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success")
                        .then((okay) => {
                            window.location.href = "/FormasDePagamento";
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