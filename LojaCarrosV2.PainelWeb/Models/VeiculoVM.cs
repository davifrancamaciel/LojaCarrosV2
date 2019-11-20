
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LojaCarrosV2.PainelWeb.Models
{

    public class VeiculoVM
    {
        public int QtdAcesso { get; set; }
        [Display(Name = "Empresa:")]
        public int IdEmpresa { get; set; }
        public DateTime DataCadastro { get; set; }
        public int Pagina { get; set; }
        [Required(ErrorMessage = "O campo Tipo é obrigatório.")]
        [Display(Name = "Tipo:")]
        public int IdTipo { get; set; }
        [Required(ErrorMessage = "O campo Combustivel é obrigatório.")]
        [Display(Name = "Combustivel:")]
        public int IdCombustivel { get; set; }
        [Required(ErrorMessage = "O campo Marca é obrigatório.")]
        [Display(Name = "Marca:")]
        public int IdMarca { get; set; }

        [Display(Name = "Ativo:")]
        public Boolean Ativo { get; set; }

        [Display(Name = "Destaque:")]
        public Boolean Destaque { get; set; }

        [Display(Name = "Exibe valor?:")]
        public Boolean ExibeValor { get; set; }

        public int IdVeiculo { get; set; }

        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo Modelo deve ter no minimo 2 e no maximo 50 caracteres.")]
        [Display(Name = "Modelo:")]
        public string Modelo { get; set; }

        [Display(Name = "Ano Fabricacao:")]
        [Required(ErrorMessage = "obrigatório.")]
        public int AnoFabricacao { get; set; }

        public int AnoModelo { get; set; }


        [Display(Name = "Renavan:")]
        public string Renavan { get; set; }

        [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
        [StringLength(3000, MinimumLength = 2, ErrorMessage = "O campo Descricao deve ter no minimo 2 e no maximo 3000 caracteres.")]
        [Display(Name = "Descricao:")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        //[StringLength(150, MinimumLength = 2, ErrorMessage = "O campo Valor deve ter no minimo 2 e no maximo 150 caracteres.")]
        [Range(0, 1000000, ErrorMessage = "O campo Valor deve ser entre R$ 0 e R$ 1000000")]
        [Display(Name = "Valor:")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        //[StringLength(150, MinimumLength = 2, ErrorMessage = "O campo Valor deve ter no minimo 2 e no maximo 150 caracteres.")]
        [Range(0, 1000000, ErrorMessage = "O campo Valor deve ser entre R$ 0 e R$ 1000000")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Valor:")]
        public float Valor2 { get; set; }





    }

    public class BuscaModel
    {
        public string Termo { get; set; }
        //public string Tipo { get; set; }
        //public string Marca { get; set; }
        //public int? AnoInicio { get; set; }
        //public int? AnoFim { get; set; }

    }

    //public class BuscaFilroVM
    //{
    //    public string IdTipo { get; set; }
    //    public string IdMarca { get; set; }
    //    public string AnoInicio { get; set; }
    //    public string AnoFim { get; set; }

    //}

    public class TokenViewModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}