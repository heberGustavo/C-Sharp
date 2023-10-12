$(document).ready(function () {

});

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
        ObrOrcamento: orcamentoFloat,
        ObrId: txtIdObraTemp.val().length <= 0 ? 0 : parseInt(txtIdObraTemp.val())
    }
}

/*MODAL*/
function ModalObraSalvar() {

    if (VerificarCamposObrigatorios()) {

        MostraLoading();

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

function ModalObraEditar(obrId) {
    LimparCamposModal();

    txtIdObraTemp.val(obrId);

    MostraLoading();

    $.ajax({
        url: "Obra/Selecionar/" + obrId,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                PreencherModalObra(response);
                AlterarVisibilidadeAtualModal('modalObra');
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