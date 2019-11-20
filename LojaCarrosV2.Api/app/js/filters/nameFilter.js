angular.module("ListaTelefonica").filter("name", function () {
    return function (input) {
        var listaDeNomes = input.split(" ");
        var listaDeNomesFormatada = listaDeNomes.map(function (nome) {
            return nome.charAt(0).toUpperCase() + nome.substring(1);
        });


        //console.log(listaDeNomesFormatada);
        return listaDeNomesFormatada.join(" ");
    };
});