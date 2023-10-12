$(document).ready(function () {

});

function ConfirmarExclusao(id, nome) {
    swal(
        "Confirmar Exclusão",
        "Tem certeza que deseja excluir a obra: " + nome + "?",
        "warning").then((confirm) => {
            if (confirm) {
                DeletarObra(id)
            }
        });
}

function DeletarObra(id) {

    MostraLoading();

    $.ajax({
        url: "/Obra/Deletar/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                EncerraLoading();
                swal("Sucesso", response.mensagem[0], "success").then((confirm) => {
                    if (confirm) {
                        BuscarListaObras();
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
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}
