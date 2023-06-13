$(document).ready(function () {

});

function ModalFuncionarioSalvar() {

    if (VerificarCamposObrigatorios()) {
        var json = ObterDadosTelaJsonCadastrar();

        $.ajax({
            url: "/Funcionario/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaFuncionarios();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal('modalFuncionario');
                        }
                    });
                }
                else {
                    swal("Opss", response.mensagem, "error");
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
        nome: nome.val(),
        id_funcao_funcionario: parseInt(selectFuncao.val()),
        diaria: diariaFloat,
        mensal: mensalFloat,
        data_contratacao: dataContratacao.val()
    }
}