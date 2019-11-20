using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Domain.Entidade
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }        
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }        
        public DateTime DataCadastro { get; set; }        
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public int IdEmpresa { get; set; }
        public Empresa Empresa { get; set; }
    }
}
