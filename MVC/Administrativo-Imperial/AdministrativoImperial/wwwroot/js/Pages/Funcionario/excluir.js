$(document).ready(function () {

});

function ConfirmacaoDesativar(funId, funNome) {
    swal(
        "Desativando Funcionário",
        "Tem certeza que deseja desativar o Funcionário: " + funNome + "?",
        "warning").then((confirm) => {
            if (confirm) {
                DesativarFuncionario(funId)
            }
        });
}

function DesativarFuncionario(funId) {
    MostraLoading();

    $.ajax({
        url: "/Funcionario/Desativar/" + parseInt(funId),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                    if (confirm) {
                        BuscarListaFuncionarios();
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
