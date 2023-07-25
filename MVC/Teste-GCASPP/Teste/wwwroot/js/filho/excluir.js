$(document).ready(function () {

});

function AbrirModalConfirmacao(id) {
    $("#idExcluir").val(id);
    $("#modalConfirmacao").modal("show");
}

function ConfirmarExclusao() {

    var id = $("#idExcluir").val();

    $.ajax({
        url: "/Filho/Excluir/" + parseInt(id),
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
                        ListarDadosFilho();
                        $("#modalConfirmacao").modal("hide");
                    });
            }
        },
        error: function (response) {
            swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
        }
    });
}