using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Domain.Entidade
{
    public class Multa
    {
        public int IdConsulta { get; set; }
        public int IdMulta { get; set; }
        public string AutoDeInfracao { get; set; }
        public string AutoDeRenainf { get; set; }
        public string DataPgtoDesconto { get; set; }
        public string Enquadramento { get; set; }
        public string DatadaInfracao { get; set; }
        public string HoraDaInfracao { get; set; }
        public string Descricao { get; set; }
        public string PlacaRelacionada { get; set; }
        public string LocalInfracao { get; set; }
        public string ValorOriginal { get; set; }
        public string ValorSerPago { get; set; }
        public string StatusPagamento { get; set; }
        public string OrgaoEmissor { get; set; }
        public string AgenteEmissor { get; set; }
    }
}
