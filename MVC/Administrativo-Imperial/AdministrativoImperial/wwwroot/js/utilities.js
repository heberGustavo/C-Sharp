function upperCase(a) {
    setTimeout(function () {
        a.value = a.value.toUpperCase();
    }, 1);
}

function VerificarHorarioDeTrabalho(horario) {

    var horarioCompleto = horario.value.length;
    if (horarioCompleto == 13) {
        var hora = horario.value.split(' - ');
        var horarioEntrada = hora[0].split(':');
        var horarioSaida = hora[1].split(':');

        var horaEntrada = parseInt(horarioEntrada[0]);
        var minutoEntrada = parseInt(horarioEntrada[1]);

        var horaSaida = parseInt(horarioSaida[0]);
        var minutoSaida = parseInt(horarioSaida[1]);

        if (horaEntrada <= 23 && minutoEntrada <= 59) {
            if (horaSaida <= 23 && minutoSaida <= 59) {
                //alert("passou")
            }
            else {
                swal("Opss", "Horário de saida inválido!", "error");
                horario.value = "";
            }
        }
        else {
            swal("Opss", "Horário de entrada inválido!", "error");
            horario.value = "";
        }
    }
    else {
        swal("Opss", "Horário inválido!", "error");
        horario.value = "";
    }

}

function MensagemAvisoManutenção() {
    swal("Opss", "Desculpe o transtorno! Tela em manutenção!", "warning");
}

function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}

function BaixarArquivo(link) {

    var json = {
        "link": link
    }

    $.ajax({
        url: "/Arquivos/Download/",
        type: "POST",
        contentType: 'application/json; charset=UTF-8',
        data: JSON.stringify(json),
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                console.log(response)
                window.location.href = response.caminho_arquivo;
            }
            else {
                swal("Opss", "Erro ao buscar empresa. Tente novamente!", "error");
            }
        },
        error: function (response) {
            console.log(response)
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
        }
    });

}

function ConverterDataParaUSA(data) {
    var dataPadrao = new Date(data);

    var mesPadrao = dataPadrao.getMonth() + 1;
    var mes = ("00" + mesPadrao).slice(-2); // "01"

    var dataFormatada = dataPadrao.getFullYear() + '-' + mes + '-' + dataPadrao.getDate();
    return dataFormatada;
}

function ConverterParaDataUSA(data) {
    if (data.indexOf('0001') != -1) {
        return '';
    }

    if (data.indexOf('T') != -1) {
        var dataCortada = data.split('T');
        return dataCortada[0];
    }

    var dataPadrao = data.split(' ');
    dataPadrao = dataPadrao[0].split('/');

    var dia = dataPadrao[0];
    var mes = dataPadrao[1];
    var ano = dataPadrao[2];

    var data = `${ano}-${mes}-${dia}`;
    return data;
}

function ConverterParaDataBr(id) {
    var dataPadrao = $('#' + id).val().split('-');

    var ano = dataPadrao[0];
    var mes = dataPadrao[1];
    var dia = dataPadrao[2];

    var data = dia + "/" + mes + "/" + ano;
    return data;
}

function ConverterParaDataComBarraParaBr(data) {
    if (data.indexOf('T') != -1) {
        var dataCortada = data.split('T');

        var dataPadraoT = dataCortada[0].split('-');
        
        var ano = dataPadraoT[0];
        var mes = dataPadraoT[1];
        var dia = dataPadraoT[2];

        var data = dia + "/" + mes + "/" + ano;
        return data;
    }

    var dataPadrao = data.split('-');

    var dia = dataPadrao[0];
    var mes = dataPadrao[1];
    var ano = dataPadrao[2];

    var data = dia + "/" + mes + "/" + ano;
    return data;
}

function RemoverLinha(botao, idTabela) {
    var linha = $(botao).parent().parent();

    $('#' + idTabela).DataTable().row(linha).remove().draw();
}

