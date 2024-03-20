$(document).ready(function () {
    $('.pgwSlideshow').pgwSlideshow();

    var textoHtml = $('#texto_longo_html').text();
    $('#contentSummernote').html('');

    setTimeout(function () {
        $('#contentSummernote').append(textoHtml);
    }, 80);

});

var itens_escolhidos = [];
function EscolhaItemDisponivel(botao) {

    //Mostra sessão
    if ($('#sessao-fixa').hasClass('d-none')) {
        $('#sessao-fixa').removeClass('d-none')
    }

    $('#numeros_selecionados').html('');

    //Remove dos itens selecionados e habilita o botao
    if ($(botao).hasClass("notactive")) {

        var numeroClicado = $(botao).text();

        if ($.inArray(numeroClicado, itens_escolhidos) !== -1) {
            itens_escolhidos.splice($.inArray(numeroClicado, itens_escolhidos), 1);
            $(botao).removeClass("notactive");

            MostrarItensSelecionados(itens_escolhidos);
        }
        return;
    }
    else {

        var numero_atual = $(botao).text();
        itens_escolhidos.push(numero_atual);

        MostrarItensSelecionados(itens_escolhidos);

        $(botao).addClass("notactive");
    }

}

function MostrarItensSelecionados(itens_escolhidos) {

    itens_escolhidos.forEach(function (value) {
        var html = `<div class="numero-escolhido">${value}</div>`;

        $('#numeros_selecionados').append(html);
    });

    var valor_rifa = ConverterParaFloat($('#valor_rifa').text());
    var valor_total_rifas = itens_escolhidos.length * valor_rifa;

    $('#quantidade_selecionado').text(itens_escolhidos.length);
    $('#valor_total').text(valor_total_rifas);
    $('#valor_total_visual').text(FormatDinheiro(ConverterParaFloat(valor_total_rifas)));
}

function ModalConsultarMeusNumeros() {
    $('#modalConsultarNumeros').modal('show');
}

function ConsultarNumerosCliente() {
    var idSorteio = $('#idSorteioSelecionado').val();

    var celularCliente = $('#celularConsultarNumero').val().trim();
    celularCliente = celularCliente.replace(' ', '%20');

    $.ajax({
        url: "/Sorteios/ConsultarNumerosCliente/" + celularCliente + "/" + parseInt(idSorteio),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.erro) {
                swal("Erro", response.mensagem, "error");
            }
            else {
                BuscarConsultaNumerosCliente(response);
            }
        },
        error: function (response) {
            swal("Erro", response.mensagem, "error");
        }
    });
}

function BuscarConsultaNumerosCliente(dados) {

    var STATUS_PENDENTE = 1;
    var STATUS_PAGO = 2;

    $('#lista_numeros_sorteio').html('');

    if (dados.length > 0) {

        $(dados).each(function (i, element) {

            if (element.id_status_pedido == STATUS_PENDENTE) {
                var item = `<button style = "margin: 2px 4px;" class="itens-numero-sorteio item-reservado" data-toggle="tooltip" data-placement="top" title="Você reservou">${element.numero.toString().padStart(3, "0")}</button>`;
            }
            else if (element.id_status_pedido == STATUS_PAGO) {
                var item = `<button style = "margin: 2px 4px;" class="itens-numero-sorteio item-pago" data-toggle="tooltip" data-placement="top" title="Você pagou">${element.numero.toString().padStart(3, "0")}</button>`;
            }

            $('#lista_numeros_sorteio').append(item);
        });

        $('[data-toggle="tooltip"]').tooltip();

    }
    else {
        $('#lista_numeros_sorteio').html('<b>Nenhum número reservado</b>');
    }

    $('#modalConsultarNumeros').modal('hide');

}