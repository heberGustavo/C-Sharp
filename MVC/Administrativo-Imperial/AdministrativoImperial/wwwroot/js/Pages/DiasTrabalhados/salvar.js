$(document).ready(function () {

});

/*MODAL*/
function ModalDiaTrabalhadoSalvar() {

    if (VerificarCamposObrigatorios()) {
        var json = ObterDadosJson();

        $.ajax({
            url: "/DiasTrabalhados/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaDiasTrabalhados();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal(modalDiaTrabalhado);
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

function ModalDiaTrabalhadoEditar(ditId) {
    LimparCamposModal();

    txtIdDiaTrabalhadoTemp.val(ditId);

    $.ajax({
        url: "DiasTrabalhados/Selecionar/" + parseInt(ditId),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                PreencherModalDiaTrabalhado(response);
                AlterarVisibilidadeAtualModal(modalDiaTrabalhado);
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

function ObterDadosJson() {
    return {
        ObrId: parseInt(ddlObra.val()),
        DitData: txtData.val(),
        FunIds: Array.from(ddlFuncionarios.val()),
        DitId: txtIdDiaTrabalhadoTemp.val().length <= 0 ? 0 : parseInt(txtIdDiaTrabalhadoTemp.val())
    }
}