function ValidateEmail(id) {
    var inputText = $('#' + id).val();
    var mensagemErro = $('#' + id).parent().find('.email-invalido');

    if (inputText == '') {
        mensagemErro.text('');
        return;
    }

    var mailformat = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if (inputText.match(mailformat)) {
        mensagemErro.text('');
        return true;
    }
    else {
        mensagemErro.text('Informe um e-mail válido!');
        return false;
    }
}

function IsNullOrEmpty(value, ignoreZeroValidation) {
    var result = false;

    if (ignoreZeroValidation) {
        if (!value || value == undefined || value == "" || value.length == 0) {
            result = true;
        }
    }
    else {
        if (!value || value == undefined || value == "" || value.length == 0 || value == "0") {
            result = true;
        }
    }

    return result;
}

function IsNullOrEmptyAcceptZero(value, ignoreZeroValidation) {
    var result = false;

    if (ignoreZeroValidation) {
        if (!value || value == undefined || value == "" || value.length == 0) {
            result = true;
        }
    }
    else {
        if (!value || value == undefined || value == "" || value.length == 0) {
            result = true;
        }
    }

    return result;
}

function AlteraValorVariavel(key, value) {
    localStorage.setItem(key, value);
}
function RetornaValorVariavel(key) {
    return localStorage.getItem(key);
}

function enterVerify(e) {
    var charCode = (typeof e.which === "number") ? e.which : e.keyCode;

    var enterKey = 13;

    if (charCode === enterKey) {
        document.activeElement.blur();
    }
}

//Função para preencher zero a esquerda no tempo
function AddZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

//Formatação dos campos de data que retornao do JSON para datetime padrão
function FormataJsonData(dataHora) {
    var currentTime = new Date(dataHora);
    var month = AddZero(currentTime.getMonth() + 1);
    var day = AddZero(currentTime.getDate());
    var year = currentTime.getFullYear();
    var dateFMT = day + "/" + month + "/" + year;

    return dateFMT;
}

function FormataJsonHora(dataHora) {
    var currentTime = new Date(dataHora);
    var h = AddZero(currentTime.getHours());
    var m = AddZero(currentTime.getMinutes());
    var timeFMT = h + ":" + m;

    return timeFMT;
}

function DesabilitarDatasAnteriores(idInputData) {
    var dateToday = new Date();

    $('#' + idInputData).datepicker({
        defaultDate: "+1w",
        minDate: dateToday
    });
}

//Formata o numero de retorno do json em formato de nota Real
function FormatDinheiro(numero) {
    //Verifica se o valor não está nulo, caso esteja retorna 0,00
    if (numero != null) {
        if (IsString(numero)) numero = ConverterParaFloat(numero);

        numero = numero.toFixed(2).split('.');
        numero[0] = numero[0].split(/(?=(?:...)*$)/).join('.');
        return numero.join(',');
    }
    else {
        return "0,00";
    }
}

function ConverterParaFloat(stringValor) {
    if (IsString(stringValor))
        return parseFloat(stringValor.replaceAll("R$", '').trim().replaceAll('.', '').replaceAll(',', '.'));
    else
        return stringValor;
}

//Função para retornar a data atul formatada - DD/MM/YYYY
function RetornarDataAtual() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    return today = dd + '/' + mm + '/' + yyyy;
}

//Função para retornar a Mes e ano atual formatada DD/MM/YYYY
function RetornaMesAnoAtual() {
    var today = new Date();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (mm < 10) {
        mm = '0' + mm;
    }

    return today = mm + '/' + yyyy;
}

// Essa função pega o mês e ano selecionados no datepicker e retorna a data com o primeiro dia do mês
function ObterDataInicioSelecionada() {

    var dpMes = $('#lblTotalMensal').datepicker().data('datepicker');
    var data = dpMes.selectedDates[0];

    var mesSelecionado = data.getMonth() + 1;
    var anoSelecionado = data.getFullYear();

    var dataInicio = '1' + '-' + mesSelecionado + '-' + anoSelecionado;
    var dataInicioFormatada = moment(dataInicio, 'DD-MM-YYYY').format('YYYY-MM-DD');

    return dataInicioFormatada;
}

