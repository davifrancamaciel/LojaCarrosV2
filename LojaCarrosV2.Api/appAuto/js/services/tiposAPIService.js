angular.module("AutoStore").service("tiposAPI", function ($http, config) {
    var _getTipos = function () {
    
        return $http.get(config.baseUrl + "/api/v1/tipos");
        //var tipoZero= $http.get(config.baseUrl + "/api/v1/tipos");
        //console.log(tipoZero);
    };
    return { getTipos: _getTipos };
});