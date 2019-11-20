using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;
using LojaCarrosV2.Infra.Interface;
using System;
using System.Collections.Generic;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class PermissaoDal : IRepositorioBase<Permissao>
    {
        public List<Permissao> Listar()
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {

                    var strQuery = "SELECT * FROM Permissao";
                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    List<Permissao> lista = new List<Permissao>();
                    while (contexto.Dr.Read())
                    {
                        Permissao permissao = new Permissao();
                        permissao.IdPermissao = Convert.ToInt32(contexto.Dr["IdPermissao"]);
                        permissao.Nome = Convert.ToString(contexto.Dr["Nome"]);
                        permissao.Valor = Convert.ToString(contexto.Dr["Valor"]);

                        lista.Add(permissao);
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}