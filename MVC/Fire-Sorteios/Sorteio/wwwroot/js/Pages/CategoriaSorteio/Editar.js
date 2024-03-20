$(document).ready(function () {

});

function Editar(elemento, id) {

    var nomeCategoriaAtual = $(elemento).parent().parent().find('td:nth-child(1)').text();

    $('#idCategoriaSorteioSelecionado').val(id);
    $('#nomeCategoria').val(nomeCategoriaAtual);
    $('#tipo_modal').text('Editar');

    $('#modalNovaCategoria').modal();
}