using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class ArquivoDal
    {

        public void Inserir(Arquivo a)
        {
            try
            {
                var strQuery = "INSERT INTO Arquivo(Nome,NomeAnterior,Tamanho,DataCadastro,IdVeiculo) ";
                strQuery += "VALUES(@p1,@p2,@p3,@p4,@p5)";
                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", a.Nome);
                        cmd.Parameters.AddWithValue("@p2", a.NomeAnterior);
                        cmd.Parameters.AddWithValue("@p3", a.Tamanho);
                        cmd.Parameters.AddWithValue("@p4", a.DataCadastro);
                        cmd.Parameters.AddWithValue("@p5", a.IdVeiculo);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// lista todos arquivos por id do tipo e usado pelo modal que exibe as fotos em tamanho maior
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Arquivo> ListarArquivosByIdVeiculo(int id)
        {
            try
            {
                var strQuery = "SELECT * FROM Arquivo AS A ";
                strQuery += "INNER JOIN Veiculo AS V ON A.IdVeiculo = V.IdVeiculo ";
                strQuery += string.Format("WHERE A.IdVeiculo = {0} ", id);


                using (Conexao contexto = new Conexao())
                {
                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    List<Arquivo> lista = new List<Arquivo>();
                    while (contexto.Dr.Read())
                    {
                        Arquivo arquivo = new Arquivo();
                        arquivo.IdArquivo = Convert.ToInt32(contexto.Dr["IdArquivo"]);
                        arquivo.Nome = Convert.ToString(contexto.Dr["Nome"]);
                        arquivo.NomeAnterior = Convert.ToString(contexto.Dr["NomeAnterior"]);
                        arquivo.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        arquivo.Tamanho = Convert.ToInt32(contexto.Dr["Tamanho"]);
                        arquivo.IdVeiculo = Convert.ToInt32(contexto.Dr["IdVeiculo"]);

                        lista.Add(arquivo);
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// lista todos arquivos por id do tipo e usado pelo modal que exibe as fotos em tamanho maior
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Arquivo> ListarPorId(int id, int idArquivo)
        {
            // ainda nao pode ser excluido pois esta sendo usado pelo metodo de exibir fotos do modal 
            try
            {
                var strQuery = "SELECT * FROM Arquivo AS A ";
                strQuery += "INNER JOIN Tipo AS T ON A.IdTipo = T.IdTipo ";
                strQuery += string.Format("WHERE A.IdTipo = {0} ", id);
                strQuery += string.Format("ORDER BY A.IdArquivo = {0} DESC", idArquivo);

                using (Conexao contexto = new Conexao())
                {
                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    List<Arquivo> lista = new List<Arquivo>();
                    while (contexto.Dr.Read())
                    {
                        Arquivo arquivo = new Arquivo();
                        arquivo.IdArquivo = Convert.ToInt32(contexto.Dr["IdArquivo"]);
                        arquivo.Nome = Convert.ToString(contexto.Dr["Nome"]);
                        arquivo.NomeAnterior = Convert.ToString(contexto.Dr["NomeAnterior"]);
                        arquivo.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        arquivo.Tamanho = Convert.ToInt32(contexto.Dr["Tamanho"]);
                        arquivo.IdVeiculo = Convert.ToInt32(contexto.Dr["IdTipo"]);

                        lista.Add(arquivo);
                    }

                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Arquivo> Listar()
        {
            var strQuery = "SELECT * FROM municipios_ibge ";
            using (Conexao contexto = new Conexao())
            {
                contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                List<Arquivo> lista = new List<Arquivo>();
                while (contexto.Dr.Read())
                {
                    Arquivo arquivo = new Arquivo();
                    arquivo.Nome = Convert.ToString(contexto.Dr["Nome"]);
                    lista.Add(arquivo);
                }
                return lista;
            }
        }
        public void Excluir(int id)
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = string.Format("DELETE FROM Arquivo WHERE IdArquivo = {0}", id);
                    contexto.ExecutaComando(strQuery);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}