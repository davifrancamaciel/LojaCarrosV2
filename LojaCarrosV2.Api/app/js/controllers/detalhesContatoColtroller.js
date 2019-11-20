//angular.module("ListaTelefonica").controller("detalhesContatoController", function ($scope, $routeParams, contatosAPI) {
    


//    contatosAPI.getContato($routeParams.idVeiculo).success(function (contato) {
//        $scope.contato = contato;
//    });
  
//});
// pode ser feito dessa meneira desde que seja passado o resolve no roteamento
angular.module("ListaTelefonica").controller("detalhesContatoController", function ($scope, $routeParams, contato) {    
    $scope.contato = contato.data;
});