$(document).ready(function () {

});

function ModalObraSalvar() {

    if (VerificarCamposObrigatorios()) {
        var json = ObterDadosTelaJsonCadastrar();

        $.ajax({
            url: "/Obra/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaObras();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal('modalObra');
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

    var orcamentoFloat;

    if (!IsNullOrEmpty(orcamento.val()))
        orcamentoFloat = ConverterParaFloat(orcamento.val());
    else
        orcamentoFloat = 0.0;

    return {
        ObrDataInicio: dataInicio.val(),
        ObrApelido: apelido.val(),
        ObrEndereco: endereco.val(),
        ObrOrcamento: orcamentoFloat
    }
}