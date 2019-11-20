using System;

namespace LojaCarrosV2.Domain.Entidade
{
    public class Veiculo
    {
        public int QtdAcesso { get; set; }
        public int IdVeiculo { get; set; }
        public string Modelo { get; set; }
        public string Descricao { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public bool Destaque { get; set; }
        public bool ExibeValor { get; set; }
        public Marca Marca { get; set; }       
        public Tipo Tipo { get; set; }        
        public Arquivo Arquivo { get; set; }
        public Combustivel Combustivel { get; set; }
        public string Renavan { get; set; }
        public int IdEmpresa { get; set; }
        public Empresa Empresa { get; set; }
    }
}
