using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Domain.Entidade
{
    public class Configuracao
    {
        public int IdConfiguracao { get; set; }
        public string Nome { get; set; }
        public string NomeContato { get; set; }
        public string EmailContato { get; set; }
        public string SenhaContato { get; set; }
    }
}
