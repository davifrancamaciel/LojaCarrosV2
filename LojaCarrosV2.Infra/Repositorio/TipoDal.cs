using System;
using System.Collections.Generic;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class TipoDal
    {
        public List<Tipo> Listar()
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "SELECT * FROM Tipo";
                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    List<Tipo> lista = new List<Tipo>();
                    while (contexto.Dr.Read())
                    {
                        Tipo tipo = new Tipo();
                        tipo.IdTipo = Convert.ToInt32(contexto.Dr["IdTipo"]);
                        tipo.Nome = Convert.ToString(contexto.Dr["Nome"]).ToUpper();

                        lista.Add(tipo);
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