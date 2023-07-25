$(document).ready(function () {

});

function Cadastrar() {

    if (VerificaSeCamposObrigatoriosPreenchidos()) {

        if ($('#idEditar').val() == "") { //REALIZAR CADASTRO

            var jsonBody = {
                nome: $("#nome").val().trim(),
                data: ConverterDataParaUSA($('#data').val()),
                idFuncionario: parseInt($("#selectFuncionario").val())
            };

            $.ajax({
                url: "/Filho/Cadastrar",
                type: "POST",
                contentType: 'application/x-www-form-urlencoded',
                dataType: "json",
                data: jsonBody,
                success: function (response) {
                    if (response.erro) {
                        swal("Opss", response.mensagem, "error");
                    }
                    else {
                        swal("Sucesso", response.mensagem, "success")
                            .then((okay) => {
                                LimparCampos();
                                ListarDadosFilho();
                            });
                    }
                },
                error: function (response) {
                    swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
                }
            });

        }
        else { //REALIZAR EDIÇÃO

            var jsonBody = {
                id: parseInt($('#idEditar').val()),
                nome: $("#nome").val().trim(),
                data: ConverterDataParaUSA($('#data').val()),
                idFuncionario: parseInt($("#selectFuncionario").val())
            };

            $.ajax({
                url: "/Filho/Editar",
                type: "POST",
                contentType: 'application/x-www-form-urlencoded',
                dataType: "json",
                data: jsonBody,
                success: function (response) {
                    if (response.erro) {
                        swal("Opss", response.mensagem, "error");
                    }
                    else {
                        swal("Sucesso", response.mensagem, "success")
                            .then((okay) => {
                                LimparCampos();
                                ListarDadosFilho();
                            });
                    }
                },
                error: function (response) {
                    swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
                }
            });

        }

    }

}

function VerificaSeCamposObrigatoriosPreenchidos() {
    if ($("#nome").val().trim() == "") {
        swal("Opss...", "Preencha o campo Nome!", "error");
        return false;
    }
    if ($("#data").val() == "") {
        swal("Opss...", "Preencha o campo Data", "error");
        return false;
    }
    if ($("#selectFuncionario").val() == 0) {
        swal("Opss...", "Preencha o campo Salário", "error");
        return false;
    }
    
    return true;
}

function LimparCampos() {
    $("#nome").val("");
    $("#data").val("");
    $("#idEditar").val("");

    $("#selectFuncionario").val("")
    $("#selectFuncionario").selectpicker('refresh');

    $("#cadastrar").text("Cadastrar");
}