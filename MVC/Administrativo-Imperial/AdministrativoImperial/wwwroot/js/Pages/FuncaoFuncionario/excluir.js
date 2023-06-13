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

    $.ajax({
        url: "/FuncaoFuncionario/Deletar/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem, "success").then((confirm) => {
                    if (confirm) {
                        BuscarListaFuncaoFuncionario();
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
