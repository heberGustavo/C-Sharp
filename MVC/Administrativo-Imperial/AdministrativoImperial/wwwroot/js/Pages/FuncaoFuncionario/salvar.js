$(document).ready(function () {

});

function ModalFuncaoSalvar() {

    if (VerificarCamposObrigatorios()) {

        var json = ObterDadosTelaJsonCadastrar();

        $.ajax({
            url: "/FuncaoFuncionario/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaFuncaoFuncionario();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal('modalFuncao');
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
        FnfId: parseInt(txtIdFuncaoTemp.val()) > 0 ? parseInt(txtIdFuncaoTemp.val()) : 0,
        FnfNome: txtNomeFuncao.val(),
        FnfStatus: selectExcluido.val() == '0' ? false : true
    }
}

function AlterarFuncao(id, nome, excluido) {
    ModalFuncao(id, nome, excluido);
}

function ModalFuncao(id, nome, excluido) {
    LimparCamposModal();

    try {
        if (id > 0 && nome.length > 0) {
            txtIdFuncaoTemp.val(id);
            txtNomeFuncao.val(nome);
            selectExcluido.val(excluido);
        }
        else
            MostrarAlertMensagemErro("Erro ao selecionar dados. Tente novamente!");

    } catch (e) {
        MostrarAlertMensagemErro("Erro ao selecionar dados. Contate o Administrador");
        console.log(e);
    }

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