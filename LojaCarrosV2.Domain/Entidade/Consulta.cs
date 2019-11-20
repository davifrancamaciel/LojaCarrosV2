using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Domain.Entidade
{
   public class Consulta
    {
        public int IdConsulta { get; set; }
        public DateTime DataCadastro { get; set; }
        public string DataConsulta { get; set; }
        public string Renavan { get; set; }
        public string QtdMultas { get; set; }
        public int IdVeiculo { get; set; }
        public Multa Multa { get; set; }
        public List<Multa> Multas { get; set; }
    }
}
