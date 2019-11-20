angular.module("ListaTelefonica").controller("ListaTelefonicaController", function ($scope, $http, contatos) {
    $scope.app = "Lista Telefonica";

    $scope.contatos = contatos.data;
    
    $scope.apagarContato = function (contatos) {
        //console.log(contatos);
        $scope.contatos = contatos.filter(function (contato) {
            if (!contato.selecionado) return contato;

        });
    };

    $scope.isContatoSelecionado = function (contatos) {
        return contatos.some(function (contato) {
            return contato.selecionado;
        });
    };

    $scope.operadoras = [];
    $scope.combustiveis = [];
    //
    //var carregarContatos = function () {
    //    contatosAPI.getContatos().success(function (data, status) {
        
    //        $scope.contatos = data;
    //    }).error(function (data,status) {
    //        $scope.error = "Não foi possivel carregar os dados";
    //    });

    //};
    //var carregarOperadoras = function () {
    //   operadorasAPI.getOperadoras().success(function (data, status) {
    //        //console.log(data);
    //        $scope.operadoras = data;
    //    });
    //};
    //var carregarCombustiveis = function () {
    //    $http.get("http://localhost:37135/api/v1/combustiveis").success(function (data, status) {
    //        //console.log(data);
    //        $scope.combustiveis = data;
    //    });
    //};
    

    $scope.ordenarPor = function (campo) {
        $scope.criterioDeOrdenacao = campo;
        $scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
    };
    //carregarContatos();
    //carregarOperadoras();
    //carregarCombustiveis();
});