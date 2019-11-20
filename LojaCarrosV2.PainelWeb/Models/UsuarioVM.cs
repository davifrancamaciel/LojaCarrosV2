using System;
using System.ComponentModel.DataAnnotations;

namespace LojaCarrosV2.PainelWeb.Models
{
       

    public class UsuarioModelLogin
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [Display(Name = "Email:")]
        //[EmailAddress(ErrorMessage = "preencha um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }
        public string Url { get; set; }
        public bool Remember { get; set; }
    }

    public class UsuarioModelCadastroAdminstrador
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo Empresa é obrigatório.")]
        [Display(Name = "Empresa:")]
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "O campo Permissão é obrigatório.")]
        [Display(Name = "Permissão:")]
        public int IdPermissao { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [StringLength(40, ErrorMessage = "Somente até 40 caracteres.")]
        [EmailAddress(ErrorMessage = "preencha um e-mail válido")]
        [Display(Name = "Email:")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Sua senha deve ter no minimo 2 e no maximo 10 caracteres.")]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "O campo Confirme sua Senha é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Sua senha deve ter no minimo 2 e no maximo 10 caracteres.")]
        [Display(Name = "Confirme sua Senha:")]
        public string SenhaConf { get; set; }

    }

    public class UsuarioEditarVM
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo Empresa é obrigatório.")]
        [Display(Name = "Empresa:")]
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "O campo Permissão é obrigatório.")]
        [Display(Name = "Permissão:")]
        public int IdPermissao { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [StringLength(40, ErrorMessage = "Somente até 40 caracteres.")]
        [EmailAddress(ErrorMessage = "preencha um e-mail válido")]
        [Display(Name = "Email:")]
        public string Email { get; set; }


        
    }

    public class UsuarioModelAlterarSenha
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }


        [Required(ErrorMessage = "O campo Senha atual é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Sua senha deve ter no minimo 2 e no maximo 10 caracteres.")]
        [Display(Name = "Senha atual:")]
        public string SenhaAtual { get; set; }


        [Required(ErrorMessage = "O campo Nova Senha é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Sua senha deve ter no minimo 2 e no maximo 10 caracteres.")]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "O campo Confirme sua nova Senha é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Sua senha deve ter no minimo 2 e no maximo 10 caracteres.")]
        [Display(Name = "Confirme sua Senha:")]
        public string SenhaConf { get; set; }
    }

    public class UsuarioRessetSenhaVM
    {
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string PermissaoValor { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Nova Senha é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Sua senha deve ter no minimo 2 e no maximo 10 caracteres.")]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "O campo Confirme sua nova Senha é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Sua senha deve ter no minimo 2 e no maximo 10 caracteres.")]
        [Display(Name = "Confirme sua Senha:")]
        public string SenhaConf { get; set; }
    }
}
