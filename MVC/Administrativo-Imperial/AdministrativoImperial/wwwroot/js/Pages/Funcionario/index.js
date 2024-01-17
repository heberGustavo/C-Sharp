var nome;
var selectFuncao;
var valorDiaria;
var valorMensal;
var dataContratacao;
var tabelaFuncionario;
var txtIdFuncionarioTemp;

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
    txtIdFuncionarioTemp = $('#txtIdFuncionarioTemp');

    tabelaFuncionario = $('#tabelaFuncionarios tbody');
}

/*MODAL*/
function ModalFuncionarioCadastrar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalFuncionario');
}

function ModalFuncionarioAtualizar(funId) {
    LimparCamposModal();
    MostraLoading();

    $("#txtIdFuncionarioTemp").val(funId);

    $.ajax({
        url: "/Funcionario/Selecionar/" + parseInt(funId),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            EncerraLoading();
            PreencherCamposModalFuncionario(response);
            AlterarVisibilidadeAtualModal('modalFuncionario');
        },
        error: function (response) {
            EncerraLoading();
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function ModalFuncionarioFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalFuncionario');
}


/*AJAX*/
function BuscarListaFuncionarios() {
    MostraLoading();

    $.ajax({
        url: "/Funcionario/Listar",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            EncerraLoading();
            $("#divListar").html(response);
        },
        error: function (response) {
            EncerraLoading();
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

/*MÉTODOS GERAIS*/
function VerificaCheckClicado() {

    var selecionado = RetornaSelecionado();

    var itemSelecionado = $(selecionado).attr('id')

    if (itemSelecionado == 'customRadioDiaria') {
        $('#grupo-diaria').removeClass('d-none');
        $('#grupo-mensal').addClass('d-none');
        $('#grupo-mensal input').val("");
    }
    else if (itemSelecionado == 'customRadioMensal') {
        $('#grupo-mensal').removeClass('d-none');
        $('#grupo-diaria').addClass('d-none');
        $('#grupo-diaria input').val("");
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

function LimparCamposModal() {
    nome.val('')
    selectFuncao.val('0');
    valorDiaria.val('')
    valorMensal.val('')
    dataContratacao.val('');
    txtIdFuncionarioTemp.val('');

}

function PreencherCamposModalFuncionario(dados) {
    var campo = dados.item;

    nome.val(campo.funNome);
    selectFuncao.val(campo.fnfId);
    dataContratacao.val(ConverterParaDataUSA(campo.funDataContratacao));
    ValidaPreenchimentoEClickNoRadioButtonModalFuncionario(campo.funDiaria, campo.funMensal);
}

function ValidaPreenchimentoEClickNoRadioButtonModalFuncionario(campoDiaria, campoMensal) {
    var valorDiariaConvertido = campoDiaria <= 0 || null ? "" : FormatDinheiro(campoDiaria);
    var valorMensalConvertido = campoMensal <= 0 || null ? "" : FormatDinheiro(campoMensal);

    valorDiaria.val(valorDiariaConvertido);
    valorMensal.val(valorMensalConvertido);

    //Click na Tab Atual
    if (valorDiariaConvertido.length > 0)
        $("#customRadioDiaria").trigger('click')
    else if (valorMensalConvertido.length > 0)
        $("#customRadioMensal").trigger('click')
    
}