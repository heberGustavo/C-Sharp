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

    window.location.href = MontarUrl("Obra", "Index");
    return;

    if (!VerificarCamposObrigatorios()) {
        EncerraLoading();
        return;
    }

    var jsonData = {
        email: txtEmail.val(),
        senha: txtSenha.val()
    }
    
    $.ajax({
        url: "/Login/Autenticar/",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                window.location.href = MontarUrl("Obra", "Index");
            }
            else {
                MostrarAlertMensagemErro(response.mensagem);
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