// Essa função retorna uma data com o último dia do mês da data passada por parâmetro
function ObterDataFinalSelecionada(dataInicioSelecionada) {

    var dataFimSelecionada = moment(dataInicioSelecionada).endOf('month').format('YYYY-MM-DD');

    return dataFimSelecionada;
}

function HabilitarBotao(nomeBotao, labelBotao) {

    $('#' + nomeBotao).html(labelBotao);
    $('#' + nomeBotao).prop('disabled', false);
}

function DesabilitarBotao(nomeBotao, labelBotao) {

    $('#' + nomeBotao).html(labelBotao);
    $('#' + nomeBotao).attr("disabled", "disabled");
}

function ObterNomeMesPorData(data) {

    var dataFormatada = new Date(data);

    var mes = dataFormatada.getMonth();

    switch (mes) {
        case 0:
            return 'Janeiro';
        case 1:
            return 'Fevereiro';
        case 2:
            return 'Março';
        case 3:
            return 'Abril';
        case 4:
            return 'Maio';
        case 5:
            return 'Junho';
        case 6:
            return 'Julho';
        case 7:
            return 'Agosto';
        case 8:
            return 'Setembro';
        case 9:
            return 'Outubro';
        case 10:
            return 'Novembro';
        case 11:
            return 'Dezembro';
        default:
            return 'Mês não existe';
    }
}

function RemoverCaracteresEspeciaisString(elemento) {

    return elemento.replace(/[^\w\s]/gi, '');
}

function RemoverEspacosString(elemento) {

    return elemento.replace(/\s/g, '');
}

function ObterValorCampoFormatado(nomeCampo) {
    var valor = 0;
    if (!IsNullOrEmpty($('#' + nomeCampo).val())) {
        valor = $('#' + nomeCampo).val();
        valor = parseFloat(valor.replace(',', '.'));
    }
    return valor;
}

function MostrarModalErroCampoObrigatorioNaoSelecionado(nomeCampo) {

    swal("Atenção", "É necessário selecionar " + nomeCampo, "error");
}

function MostrarModalErroCampoObrigatorioNaoPreenchido(nomeCampo) {

    swal("Atenção", "É necessário preencher o campo " + nomeCampo, "error");
}

function MostrarModalErroComFocusNoCampo(mensagem, nomeCampo) {

    swal("Atenção", mensagem, "warning");
    $('#' + nomeCampo).focus();
}

function MostrarModalErro(nomeDoCampoNaMensagem, nomeCampo) {

    swal("Atenção", 'É necessário preencher o campo ' + nomeDoCampoNaMensagem, "warning");
    $('#' + nomeCampo).focus();
    return false;
}

function MostrarModalErroMensagemPersonalizada(mensgem) {

    swal("Atenção", mensgem, "warning");
    return false;
}

function MostrarAlertMensagemErro(mensagem) {
    Toast.danger(mensagem, {
        showProgress: true,
        duration: 5000,
        toastLocation: 'top'
    });
}

function IsString(value) {
    return typeof value === 'string' || value instanceof String;
}


/******** MÉTODOS DE FORMATAÇÃO ********/

function FormatarDataDiaMesParaDataCompleta(dataDiaMes) {

    var year = moment().format('YYYY');
    dataDiaMes = dataDiaMes.replace("/", "-");
    return dataDiaMes + "-" + year;
}

function FormatarDataMesAno(data) {

    var dataIniFMT = moment(data, 'MM/YYYY').format('MM-DD-YYYY');
    return dataIniFMT + " 00:00:00";
}

function FormatarData(dataNascimento) {
    var dataNascimentoFormatada = AddZero(dataNascimento.getDate()) + '/' + AddZero((dataNascimento.getMonth() + 1)) + '/' + dataNascimento.getFullYear();
    return dataNascimentoFormatada;
}

