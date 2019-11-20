// registra um objeto com a url base para que seja usado pela aplicacao se que seja 
// necessario colocar o trecho incial da url em toda a aplicacao


angular.module("ListaTelefonica").value("config", {
    baseUrl: "http://localhost:37135"
});