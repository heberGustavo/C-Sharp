$(document).ready(function () {

});

function ModalMaterialSalvar() {

    if (VerificarCamposObrigatorios()) {
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
            },
            error: function (response) {
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

function AlterarFuncao(id, nome) {
    try {
        if (id <= 0 && nome.length <= 0) {
            MostrarAlertMensagemErro("Erro ao selecionar dados. Tente novamente!");
            return;
        }

        ModalFuncao(id, nome);

    } catch (e) {
        MostrarAlertMensagemErro("Erro ao selecionar dados. Contate o Administrador");
        console.log(e);
    }
}