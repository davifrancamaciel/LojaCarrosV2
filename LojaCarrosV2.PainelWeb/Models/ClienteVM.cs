using System;
using System.ComponentModel.DataAnnotations;

namespace LojaCarrosV2.PainelWeb.Models
{
    public class ClienteVM
    {
        [Display(Name = "Empresa:")]
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "Somente até 150 caracteres.")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }


        [Display(Name = "Email:")]
        [StringLength(40, ErrorMessage = "Somente até 40 caracteres.")]
        [EmailAddress(ErrorMessage = "E-mail incorreto")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "O campo CEP é obrigatório.")]
        [StringLength(9, ErrorMessage = "Somente até 9 caracteres. Ex.: 25601-234")]
        [Display(Name = "CEP:")]
        public string CEP { get; set; }

        //[Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        [StringLength(200, ErrorMessage = "Somente até 200 caracteres.")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        //[Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        [StringLength(40, ErrorMessage = "Somente até 40 caracteres.")]
        [Display(Name = "Bairro:")]
        public string Bairro { get; set; }



        //[Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        [StringLength(40, ErrorMessage = "Somente até 40 caracteres.")]
        [Display(Name = "Cidade:")]
        public string Cidade { get; set; }

        [Display(Name = "Estado:")]
        public string Estado { get; set; }
        public DateTime DataCadastro { get; set; }



        [Required(ErrorMessage = "O campo Telefone celular é obrigatório.")]
        [StringLength(20, ErrorMessage = "Somente até 20 caracteres.")]
        [Display(Name = "Telefone celular:")]
        public string Telefone1 { get; set; }

        [StringLength(20, ErrorMessage = "Somente até 20 caracteres.")]
        [Display(Name = "Telefone fixo:")]
        public string Telefone2 { get; set; }
    }
}