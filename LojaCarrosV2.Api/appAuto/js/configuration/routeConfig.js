angular.module("AutoStore").config(function ($routeProvider) {

    $routeProvider.when("/", {
        templateUrl: "views/home/Home.html",
        controller: "veiculoController",
        resolve: {
            veiculos: function (veiculosAPI) {
                return veiculosAPI.getVeiculosDestaque();
            },
            tipos: function (tiposAPI) {
                return tiposAPI.getTipos();
            },
            marcas: function (marcasAPI) {
                return marcasAPI.getMarcas();
            }
        }
    });
    $routeProvider.when("/veiculos", {
        templateUrl: "views/veiculos/Veiculos.html",
        controller: "veiculoController",
        
        resolve: {
            veiculos: function (veiculosAPI) {
                return veiculosAPI.getVeiculos();
            },
            tipos: function (tiposAPI) {
                return tiposAPI.getTipos();
            },
            marcas: function (marcasAPI) {
                return marcasAPI.getMarcas();
            }
        }
    });
    $routeProvider.when("/veiculos/:tipo", {
        templateUrl: "views/veiculos/Veiculos.html",
        controller: "veiculoController",
        resolve: {
            veiculos: function (veiculosAPI) {
                return veiculosAPI.getVeiculos();
            },
            tipos: function (tiposAPI) {
                return tiposAPI.getTipos();
            },
            marcas: function (marcasAPI) {
                return marcasAPI.getMarcas();
            }
        }
    });
    $routeProvider.when("/veiculos/:tipo/:marca", {
        templateUrl: "views/veiculos/Veiculos.html",
        controller: "veiculoController",
        resolve: {
            veiculos: function (veiculosAPI) {
                return veiculosAPI.getVeiculos();
            },
            tipos: function (tiposAPI) {
                return tiposAPI.getTipos();
            },
            marcas: function (marcasAPI) {
                return marcasAPI.getMarcas();
            }
        }
    });
    $routeProvider.when("/veiculos/:tipo/:marca/:anoInicio", {
        templateUrl: "views/veiculos/Veiculos.html",
        controller: "veiculoController",
        resolve: {
            veiculos: function (veiculosAPI) {
                return veiculosAPI.getVeiculos();
            },
            tipos: function (tiposAPI) {
                return tiposAPI.getTipos();
            },
            marcas: function (marcasAPI) {
                return marcasAPI.getMarcas();
            }
        }
    });
    $routeProvider.when("/veiculos/:tipo/:marca/:anoInicio/:anoFim", {
        templateUrl: "views/veiculos/Veiculos.html",
        controller: "veiculoController",
        resolve: {
            veiculos: function (veiculosAPI) {
                return veiculosAPI.getVeiculos();
            },
            tipos: function (tiposAPI) {
                return tiposAPI.getTipos();
            },
            marcas: function (marcasAPI) {
                return marcasAPI.getMarcas();
            }
        }
    });
    $routeProvider.when("/:nome-:idVeiculo", {
        templateUrl: "views/veiculos/Detalhes.html",
        controller: "veiculoDetalheController",
        resolve: {
            veiculo: function (veiculosAPI, $route) {

                return veiculosAPI.getVeiculo($route.current.params.idVeiculo);
            },
            veiculos: function (veiculosAPI) {
                return veiculosAPI.getVeiculos();
            },
        },

    });
    $routeProvider.when("/sobre", {
        templateUrl: "views/home/Sobre.html"//,
        //controller: "novoContatoController",
        //resolve: {
        //    operadoras: function (operadorasAPI) {
        //        return operadorasAPI.getOperadoras();
        //    }
        //}
    });
    $routeProvider.when("/contato", {
        templateUrl: "views/home/Contato.html"//,
        //controller: "novoContatoController",
        //resolve: {
        //    operadoras: function (operadorasAPI) {
        //        return operadorasAPI.getOperadoras();
        //    }
        //}
    });
    // para fazer um redirect pode ser de pagina de erro
    //$routeProvider.otherwise({redirectTo:"/contatos"})
});