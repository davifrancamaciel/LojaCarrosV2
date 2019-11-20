

$("#cabecalho").hide();
$("#Renavan").mask("00000000000", {
    onComplete: function (texto) {
        $("#buscar-btn").removeAttr("disabled");
    },
    onKeyPress: function (cep, event, currentField, options) {
        $("#buscar-btn").attr("disabled", "disabled");
        PreencheDados(null, true)
    }
});

$("#buscar-btn").on("click", function () {
    $("#buscar-modal").modal({ show: true });
});

$("#buscar-modal").on("show.bs.modal", function () {
    BuscarCaptcha();
});

$("#buscar-modal").on("shown.bs.modal", function () {
    $("#img-input").focus();
});

$("#buscar-modal").on("hidden.bs.modal", function () {
    $("#img-input").val("");
});

$("#buscar-captcha-btn").on("click", function () {
    $("#captcha-img").fadeOut(1000, function () {
        $(this).attr('src', "");
        BuscarCaptcha();
    });

});

$("#buscarDados-btn").on("click", function () {
    ObterDados();
});


//var pathLoader = "@Url.Content("~/images/ajax-loader-facebook.gif ")";
var pathLoader = "/images/ajax-loader-facebook.gif";
var $loader = $('<img class="loader-facebook" src="' + pathLoader + '"/> <em>Buscando ...</em>');

var BuscarCaptcha = function () {
    //var strUrl = '@Url.Action("GetCaptchaDetran","Detran")';
    var strUrl = '/Detran/GetCaptchaDetran';
    $.ajax({
        type: 'GET',
        url: strUrl,
        dataType: 'json',
        cache: false,
        async: true,
        beforeSend: function () {
            $loader.insertAfter($("#captcha-img"));
        },
        success: function (data) {
            $("#captcha-img").removeClass("hidden").attr('src', data);
            $("#captcha-img").fadeIn(1000);
        },
        complete: function () {
            $loader.remove();
            $("#img-input").focus();
        },
        error: function () {
            alert("erro na tentativa de obter o captcha");
        }
    });
};

var ObterDados = function () {
    // var strUrl = '@Url.Action("ConsultarDadosDetran","Detran")';
    var strUrl = '/Detran/ConsultarDadosDetran';
    $.ajax({
        type: 'post',
        url: strUrl,
        cache: false,
        async: true,
        data: { renavan: $("#Renavan").val(), captcha: $("#img-input").val(), id: $("#IdVeiculo").val() },
        beforeSend: function () {
            $loader.insertBefore($("#fechar-button"));
        },
        success: function (data) {


            console.log(data);

            $loader.remove();
            if (data.erro.length > 0) {
                $("#msgErro-span").text(data.erro).closest("p").removeClass("hidden");
                $("#captcha-img").fadeOut(1000, function () {
                    $(this).attr('src', "");
                    BuscarCaptcha();
                    $("#img-input").focus();
                });
                setTimeout(function () {
                    $("#msgErro-span").closest("p").addClass("hidden");
                }, 2000);
            } else {
                if (data.dados != null) {
                    PreencheDados(data.dados, false);
                    $("#buscar-modal").modal("hide");
                } else {
                    $("#msgErro-span").text("erro de comunicação com a receita.").closest("p").removeClass("hidden");
                    $("#captcha-img").fadeOut(1000, function () {
                        $(this).attr('src', "");
                        BuscarCaptcha();
                        $("#img-input").focus();
                    });
                    setTimeout(function () {
                        $("#msgErro-span").closest("p").addClass("hidden");
                    }, 2000);
                }

            }
        },
        error: function (data) {
            $loader.remove();
            alert("erro de comunicação.");
        },
    });
};

