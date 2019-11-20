

using LojaCarrosV2.Utilidade;
using System;

namespace LojaCarrosV2.Domain.Entidade
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string TokenUsuario { get; set; }
        public DateTime? DataCriacao { get; set; }
        public int? ValidadeTokenUsuario { get; set; }
        public int IdPermissao { get; set; }
        public int IdEmpresa { get; set; }
        public Empresa Empresa { get; set; }
        public Permissao Permissao { get; set; }

        //public string[] Roles { get; set; }        
              
        
        public void ValidaSenha(string senha, string senhaConfirm)
        {
            AssertionConcern.AssertArgumentNotNull(senha, "Senha invalida!");
            AssertionConcern.AssertArgumentNotNull(senhaConfirm, "Confirmação de Senha invalida!");
            AssertionConcern.AssertArgumentEquals(senha, senhaConfirm, "As senhas não conferem!");
            AssertionConcern.AssertArgumentLength(senha, 2, 20, "As senhas devem ter no minimo 2 e no maxiomo 20 caracteres!");

            this.Senha = senha;
        }
    }   
}
