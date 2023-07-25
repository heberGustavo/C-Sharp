$(document).ready(function () {
    $('.telefone_res').mask('(00) 0000-0000');
    $('.celular').mask('(00) 00000-0000');
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
});

function IsString(value) {
    return typeof value === 'string' || value instanceof String;
}

function ConverterDataParaUSA(data) {
    var dataPadrao = new Date(data);

    var diaPadrao = dataPadrao.getDate() + 1;
    var mesPadrao = dataPadrao.getMonth() + 1;
    var anoPadrao = dataPadrao.getFullYear();

    var dia = ("00" + diaPadrao).slice(-2); // "00"
    var mes = ("00" + mesPadrao).slice(-2); // "00"
    var ano = ("0000" + anoPadrao).slice(-4); // "0000"

    var dataFormatada = ano + '-' + mes + '-' + dia;
    return dataFormatada;
}

function ConverterDataParaUSARead(data) {
    var dataPadrao = new Date(data);

    var diaPadrao = dataPadrao.getDate();
    var mesPadrao = dataPadrao.getMonth() + 1;
    var anoPadrao = dataPadrao.getFullYear();

    var dia = ("00" + diaPadrao).slice(-2); // "00"
    var mes = ("00" + mesPadrao).slice(-2); // "00"
    var ano = ("0000" + anoPadrao).slice(-4); // "0000"

    var dataFormatada = ano + '-' + mes + '-' + dia;
    return dataFormatada;
}

function ConverterParaFloat(stringValor) {
    if (IsString(stringValor))
        return parseFloat(stringValor.replace("R$", '').trim().replace('.', '').replace(',', '.'));
    else
        return stringValor;
}

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