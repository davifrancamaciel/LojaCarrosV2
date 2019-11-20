angular.module("AutoStore").controller("tipoController", function ($scope, tipos) {
    $scope.tipos = tipos.data;

});