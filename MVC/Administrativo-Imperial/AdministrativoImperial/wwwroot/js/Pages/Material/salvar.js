$(document).ready(function () {

});

function ModalMaterialSalvar() {

    if (VerificarCamposObrigatorios()) {
        MostraLoading();

        var json = ObterDadosTelaJsonCadastrar();

        $.ajax({
            url: "/Material/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaMateriais();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal('modalMaterial');
                        }
                    });
                }
                else {
                    $.each(response.mensagem, function (index, value) {
                        MostrarAlertMensagemErro(value)
                    });
                }

                EncerraLoading();
            },
            error: function (response) {
                EncerraLoading();
                console.log(response);
                swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            }
        });
    }
    
}

function ObterDadosTelaJsonCadastrar() {
    return {
        MtrId: parseInt(txtIdMaterialTemp.val()) > 0 ? parseInt(txtIdMaterialTemp.val()) : 0,
        ObrId: parseInt(ddlListaObras.val()),
        MtrNome: txtNomeMaterial.val(),
        MtrDescricao: txtDescricao.val(),
        MtrValor: ConverterParaFloat(txtValor.val()),
        MtrDataCompra: txtDataCompra.val(),
    }
}