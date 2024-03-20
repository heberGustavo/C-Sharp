$(document).ready(function () {

    datatableListaParticipante = $('.datatableListaParticipante').DataTable({
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.16/i18n/Portuguese-Brasil.json"
        },
        "columns.data": [
            null,
            null,
            null,
            null,
            { "width": "110px" }
        ],
        responsive: true,
        "bInfo": false,
        "lengthChange": false,
        language: {
            search: "",
            searchPlaceholder: "Pesquisar",
            paginate: {
                previous: '‹‹',
                next: '››'
            },
            "emptyTable": "Nenhum resultado encontrado",
        },
        "fnInitComplete": function (oSettings) {
            oSettings.oLanguage.sZeroRecords = "Nenhum resultado encontrado"
        }
    });

});


function ObterParticipantesSorteio(idSorteio) {

    var collapse = `collapse_${idSorteio}`;

    if ($('#accordion').find('#' + collapse).hasClass('show')) {
        return;
    }

    $('#loading').removeClass('d-none');

    $.ajax({
        url: "/Sorteios/ObterParticipantesSorteioPorId/" + idSorteio,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                PreencherTabelaListaDeParticipantes(response);
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

function PreencherTabelaListaDeParticipantes(dados) {

    datatableListaParticipante.clear().draw();

    $(dados).each(function (i, value) {

        datatableListaParticipante.row.add([
            value.id_pedido.toString().padStart(7, "0"),
            value.nome,
            value.cpf,
            CriarBotaoVerNumerosPedido(value.id_pedido),
            CriarBotaoAções(value.id_status_pedido, value.id_pedido),
        ]).node().id = value.id_pedido;

    });

    datatableListaParticipante.draw();
}

function CriarBotaoVerNumerosPedido(idPedido) {

    return `<td>
                <a class='btn btn-sm btn-dark text-light' onclick="VisualizarNumerosPorPedido('${idPedido}')">N° Pedido <span class="material-icons">receipt_long</span></a>
           </td>`;
}

function CriarBotaoAções(idStatus, idPedido) {

    var STATUS_PENDENTE = '1';
    var STATUS_PAGO = '2';

    if (idStatus == STATUS_PAGO) {
        return `<td>
                    <a class='btn btn-sm btn-success text-light'>Pago <span class="material-icons">thumb_up</span></a>
                </td>`;
    }
    if (idStatus == STATUS_PENDENTE) {
        return `<td>
                    <a class='btn btn-sm btn-warning text-light' onclick="ConfirmarPagamento('${idPedido}')">Confirmar <span class="material-icons">paid</span></a>
                </td>`;
    }
    else {
        return `<td>
                    <a class='btn btn-sm btn-danger text-light'>Cancelado <span class="material-icons">clear</span></a>
                </td>`;
    }

}