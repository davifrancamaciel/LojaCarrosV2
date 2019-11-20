using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Domain.Entidade
{
   public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public string CNPJ { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DiaVencimento { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }        
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Observacoes { get; set; }
        public int QtdVeiculos { get; set; }
    }
}
