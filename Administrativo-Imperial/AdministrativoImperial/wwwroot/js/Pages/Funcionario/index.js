var nome;
var selectFuncao;
var valorDiaria;
var valorMensal;
var dataContratacao;
var tabelaFuncionario;

$(document).ready(function () {
    InicializaVariaveis();
});

function InicializaVariaveis() {
    nome = $('#nome');
    selectFuncao = $('#selectFuncao');
    valorDiaria = $('#valorDiaria');
    valorMensal = $('#valorMensal');
    dataContratacao = $('#dataContratacao');
    tabelaFuncionario = $('#tabelaFuncionarios tbody');
}

function ModalFuncionario() {
    $('#modalFuncionario').modal('show');
}

function VerificaCheckClicado() {

    var selecionado = RetornaSelecionado();

    var itemSelecionado = $(selecionado).attr('id')

    if (itemSelecionado == 'customRadioDiaria') {
        $('#grupo-diaria').removeClass('d-none');
        $('#grupo-mensal').addClass('d-none');
    }
    else if (itemSelecionado == 'customRadioMensal') {
        $('#grupo-mensal').removeClass('d-none');
        $('#grupo-diaria').addClass('d-none');
    }

}

function RetornaSelecionado() {
    var item;

    $('input[type=radio]:checked').each(function () {
        item = this;
    })

    return item;
}

function VerificarCamposObrigatorios() {
    if (IsNullOrEmpty(nome.val())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Nome');
        return false;
    }
    else if (IsNullOrEmpty(selectFuncao.val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('Função');
        return false;
    }
    else if (IsNullOrEmpty(valorDiaria.val()) && IsNullOrEmpty(valorMensal.val())) {
        MostrarModalErroCampoObrigatorioNaoSelecionado('');
        swal("Atenção", "É necessário preencher valor de Diária ou Mensal", "error");
        return false;
    }

    return true;
}

function BuscarListaFuncionarios() {

    $.ajax({
        url: "/Funcionario/ObterTodosFuncionarios",
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            PreencherTabelaFuncionarios(response);
        },
        error: function (response) {
            console.log(response);
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function PreencherTabelaFuncionarios(dados) {
    tabelaFuncionario.html("");

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
                        <td>
                            <p class="text-xs font-weight-bold mb-0">${this.id_funcao_funcionario}</p>
                        </td>
                        <td class="align-middle">
                            <p class="text-xs font-weight-bold mb-0">R$ ${!IsNullOrEmpty(this.mensal) ? FormatDinheiro(ConverterParaFloat(this.mensal)) : ''} </p>
                        </td>
                        <td class="align-middle">
                            <p class="text-xs font-weight-bold mb-0">R$ ${!IsNullOrEmpty(this.diaria) ? FormatDinheiro(ConverterParaFloat(this.diaria)) : ''}</p>
                        </td>
                        <td class="align-middletext-sm">`

                            if (this.excluido == false)
                            {
                                linhaParte2 = `<span class="badge badge-sm bg-gradient-success">Ativo</span>`
                            }
                            else
                            {
                                linhaParte2 = `<span class="badge badge-sm bg-gradient-secondary">Desativo</span>`
                            }

                        linhaParte3 = 
                        `</td>
                        <td class="align-middle">
                            <a href="javascript:;" class="text-secondary font-weight-bold text-xs" data-toggle="tooltip" data-original-title="Edit user">
                                Edit
                            </a>
                        </td>
                    </tr>`;

        tabelaFuncionario.append(linhaParte1 + linhaParte2 + linhaParte3)
    });
}

function LimparCamposModal() {
    nome.val('')
    selectFuncao.val('0');
    valorDiaria.val('')
    valorMensal.val('')
    dataContratacao.val('')
}

function ModalFuncionarioFechar() {
    LimparCamposModal();
    AlterarVisibilidadeAtualModal('modalFuncionario');
}