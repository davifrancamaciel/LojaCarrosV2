// formas de criar servicos essa forma é a fabrica onde invoca a funcao injetando $http one se tem um objeto de retorno
// criado Ex

//var criarPessoa = function (nome, idade) {
//    return {
//        nome: nome,
//        idade: idade
//    };
//};

//var maria = criarPessoa("Maria", 20);
//console.log(maria);


angular.module("ListaTelefonica").factory("contatosAPI", function ($http, config) {
    var _getContatos = function () {
        return $http.get(config.baseUrl + "/api/v1/veiculos");
    };
    var _getContato = function (idVeiculo) {
        return $http.get(config.baseUrl + "/api/v1/veiculos/" + idVeiculo);
    };
    var _saveContato = function (contato) {
        return $http.post(config.baseUrl + "/api/v1/veiculo", contato);
    };


    return {
        getContatos: _getContatos,
        getContato: _getContato,
        saveContato: _saveContato
    };
});



