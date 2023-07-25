$(document).ready(function () {

});

function EditarFuncionario(id) {

    $("#cadastrar").text("Editar");

    $.ajax({
        url: "/Funcionario/ObterFuncionarioPorId/" + parseInt(id),
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

    var data = ConverterDataParaUSA(dados.data_de_nascimento);
    var salario = FormatDinheiro(ConverterParaFloat(dados.salario))

    $("#idEditar").val(dados.id);
    $("#nome").val(dados.nome);
    $("#data").val(data);
    $("#salario").val(salario);
}