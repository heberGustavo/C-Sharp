$(document).ready(function () {

});

function AbrirModalFinalizarSorteio(id) {
    $('#idSorteioSelecionado').val(id);

    $('#modalFinalizarSorteio').modal();
}

function FinalizarSorteio() {

    if (VerificarCamposObrigatorios()) {
        var dadosJson = GerarJsonCamposObrigatorios();

        $.ajax({
            url: "/Sorteios/FinalizarSorteio/",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(dadosJson),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success")
                        .then((okay) => {
                            window.location.href = "/Sorteios";
                        });
                }
                else {
                    swal("Opss", response.mensagem, "error");
                }
            },
            error: function (response) {
                swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                console.log(response);
            }
        });
    }

}

function GerarJsonCamposObrigatorios() {
    return {
        data_sorteio: $('#data_sorteio').val().trim(),
        numero_sorteado: parseInt($('#numero_sorteado').val().trim()),
        id_sorteio: parseInt($('#idSorteioSelecionado').val()),
        id_usuario: parseInt($('#select_nome_cliente').val())
    }
}

function VerificarCamposObrigatorios() {

    if (IsNullOrEmpty($('#data_sorteio').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Data');
        return false;
    }
    else if (IsNullOrEmpty($('#select_nome_cliente').val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('Ganhador');
        return false;
    }
    else if (IsNullOrEmpty($('#numero_sorteado').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Número sorteado');
        return false;
    }

    return true;
}