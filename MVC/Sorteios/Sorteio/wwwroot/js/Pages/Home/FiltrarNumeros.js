$(document).ready(function () {

});

function BuscarTodosNumeros(idSorteio) {
    EsconderLimparCampos();

    $.ajax({
        url: "/Sorteios/BuscarTodosNumerosSorteioPorId/" + idSorteio,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesTodosNumeros(response);
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

function CriarBotoesTodosNumeros(dados) {
    $('#lista_numeros_sorteio').html('');

    var STATUS_PENDENTE = 1;
    var STATUS_PAGO = 2;

    var quantidadeDeNumeros = parseInt($('#quantidadeNumerosSorteio').val());
    var meuArrayNumeroPagoOuReservado = [];
    var meuArraySomenteNumeros = [];

    $(dados).each(function (i, element) {
        meuArrayNumeroPagoOuReservado.push({ "nome_usuario": element.nome_usuario, "numero": element.numero, "status": element.id_status_pedido })
        meuArraySomenteNumeros.push(element.numero);
    });

    for (i = 0; i < quantidadeDeNumeros; i++) {

        if (($.inArray(i, meuArraySomenteNumeros) > -1)) {

            var posicao = meuArraySomenteNumeros.indexOf(i);

            if (meuArrayNumeroPagoOuReservado[posicao].status == STATUS_PENDENTE) {
                var item = `<button style = "margin: 2px 4px;" class="itens-numero-sorteio item-reservado" data-toggle="tooltip" data-placement="top" title="Reservado por: ${meuArrayNumeroPagoOuReservado[posicao].nome_usuario}">${meuArrayNumeroPagoOuReservado[posicao].numero.toString().padStart(3, "0")}</button>`;
                $('#lista_numeros_sorteio').append(item);
            }
            if (meuArrayNumeroPagoOuReservado[posicao].status == STATUS_PAGO) {
                var item = `<button style = "margin: 2px 4px;" class="itens-numero-sorteio item-pago" data-toggle="tooltip" data-placement="top" title="Pago por: ${meuArrayNumeroPagoOuReservado[posicao].nome_usuario}">${meuArrayNumeroPagoOuReservado[posicao].numero.toString().padStart(3, "0")}</button>`;
                $('#lista_numeros_sorteio').append(item);
            }
        }
        else { //Mostra disponivel
            if ($.inArray(i, meuArraySomenteNumeros) == -1) {

                var item = `<button style = "margin: 2px 4px;" onclick="EscolhaItemDisponivel(this)" class="itens-numero-sorteio item-disponivel">${i.toString().padStart(3, "0")}</button>`;
                $('#lista_numeros_sorteio').append(item);

            }
        }

        $('[data-toggle="tooltip"]').tooltip();

    }

}

function BuscarNumerosDisponiveis(idSorteio) {
    EsconderLimparCampos();

    $.ajax({
        url: "/Sorteios/BuscarTodosNumerosSorteioPorId/" + idSorteio,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesNumerosDisponiveis(response);
            }
            else {
                swal("Opss", "Erro ao filtrar dados.", "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }

    });
}

function CriarBotoesNumerosDisponiveis(dados) {

    $('#lista_numeros_sorteio').html('');
    var quantidadeDeNumeros = parseInt($('#quantidadeNumerosSorteio').val());
    var meuArrayNumero = [];

    $(dados).each(function (i, element) {
        meuArrayNumero.push(element.numero)
    });

    for (i = 0; i < quantidadeDeNumeros; i++) {

        if (!meuArrayNumero.includes(i)) {
            var item = `<button style = "margin: 2px 4px;" onclick="EscolhaItemDisponivel(this)" class="itens-numero-sorteio item-disponivel">${i.toString().padStart(3, "0")}</button>`;
            $('#lista_numeros_sorteio').append(item);
        }

    }

}

function BuscarNumerosReservados(idSorteio, idStatusReservado) {
    EsconderLimparCampos();

    $.ajax({
        url: "/Sorteios/BuscarNumerosReservadoOuPagoSorteioPorId/" + idSorteio + "/" + idStatusReservado,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesNumerosReservadoOuPago(response, 'item-reservado')
            }
            else {
                swal("Opss", "Erro ao filtrar dados.", "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }

    });

}

function BuscarNumerosPagos(idSorteio, idStatusPago) {
    EsconderLimparCampos();

    $.ajax({
        url: "/Sorteios/BuscarNumerosReservadoOuPagoSorteioPorId/" + idSorteio + "/" + idStatusPago,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesNumerosReservadoOuPago(response, 'item-pago');
            }
            else {
                swal("Opss", "Erro ao filtrar dados.", "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }

    });

}

function CriarBotoesNumerosReservadoOuPago(dados, classeBotao) {

    $('#lista_numeros_sorteio').html('');

    $(dados).each(function (i, element) {

        var item = `<button style = "margin: 2px 4px;" class="itens-numero-sorteio ${classeBotao}" data-toggle="tooltip" data-placement="top" title="Pago por: ${element.nome_usuario}">${element.numero.toString().padStart(3, "0")}</button>`;
        $('#lista_numeros_sorteio').append(item);
    });

    $('[data-toggle="tooltip"]').tooltip();

}

function EsconderLimparCampos() {
    itens_escolhidos = [];
    $('#sessao-fixa').addClass('d-none');
}