function FormatarDataParaCSharp(data) {

    var dataFormatada = moment(data, 'DD/MM/YYYY').format('YYYY-MM-DD');
    var dataHoraFormatada = dataFormatada + " 00:00:00";

    return dataHoraFormatada;
}


/******** MÉTODOS DE VALIDACAO ********/

function ValidarDataInicioMenorDataFim(dataInicio, dataFim) {

    var dataInicioEhMenorDataFim = moment(dataInicio).isBefore(dataFim);

    return dataInicioEhMenorDataFim;
}

function ValidarDataInicioIgualDataFim(dataInicio, dataFim) {

    var dataInicioEhIgualDataFim = moment(dataInicio).isSame(dataFim);

    return dataInicioEhIgualDataFim;
}

function ValidarDataInicioMenorOuIgualDataFim(dataInicio, dataFim) {

    var dataInicioFormatada = moment(dataInicio, 'DD/MM/YYYY').format('YYYY-MM-DD');
    var dataFimFormatada = moment(dataFim, 'DD/MM/YYYY').format('YYYY-MM-DD');

    var dataInicioEhMenorDataFim = moment(dataInicioFormatada).isBefore(dataFimFormatada);

    if (!dataInicioEhMenorDataFim) {
        var dataInicioEhIgualDataFim = moment(dataInicioFormatada).isSame(dataFimFormatada);
        return dataInicioEhIgualDataFim;
    }

    return dataInicioEhMenorDataFim;
}

function ValidarMes(mes) {

    return mes <= 0 || mes > 12 ? false : true;
}

function VerificarDiaEhValido(dia, numeroDiasMes) {

    return dia <= numeroDiasMes ? true : false;

}

function ValidarDiaMes(dia, mes) {

    switch (mes) {
        case 1:
            return VerificarDiaEhValido(dia, 31);
        case 2:
            return VerificarDiaEhValido(dia, 28);
        case 3:
            return VerificarDiaEhValido(dia, 31);
        case 4:
            return VerificarDiaEhValido(dia, 30);
        case 5:
            return VerificarDiaEhValido(dia, 31);
        case 6:
            return VerificarDiaEhValido(dia, 30);
        case 7:
            return VerificarDiaEhValido(dia, 31);
        case 8:
            return VerificarDiaEhValido(dia, 31);
        case 9:
            return VerificarDiaEhValido(dia, 30);
        case 10:
            return VerificarDiaEhValido(dia, 31);
        case 11:
            return VerificarDiaEhValido(dia, 30);
        case 12:
            return VerificarDiaEhValido(dia, 31);
        default:
            false;
    }
}

function ValidarAno(ano) {

    return ano <= 1990 || ano > 2100 ? false : true;
}

function ValidarData(nomeCampo) {

    var data = $('#' + nomeCampo).val();

    if (IsNullOrEmpty(data)) {
        MostrarModalErroComFocusNoCampo("É necessário que o campo data seja preenchido!", nomeCampo);
        return false;
    }
    else if (data.length < 10) {
        MostrarModalErroComFocusNoCampo("A data é inválida!", nomeCampo);
        return false;
    }
    else if (!ValidarDiaMes(parseInt(data.substring(0, 2)), parseInt(data.substring(3, 5)))) {
        MostrarModalErroComFocusNoCampo("A data é inválida!", nomeCampo);
        return false;
    }
    else if (!ValidarAno(parseInt(data.substring(6)))) {
        MostrarModalErroComFocusNoCampo("O ano da data é inválido!", nomeCampo);
        return false;
    }
    return true;
}

