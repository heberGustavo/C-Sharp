var txtIdFuncaoTemp;
var txtNomeFuncao;
var selectExcluido;

var tabelaFuncao;

$(document).ready(function () {
    Init();
});

function Init() {
    Variaveis();
    BuscarListaFuncaoFuncionario();
}

function Variaveis() {
    txtIdFuncaoTemp = $("#idFuncaoTemp");
    txtNomeFuncao = $('#nome');
    selectExcluido = $('#selectExcluido');

    tabelaFuncao = $('#tabelaFuncao tbody');
}

function BuscarListaFuncaoFuncionario() {

    MostraLoading();

    $.ajax({
        url: "/FuncaoFuncionario/Listar",
        type: "GET",
        cache: false,
        success: function (response) {
            $("#divListar").html(response);
            EncerraLoading();
        },
        error: function (response) {
            console.log(response);
            EncerraLoading();
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(txtNomeFuncao.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }

    return true;
}