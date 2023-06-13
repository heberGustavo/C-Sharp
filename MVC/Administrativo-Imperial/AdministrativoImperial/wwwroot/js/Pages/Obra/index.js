var dataInicio;
var apelido;
var endereco;
var orcamento;
var tabelaObra;
var divListar;

$(document).ready(function () {

    InicializaVariaveis();
    BuscarListaObras();   

});

function InicializaVariaveis() {
    dataInicio = $('#dataInicio');
    apelido = $('#apelido');
    endereco = $('#endereco');
    orcamento = $('#orcamento');
    tabelaObra = $('#tabelaObra tbody');
    divListar = $('.divListar');
}

function ModalObra() {
    $('#modalObra').modal('show');
}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(dataInicio.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Data de Início');
        return false;
    }
    else if (IsNullOrEmpty(apelido.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Apelido');
        return false;
    }

    return true;
}

function BuscarListaObras() {

    $.ajax({
        url: "/Obra/Listar",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            divListar.html(response)
        },
        error: function (response) {
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function LimparCamposModal() {
    dataInicio.val('')
    apelido.val('')
    endereco.val('')
    orcamento.val('')
}

function ModalObraFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalObra');
}