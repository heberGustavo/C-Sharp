$(document).ready(function () {

});

function GerarTicket() {
    $('#modalGerarTicket').modal();
}

function RealizarLogin() {

    var USUARIO_ADMIN = 1;
    var USUARIO_CLIENTE = 2;

    if (VerificaCamposPreenchidosLogin()) {

        var arrayItensEscolhidos = [];
        $('.numero-escolhido').each(function (i, element) {
            var numero = $(element).text();
            arrayItensEscolhidos.push(numero)
        })

        var jsonBody = {
            "nome": $('#nomeLogin').val(),
            "celular": $('#celularLogin').val(),
            "id_sorteio": parseInt($('#idSorteioSelecionado').val()),
            "valor_total": ConverterParaFloat($('#valor_total').text()),
            "numeroSorteios": GerarJsonNumerosSorteios(arrayItensEscolhidos)
        };

        $.ajax({
            url: "/Login/LogarCadastraNumeros/",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(jsonBody),
            success: function (response) {
                if (response.erro) {
                    swal("Erro", response.mensagem, "error");
                }
                else {
                    if (parseInt(response.model.id_tipo_usuario) == USUARIO_ADMIN) {
                        window.location.href = "/Sorteios";
                    }
                    else if (parseInt(response.model.id_tipo_usuario) == USUARIO_CLIENTE) {
                        alert('cadastro cliente')
                    }
                    else {
                        swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                    }
                }
            },
            error: function (response) {
                swal("Erro", response.mensagem, "error");
            }
        });
    }

}

function GerarJsonNumerosSorteios(arrayItensEscolhidos) {

    var item;
    var listaItensEscolhidos = [];

    $(arrayItensEscolhidos).each(function (i, element) {

        item = {
            "numero": parseInt(element)
        };

        listaItensEscolhidos.push(item);

    });

    return listaItensEscolhidos;

}

function VerificaCamposPreenchidosLogin() {

    if (IsNullOrEmpty($('#nomeLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }
    else if (IsNullOrEmpty($('#celularLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Celular');
        return false;
    }

    return true;

}