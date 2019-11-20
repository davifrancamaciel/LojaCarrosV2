angular.module("AutoStore").factory("veiculosAPI", function ($http, config) {
    var _getVeiculos = function () {
        return $http.get(config.baseUrl + "/api/v1/empresa/2/veiculos");
    };
    var _getVeiculo = function (idVeiculo) {
        return $http.get(config.baseUrl + "/api/v1/empresa/2/veiculo/" + idVeiculo);
    };

    var _getVeiculosDestaque = function () {
        return $http.get(config.baseUrl + "/api/v1/empresa/2/veiculosdestaque");
    };
    var _saveVeiculo = function (contato) {
        return $http.post(config.baseUrl + "/api/v1/empresa/2/veiculo", contato);
    };


    return {
        getVeiculos: _getVeiculos,
        getVeiculo: _getVeiculo,
        getVeiculosDestaque: _getVeiculosDestaque,
        saveCVeiculo: _saveVeiculo
    };
});