function ValidarDataMesAno(nomeCampoData) {

    var data = $('#' + nomeCampoData).val();

    if (IsNullOrEmpty(data)) {
        MostrarModalErroComFocusNoCampo("É necessário que o campo data seja preenchida!", nomeCampoData);
        return false;
    }
    else if (data.length < 7) {
        MostrarModalErroComFocusNoCampo("A data é inválida!", nomeCampoData);
        return false;
    }
    else if (!ValidarMes(parseInt(data.substring(0, 2)))) {
        MostrarModalErroComFocusNoCampo("O mês da data é inválido!", nomeCampoData);
        return false;
    }
    else if (!ValidarAno(parseInt(data.substring(3)))) {
        MostrarModalErroComFocusNoCampo("O ano da data é inválido!", nomeCampoData);
        return false;
    }

    return true;
}

function ValidarDataInicioEDataFim(nomeCampoDataInicio, nomeCampoDataFim) {

    if (!ValidarData(nomeCampoDataInicio)) return;
    else if (!ValidarData(nomeCampoDataFim)) return;
    else if (!ValidarDataInicioMenorOuIgualDataFim($('#' + nomeCampoDataInicio).val(), $('#' + nomeCampoDataFim).val())) {
        MostrarModalErroComFocusNoCampo("A data inicial precisa ser menor que a data final!", nomeCampoDataInicio);
        return false;
    }
    return true;
}

function ValidarDataInicioEDataFimDiaMes(nomeCampoDataInicio, nomeCampoDataFim) {

    var dataInicio = $('#' + nomeCampoDataInicio).val();
    var dataFim = $('#' + nomeCampoDataFim).val();

    if (IsNullOrEmpty(dataInicio)) {
        MostrarModalErroComFocusNoCampo("É necessário que o campo data inicial seja preenchido!", nomeCampoDataInicio);
        return false;
    }
    else if (dataInicio.length < 5) {
        MostrarModalErroComFocusNoCampo("A data inicial é inválida!", nomeCampoDataInicio);
        return false;
    }
    else if (IsNullOrEmpty(dataFim)) {
        MostrarModalErroComFocusNoCampo("É necessário que o campo data final seja preenchida!", nomeCampoDataFim);
        return false;
    }
    else if (dataFim.length < 5) {
        MostrarModalErroComFocusNoCampo("A data final é inválida!", nomeCampoDataFim);
        return false;
    }
    else if (!ValidarDiaMes(parseInt(dataInicio.substring(0, 2)), parseInt(dataInicio.substring(3)))) {
        MostrarModalErroComFocusNoCampo("Data inicial é inválida!", nomeCampoDataInicio);
        return false;
    }
    else if (!ValidarDiaMes(parseInt(dataFim.substring(0, 2)), parseInt(dataFim.substring(3)))) {
        MostrarModalErroComFocusNoCampo("Data final é inválida!", nomeCampoDataFim);
        return false;
    }
    else if (!ValidarDataInicioMenorOuIgualDataFim(FormatarDataDiaMesParaDataCompleta(dataInicio), FormatarDataDiaMesParaDataCompleta(dataFim))) {
        MostrarModalErroComFocusNoCampo("A data inicial precisa ser menor que a data final!", nomeCampoDataInicio);
        return false;
    }
    return true;
}

function ValidarDataInicioEDataFimMesAno(nomeCampoDataInicio, nomeCampoDataFim) {

    if (!ValidarDataMesAno(nomeCampoDataInicio)) return false;
    else if (!ValidarDataMesAno(nomeCampoDataFim)) return false;
    else if (!ValidarDataInicioMenorOuIgualDataFim(FormatarDataMesAno($('#' + nomeCampoDataInicio).val()), FormatarDataMesAno($('#' + nomeCampoDataFim).val()))) {
        MostrarModalErroComFocusNoCampo("A data inicial precisa ser menor que a data final!", nomeCampoDataInicio);
        return false;
    }
    return true;
}

function InicializarDateTimePicker(idElemento) {
    $("#" + idElemento).datepicker({
        todayButton: new Date(),
        autoClose: true
    });
    dpDia = $('#' + idElemento).datepicker().data('datepicker');
    dpDia.selectDate(new Date());
}

