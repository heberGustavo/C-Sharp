var txtIdFuncaoTemp;
var txtNomeFuncao;
var selectExcluido;

var tabelaFuncao;

$(document).ready(function () {
    InicializaVariaveis();
});

function InicializaVariaveis() {
    txtIdFuncaoTemp = $("#idFuncaoTemp");
    txtNomeFuncao = $('#nome');
    selectExcluido = $('#selectExcluido');
    
    tabelaFuncao = $('#tabelaFuncao tbody');
}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(txtNomeFuncao.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }

    return true;
}

function BuscarListaFuncaoFuncionario() {

    $.ajax({
        url: "/FuncaoFuncionario/ObterTodasFuncoes",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            PreencherTabelaFuncoes(response);
        },
        error: function (response) {
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function PreencherTabelaFuncoes(dados) {
    tabelaFuncao.html("");

    $(dados.resultado).each(function () {
        var linhaParte1;
        var linhaParte2;
        var linhaParte3;

        linhaParte1 = `<tr>
                        <td>
                            <div class="d-flex px-3 py-1">
                                <div class="d-flex flex-column justify-content-center">
                                    <h6 class="mb-0 text-sm">${this.nome}</h6>
                                </div>
                            </div>
                        </td>
                        <td class="align-middletext-sm">`

                        if (this.excluido == false) {
                            linhaParte2 = `<span class="badge badge-sm bg-gradient-success">Ativo</span>`
                        }
                        else {
                            linhaParte2 = `<span class="badge badge-sm bg-gradient-secondary">Desativo</span>`
                        }

                        linhaParte3 =
                        `</td>
                         <td class="align-middle">
                            <button class="btn btn-border-red btnAlterarFuncao" type="button" data-id="${this.id_funcao_funcionario}" data-nome="${this.nome}" data-excluido="${this.excluido}" data-toggle="tooltip" data-original-title="Editar Função">
                                <span class="btn-inner--icon color-red"><i class="fas fa-edit"></i></span>
                            </button>
                            <button class="btn btn-border-blue btnExcluir" data-id="${this.id_funcao_funcionario}" data-nome="${this.nome}" type="button" data-toggle="tooltip" data-original-title="Deletar Função">
                                <span class="btn-inner--icon color-blue"><i class="fa-solid fa-trash"></i></span>
                            </button>
                        </td>
                    </tr>`;


        tabelaFuncao.append(linhaParte1 + linhaParte2 + linhaParte3)
    });
}