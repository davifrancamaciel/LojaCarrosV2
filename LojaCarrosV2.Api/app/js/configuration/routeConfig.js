angular.module("ListaTelefonica").config(function ($routeProvider) {

    $routeProvider.when("/contatos", {
        templateUrl: "views/contatos.html",
        controller: "ListaTelefonicaController",
        resolve: {
            contatos: function (contatosAPI) {
                return contatosAPI.getContatos();
            },
            operadoras: function (operadorasAPI) {
                return operadorasAPI.getOperadoras();
            }
        }
    });
    $routeProvider.when("/novocontato", {
        templateUrl: "views/novoContato.html",
        controller: "novoContatoController",
        resolve: {
            operadoras: function (operadorasAPI) {
                return operadorasAPI.getOperadoras();
            }
        }
    });
    $routeProvider.when("/detalhescontato/:idVeiculo", {
        templateUrl: "views/detalhesContato.html",
        controller: "detalhesContatoController",
        resolve: {
            contato: function (contatosAPI,$route) {
                return contatosAPI.getContato($route.current.params.idVeiculo);
            }
        }
       
    });

    // para fazer um redirect pode ser de pagina de erro
    //$routeProvider.otherwise({redirectTo:"/contatos"})
});