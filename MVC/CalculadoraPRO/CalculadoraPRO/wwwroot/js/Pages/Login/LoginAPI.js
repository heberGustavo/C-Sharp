$(document).ready(function () {
    loadList();
});

function loadList() {
    $.ajax({
        type: "GET",
        url: "Contas/Listar",
        cache: false,
        success: function (data) {
            $("#listagem-contas").html(data);
        }
    });
}