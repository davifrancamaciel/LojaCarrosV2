
using LojaCarrosV2.Domain.Entidade;
using System;
namespace LojaCarrosV2.Domain.Entidade
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string Nome { get; set; }
        public Tipo Tipo { get; set; }
        public int QtdVeiculo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
