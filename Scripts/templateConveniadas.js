$(document).ready(function () {

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("#rua").val("");
        $("#bairro").val("");
        $("#cidade").val("");
        $("#uf").val("");
        $("#ibge").val("");
    }

    //Quando o campo cep perde o foco.
    $("#cep").blur(function () {

        //Nova variável "cep" somente com dígitos.
        var cep = $(this).val().replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep != "") {

            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                //Preenche os campos com "..." enquanto consulta webservice.
                $("#rua").val("...");
                $("#bairro").val("...");
                $("#cidade").val("...");
                $("#uf").val("...");
                $("#ibge").val("...");

                //Consulta o webservice viacep.com.br/
                $.getJSON("//viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        //Atualiza os campos com os valores da consulta.
                        $("#rua").val(dados.logradouro);
                        $("#bairro").val(dados.bairro);
                        $("#cidade").val(dados.localidade);
                        $("#uf").val(dados.uf);
                        $("#ibge").val(dados.ibge);
                    } //end if.
                    else {
                        //CEP pesquisado não foi encontrado.
                        limpa_formulário_cep();
                        alert("CEP não encontrado.");
                    }
                });
            } //end if.
            else {
                //cep é inválido.
                limpa_formulário_cep();
                alert("Formato de CEP inválido.");
            }
        } //end if.
        else {
            //cep sem valor, limpa formulário.
            limpa_formulário_cep();
        }
    });

    //Busca CEP Material
    function limpa_formulário_cep_Material() {
        // Limpa valores do formulário de cep.
        $("#rua2").val("");
        $("#bairro2").val("");
        $("#cidade2").val("");
        $("#uf2").val("");
        $("#ibge2").val("");
    }

    //Quando o campo cep perde o foco.
    $("#cep2").blur(function () {

        //Nova variável "cep" somente com dígitos.
        var cep = $(this).val().replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep != "") {

            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                //Preenche os campos com "..." enquanto consulta webservice.
                $("#rua2").val("...");
                $("#bairro2").val("...");
                $("#cidade2").val("...");
                $("#uf2").val("...");
                $("#ibge2").val("...");

                //Consulta o webservice viacep.com.br/
                $.getJSON("//viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        //Atualiza os campos com os valores da consulta.
                        $("#rua2").val(dados.logradouro);
                        $("#bairro2").val(dados.bairro);
                        $("#cidade2").val(dados.localidade);
                        $("#uf2").val(dados.uf);
                        $("#ibge2").val(dados.ibge);
                    } //end if.
                    else {
                        //CEP pesquisado não foi encontrado.
                        limpa_formulário_cep_Material();
                        alert("CEP não encontrado.");
                    }
                });
            } //end if.
            else {
                //cep é inválido.
                limpa_formulário_cep_Material();
                alert("Formato de CEP inválido.");
            }
        } //end if.
        else {
            //cep sem valor, limpa formulário.
            limpa_formulário_cep_Material();
        }
    });

    $(".next-step").click(function (e) {

        var $active = $('.nav-pills li.active');
        $active.next().removeClass('disabled');
        nextTab($active);

    });
    $(".prev-step").click(function (e) {

        var $active = $('.nav-pills li.active');
        prevTab($active);

    });

    //--Máscara e Validação de data
    $(function () {
        
        //--Máscara date
        $(".datepicker").datepicker({
            dateFormat: 'dd/mm/yy',
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            nextText: 'Próximo',
            prevText: 'Anterior'
        });
        $(".datepicker").on('focusout', function () {
            var $d = $(this);
            var dataexprg_valida = isDate($d.val(), "/", 0, 1, 2);
            var data = $d.val();
            if (dataexprg_valida == false && data != "") {
                alert("Favor informar uma data valida.");
                $d.val('').focus();
                return false;
            }
            return true;
        })
    });

    //Validar data
    function isDate(value, sepVal, dayIdx, monthIdx, yearIdx) {

        try {
            var DayIndex = dayIdx !== undefined ? dayIdx : 0;
            var MonthIndex = monthIdx !== undefined ? monthIdx : 0;
            var YearIndex = yearIdx !== undefined ? yearIdx : 0;

            value = value.replace(/-/g, "/").replace(/\./g, "/");
            var SplitValue = value.split(sepVal || "/");
            var OK = true;
            if (!(SplitValue[DayIndex].length == 1 || SplitValue[DayIndex].length == 2)) {
                OK = false;
            }
            if (OK && !(SplitValue[MonthIndex].length == 1 || SplitValue[MonthIndex].length == 2)) {
                OK = false;
            }
            if (OK && SplitValue[YearIndex].length != 4) {
                OK = false;
            }
            if (OK) {
                var Day = parseInt(SplitValue[DayIndex], 10);
                var Month = parseInt(SplitValue[MonthIndex], 10);
                var Year = parseInt(SplitValue[YearIndex], 10);

                if (OK = ((Year > 1900) && (Year <= new Date().getFullYear()))) {
                    if (OK = (Month <= 12 && Month > 0)) {

                        var LeapYear = (((Year % 4) == 0) && ((Year % 100) != 0) || ((Year % 400) == 0));

                        if (OK = Day > 0) {
                            if (Month == 2) {
                                OK = LeapYear ? Day <= 29 : Day <= 28;
                            }
                            else {
                                if ((Month == 4) || (Month == 6) || (Month == 9) || (Month == 11)) {
                                    OK = Day <= 30;
                                }
                                else {
                                    OK = Day <= 31;
                                }
                            }
                        }
                    }
                }
            }
            return OK;
        }
        catch (e) {
            return false;
        }
    }

    //Calcula total de alunos Prefeitura
    $("#tableTotalAlunos").keyup(function () {
        debugger;
        var a = 0;
        var b = 0;
        var c = 0;
        var d = 0;

        if ($("#totalInfantil").val() != "")
            a = parseInt($("#totalInfantil").val());
        if ($("#totalFund1").val() != "")
            b = parseInt($("#totalFund1").val());
        if ($("#totalFund2").val() != "")
            c = parseInt($("#totalFund2").val());
        if ($("#totalMedio").val() != "")
            d = parseInt($("#totalMedio").val());

        var total = a + b + c + d;
        $("#totalAlunos").val(total);
    });

    $('#fechar').on('click', function () {
        $('#mensagem').hide();
    });

    //Máscaras
    $.mask.definitions['~'] = "[+-]";
    $("#date").mask("99/99/9999");
    $("#cnpj").mask("99.999.999/9999-99");
    $("#cep").mask("99999-999");
    //$("#tel1").mask("(99)9999-9999");
    //$("#tel2").mask("(99)9999-9999");
    //$("#cel").mask("(99)99999-9999");   
    $("#cep2").mask("99999-999");
});
function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}