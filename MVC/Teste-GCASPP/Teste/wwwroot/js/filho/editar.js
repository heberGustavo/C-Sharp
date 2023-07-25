$(document).ready(function () {

});

function EditarFilho(id) {

    $("#cadastrar").text("Editar");

    $.ajax({
        url: "/Filho/ObterFilhoPorId/" + parseInt(id),
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            PreencherCamposEditar(response)
        },
        error: function (response) {
            swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
        }
    });

}

function PreencherCamposEditar(dados) {

    var data = ConverterDataParaUSARead(dados.data_de_nascimento);

    $("#idEditar").val(dados.id);
    $("#nome").val(dados.nome);
    $("#data").val(data);

    $("#selectFuncionario").selectpicker('val', dados.id_funcionario)
}