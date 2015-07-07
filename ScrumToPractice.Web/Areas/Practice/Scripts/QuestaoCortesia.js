$(document).ready(function () {

    // grava atual e exibe proxima questao
    $('#btnNext').click(function () {
        var alternativas = [];
        var idCortesia = parseInt($('#idCortesia').val());
        var idQuestao = parseInt($(this).data("questao"));

        $('.alternativa').each(function () {
            if ($(this).is(':checked')) {
                alternativas.push(parseInt($(this).val()));
            }
        });

        $.ajax({
            traditional: true,
            type: 'GET',
            url: '/Practice/Practice/Proxima/',
            data: { selecionadas: alternativas, idCortesia: idCortesia, idQuestao: idQuestao },
            success: function (data) {
                $('#simulado').html(data);
            },
            error: function (request, status, error) {
                alert(error);
            }
        });
    });

    // grava atual e exibe questao anterior
    $('#btnPrevious').click(function () {
        var alternativas = [];
        var idCortesia = parseInt($('#idCortesia').val());
        var idQuestao = parseInt($(this).data("questao"));

        $('.alternativa').each(function () {
            if ($(this).is(':checked')) {
                alternativas.push(parseInt($(this).val()));
            }
        });

        $.ajax({
            traditional: true,
            type: 'GET',
            url: '/Practice/Practice/Anterior/',
            data: { selecionadas: alternativas, idCortesia: idCortesia, idQuestao: idQuestao },
            success: function (data) {
                $('#simulado').html(data);
            },
            error: function (request, status, error) {
                alert(error);
            }
        });
    });

    // grava atual e finaliza o simulado e redireciona para resultado
    $('#btnFinish').click(function () {
        var alternativas = [];
        $('.alternativa').each(function () {
            if ($(this).is(':checked')) {
                alternativas.push(parseInt($(this).val()));
            }
        });

        var idCortesia = parseInt($('#idCortesia').val());
        var idQuestao = parseInt($(this).data("questao"));
        var data = { selecionadas: alternativas, idCortesia: idCortesia, idQuestao: idQuestao };
        var url = '/Practice/Practice/Finish/';

        $.post(url, data, function (response) {
            if (response.result == 'Error') {
                alert("Fail");
            }
            else if (response.result == 'Redirect') {
                window.location = response.url;
            }
        });
    });

    // assinala esta questao para ser respondida depois
    $('#btnAnswerLater').click(function () {
        var idCortesia = parseInt($('#idCortesia').val());
        var idQuestao = parseInt($(this).data("questao"));

        $.ajax({
            traditional: true,
            type: 'GET',
            url: '/Practice/Practice/ResponderDepois',
            data: { idCortesia: idCortesia, idQuestao: idQuestao },
            success: function (data) {
                $('#simulado').html(data);
            },
            error: function (request, status, error) {
                alert(error);
            }
        });
    });

    // exibe uma questao que estava assinalada para ser respondida depois
    $('.answer').click(function () {
        // grava tb a questao atual
        var alternativas = [];
        $('.alternativa').each(function () {
            if ($(this).is(':checked')) {
                alternativas.push(parseInt($(this).val()));
            }
        });

        var idCortesia = parseInt($('#idCortesia').val());
        var idQuestao = parseInt($(this).data('questao'));
        var idQuestaoAtual = parseInt($('#btnNext').data("questao"));

        $.ajax({
            traditional: true,
            type: 'GET',
            url: '/Practice/Practice/ExibirQuestao',
            data: { selecionadas: alternativas, idCortesia: idCortesia, idQuestao: idQuestao, idQuestaoAtual: idQuestaoAtual },
            success: function (data) {
                $('#simulado').html(data);
            },
            error: function (request, status, error) {
                alert(error);
            }
        });
    });

});