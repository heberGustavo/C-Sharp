var txtEmail;
var txtSenha;

$(document).ready(function () {
    Init();
});

function Init() {
    EsconderLoading();
    Variaveis();
}

function Variaveis() {
    txtEmail = $("#email");
    txtSenha = $("#senha")
}

function Autenticar() {
    MostraLoading();

    if (!VerificarCamposObrigatorios()) {
        EncerraLoading();
        return;
    }

    var jsonData = {
        email: txtEmail.val(),
        senha: txtSenha.val()
    }

    console.log(jsonData)

    $.ajax({
        url: "/Login/Autenticar/",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            if (!response.erro) {

            }
            else {
                $.each(response.mensagem, function (index, value) {
                    MostrarAlertMensagemErro(value)
                });
            }
        },
        error: function (response) {
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        },
        complete: function () {
            EncerraLoading();
        }
    });

}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(txtEmail.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido("Email")
        return false;
    }
    else if (IsNullOrEmpty(txtSenha.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido("Senha")
        return false;
    }

    return true;
}