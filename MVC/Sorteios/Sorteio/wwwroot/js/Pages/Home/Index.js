$(document).ready(function () {
});

$('#select_categoria_sorteio').change(function () {

    var FILTRO_TODOS = 0;

    var idCategoriaSelecionada = parseInt($('#select_categoria_sorteio').val());
    console.log(idCategoriaSelecionada);

    if (parseInt(idCategoriaSelecionada) == FILTRO_TODOS) {
        $.ajax({
            url: "/Sorteios/ObterTodosUltimosSorteiosRealizados/",
            type: "GET",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            success: function (response) {
                if (!response.erro) {
                    PreencherNaTelaUltimosSorteios(response);
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

    else {
        $.ajax({
            url: "/Sorteios/FiltrarSorteioPorCategoria/" + idCategoriaSelecionada,
            type: "GET",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            success: function (response) {
                if (!response.erro) {
                    PreencherNaTelaUltimosSorteios(response);
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

});

function PreencherNaTelaUltimosSorteios(dados) {
    $('#lista_ultimos_sorteios').html('');

    console.log(dados);

    $(dados).each(function (i, element) {

        var post = `<div class="col-md-4">
                        <div class="info">
                            <div class="card card-nav-tabs">
                                <div class="card-body card-img-principal" style="background: url('${element.url_imagem}'); width: 100%; height: 250px; background-position: center; background-size: contain; background-repeat: no-repeat;">
                                </div>
                                <div class="card-header card-header-danger">
                                    <text class="titulo-card-sorteios"><b>${element.edicao}° Edição</b></text>
                                </div>
                                <div class="card-body">
                                    <h4>
                                        <b>GANHADOR</b> <br />
                                        ${element.nome_ganhador}
                                    </h4>
                                    <h5>
                                        <b>Sorteado: </b> n°${element.numero_sorteado}
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </div>`;

        $('#lista_ultimos_sorteios').append(post);
    });
}