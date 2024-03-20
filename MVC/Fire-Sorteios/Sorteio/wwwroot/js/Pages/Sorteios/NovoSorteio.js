$(document).ready(function () {

    // Inicialzia o Editor
    //$('.textarea-editor').wysihtml5();
    $('.summernote').summernote({
        height: 300,
        minHeight: null,
        maxHeight: null,
        focus: true,
        lang: 'pt-BR',
        toolbar: [
            ['style', ['bold', 'italic', 'underline']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']]
        ],
        placeholder: 'Descreva as característica do produto',
    });

    $('#edit').click(function (e) {
        e.preventDefault();
        $('.summernote').summernote({ focus: true });
    });

    $('#save').click(function (e) {
        e.preventDefault();
        var markup = $('.summernote').summernote('code');
        $('.summernote').summernote('destroy');
    });
});


function CadastrarSorteio() {

    if (VerificarCamposObrigatorios()) {

        var dadosJson = GerarJsonCamposObrigatorios();

        $.ajax({
            url: "/Sorteios/CriarNovoSorteio",
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
    var arrayImagens = [];

    $('.input_dinamico .imagens_galeria').each(function (i, elemento) {
        arrayImagens.push(elemento.value);
    })

    return {
        sorteio: GerarObjetoJsonSorteio(),
        linkImagens: GerarObjetoJsonLinkImagens(arrayImagens)
    }
}

function GerarObjetoJsonSorteio() {
    return {
        nome: $('#nome').val().trim(),
        edicao: $('#edicao').val().trim(),
        valor: parseFloat($('#valor').val().trim()),
        quantidade_numeros: parseInt($('#quantidade_numeros').val().trim()),
        descricao_curta: $('#descricao_curta').val().trim(),
        descricao_longa: $('#contentSummernote').summernote('code'),
        id_categoria_sorteio: parseInt($('#select_categoria_sorteio').val())
    }
}

function GerarObjetoJsonLinkImagens(arrayImagens) {

    var itemImagem;
    var listaImagens = [];

    $(arrayImagens).each(function (i, item) {

        itemImagem = {
            "url_imagem": item
        }

        listaImagens.push(itemImagem);

    });

    return listaImagens;

}

function VerificarCamposObrigatorios() {

    if (IsNullOrEmpty($('#nome').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome do Sorteio');
        return false;
    }
    else if (IsNullOrEmpty($('#edicao').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Edição');
        return false;
    }
    else if (IsNullOrEmpty($('#valor').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Valor');
        return false;
    }
    else if (IsNullOrEmpty($('#quantidade_numeros').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Quantidade de números');
        return false;
    }
    else if (IsNullOrEmpty($('#descricao_curta').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Descrição Curta');
        return false;
    }
    else if (IsNullOrEmpty($('#select_categoria_sorteio').val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('Categoria do Sorteio')
        return false;
    }

    return true;
}



document.querySelector('#file-input').addEventListener("change", previewImages);
function previewImages() {
    $('#preview').html('');
    var lista_arquivos = [];
    var preview = document.querySelector('#preview');

    if (this.files) {
        [].forEach.call(this.files, GerarListaDeArquivos);
    }

    function GerarListaDeArquivos(file) {
        lista_arquivos.push(file);
    }

    UploadGalleryImage(lista_arquivos); /**/

    function readAndPreview(file) {

        // Make sure `file.name` matches our extensions criteria
        if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
            return alert(file.name + " is not an image");
        } // else...

        var reader = new FileReader();

        reader.addEventListener("load", function () {
            var image = new Image();
            image.height = 100;
            image.title = file.name;
            image.src = this.result;
            preview.appendChild(image);
        });

        reader.readAsDataURL(file);

    }

}

function CriarInputsDinamicamenteComLinkDosArquivos(caminhosArquivo) {
    $('#inputs_de_links_gerados').html('');

    var listaInputs = [];
    $(caminhosArquivo).each(function (i, link) {
        var input = `
                        <div class="input_dinamico">
                            <input type="hidden" class="imagens_galeria" value="${link}"/>
                        </div>
                    `;
        listaInputs.push(input);
    });

    var quantidadeArquivos = caminhosArquivo.length;
    $('#quantidade_arquivo').html(quantidadeArquivos + " arquivos");

    $('#inputs_de_links_gerados').append(listaInputs);
}