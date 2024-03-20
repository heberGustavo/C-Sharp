function UploadImage(sender, idElementoLabel, idElementoInputCaminhoArquivo, idInput) {
    $('#loading').removeClass('d-none');

    var MEGAS_5 = 5242880;
    var fi = document.getElementById(idInput);

    if (fi.files.length < 2) {
        var fsizet = 0;

        for (var i = 0; i <= fi.files.length - 1; i++) {

            // TAMANHO DO ARQUIVO
            var fsize = fi.files.item(i).size;

            // TOTAL
            fsizet = fsizet + fsize;
        }

        if (fsizet > 0 && fsizet <= MEGAS_5) {

            var idFile = $(sender).attr('id');
            var file = sender.files[0];
            var formData = new FormData();

            var totalFiles = document.getElementById(idFile).files.length;
            for (var i = 0; i < totalFiles; i++) {
                file = document.getElementById(idFile).files[i];
                formData.append("file", file);
                $('#' + idElementoLabel).html(file.name);
            }

            $.ajax({
                type: "POST",
                url: "/Arquivos/Upload",
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (response) {
                    $('#loading').addClass('d-none');

                    swal("Sucesso", "Arquivo foi enviado com sucesso!", "success");
                    $('#' + idElementoInputCaminhoArquivo).val(response.caminhoArquivo);
                },
                error: function (error) {
                    console.log(error)
                    swal("Ops!", "Erro so submeter a foto." + error, "error");
                    $('#loading').addClass('d-none');
                }
            });
        }
        else if (fsizet > MEGAS_5) {
            alert('Não permetido maior que 5mb');
            $('#loading').addClass('d-none');
        }
    }
    else {
        alert('Maximo de 1 arquivos.');
        $('#loading').addClass('d-none');
    }
}

function UploadGalleryImage(files) {
    $('#loading').removeClass('d-none');

    var MEGAS_5 = 5242880;
    var fsizet = 0;

    for (var i = 0; i <= files.length - 1; i++) {

        // TAMANHO DO ARQUIVO
        var fsize = files[i].size;

        // TOTAL
        fsizet = fsizet + fsize;

        if (fsizet > 0 && fsizet <= MEGAS_5) {

            var formData = new FormData();

            var totalFiles = files.length;
            for (var i = 0; i < totalFiles; i++) {
                file = files[i];
                formData.append("file", file);
            }

            $.ajax({
                type: "POST",
                url: "/Arquivos/UploadImages",
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (response) {
                    $('#loading').addClass('d-none');
                    CriarInputsDinamicamenteComLinkDosArquivos(response.caminhosArquivo);
                    swal("Sucesso", "Fotos anexadas!", "success");
                },
                error: function (error) {
                    console.log(error)
                    swal("Ops!", "Erro so submeter a foto." + error, "error");
                    $('#loading').addClass('d-none');
                }
            });

        } else if (fsizet > MEGAS_5) {
            alert('Não permetido maior que 5mb');
            $('#loading').addClass('d-none');
        }
    }
}