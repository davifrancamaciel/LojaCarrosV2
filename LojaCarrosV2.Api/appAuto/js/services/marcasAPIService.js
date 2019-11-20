// forma service com funcao construtra "constructor Function que deve ser chamada de forma a dar new nesta funcao"
//var Pessoa = function (nome, idade) {
//    this.nome = nome,
//    this.idade = idade
//};

//var carlos = new Pessoa("Carlos", 25)
//console.log(carlos);


angular.module("AutoStore").service("marcasAPI", function ($http, config) {

    var _getMarcas = function () {

        return $http.get(config.baseUrl + "/api/v1/tipo/carro/marcas");
    };
    var _getMarcasByTipo = function (tipo) {

        return $http.get(config.baseUrl + "/api/v1/tipo/" + tipo + "/marcas");
    };
    return {
        getMarcas: _getMarcas,
        getMarcasByTipo: _getMarcasByTipo
    };
});