// script para excludao onde e preciso enviar o nome do contreller o id do item e o nome que sera exibido no modal

$(document).ready(function () {


    $("#AnoFabricacao").change(function () {
        $('#loadingAno').show();//testar depois
        $("#AnoModelo").empty();
        $("#modelo").empty();
        if ($("#AnoFabricacao").val() != 0) {
            $.ajax({
                type: 'POST',
                url: "/Veiculos/ListAnoMax",
                dataType: 'json',
                data: { anoMin: $("#AnoFabricacao").val() },
                success: function (anoMinMax) {

                    $.each(anoMinMax, function (i, ano) {
                        $("#AnoModelo").append('<option value="' + ano.AnoLista + '">' + ano.AnoLista + '</option>');
                        //$("#anoInicio").append('<option value="' + city.Value + '">' + city.Text + '</option>');
                    });

                    $('#loadingAno').hide(); //testar depois
                },
                error: function (ex) {
                    alert('Ocorreu em nossa busca.' + ex);
                }
            });
        } else {
            $("#AnoModelo").append('<option value="">-Selecione-</option>');
            $('#loadingAno').hide();
        }

        return false;
    }),


    $("#IdTipo").change(function () {
        $('#loadingMarca').show();//testar depois
        $("#marca").empty();


        $("#anoInicio").append('<option value="">' + " - TODOS - " + '</option>');
        $("#anoFim").append('<option value="">' + " - TODOS - " + '</option>');

        if ($("#IdTipo").val() != 0) {
            $.ajax({
                type: 'POST',
                url: "/Veiculos/ListMarca",
                dataType: 'json',
                data: { tipo: $("#IdTipo").val() },

                success: function (marcas) {
                    console.log(marcas);
                    $.each(marcas, function (i, marca) {

                        $("#marca").append('<option value="' + marca.IdMarca + '">' + marca.Nome + '</option>');
                    });
                    $('#loadingMarca').hide(); //testar depois
                },
                error: function (ex) {
                    alert('Ocorreu em nossa busca.' + ex);
                }
            });

        }
        return false;
    })
    //$("#IdTipo").change(function () {
    //    $('#loadingMarca').show();//testar depois
    //    $("#marca").empty();
    //    $("#modelo").empty();



    //    if ($("#IdTipo").val() != 0) {
    //        $.ajax({
    //            type: 'GET',
    //            url: "https://fipe-parallelum.rhcloud.com/api/v1/" + $("#IdTipo option:selected").text().toLowerCase() + "s/marcas",
    //            dataType: 'json',
    //            // data: { tipo: $("#IdTipo").val() },

    //            success: function (marcas) {
    //                console.log("https://fipe-parallelum.rhcloud.com/api/v1/" + $("#IdTipo option:selected").text().toLowerCase() + "s/marcas");
    //                $.each(marcas, function (i, marca) {

    //                    $("#marca").append('<option value="' + marca.codigo + '">' + marca.nome + '</option>');
    //                });
    //                $('#loadingMarca').hide(); //testar depois
    //            },
    //            error: function (ex) {
    //                alert('Ocorreu em nossa busca.' + ex);
    //            }
    //        });

    //    }
    //    return false;
    //})
    //$("#marca").change(function () {
    //    $('#loadingMarca').show();
    //    $("#modelo").empty();




    //    if ($("#marca").val() != 0) {
    //        $.ajax({
    //            type: 'GET',
    //            url: "https://fipe-parallelum.rhcloud.com/api/v1/" + $("#IdTipo option:selected").text().toLowerCase() + "s/marcas/" + $("#marca").val() + "/modelos",
    //            dataType: 'json',


    //            success: function (dados) {
    //                //var teste = "https://fipe-parallelum.rhcloud.com/api/v1/" + $("#IdTipo option:selected").text().toLowerCase() + "s/marcas/" + $("#marca").val() + "/modelos";
    //                //console.log(teste);

    //                $.each(dados.modelos, function (i, modelo) {
    //                    //console.log(modelos[i]);
    //                    $("#modelo").append('<option value="' + modelo.codigo + '">' + modelo.nome + '</option>');

    //                });
    //                $.each(dados.anos, function (i, modelo) {
    //                    //console.log(modelos[i]);

    //                    $("#ano").append('<option value="' + modelo.codigo + '">' + modelo.nome + '</option>');
    //                });

    //                $('#loadingMarca').hide();
    //            },
    //            error: function (ex) {
    //                alert('Ocorreu em nossa busca.' + ex);
    //            }
    //        });

    //    }
    //    return false;
    //})





});
$(document).ready(function () {

    var idVeiculo = $("#IdVeiculo").val();
    //var idVeiculo = 83;
    console.log(idVeiculo);

    if (idVeiculo !== "0") {
        $('#upload').show();
        $("#fileuploader").uploadFile({

            url: "/Veiculos/Upload?id=" + idVeiculo,
            fileName: "myfile",
            showPreview: false,
            previewHeight: "100px",
            previewWidth: "100px",
            acceptFiles: "image/*",
            autoSubmit: true,
            sequential: true,
            sequentialCount: 1,

            maxFileCountErrorStr: "Não foi gravado. O máximo de Arquivos por vez é:",
            //maxFileSize: 1048576,
            maxFileSize: 2097152,//2mb
            sizeErrorStr: "Esta imagem ultrapassou o limite de: ",
            maxFileCount: 15,

            afterUploadAll: function (obj) {
                //if ($("#hddAcao").val() === 'editar') {
                //    var paginaRetorno = $("#hddPagina").val();
                location.reload();
                $(location).attr('href', '/veiculos/editar/' + idVeiculo)
                //$(location).attr('href', '/painel/veiculos?pagina=' + paginaRetorno)

                //} else {
                //    location.reload();
                //    //$(location).attr('href', '/painel/veiculos')
                //    $(location).attr('href', '/veiculos')
                //}
            }
        });

    } else {
        $('#upload').hide();
    }

});



