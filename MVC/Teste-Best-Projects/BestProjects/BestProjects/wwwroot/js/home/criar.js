$(document).ready(function () {

});

function CadastrarContato() {

    if (VerificaSeCamposObrigatoriosPreenchidos()) {

        var jsonBody = {
            nome: $("#nome").val().trim(),
            telefone: $("#telefone").val().trim(),
            celular: $("#celular").val().trim()
        };

        $.ajax({
            url: "/Home/CadastrarContato",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(jsonBody),
            success: function (response) {
                if (response.erro) {
                    swal("Opss", response.mensagem, "error");
                }
                else {
                    swal("Sucesso", response.mensagem, "success")
                        .then((okay) => {
                            LimparCamposFormularioCadastro();
                            BuscarTodosUsuariosCadastrados();
                        });
                }
            },
            error: function (response) {
                swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
            }
        });

    }

}

function VerificaSeCamposObrigatoriosPreenchidos() {
    if ($("#nome").val().trim() == "") {
        swal("Opss...", "Preencha o campo Nome!", "error");
        return false;
    }
    if ($("#telefone").val().trim() == "") {
        swal("Opss...", "Preencha o campo Telefone Res.!", "error");
        return false;
    }

    return true;
}

function LimparCamposFormularioCadastro() {
    $("#nome").val("");
    $("#telefone").val("");
    $("#celular").val("");
}