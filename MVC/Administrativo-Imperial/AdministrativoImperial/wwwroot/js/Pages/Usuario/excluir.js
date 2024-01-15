$(document).ready(function () {

});

function ConfirmarExclusao(id, nome) {
    swal(
        "Confirmar Exclusão",
        "Tem certeza que deseja excluir o usuário: " + nome + "?",
        "warning").then((confirm) => {
            if (confirm) {
                DeletarUsuario(id)
            }
        });
}

function DeletarUsuario(id) {

    MostraLoading();

    $.ajax({
        url: "/Usuario/Deletar/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                    if (confirm) {
                        BuscarListaUsuarios();
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
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}
