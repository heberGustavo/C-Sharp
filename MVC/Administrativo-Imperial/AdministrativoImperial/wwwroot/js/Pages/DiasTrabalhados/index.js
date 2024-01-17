var ddlObra;
var txtData;
var ddlFuncionarios;
var tabelaDiaTrabalhado;
var txtIdDiaTrabalhadoTemp;
var modalDiaTrabalhado;

$(document).ready(function () {
    Init();
});

function Init() {
    Variaveis();
    BuscarListaDiasTrabalhados();
}

/*METODOS GERAIS*/
function Variaveis() {
    ddlObra = $('#ddlObra');
    txtData = $('#txtData');
    ddlFuncionarios = $('#ddlFuncionarios');
    tabelaDiaTrabalhado = $('#tabelaDiaTrabalhado tbody');
    txtIdDiaTrabalhadoTemp = $("#txtIdDiaTrabalhadoTemp");
    modalDiaTrabalhado = "modalDiaTrabalhado";
}

function ModalDiaTrabalhado() {
    AlterarVisibilidadeAtualModal(modalDiaTrabalhado);
}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(ddlObra.val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('Obra');
        return false;
    }
    else if (IsNullOrEmpty(txtData.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Data');
        return false;
    }
    else if (IsNullOrEmpty(ddlFuncionarios.val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('Funcionários');
        return false;
    }

    return true;
}

function LimparCamposModal(temp) {
    ddlObra.val(0);
    txtData.val('');
    ddlFuncionarios.val(0);
    txtIdDiaTrabalhadoTemp.val('')
}

function ModalDiaTrabalhadoFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal(modalDiaTrabalhado);
}

/*AJAX*/
function BuscarListaDiasTrabalhados() {

    $.ajax({
        url: "/DiasTrabalhados/Listar",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            $("#divListar").html(response)
        },
        error: function (response) {
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function PreencherModalDiaTrabalhado(response) {

    console.log(response)

    ddlObra.val(response.data.obrId);
    txtData.val(ConverterParaDataUSA(response.data.ditData));
    ddlObra.val(response.data.funId);
}