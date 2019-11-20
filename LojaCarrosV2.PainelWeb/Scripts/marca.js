// ussado para cadastro e edicao no modal de marca para que seja usado um modal só

$(document).ready(
    function () {
        var id = 0;
        var controller;
        $(".lkbEditar").click(
             function () {
                 id = $(this).attr("data-id");
                 //var nome = $(this).attr("data-nome");
                 controller = $(this).attr("data-controller");

                 $("#myModalLabel").html('<span class="icon-stack" id="icone"><i class="fa fa-pencil"></i></span> Editar Marca');

                 $.ajax({
                     dataType: "json",
                     contentType: 'application/json',
                     type: "GET",
                     url: "/" + controller + "/Editar",
                     data: { 'id': id },
                     success: function (dados) {

                         $.each(dados, function (i, dado) {
                             $("#tipoMarca option[value=" + dado.Tipo.IdTipo + "]").prop('selected', true);
                             $("#txtNome").val(dado.Nome);
                             $("#hddIdMarca").val(dado.IdMarca);
                             //console.log(dado.IdMarca);
                         });
                     },
                     error: function (ex) {
                         alert('Ocorreu em nossa busca.' + ex);
                     }
                 });
             });
        $(".lkbCadastrar").click(
            function () {

                $("#myModalLabel").html('<span class="icon-stack" id="icone"><i class="fa fa-plus"></i></span> Cadastrar Marca');

                $("#tipoMarca").val('');
                $("#txtNome").val('');
                $("#hddIdMarca").val(0);
            });
    });