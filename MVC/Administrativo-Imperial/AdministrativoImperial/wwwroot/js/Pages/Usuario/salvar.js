$(document).ready(function () {

});

function ObterDadosJson() {
    return {
        UsaNome: txtNome.val(),
        UsaEmail: txtEmail.val(),
        senha: txtSenha.val(),
        UsaId: txtIdUsuarioTemp.val().length <= 0 ? 0 : parseInt(txtIdUsuarioTemp.val())
    }
}

/*MODAL*/
function ModalUsuarioSalvar() {

    if (VerificarCamposObrigatorios()) {

        MostraLoading();

        var json = ObterDadosJson();

        $.ajax({
            url: "/Usuario/Cadastrar",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(json),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                        if (confirm) {
                            BuscarListaUsuarios();
                            LimparCamposModal();
                            AlterarVisibilidadeAtualModal(modalUsuario);
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

function ModalUsuarioEditar(usaId) {
    LimparCamposModal();
    MostraLoading();

    txtIdUsuarioTemp.val(usaId);

    $.ajax({
        url: "Usuario/Selecionar/" + usaId,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                PreencherModalUsuario(response);
                AlterarVisibilidadeAtualModal(modalUsuario);
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