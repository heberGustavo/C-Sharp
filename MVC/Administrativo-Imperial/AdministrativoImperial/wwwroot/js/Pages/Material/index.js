var txtIdMaterialTemp;
var txtDataCompra;
var ddlListaObras;
var txtNomeMaterial;
var txtDescricao;
var txtValor;

var tabelaMateriais;

$(document).ready(function () {
    Init();
});

function Init() {
    Variaveis();
    BuscarListaMateriais();
}

function Variaveis() {
    txtIdMaterialTemp = $("#txtIdMaterialTemp");
    txtDataCompra = $('#txtDataCompra');
    ddlListaObras = $('#ddlListaObras');
    txtNomeMaterial = $('#txtNomeMaterial');
    txtDescricao = $('#txtDescricao');
    txtValor = $('#txtValor');

    tabelaMateriais = $('#tabelaMateriais tbody');
}

function BuscarListaMateriais() {
    MostraLoading();

    $.ajax({
        url: "/Material/Listar",
        type: "GET",
        cache: false,
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

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(txtDataCompra.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Data de Compra');
        return false;
    }
    else if (IsNullOrEmpty(txtNomeMaterial.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }
    else if (IsNullOrEmpty(txtValor.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Valor');
        return false;
    }

    return true;
}

function PreencherCamposModalMaterial(dados) {
    var campo = dados.item;

    txtDataCompra.val(ConverterDataParaUSA(campo.mtrDataCompra));
    ddlListaObras.val(campo.obrId);
    txtNomeMaterial.val(campo.mtrNome);
    txtDescricao.val(campo.mtrDescricao);
    txtValor.val(FormatDinheiro(campo.mtrValor));
}

/*MODAL*/
function ModalMaterial() {
    LimparCamposModal();
    $('#modalMaterial').modal('show');
}

function ModalMaterialEditar(mtrId) {
    MostraLoading();
    LimparCamposModal();

    txtIdMaterialTemp.val(mtrId);

    $.ajax({
        url: "/Material/Selecionar/" + parseInt(mtrId),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            EncerraLoading();
            PreencherCamposModalMaterial(response);
            AlterarVisibilidadeAtualModal('modalMaterial');
        },
        error: function (response) {
            EncerraLoading();
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function ModalMaterialFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalMaterial');
}

function LimparCamposModal() {
    $("#frmMaterialReset").trigger("click");
}