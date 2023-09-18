$(document).ready(function () {

});

function Cadastrar() {

    if (!ConfirmacaoNaoRobo()) {
        swal("Opss", "Tente novamente, parece que você é um robo! Ai não meu chapa!", "error");
        return
    }

    if (VerificaSeCamposObrigatoriosPreenchidos()) {

        if ($('#idEditar').val() == "") { //REALIZAR CADASTRO

            var jsonBody = {
                nome: $("#nome").val().trim(),
                data: ConverterDataParaUSA($('#data').val()),
                salario: $("#salario").val()
            };

            $.ajax({
                url: "/Funcionario/Cadastrar",
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
                                ListarDadosFuncionario();
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
                salario: $("#salario").val()
            };

            $.ajax({
                url: "/Funcionario/Editar",
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
                                ListarDadosFuncionario();
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
    if ($("#salario").val().trim() == "") {
        swal("Opss...", "Preencha o campo Salário", "error");
        return false;
    }
    if (ConverterParaFloat($("#salario").val()) <= 0.0000000) {
        swal("Opss...", "Informe um valor maior que 0", "error");
        return false;
    }

    return true;
}

function LimparCampos() {
    $("#nome").val("");
    $("#data").val("");
    $("#salario").val("");
    $("#idEditar").val("");

    $("#cadastrar").text("Cadastrar");
}