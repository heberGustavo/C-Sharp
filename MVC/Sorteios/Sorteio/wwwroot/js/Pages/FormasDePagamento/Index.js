$(document).ready(function () {
    
    tabelaListaFormasDePagamento = $('#datatableListaFormasDePagamento').DataTable({
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

    tabelaListaFormasDePagamentoPix = $('#datatableListaFormasDePagamentoPix').DataTable({
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

function ObterTodasFormasDePagamentoAtivo() {
    $.ajax({
        url: "/FormasDePagamento/ObterTodasFormasDePagamentoAtivo/",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                PreencherTabelaFormasDePagamento(response);
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

function PreencherTabelaFormasDePagamento(dados) {
    
    tabelaListaFormasDePagamento.clear().draw();

    $(dados).each(function (i, value) {

        tabelaListaFormasDePagamento.row.add([
            value.nome_banco,
            value.agencia,
            value.conta,
            CriarBotaoAções(value.id_forma_de_pagamento, value.nome_banco),
        ]).node().id = value.id_forma_de_pagamento;

    });

    tabelaListaFormasDePagamento.draw();
}

function CriarBotaoAções(id, nomeBanco) {
    return `<td>
                <a class='btn btn-sm btn-dark text-light' onclick="Editar(this, '${id}')"><i class='fas fa-edit'></i></a>
                <a class='btn btn-sm btn-danger' onclick="ConfirmarExclusao('${id}', '${nomeBanco}')"><i class='fas fa-trash'></i></a>
            </td>`;
}

function FecharModalExcluirFormaDePagamentoLimparCampos() {
    $('#modalFinalizarSorteio').modal('hide');

    $('#idFormaDePagamentoExclusaoSelecionada').val('');
    $('#nomeBancoExclusao').text('');
}