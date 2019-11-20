
$(document).ready(function () {

    $("#tipo").change(function () {
        $('#loading').show();//testar depois
        $("#marca").empty();
        $("#anoInicio").empty();
        $("#anoFim").empty();

        $("#marca").append('<option value="">' + " - TODOS - " + '</option>');
        $("#anoInicio").append('<option value="0">' + " - TODOS - " + '</option>');
        $("#anoFim").append('<option value="">' + " - TODOS - " + '</option>');

        if ($("#tipo").val() != 0) {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: "/Base/ListMarca",
                data: { tipo: $("#tipo").val() },

                success: function (marcas) {

                    $.each(marcas, function (i, marca) {

                        $("#marca").append('<option value="' + marca.Nome + '">' + marca.Nome + ' (' + marca.QtdVeiculo + ')' + '</option>');
                    });
                    $('#loading').hide(); //testar depois
                },
                error: function (ex) {

                    alert('Ocorreu em nossa busca.' + ex);
                }
            });
        }
        return false;
    })
});

$(document).ready(function () {

    $("#marca").change(function () {
        $('#loading').show();
        $("#anoInicio").empty();
        $("#anoFim").empty();
        $("#anoInicio").append('<option value="0">' + " - TODOS - " + '</option>');
        $("#anoFim").append('<option value="">' + " - TODOS - " + '</option>');

        if ($("#marca").val() != 0) {
            $.ajax({

                type: 'POST',
                dataType: 'json',
                url: "/Base/ListAnoFiltro",
                data: { marca: $("#marca").val() },

                success: function (anoMinMax) {

                    $.each(anoMinMax, function (i, ano) {
                        $("#anoInicio").append('<option value="' + ano.AnoLista + '">' + ano.AnoLista + '</option>');                     
                        $('#loading').hide();
                    });
                },
                error: function (ex) {
                    alert('Ocorreu em nossa busca filtro marca.' + ex);
                }
            });
        }
        return false;
    })
});


$(document).ready(function () {

    $("#anoInicio").change(function () {
        $('#loading').show();
        $("#anoFim").empty();
        $("#anoFim").append('<option value="">' + " - TODOS - " + '</option>');
        if ($("#anoInicio").val() != 0) {
            $.ajax({
                type: 'POST',
                url: "/Base/ListAnoMax",
                dataType: 'json',
                data: { anoMin: $("#anoInicio").val(), marca: $("#marca").val() },
                success: function (anoMinMax) {

                    $.each(anoMinMax, function (i, ano) {
                        $("#anoFim").append('<option value="' + ano.AnoLista + '">' + ano.AnoLista + '</option>');
                        $('#loading').hide();
                    });
                },
                error: function (ex) {
                    alert('Ocorreu em nossa busca.' + ex);
                }
            });
        }
        return false;
    })
});





$(document).ready(
      function () {
          var urlDestino = '/veiculos';


          $("#tipo").change(
          function () {
              //console.log('/veiculos?tipo=' + $("#tipo").val().toLowerCase()),
              urlDestino = '/veiculos?tipo=' + $("#tipo").val().toLowerCase()
          });
          $("#marca").change(function () {
              //console.log('/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase()),
              urlDestino = '/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase()
          });
          $("#anoInicio").change(function () {
              //console.log('/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase() + '&anoInicio=' + $("#anoInicio").val()),
              urlDestino = '/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase() + '&ai=' + $("#anoInicio").val()
          }),
          $("#anoFim").change(function () {
              //console.log('/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase() + '&anoInicio=' + $("#anoInicio").val() + '&anoFim=' + $("#anoFim").val()),
              urlDestino = '/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase() + '&ai=' + $("#anoInicio").val() + '&af=' + $("#anoFim").val()
          })

          $("#btnBuscar").click(
         function () {

             //console.log(urlDestino)
             window.location.href = urlDestino
             //$(location).attr('href', urlDestino)
             //$(location).empty();
         });
      });

//function getParameterByName(name, url) {
//    if (!url) url = window.location.href;
//    name = name.replace(/[\[\]]/g, "\\$&");
//    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
//        results = regex.exec(url);
//    if (!results) return null;
//    if (!results[2]) return '';
//    return decodeURIComponent(results[2].replace(/\+/g, " "));
//}
//$(function () {
//    var mostrar = getParameterByName('so');
//    if (mostrar != null) {
//        var qtdItens = document.getElementById('intensOrder');

//        qtdItens.innerHTML = mostrar;
//    }
//});

//$(function () {
//    var paginaTamanho = getParameterByName('pt');
//    if (paginaTamanho != null) {
//        var qtdItens = document.getElementById('intensPg');

//        qtdItens.innerHTML = paginaTamanho;
//    }
//});



