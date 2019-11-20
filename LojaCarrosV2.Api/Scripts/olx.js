
$("#logOlx").click(
  function () {
      //alert('log');
      //OpenLoading();
      $.ajax(
          {
              dataType: "json",
              contentType: 'application/json',
              type: "GET",
              url: "https://auth.olx.com.br/oauth?scope=basic_user_info&state=/profile&redirect_uri=painel.enriqueautomoveis.com.br/code&response_type=code&client_id=f78b32a2df237759e36b8e9f078bd991c16687e4",
              //data: { client_id: 'f78b32a2df237759e36b8e9f078bd991c16687e4' },

              success: function (dados) {
                  console.log("sucesso");
                  //CloeseLoading();
                  //location.reload();
              },
              error: function (dados) {
                  console.log("erro");
                  //alert(dados);
                  //location.reload();

              }

          });
  })
