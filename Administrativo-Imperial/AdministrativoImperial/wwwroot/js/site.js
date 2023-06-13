$(document).ready(function () {
    "use strict";

    AtivarTelaAtual();

    /* 
    ------------------------------------------------
    mascaras
    ------------------------------------------------*/
    $('.cpfcnpj').mask('000.000.000-00', {
        onKeyPress: function (cpfcnpj, e, field, options) {
            const masks = ['000.000.000-000', '00.000.000/0000-00'];
            const mask = (cpfcnpj.length > 14) ? masks[1] : masks[0];
            $('.cpfcnpj').mask(mask, options);
        }
    });

    $(".valor").blur(function () {
        if (this.value === '' || this.value === ',' || this.value === null || this.value === 'NaN') {
            this.value = '0,00';
        }
        else {
            this.value = this.value.replace(',', '.');
            this.value = parseFloat(this.value).toFixed(2).replace('.', ',');
        }
    });

    $(".time").blur(function () {
        if (this.value === '' || this.value === '' || this.value === null || this.value === 'NaN') {
            return;
        }
        else {
            if (!/:/.test(this.value)) {
                this.value += ':00';
            }
            //Caso esteja como 14:
            else if (this.value.length === 3) {
                this.value += '00';
            }

            this.value = this.value.replace(/^\d{1}:/, '0$&').replace(/:\d{1}$/, '$&0');
        }
    });

    $(".code").blur(function () {
        if (this.value === '' || this.value === '' || this.value === null || this.value === 'NaN') {
            return;
        }
        else {
            this.value = PadLeft(this.value, 6);
        }
    });

    $('.code').mask('000000');
    $('.counting').mask('00000');
    $('.year').mask('0000');
    $('.date').mask('00/00/0000');
    $('.date-month-year').mask('00/0000');
    $('.date-day-month').mask('00/00');
    $('.time').mask('00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 0000-0000');
    $('.cel_with_ddd')
        .mask('(00) 00000-0000')
        .focusout(function (event) {
            var target, phone, element;
            target = (event.currentTarget) ? event.currentTarget : event.srcElement;
            phone = target.value.replace(/\D/g, '');
            if (phone.length < 11) {
                $(this).val('');
            }
        });
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.rg').mask('00.000.000-0', { reverse: true });
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
    $('.money2').mask("#.##0,00", { reverse: true });
    $('.ip_address').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
        translation: {
            'Z': {
                pattern: /[0-9]/, optional: true
            }
        }
    });
    $('.ip_address').mask('099.099.099.099');
    $('.percent').mask('##0,00%', { reverse: true });
    $('.percent-first').mask('##0%', { reverse: true });
    $('.valor').mask('9999999999999999,99', { reverse: true });
    $('.clear-if-not-match').mask("00/00/0000", { clearIfNotMatch: true });
    $('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" });
    $('.fallback').mask("00r00r0000", {
        translation: {
            'r': {
                pattern: /[\/]/,
                fallback: '/'
            },
            placeholder: "__/__/____"
        }
    });
    $('.selectonfocus').mask("00/00/0000", { selectOnFocus: true });
    $('.ctps').mask('0000000');
    $('.serie').mask('00000');
    $('.pis').mask('00000000000');
    $('.number').mask('#0', { reverse: true });
    $('.titulo_eleitor').mask('000000000000');
    $('.mask_cnae').mask('0000-0/00', { reverse: true });
    $('.mask_nire').mask('00-0-0000000-0', { reverse: true });
    $('.uf').mask('SS');
    $('.horario-entrada-saida').mask('00:00 - 00:00');


    /*CUSTOMIZAR DATATABLE*/
    setTimeout(function () {
        $('.dataTables_length label>select').addClass('form-control-custom');
        $('.dataTables_filter input').addClass('form-control-custom');
    }, 50);

    EncerraLoading();
});

//Sempre que abrir um Modal, o ultimo aberto estará na frente
$(document).on('show.bs.modal', '.modal', function () {
    var zIndex = 1040 + (10 * $('.modal:visible').length);
    $(this).css('z-index', zIndex);
    setTimeout(function () {
        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
    }, 0);
});

function AtivarTelaAtual() {
    //Verifica URL Atual
    var urlAtual = location.href.split('/').pop();

    $('#sidenav-collapse-main a.nav-link').each(function (index, element) {

        var nomePaginaAtiva = $('.nome-pagina-ativa');
        var nomeTelaNav = $(this).find('.nome-tela').text();

        if (urlAtual == nomeTelaNav.replace(' ', '')) {
            var ativar = this;
            $(ativar).addClass('active')
            $(nomePaginaAtiva).text(nomeTelaNav)
        }
    });
}