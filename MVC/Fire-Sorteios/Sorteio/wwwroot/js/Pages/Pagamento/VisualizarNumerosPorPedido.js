$(document).ready(function () {

});

function VisualizarNumerosPorPedido(idPedido) {

    $.ajax({
        url: "/Sorteios/VisualizarNumerosPorIdPedido/" + idPedido,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarSessaoNumerosDoPedido(response);
            }
            else {
                swal("Opss", response.mensagem, "error");
            }
            setTimeout(function () {
                $('#loading').addClass('d-none');
            }, 500)
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
            $('#loading').addClass('d-none');
        }
    });
}

function CriarSessaoNumerosDoPedido(dados) {
    $('.numeros-pedido').html('');

    $(dados).each(function (i, element) {
        var item = `<button class="itens-numero-sorteio item-numero-pedido">${element.numero}</button>`;
        $('.numeros-pedido').append(item);
    });

    $('#modalVisualizarNumerosPorPedido').modal();
}