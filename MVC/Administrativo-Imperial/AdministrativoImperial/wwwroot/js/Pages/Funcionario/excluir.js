$(document).ready(function () {

});

function DesativarFuncionario(funId, funNome) {
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
        },
        error: function (response) {
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}
