using System;

namespace LojaCarrosV2.Domain.Entidade
{
    public class Arquivo
    {
        public int IdArquivo { get; set; }
        public string Nome { get; set; }
        public string NomeAnterior { get; set; }
        public int Tamanho { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdVeiculo { get; set; }     

    }
}
