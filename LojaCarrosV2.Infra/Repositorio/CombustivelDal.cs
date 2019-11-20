using System;
using System.Collections.Generic;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class CombustivelDal
    {
        public List<Combustivel> Listar()
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "SELECT * FROM Combustivel";
                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    List<Combustivel> lista = new List<Combustivel>();
                    while (contexto.Dr.Read())
                    {
                        Combustivel c = new Combustivel();
                        c.IdCombustivel = Convert.ToInt32(contexto.Dr["IdCombustivel"]);
                        c.Nome = Convert.ToString(contexto.Dr["Nome"]).ToUpper();

                        lista.Add(c);
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
