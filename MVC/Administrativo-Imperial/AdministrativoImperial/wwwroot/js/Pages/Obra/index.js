var dataInicio;
var apelido;
var endereco;
var orcamento;
var tabelaObra;
var txtIdObraTemp;

$(document).ready(function () {
    Init();
});

function Init() {
    Variaveis();
    BuscarListaObras();
}

/*METODOS GERAIS*/
function Variaveis() {
    dataInicio = $('#dataInicio');
    apelido = $('#apelido');
    endereco = $('#endereco');
    orcamento = $('#orcamento');
    tabelaObra = $('#tabelaObra tbody');
    txtIdObraTemp = $("#txtIdObraTemp");
}

function ModalObra() {
    AlterarVisibilidadeAtualModal('modalObra');
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

function LimparCamposModal() {
    dataInicio.val('')
    apelido.val('')
    endereco.val('')
    orcamento.val('')
    txtIdObraTemp.val('');
}

function ModalObraFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalObra');
}

/*AJAX*/
function BuscarListaObras() {

    MostraLoading();
    $.ajax({
        url: "/Obra/Listar",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            EncerraLoading();
            $("#divListar").html(response)
        },
        error: function (response) {
            EncerraLoading();
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function PreencherModalObra(response) {
    dataInicio.val(ConverterParaDataUSA(response.data.obrDataInicio));
    apelido.val(response.data.obrApelido);
    endereco.val(response.data.obrEndereco);
    orcamento.val(FormatDinheiro(response.data.obrOrcamento));
}