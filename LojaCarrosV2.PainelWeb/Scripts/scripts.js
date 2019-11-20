$(document).ready(function () {

    $(".img-responsive").fadeOut(1000, function () {
        //$(this).attr('src', "");
        //BuscarCaptcha();
    });

});
$(window).load(function () {
    $(".img-responsive").fadeIn(1000);
});

function OpenLoading() {
    loading = document.getElementById("loading");
    loading.style.display = "block";
}

function CloeseLoading() {
    setTimeout(function () {
        loading = document.getElementById("loading");
        loading.style.display = "none";
    }, 600);

}


$(document).ready(
      function () {
          var id = 0;
          var controller;
          $(".lkbExcluir").click(
               function () {
                   var nome = $(this).attr("data-nome");
                   controller = $(this).attr("data-controller");

                   $("#nomeItem").html(nome);
               });
          $("#btnConfirmar").click(
          function () {

              OpenLoading();
              $.ajax(
                  {
                      dataType: "json",
                      contentType: 'application/json',
                      type: "GET",
                      url: controller,

                      success: function (dados) {
                          CloeseLoading();
                          location.reload();
                      },
                      error: function (dados) {
                          CloeseLoading();
                          location.reload();
                      }
                  });
          })
      });

// para que o menu lateral do adm recolha e mostre corretamente 
$(function () {
    $('.navbar-toggle-sidebar').click(function () {
        $('.navbar-nav').toggleClass('slide-in');
        $('.side-body').toggleClass('body-slide-in');
        $('#search').removeClass('in').addClass('collapse').slideUp(200);
    });

    $('#search-trigger').click(function () {
        $('.navbar-nav').removeClass('slide-in');
        $('.side-body').removeClass('body-slide-in');
        $('.search-input').focus();
    });
});


// para mudara a classe active item de posisao de acordo com a navegacao
//usado na parte adm no menu lateral
$(function () {
    var path = window.location.pathname;
    if (path != "/") {
        $(".navbar-nav li").removeClass("active");
        $(".navbar-nav li").each(function () {
            var href = $(this).children().first().attr("href");
            if (path == href) {
                $(this).addClass("active");
            }
        });
    } else {
        $(".navbar-nav li").removeClass("active");
        $(".navbar-nav li").each(function () {
            var href = $(this).children().first().attr("href");
            if (path == href) {
                $(this).addClass("active");
            }
        });
    }
});
$(function () {
    var path = window.location.pathname;
    //console.log('passei')
    if (path != "/") {
        $(".top-menu .nav .navbar-nav li a").removeClass("active scroll");
        $(".top-menu .nav .navbar-nav li a").each(function () {
            var href = $(this).children().first().attr("href");
            if (path == href) {
                $(this).addClass("active scroll");
            }
        });
    } else {
        $(".top-menu .nav .navbar-nav li a").each(function () {
            var href = $(this).children().first().attr("href");
            if (path == href) {
                $(this).addClass("active scroll");
            }
        });
    }
});





$('#CEP').change(function (e) {
    OpenLoading();
    e.preventDefault();
    $("#Logradouro").val('');
    $("#Bairro").val('');
    $("#Cidade").val('');
    //$("Estado").val('');
    //console.log("cep");

    var cep = $('#CEP').val().replace("-", "");
    $.getJSON("http://cep.republicavirtual.com.br/web_cep.php?cep=" + cep + "  &formato=json", {}, function (data) {
        if (data.resultado_txt == 'sucesso - cep completo') {
            $("#Logradouro").val(data.tipo_logradouro + ' ' + data.logradouro);
            $("#Bairro").val(data.bairro);
            $("#Cidade").val(data.cidade);
            $("#Estado").val(data.uf);
        }
        CloeseLoading();
    });
});

//script para mover para o topo
$(document).ready(function () {
    $().UItoTop({ easingType: 'easeOutQuart' });
});


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(function () {
    var sortOrder = getParameterByName('so');
    var currentSort = getParameterByName('cs');


    //console.log(sortOrder);
    switch (sortOrder) {

        case 'modelo':
            if (sortOrder == currentSort) {
                document.getElementById('orderModelo').innerHTML = '<i class="fa fa-sort-desc"></i>';
            } else {
                document.getElementById('orderModelo').innerHTML = '<i class="fa fa-sort-asc"></i>';
            }
            break;
        case 'ano':
            if (sortOrder == currentSort) {
                document.getElementById('orderAno').innerHTML = '<i class="fa fa-sort-desc"></i>';
            } else {
                document.getElementById('orderAno').innerHTML = '<i class="fa fa-sort-asc"></i>';
            }
            break;
        case 'marca':
            if (sortOrder == currentSort) {
                document.getElementById('orderMarca').innerHTML = '<i class="fa fa-sort-desc"></i>';
            } else {
                document.getElementById('orderMarca').innerHTML = '<i class="fa fa-sort-asc"></i>';
            }
            break;
        case 'tipo':
            if (sortOrder == currentSort) {
                document.getElementById('orderTipo').innerHTML = '<i class="fa fa-sort-desc"></i>';
            } else {
                document.getElementById('orderTipo').innerHTML = '<i class="fa fa-sort-asc"></i>';
            }
            break;
        case 'nome':
            if (sortOrder == currentSort) {
                document.getElementById('orderNome').innerHTML = '<i class="fa fa-sort-desc"></i>';
            } else {
                document.getElementById('orderNome').innerHTML = '<i class="fa fa-sort-asc"></i>';
            }
            break;
        case 'email':
            if (sortOrder == currentSort) {
                document.getElementById('orderEmail').innerHTML = '<i class="fa fa-sort-desc"></i>';
            } else {
                document.getElementById('orderEmail').innerHTML = '<i class="fa fa-sort-asc"></i>';
            }
            break;

        default:
            break;

    }

});




//para mostrar e recolher o menu quando estiver em layouts pequenos
$("span.menu").click(function () {
    $(".top-menu").slideToggle("slow", function () {
        // Animation complete.
    });
});

$(function () {

    function toggleChevron(e) {
        $(e.target)
                .prev('.panel-heading')
                .find("i")
                .toggleClass('rotate-icon');
        $('.panel-body.animated').toggleClass('zoomIn zoomOut');
    }

    $('#accordion').on('hide.bs.collapse', toggleChevron);
    $('#accordion').on('show.bs.collapse', toggleChevron);
    //$('.accordionInterno').on('hide.bs.collapse', toggleChevron);
    //$('.accordionInterno').on('show.bs.collapse', toggleChevron);
    $('#accordionInterno').on('hide.bs.collapse', toggleChevron);
    $('#accordionInterno').on('show.bs.collapse', toggleChevron);
})