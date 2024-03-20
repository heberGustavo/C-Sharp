$(document).ready(function () {

    $('#select_tipo_conta').change(function () {

        var idSelecionado = $(this).val();
        var idPix = 2;

        if (parseInt(idSelecionado) == idPix) {
            $('#campo_chave_pix').removeClass('d-none');
        }
        else {
            $('#campo_chave_pix').addClass('d-none');
        }
    });
    
});

function CadastrarFormaDePagamento() {

    if (VerificarCamposObrigatorios()) {

        var dadosJson = GerarJsonCamposObrigatorios();
        console.log(dadosJson)

        $.ajax({
            url: "/FormasDePagamento/CriarNovaFormaDePagamento",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(dadosJson),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success")
                        .then((okay) => {
                            window.location.href = "/FormasDePagamento";
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
        nome_banco: $('#nome_banco').val().trim(),
        codigo_banco: $('#codigo_banco').val().trim(),
        favorecido: $('#favorecido').val().trim(),
        cpf: $('#cpf').val().trim(),
        agencia: $('#agencia').val().trim(),
        conta: $('#conta').val().trim(),
        url_imagem: $('#caminhoArquivoLogoBanco').val().trim(),
        id_tipo_forma_de_pagamento: parseInt($('#select_tipo_conta').val()),
        pix: $('#pix').val()
    }
}

function VerificarCamposObrigatorios() {
    var idPix = 2;

    if (IsNullOrEmpty($('#favorecido').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Favorecido');
        return false;
    }
    else if (IsNullOrEmpty($('#cpf').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('CPF ou CNPJ');
        return false;
    }
    else if (IsNullOrEmpty($('#nome_banco').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome do Banco');
        return false;
    }
    else if (IsNullOrEmpty($('#codigo_banco').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Código do Banco');
        return false;
    }
    else if (IsNullOrEmpty($('#agencia').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Agência');
        return false;
    }
    else if (IsNullOrEmpty($('#conta').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Número da Conta');
        return false;
    }
    else if (IsNullOrEmpty($('#caminhoArquivoLogoBanco').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Logo do Banco');
        return false;
    }
    else if (IsNullOrEmpty($('#select_tipo_conta').val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('Tipo de Conta')
        return false;
    }
    else if (parseInt($('#select_tipo_conta').val()) == idPix && IsNullOrEmpty($('#pix').val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Chave Pix')
        return false;
    }

    return true;
}