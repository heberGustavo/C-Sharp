$(document).ready(function () {

    var textoHtml = $('#texto_longo_html').text();

    setTimeout(function () {
        $('#contentSummernote').summernote('code', textoHtml);
    }, 80);

});

function AtualizarSorteio(id) {

    if (VerificarCamposObrigatorios()) {

        var dadosJson = GerarJsonCamposObrigatoriosEdicaoSorteio(id);
        console.log(dadosJson);

        $.ajax({
            url: "/Sorteios/EditarSorteio",
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

function GerarJsonCamposObrigatoriosEdicaoSorteio(id) {
    var arrayImagens = [];

    $('.input_dinamico .imagens_galeria').each(function (i, elemento) {
        arrayImagens.push(elemento.value);
    });

    return {
        sorteio: {
            id_sorteio: parseInt(id),
            nome: $('#nome').val().trim(),
            edicao: $('#edicao').val().trim(),
            valor: parseFloat($('#valor').val().trim()),
            quantidade_numeros: parseInt($('#quantidade_numeros').val().trim()),
            descricao_curta: $('#descricao_curta').val().trim(),
            descricao_longa: $('#contentSummernote').summernote('code'),
            id_categoria_sorteio: parseInt($('#select_categoria_sorteio').val()),
            status: ObterStatusBoleanoNoInput('statusSorteioSelecionado')
        },
        linkImagens: GerarObjetoJsonLinkImagens(arrayImagens)
    }
}