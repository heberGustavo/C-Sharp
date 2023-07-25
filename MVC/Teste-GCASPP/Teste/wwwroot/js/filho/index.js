$(document).ready(function () {
    ListarDadosFilho();
});

function ListarDadosFilho() {

    $.ajax({
        url: "/Filho/ListarDadosFilho",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            $("#divListarFilho").html(response)
        },
        error: function (response) {
            console.log(response)
            swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
        }
    });

}