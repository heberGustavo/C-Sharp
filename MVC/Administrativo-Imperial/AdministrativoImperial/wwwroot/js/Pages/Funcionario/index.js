var nome;
var selectFuncao;
var valorDiaria;
var valorMensal;
var dataContratacao;
var tabelaFuncionario;

$(document).ready(function () {
    Init();
});

function Init() {
    Variaveis();
    BuscarListaFuncionarios();
}

function Variaveis() {
    nome = $('#nome');
    selectFuncao = $('#selectFuncao');
    valorDiaria = $('#valorDiaria');
    valorMensal = $('#valorMensal');
    dataContratacao = $('#dataContratacao');
    tabelaFuncionario = $('#tabelaFuncionarios tbody');
}

function ModalFuncionario() {
    $('#modalFuncionario').modal('show');
}

function VerificaCheckClicado() {

    var selecionado = RetornaSelecionado();

    var itemSelecionado = $(selecionado).attr('id')

    if (itemSelecionado == 'customRadioDiaria') {
        $('#grupo-diaria').removeClass('d-none');
        $('#grupo-mensal').addClass('d-none');
    }
    else if (itemSelecionado == 'customRadioMensal') {
        $('#grupo-mensal').removeClass('d-none');
        $('#grupo-diaria').addClass('d-none');
    }

}

function RetornaSelecionado() {
    var item;

    $('input[type=radio]:checked').each(function () {
        item = this;
    })

    return item;
}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(nome.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }
    else if (IsNullOrEmpty(selectFuncao.val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('Função');
        return false;
    }
    else if (IsNullOrEmpty(valorDiaria.val()) && IsNullOrEmpty(valorMensal.val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('');
        swal("Atenção", "É necessário preencher valor de Diária ou Mensal", "error");
        return false;
    }

    return true;
}

function BuscarListaFuncionarios() {

    $.ajax({
        url: "/Funcionario/Listar",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            $("#divListar").html(response);
        },
        error: function (response) {
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function LimparCamposModal() {
    nome.val('')
    selectFuncao.val('0');
    valorDiaria.val('')
    valorMensal.val('')
    dataContratacao.val('')
}

function ModalFuncionarioFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalFuncionario');
}