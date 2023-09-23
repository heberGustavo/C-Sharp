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
                    //COLOCA ALERTA FLUTUANTE
                    $.each(response.mensagem, function (index, value) {
                        swal("Opss", value, "error");
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

    if (id && nome && excluido) {
        var situacaoItem = excluido.toLowerCase() == "true" ? 1 : 0;

        txtIdFuncaoTemp.val(id);
        txtNomeFuncao.val(nome);
        selectExcluido.val(situacaoItem);
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