using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class EmpresaDal
    {
        private void Inserir(Empresa e)
        {
            try
            {
                var strQuery = "INSERT INTO Empresa(Nome, Email, CEP, Logradouro, Bairro, Cidade, Estado, DataCadastro, Telefone1, Telefone2, URL, CNPJ, Ativa, DiaVencimento, Observacoes) ";
                strQuery += "VALUES(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15)";

                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", e.Nome);
                        cmd.Parameters.AddWithValue("@p2", e.Email);
                        cmd.Parameters.AddWithValue("@p3", e.CEP);
                        cmd.Parameters.AddWithValue("@p4", e.Logradouro);
                        cmd.Parameters.AddWithValue("@p5", e.Bairro);
                        cmd.Parameters.AddWithValue("@p6", e.Cidade);
                        cmd.Parameters.AddWithValue("@p7", e.Estado);
                        cmd.Parameters.AddWithValue("@p8", e.DataCadastro);
                        cmd.Parameters.AddWithValue("@p9", e.Telefone1);
                        cmd.Parameters.AddWithValue("@p10", e.Telefone2);
                        cmd.Parameters.AddWithValue("@p11", e.URL);
                        cmd.Parameters.AddWithValue("@p12", e.CNPJ);
                        cmd.Parameters.AddWithValue("@p13", e.Ativa);
                        cmd.Parameters.AddWithValue("@p14", e.DiaVencimento);
                        cmd.Parameters.AddWithValue("@p15", e.Observacoes);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Salvar(Empresa empresa)
        {
            if (empresa.IdEmpresa > 0)
                Alterar(empresa);
            else
                Inserir(empresa);
        }

        private void Alterar(Empresa e)
        {
            try
            {
                var strQuery = "UPDATE Empresa SET ";
                strQuery += "Nome = @p1, Email = @p2, CEP = @p3 ,Logradouro = @p4 ,Bairro = @p5, ";
                strQuery += "Cidade = @p6, Estado = @p7, Telefone1 = @p8,Telefone2 = @p9, ";
                strQuery += "URL = @p10, CNPJ = @p11, Ativa = @p12, DiaVencimento = @p13, Observacoes = @p14 ";
                strQuery += string.Format("WHERE IdEmpresa = {0}", e.IdEmpresa);

                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", e.Nome);
                        cmd.Parameters.AddWithValue("@p2", e.Email);
                        cmd.Parameters.AddWithValue("@p3", e.CEP);
                        cmd.Parameters.AddWithValue("@p4", e.Logradouro);
                        cmd.Parameters.AddWithValue("@p5", e.Bairro);
                        cmd.Parameters.AddWithValue("@p6", e.Cidade);
                        cmd.Parameters.AddWithValue("@p7", e.Estado);
                        cmd.Parameters.AddWithValue("@p8", e.Telefone1);
                        cmd.Parameters.AddWithValue("@p9", e.Telefone2);
                        cmd.Parameters.AddWithValue("@p10", e.URL);
                        cmd.Parameters.AddWithValue("@p11", e.CNPJ);
                        cmd.Parameters.AddWithValue("@p12", e.Ativa);
                        cmd.Parameters.AddWithValue("@p13", e.DiaVencimento);
                        cmd.Parameters.AddWithValue("@p14", e.Observacoes);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Empresa> Listar(int? id)
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {

                    var strQuery = "SELECT E.*, COUNT(V.IdVeiculo) AS Total FROM Empresa AS E ";
                    strQuery += " LEFT JOIN Veiculo AS V ON V.IdEmpresa = E.IdEmpresa ";
                    if (id != null)
                        strQuery += string.Format("WHERE E.IdEmpresa = {0}", id);
                    strQuery += " GROUP BY E.IdEmpresa ";


                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    List<Empresa> lista = new List<Empresa>();
                    while (contexto.Dr.Read())
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(contexto.Dr["IdEmpresa"]);
                        empresa.Nome = Convert.ToString(contexto.Dr["Nome"]);
                        empresa.URL = Convert.ToString(contexto.Dr["URL"]);
                        empresa.CNPJ = Convert.ToString(contexto.Dr["CNPJ"]);
                        empresa.DiaVencimento = Convert.ToInt32(contexto.Dr["DiaVencimento"]);
                        empresa.Ativa = Convert.ToBoolean(contexto.Dr["Ativa"]);
                        empresa.Email = Convert.ToString(contexto.Dr["Email"]);
                        empresa.CEP = Convert.ToString(contexto.Dr["CEP"]);
                        empresa.Logradouro = Convert.ToString(contexto.Dr["Logradouro"]);
                        empresa.Bairro = Convert.ToString(contexto.Dr["Bairro"]);
                        empresa.Cidade = Convert.ToString(contexto.Dr["Cidade"]);
                        empresa.Estado = Convert.ToString(contexto.Dr["Estado"]);
                        empresa.Telefone1 = Convert.ToString(contexto.Dr["Telefone1"]);
                        empresa.Telefone2 = Convert.ToString(contexto.Dr["Telefone2"]);
                        empresa.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        empresa.Observacoes = Convert.ToString(contexto.Dr["Observacoes"]);
                        if (!string.IsNullOrEmpty(contexto.Dr["Total"].ToString()))
                            empresa.QtdVeiculos = Convert.ToInt32(contexto.Dr["Total"]);
                        else
                            empresa.QtdVeiculos = 0;
                        lista.Add(empresa);
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Empresa> Listar()
        {
            try
            {
                return Listar(null);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Empresa ListarPorId(int id)
        {
            try
            {
                return Listar(id).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                var strQuery = string.Format("DELETE FROM Empresa WHERE IdEmpresa = {0}", id);
                using (Conexao contexto = new Conexao())
                {
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
