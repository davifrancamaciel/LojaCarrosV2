using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LojaCarrosV2.PainelWeb.Areas.master.Models
{
    public class EmpresaVM
    {
        [ScaffoldColumn(false)]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]        
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo URL é obrigatório.")]        
        public string URL { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        public string CNPJ { get; set; }
                
        public bool Ativa { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Dia do vencimento")]
        public int DiaVencimento { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string CEP { get; set; }
        
        [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        public string Logradouro { get; set; }
        
        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        public string Bairro { get; set; }
        
        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public string Cidade { get; set; }
        
        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        public string Estado { get; set; }
        
        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [Display(Name = "Telefone principal")]
        public string Telefone1 { get; set; }

        [Display(Name = "Telefone adicional")]
        public string Telefone2 { get; set; }
        
        public string Observacoes { get; set; }
    }
}