var PreencheDados = function (dados, clear) {


    if (clear) {
        $(".caixa-grande").val("");
    } else {
        //$("#DataConsulta-input").val(dados.DataConsulta);
        //$("#QtdMultas-input").val(dados.QtdMultas);

        $("#cabecalho").show();

        $("#p-multas").html(dados.QtdMultas);
        $("#dataConsulta").html(dados.DataConsulta);
        $("#p-renavan").html(dados.Renavan);

        $("#resultado").html('');

        var grid = "";

        for (var i = 0; i < dados.Multas.length; i++) {
            var dado = dados.Multas[i];

            // grid += '<p>AgenteEmissor  &nbsp; &nbsp; ' + dado.AgenteEmissor +
            //'</br>' + 'AutoDeInfracao  &nbsp; &nbsp ' + dado.AutoDeInfracao +
            //'</br>' + 'AutoDeRenainf  &nbsp; &nbsp' + dado.AutoDeRenainf +
            // '</br>' + 'DataPgtoDesconto  &nbsp; &nbsp' + dado.DataPgtoDesconto +
            // '</br>' + 'DatadaInfracao  &nbsp; &nbsp' + dado.DatadaInfracao +
            // '</br>' + 'Descricao  &nbsp; &nbsp' + dado.Descricao +
            // '</br>' + 'Enquadramento  &nbsp; &nbsp' + dado.Enquadramento +
            // '</br>' + 'HoraDaInfracao  &nbsp; &nbsp' + dado.HoraDaInfracao +
            // '</br>' + 'LocalInfracao  &nbsp; &nbsp' + dado.LocalInfracao +
            // '</br>' + 'OrgaoEmissor  &nbsp; &nbsp' + dado.OrgaoEmissor +
            // '</br>' + 'PlacaRelacionada  &nbsp; &nbsp' + dado.PlacaRelacionada +
            // '</br>' + 'StatusPagamento  &nbsp; &nbsp' + dado.StatusPagamento +
            // '</br>' + 'ValorOriginal  &nbsp; &nbsp' + dado.ValorOriginal +
            // '</br>' + 'ValorSerPago  &nbsp; &nbsp' + dado.ValorSerPago +'</p>'




            grid += "<table class=\"table table-striped table-bordered\">"
                      + "<tbody>"
                          + "<tr>"
                            + "<td><span><label class=\"control-label\"> Auto de Infração:</label><br></span>" + dado.AutoDeInfracao + "</td>"
                            + "<td><span><label class=\"control-label\">Auto de Renainf:</label><br></span>" + dado.AutoDeRenainf + "</td>"
                            + "<td colspan=\"2\"><span><label class=\"control-label\">Data para pagamento com desconto:</label><br></span>" + dado.DataPgtoDesconto + "</td>"
                          + "</tr>"
                          + "<tr>"
                            + "<td colspan=\"2\"><span><label class=\"control-label\">Enquadramento da Infração:</label><br></span>" + dado.Enquadramento + "</td>"
                            + "<td><span><label class=\"control-label\">Data da Infração:</label><br></span>" + dado.DatadaInfracao + "</td>"
                            + "<td><span><label class=\"control-label\">Hora:</label><br></span>" + dado.HoraDaInfracao + "</td>"
                          + "</tr>"
                            + "<tr>"
                            + "<td colspan=\"3\"><span><label class=\"control-label\">Descrição:</label><br></span>" + dado.Descricao + "</td>"
                            + "<td><span><label class=\"control-label\">Placa Relacionada:</label><br></span>" + dado.PlacaRelacionada + "</td>"
                         + "</tr>"
                         + "<tr>"
                            + "<td colspan=\"2\"><span><label class=\"control-label\">Local da Infração:</label><br></span>" + dado.LocalInfracao + "</td>"
                            + "<td><span><label class=\"control-label\">Valor original R$:</label><br></span>" + dado.ValorOriginal + "</td>"
                            + "<td colspan=\"2\"><span><label class=\"control-label\">Valor a ser pago R$:</label><br></span>" + dado.ValorSerPago +"</td>"
                          + "</tr>"
                          + "<tr>"
                            + "<td colspan=\"2\"><span><label class=\"control-label\">Status de Pagamento:</label><br></span>" + dado.StatusPagamento + "</td>"
                            + "<td><span><label class=\"control-label\">Órgão Emissor:</label><br></span>" + dado.OrgaoEmissor + "</td>"
                            + "<td><span><label class=\"control-label\">Agente Emissor:</label><br></span>" + dado.AgenteEmissor + "</td>"
                          + "</tr>"
                      + "</tbody>"
                  + "</table>"
        }

        $("#resultado").html(grid);
    }
};
