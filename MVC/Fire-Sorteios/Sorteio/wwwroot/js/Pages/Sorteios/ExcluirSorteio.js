$(document).ready(function () {

});

function AbrirModalExcluirSorteio(id) {
    $('#idSorteioSelecionadoExclusao').val(id);

    $('#modalExcluirSorteio').modal();
}

function ExcluirSorteio() {

    var id = $('#idSorteioSelecionadoExclusao').val();

    $.ajax({
        url: "/Sorteios/ExcluirSorteio/" + id,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem, "success")
                    .then((okay) => {
                        window.location.href = "/Sorteios";
                    });
            }
            else {
                swal("Opss", response.mensagem, "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }
    });

}