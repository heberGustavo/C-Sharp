var txtNome;
var txtEmail;
var txtSenha;
var txtIdUsuarioTemp;
var tabelaUsuario;
var modalUsuario;

$(document).ready(function () {
    Init();
});

function Init() {
    Variaveis();
    BuscarListaUsuarios();
}

/*METODOS GERAIS*/
function Variaveis() {
    txtNome = $('#txtNome');
    txtEmail = $('#txtEmail');
    txtSenha = $('#txtSenha');
    tabelaUsuario = $('#tabelaUsuario tbody');
    txtIdUsuarioTemp = $("#txtIdUsuarioTemp");
    modalUsuario = "modalUsuario";
}

function ModalUsuario() {
    AlterarVisibilidadeAtualModal(modalUsuario);
    ToogleCampoSenha(false);
}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(txtNome.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }
    else if (IsNullOrEmpty(txtEmail.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('E-mail');
        return false;
    }
    else if (IsNullOrEmpty(txtIdUsuarioTemp.val()) && IsNullOrEmpty(txtSenha.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Senha');
        return false;
    }

    return true;
}

function LimparCamposModal() {
    txtNome.val('')
    txtEmail.val('')
    txtSenha.val('')
    txtIdUsuarioTemp.val('');
}

function ModalUsuarioFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal(modalUsuario);
}

/*AJAX*/
function BuscarListaUsuarios() {

    MostraLoading();
    $.ajax({
        url: "/Usuario/Listar",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            EncerraLoading();
            $("#divListar").html(response)
        },
        error: function (response) {
            EncerraLoading();
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function PreencherModalUsuario(response) {
    txtNome.val(response.data.usaNome);
    txtEmail.val(response.data.usaEmail);
    txtIdUsuarioTemp.val(response.data.usaId);

    ToogleCampoSenha(true)
}

function ToogleCampoSenha(bool) {
    txtSenha.attr("disabled", bool)
}