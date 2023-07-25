$(document).ready(function () {

});

function BuscarTodosUsuariosCadastrados() {

    $.ajax({
        url: "/Home/ObterTodosContatos",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                PreencherDadosTabelaContatos(response.resultado);
            }
        },
        error: function (response) {
            swal("Opss", "Aconteceu um imprevisto. Contate o administrador!", "error");
        }
    });

}

function PreencherDadosTabelaContatos(contatos) {

    $('table tbody').html("")

    $(contatos).each(function () {

        var linha = 
        `
        <tr>
            <td>${this.nome}</td>
            <td>${this.telefone}</td>
            <td>${this.celular}</td>
            <td>
                <button onclick="AbrirModalRemoverContato(${this.id_usuario}, '${this.nome}')" class="btn btn-sm btn-outline-danger">Excluir</button>
            </td>
        </tr>
        `;

        $('table tbody').append(linha);
    });

}