$(document).ready(function () {

});

function ConfirmarExclusao(id, nome) {
    swal(
        "Confirmar Exclusão",
        "Tem certeza que deseja excluir a função: " + nome + "?",
        "warning").then((confirm) => {
            if (confirm) {
                DeletarFuncao(id)
            }
        });
}

function DeletarFuncao(id) {

    MostraLoading();

    $.ajax({
        url: "/FuncaoFuncionario/Deletar/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                EncerraLoading();
                swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                    if (confirm) {
                        BuscarListaFuncaoFuncionario();
                    }
                });
            }
            else {
                EncerraLoading();
                $.each(response.mensagem, function (index, value) {
                    MostrarAlertMensagemErro(value)
                });
            }
        },
        error: function (response) {
            EncerraLoading();
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}
