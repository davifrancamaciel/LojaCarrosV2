// registra um objeto com a url base para que seja usado pela aplicacao se que seja 
// necessario colocar o trecho incial da url em toda a aplicacao


angular.module("AutoStore").value("config", {
    baseUrl: "http://localhost:37135",
    diretorioArquivo: "http://localhost:28265/arquivos/redimensionados/"

    //baseUrl: "http://localhost:84/api",
    //diretorioArquivo: "http://localhost:28265/arquivos/miniaturas/"

    //baseUrl: "http://www.api.enriqueautomoveis.com.br",
    //diretorioArquivo: "http://www.painel.enriqueautomoveis.com.br/arquivos/miniaturas/"
});

