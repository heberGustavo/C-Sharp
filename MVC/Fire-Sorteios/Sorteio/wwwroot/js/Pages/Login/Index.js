$(document).ready(function () {
    swal("Usuarios de Acesso", "Usuario Admin: admin@sorteios.com - Senha: admin \n Usuario Comum: user@sorteios.com - Senha: user");
});

function RealizarLogin() {

    var USUARIO_ADMIN = 1;
    var USUARIO_CLIENTE = 2;

    if (VerificaCamposPreenchidos()) {

        var jsonBody = {
            "email": $('#email').val(),
            "senha": $('#senha').val()
        };

        $.ajax({
            url: "/Login/Login",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(jsonBody),
            success: function (response) {
                console.log(response);
                if (response.erro) {
                    swal("Erro", response.mensagem, "error");
                }
                else {
                    if (parseInt(response.model.id_tipo_usuario) == USUARIO_ADMIN) {
                        window.location.href = "/Sorteios";
                    }
                    else if (parseInt(response.model.id_tipo_usuario) == USUARIO_CLIENTE) {
                        window.location.href = "/AcessoInterno";
                    }
                    else {
                        swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                    }
                }
            },
            error: function (response) {
                swal("Erro", response.mensagem, "error");
            }
        });
    }

}

function VerificaCamposPreenchidos() {

    if (IsNullOrEmpty($('#email').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Email');
        return false;
    }
    else if (IsNullOrEmpty($('#senha').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Senha');
        return false;
    }

    return true;
}