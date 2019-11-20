// forma service com funcao construtra "constructor Function que deve ser chamada de forma a dar new nesta funcao"
//var Pessoa = function (nome, idade) {
//    this.nome = nome,
//    this.idade = idade
//};

//var carlos = new Pessoa("Carlos", 25)
//console.log(carlos);


angular.module("ListaTelefonica").service("operadorasAPI", function ($http,config) {
    this.getOperadoras = function () {
        return $http.get(config.baseUrl + "/api/v1/tipo/carro/marcas");
    };
});