function SetarMenuAtivo(nomeMenu) {
    if (nomeMenu === "Agenda") $("#menuitem-Agenda").addClass("active");
    else return;
}

function MostraLoading() {
    $('#loader').show();
}

function EncerraLoading() {
    $('#loader').show().delay(300).fadeOut();
}

function EsconderLoading() {
    $('#loader').hide();
}

function PadLeft(str, max) {
    str = str.toString();
    return str.length < max ? PadLeft("0" + str, max) : str;
}

function BuscarPessoasPreencherSelectPicker(idSelectPicker, idValorDefault, descricaoValorDefault) {
    $.ajax({
        url: "/Pessoa/ObterPessoasSelectPicker",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            swal("Ops!", "Houve um erro ao carregar os dados. Tente novamente em instantes.", "error");
        },
        success: function (response) {
            if (response.erro)
                swal("Ops!", "Houve um erro ao carregar os dados. Tente novamente em instantes.", "error");
            else {
                var select = document.getElementById(idSelectPicker);
                select.options.length = 0;

                select.options[select.options.length] = new Option(descricaoValorDefault, idValorDefault);

                for (var i = 0; i < response.dados.length; i++) {
                    select.options[select.options.length] = new Option(response.dados[i].descricao, response.dados[i].id);
                }

                $('#' + idSelectPicker).selectpicker('refresh');
            }
        }
    });
}

function BuscarDadosPreencherSelectPicker(idSelectPicker, idValorDefault, descricaoValorDefault, endpointURL) {
    $.ajax({
        url: endpointURL,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            swal("Ops!", "Houve um erro ao carregar os dados. Tente novamente em instantes.", "error");
        },
        success: function (response) {
            if (response.erro)
                swal("Ops!", "Houve um erro ao carregar os dados. Tente novamente em instantes.", "error");
            else {
                var select = document.getElementById(idSelectPicker);
                select.options.length = 0;

                select.options[select.options.length] = new Option(descricaoValorDefault, idValorDefault, null, true);

                for (var i = 0; i < response.dados.length; i++) {    
                        select.options[select.options.length] = new Option(response.dados[i].descricao, response.dados[i].id);
                }

                var optionDefault = document.getElementById(idSelectPicker).options[0];
                $(optionDefault).attr('disabled', true);

                $('#' + idSelectPicker).selectpicker('refresh');

            }
        }
    });
}

function InicializarDateTimePickerCallbackSetandoValorVariavel(idDatetimePicker, callbackFunction) {
    $('#' + idDatetimePicker).datepicker({
        todayButton: new Date(),
        autoClose: true,
        minutesStep: 5,
        onSelect: function (fd, d, inst) {
            callbackFunction(d.addHours(-3));
        }
    });
}

Date.prototype.addHours = function (h) {
    this.setTime(this.getTime() + (h * 60 * 60 * 1000));
    return this;
}

function LimparTabela(variavelTabela) {
    variavelTabela.clear();
}

function AlterarVisibilidadeAtualModal(idModal) {
    $('#' + idModal).modal('toggle');
}

function LimparInputRemoverPreenchimento(id) {
    var campo = $('#' + id);
    campo.val('');
    campo.parent().removeClass('is-filled');
}

function ObterValorCheckBoxOuDefault(nomeCheckBox) {

    if ($("#" + nomeCheckBox).length === 0)
        return false;

    return document.getElementById(nomeCheckBox).checked ? true : false;
}

function ObterValorSelectOuDefault(idElemento) {

    return ElementoExiste(idElemento) ? parseInt($("#" + idElemento).val()) : 0;
}

function AbrirModal(idModal) {
    $("#" + idModal).modal("show");
    $("#" + idModal).appendTo("body");
}

function ElementoExiste(idElemento) {
    return document.getElementById(idElemento);
}

function MontarUrl(controller, action) {
    var urlBase = window.location.origin + "/";

    var url = urlBase + controller;

    if (action != undefined && action.length > 0) {
        url += '/' + action;
    }

    return url;
}
