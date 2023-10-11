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

    $.ajax({
        url: "/Material/Listar",
        type: "GET",
        cache: false,
        success: function (response) {
            $("#divListar").html(response)
        },
        error: function (response) {
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

function ModalMaterial() {
    LimparCamposModal();
    $('#modalMaterial').modal('show');
}

function ModalMaterialFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalMaterial');
}

function LimparCamposModal() {
    $("#frmMaterialReset").trigger("click");
}