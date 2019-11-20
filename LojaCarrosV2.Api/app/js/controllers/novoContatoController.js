angular.module("ListaTelefonica").controller("novoContatoController", function ($scope, $http, contatosAPI, $location, operadoras) {
    $scope.operadoras = operadoras.data;




    //var carregarOperadoras = function () {
    //    operadorasAPI.getOperadoras().success(function (data, status) {
    //        //console.log(data);
    //        $scope.operadoras = data;
    //    });
    //};
    var carregarCombustiveis = function () {
        $http.get("http://localhost:37135/api/v1/combustiveis").success(function (data, status) {
            //console.log(data);
            $scope.combustiveis = data;
        });
    };
    $scope.adicionarContato = function (contato) {

        contatosAPI.saveContato(contato).success(function (data, status) {
            delete $scope.contato;
            $scope.contatoForm.$setPristine();
            $location.path("/contatos");

        });
    };
    //carregarOperadoras();
    carregarCombustiveis();
});