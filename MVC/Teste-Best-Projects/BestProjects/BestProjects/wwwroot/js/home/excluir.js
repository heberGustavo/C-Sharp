$(document).ready(function () {

});

function AbrirModalRemoverContato(id, nome) {

    $("#idUsuarioExcluir").val(id);
    $("#nomeContato").text(nome);

    $("#modalExcluirContato").modal("show");
}

function ConfirmarExclusao() {

    var id = $("#idUsuarioExcluir").val();

    $.ajax({
        url: "/Home/ExcluirContato/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (response.erro) {
                swal("Opss", response.mensagem, "error");
            }
            else {
                swal("Sucesso", response.mensagem, "success")
                    .then((okay) => {
                        BuscarTodosUsuariosCadastrados();
                        $("#modalExcluirContato").modal("hide");
                    });
            }
        },
        error: function (response) {
            swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
        }
    });
}