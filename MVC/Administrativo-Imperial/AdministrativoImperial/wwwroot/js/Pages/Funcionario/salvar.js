$(document).ready(function () {

});

function ModalFuncionarioSalvar() {

    if (VerificarCamposObrigatorios()) {
        MostraLoading();

        var json = ObterDadosTelaJsonCadastrar();

        $.ajax({
            url: "/Funcionario/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaFuncionarios();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal('modalFuncionario');
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

    var diariaFloat;
    var mensalFloat;

    if (!IsNullOrEmpty(valorDiaria.val()))
        diariaFloat = ConverterParaFloat(valorDiaria.val());
    else
        diariaFloat = 0.0;

    if (!IsNullOrEmpty(valorMensal.val()))
        mensalFloat = ConverterParaFloat(valorMensal.val());
    else
        mensalFloat = 0.0;

    return {
        FunNome: nome.val(),
        FnfId: parseInt(selectFuncao.val()),
        FunDiaria: diariaFloat,
        FunMensal: mensalFloat,
        FunDataContratacao: dataContratacao.val(),
        FunId: txtIdFuncionarioTemp.val().length <= 0 ? 0 : parseInt(txtIdFuncionarioTemp.val())
    }
}