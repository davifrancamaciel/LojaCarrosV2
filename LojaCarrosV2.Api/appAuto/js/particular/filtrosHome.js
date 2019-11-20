
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
                url: "/Veiculos/ListMarca",
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
                url: "/Veiculos/ListAnoFiltro",
                data: { marca: $("#marca").val() },

                success: function (anoMinMax) {

                    $.each(anoMinMax, function (i, ano) {
                        $("#anoInicio").append('<option value="' + ano.AnoLista + '">' + ano.AnoLista + '</option>');
                        //$("#anoFim").append('<option value="' + ano.AnoLista + '">' + ano.AnoLista + '</option>');
                        //$("#anoInicio").append('<option value="' + city.Value + '">' + city.Text + '</option>');
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
                url: "/Veiculos/ListAnoMax",
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
              urlDestino = '/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase() + '&anoInicio=' + $("#anoInicio").val()
          }),
          $("#anoFim").change(function () {
              //console.log('/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase() + '&anoInicio=' + $("#anoInicio").val() + '&anoFim=' + $("#anoFim").val()),
              urlDestino = '/veiculos?tipo=' + $("#tipo").val().toLowerCase() + '&marca=' + $("#marca").val().toLowerCase() + '&anoInicio=' + $("#anoInicio").val() + '&anoFim=' + $("#anoFim").val()
          })

          angular.element("#btnBuscar").click(
             
          //$("#btnBuscar").click(
         function () {
             console.log("Peguei")
             //console.log(urlDestino)
             window.location.href = urlDestino
             //$(location).attr('href', urlDestino)
             //$(location).empty();
         });
      });

// $(document).ready(
//function () {
//    var urlDestino = '/veiculos';
//    $("#tipo").change(
//    function () {
//        console.log('/veiculos/' + $("#tipo").val().toLowerCase()),
//        urlDestino = '/veiculos/' + $("#tipo").val().toLowerCase()
//    });
//    $("#marca").change(function () {
//        console.log('/veiculos/' + $("#tipo").val().toLowerCase() + '/' + $("#marca").val().toLowerCase() ),
//        urlDestino = '/veiculos/' + $("#tipo").val().toLowerCase() + '/' + $("#marca").val().toLowerCase()
//    });
//    $("#anoInicio").change(function () {
//        console.log('/veiculos/' + $("#tipo").val().toLowerCase() + '/' + $("#marca").val().toLowerCase() + '/' + $("#anoInicio").val()),
//        urlDestino = '/veiculos/' + $("#tipo").val().toLowerCase() + '/' + $("#marca").val().toLowerCase() + '/' + $("#anoInicio").val()
//    }),
//    $("#anoFim").change(function () {
//        console.log('/veiculos/' + $("#tipo").val().toLowerCase() + '/' + $("#marca").val().toLowerCase() + '/' + $("#anoInicio").val() + '/' + $("#anoFim").val()),
//        urlDestino = '/veiculos/' + $("#tipo").val().toLowerCase() + '/' + $("#marca").val().toLowerCase() + '/' + $("#anoInicio").val() + '/' + $("#anoFim").val()
//    })

//    $("#btnBuscar").click(
//   function () {

//       console.log(urlDestino)
//       $(location).attr('href', urlDestino)
//       $(location).empty();
//   });
//});




// script para excludao onde e preciso enviar o nome do contreller o id do item e o nome que sera exibido no modal
$(document).ready(
      function () {
          var id = 0;
          var controller;
          $(".lkbExcluir").click(
               function () {
                   id = $(this).attr("data-id");
                   var nome = $(this).attr("data-nome");
                   controller = $(this).attr("data-controller");
                   //console.log(id);
                   //console.log(controller);
                   $("#nomeItem").html(nome);
               });
          $("#btnConfirmar").click(
          function () {
              //console.log(id);
              $.ajax(
                  {
                      dataType: "json",
                      contentType: 'application/json',
                      type: "GET",
                      url: "/Admin/" + controller + "/Excluir",
                      data: { 'id': id },

                      success: function (dados) {
                          location.reload();
                      },
                      error: function (dados) {
                          //alert(dados);
                          location.reload();
                      }
                  });
          })
      });
