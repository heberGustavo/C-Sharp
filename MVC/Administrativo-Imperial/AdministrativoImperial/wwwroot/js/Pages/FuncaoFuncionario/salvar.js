$(document).ready(function () {

});

function ModalFuncaoSalvar() {

    if (VerificarCamposObrigatorios()) {

        MostraLoading();

        var json = ObterDadosTelaJsonCadastrar();

        $.ajax({
            url: "/FuncaoFuncionario/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    EncerraLoading();
                    swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaFuncaoFuncionario();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal('modalFuncao');
                        }
                    });
                }
                else {
                    EncerraLoading();
                    $.each(response.mensagem, function (index, value) {
                        MostrarAlertMensagemErro(value)
                    });
                }
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
        FnfId: parseInt(txtIdFuncaoTemp.val()) > 0 ? parseInt(txtIdFuncaoTemp.val()) : 0,
        FnfNome: txtNomeFuncao.val()
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

function ModalFuncao(id, nome) {
    LimparCamposModal();

    txtIdFuncaoTemp.val(id);
    txtNomeFuncao.val(nome);

    $('#modalFuncao').modal('show');
}

function LimparCamposModal() {
    txtIdFuncaoTemp.val('')
    txtNomeFuncao.val('')
    selectExcluido.val(0)
}

function ModalFuncaoFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalFuncao');
}