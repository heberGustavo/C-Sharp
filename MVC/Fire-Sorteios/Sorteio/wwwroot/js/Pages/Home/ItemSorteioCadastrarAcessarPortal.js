$(document).ready(function () {

});

function CadastrarAcessarPortal() {

    if (VerificarCamposObrigatoriosCadastroUsuario()) {

        var arrayItensEscolhidos = [];
        $('.numero-escolhido').each(function (i, element) {
            var numero = $(element).text();
            arrayItensEscolhidos.push(numero)
        })

        var jsonBody = {
            "id_sorteio": parseInt($('#idSorteioSelecionado').val()),
            "valor_total": ConverterParaFloat($('#valor_total').text()),
            "usuario": GerarDadosJsonCadastrarUsuario(),
            "numeroSorteios": GerarJsonNumerosSorteios(arrayItensEscolhidos)
        };

        $.ajax({
            url: "/Login/CadastrarUsuarioCadastrarNumeros",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(jsonBody),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success")
                        .then((okay) => {
                            window.location.reload(true);
                            //RealizarLoginSimples(response);
                        });
                }
                else {
                    swal("Opss", response.mensagem, "error")
                        .then((okay) => {
                            window.location.reload(true);
                        });
                }
            },
            error: function (response) {
                swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                console.log(response);
            }

        });

    }

}

function GerarDadosJsonCadastrarUsuario() {
    return {
        nome: $('#nomeLogin').val().trim(),
        celular: $('#celularLogin').val().trim()
    }
}

function VerificarCamposObrigatoriosCadastroUsuario() {

    if (IsNullOrEmpty($('#nomeLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }
    else if (IsNullOrEmpty($('#celularLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Email');
        return false;
    }

    return true;
}

function RealizarLoginSimples(dados) {

    var idUsuario = dados.id_cadastrado;

    $.ajax({
        url: "/Sorteios/MostrarNumerosDoUsuario/" + idUsuario,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.erro) {
                swal("Erro", response.mensagem, "error");
            }
            else {
                BuscarNumerosCliente(response);
            }
        },
        error: function (response) {
            swal("Erro", response.mensagem, "error");
        }
    });

}

function BuscarNumerosCliente(dados) {

    $('#lista_numeros_sorteio').html('');

    $(dados).each(function (i, element) {

        var item = `<button style = "margin: 2px 4px;" class="itens-numero-sorteio item-reservado" data-toggle="tooltip" data-placement="top" title="Você reservou">${element.numero.toString().padStart(3, "0")}</button>`;
        $('#lista_numeros_sorteio').append(item);
    });

    $('[data-toggle="tooltip"]').tooltip();

} $(document).ready(function () {

});

function CadastrarAcessarPortal() {

    if (VerificarCamposObrigatoriosCadastroUsuario()) {

        var arrayItensEscolhidos = [];
        $('.numero-escolhido').each(function (i, element) {
            var numero = $(element).text();
            arrayItensEscolhidos.push(numero)
        })

        var jsonBody = {
            "id_sorteio": parseInt($('#idSorteioSelecionado').val()),
            "valor_total": ConverterParaFloat($('#valor_total').text()),
            "usuario": GerarDadosJsonCadastrarUsuario(),
            "numeroSorteios": GerarJsonNumerosSorteios(arrayItensEscolhidos)
        };

        $.ajax({
            url: "/Login/CadastrarUsuarioCadastrarNumeros",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(jsonBody),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success")
                        .then((okay) => {
                            window.location.reload(true);
                            //RealizarLoginSimples(response);
                        });
                }
                else {
                    swal("Opss", response.mensagem, "error")
                        .then((okay) => {
                            window.location.reload(true);
                        });
                }
            },
            error: function (response) {
                swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                console.log(response);
            }

        });

    }

}

function GerarDadosJsonCadastrarUsuario() {
    return {
        nome: $('#nomeLogin').val().trim(),
        celular: $('#celularLogin').val().trim()
    }
}

function VerificarCamposObrigatoriosCadastroUsuario() {

    if (IsNullOrEmpty($('#nomeLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }
    else if (IsNullOrEmpty($('#celularLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Email');
        return false;
    }

    return true;
}

function RealizarLoginSimples(dados) {

    var idUsuario = dados.id_cadastrado;

    $.ajax({
        url: "/Sorteios/MostrarNumerosDoUsuario/" + idUsuario,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.erro) {
                swal("Erro", response.mensagem, "error");
            }
            else {
                BuscarNumerosCliente(response);
            }
        },
        error: function (response) {
            swal("Erro", response.mensagem, "error");
        }
    });

}

function BuscarNumerosCliente(dados) {

    $('#lista_numeros_sorteio').html('');

    $(dados).each(function (i, element) {

        var item = `<button style = "margin: 2px 4px;" class="itens-numero-sorteio item-reservado" data-toggle="tooltip" data-placement="top" title="Você reservou">${element.numero.toString().padStart(3, "0")}</button>`;
        $('#lista_numeros_sorteio').append(item);
    });

    $('[data-toggle="tooltip"]').tooltip();

}