$(document).ready(function () {

    tabelaListaCategorias = $('#datatableListaCategorias').DataTable({
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

function ObterTodosCategoriaSorteioAtivo() {

    $.ajax({
        url: "/CategoriaSorteio/ObterTodosCategoriaSorteioAtivo/",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                PreencherTabelaCategoriaSorteio(response);
                FecharModalNovaCategoriaLimparCampos();
            }
            else {
                swal("Opss", "Erro ao buscar categorias. Tente novamente!", "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }

    });
}

function PreencherTabelaCategoriaSorteio(dados) {
    tabelaListaCategorias.clear().draw();

    $(dados).each(function (i, value) {

        tabelaListaCategorias.row.add([
            value.nome,
            CriarBotaoAçõesCategoriaSorteio(value.id_categoria_sorteio, value.nome),
        ]).node().id = value.id_categoria_sorteio;

    });

    tabelaListaCategorias.draw();
}

function CriarBotaoAçõesCategoriaSorteio(id, nomeCategoria) {
    return `<td>
                <a class='btn btn-sm btn-dark text-light' onclick="Editar(this, '${id}')"><i class='fas fa-edit'></i></a>
                <a class='btn btn-sm btn-danger' onclick="ConfirmarExclusao('${id}', '${nomeCategoria}')"><i class='fas fa-trash'></i></a>
            </td>`;
}

function FecharModalNovaCategoriaLimparCampos() {
    $('#modalNovaCategoria').modal('hide');
    $('#nomeCategoria').val('')
}

function FecharModalExcluirCategoriaLimparCampos() {
    $('#modalFinalizarSorteio').modal('hide');

    $('#idCategoriaExclusaoSelecionada').val('');
    $('#nomeCategoriaExclusao').text('');
}

function AbrirModalNovaCategoriaSorteio() {
    $('#idCategoriaSorteioSelecionado').val('');
    $('#nomeCategoria').val('');
    $('#tipo_modal').text('Cadastrar');


    $('#modalNovaCategoria').modal();
}