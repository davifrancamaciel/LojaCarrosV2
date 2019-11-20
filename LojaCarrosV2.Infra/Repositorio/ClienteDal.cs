using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;
using System.Data;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class ClienteDal
    {
        private void Inserir(Cliente c)
        {
            try
            {
                var strQuery = "INSERT INTO Cliente(Nome, Email, CEP, Logradouro, Bairro, Cidade, Estado, DataCadastro, Telefone1, Telefone2, IdEmpresa) ";
                strQuery += "VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10, @p11)";

                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", c.Nome);
                        cmd.Parameters.AddWithValue("@p2", c.Email);
                        cmd.Parameters.AddWithValue("@p3", c.CEP);
                        cmd.Parameters.AddWithValue("@p4", c.Logradouro);
                        cmd.Parameters.AddWithValue("@p5", c.Bairro);
                        cmd.Parameters.AddWithValue("@p6", c.Cidade);
                        cmd.Parameters.AddWithValue("@p7", c.Estado);
                        cmd.Parameters.AddWithValue("@p8", c.DataCadastro);
                        cmd.Parameters.AddWithValue("@p9", c.Telefone1);
                        cmd.Parameters.AddWithValue("@p10", c.Telefone2);
                        cmd.Parameters.AddWithValue("@p11", c.IdEmpresa);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Salvar(Cliente cliente)
        {
            if (cliente.IdCliente > 0)
                Alterar(cliente);
            else
                Inserir(cliente);
        }

        private void Alterar(Cliente c)
        {
            try
            {
                var strQuery = "UPDATE Cliente SET ";
                strQuery += "Nome = @p1, Email = @p2, CEP = @p3 ,Logradouro = @p4 ,Bairro = @p5, ";
                strQuery += "Cidade = @p6, Estado = @p7, Telefone1 = @p8,Telefone2 = @p9, ";
                strQuery += "IdEmpresa = IF(@p10 > 0, @p10, IdEmpresa) ";
                strQuery += string.Format("WHERE IdCliente = {0}", c.IdCliente);

                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", c.Nome);
                        cmd.Parameters.AddWithValue("@p2", c.Email);
                        cmd.Parameters.AddWithValue("@p3", c.CEP);
                        cmd.Parameters.AddWithValue("@p4", c.Logradouro);
                        cmd.Parameters.AddWithValue("@p5", c.Bairro);
                        cmd.Parameters.AddWithValue("@p6", c.Cidade);
                        cmd.Parameters.AddWithValue("@p7", c.Estado);
                        cmd.Parameters.AddWithValue("@p8", c.Telefone1);
                        cmd.Parameters.AddWithValue("@p9", c.Telefone2);
                        cmd.Parameters.AddWithValue("@p10", c.IdEmpresa);
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
        /// lista os todos os servicos ativos na pagina de servicos listar servicos do administrador
        /// </summary>
        /// <returns></returns>
        public List<Cliente> Listar(string nome, int? idEmpresa)
        {
            try
            {
                var strQuery = "SELECT ";
                strQuery += "C.*, E.Nome AS NomeEmpresa ";
                strQuery += "FROM Cliente AS C ";
                strQuery += "INNER JOIN Empresa AS E ON E.IdEmpresa = C.IdEmpresa ";
                strQuery += string.Format("WHERE C.Nome LIKE '{0}' AND (C.IdEmpresa = @p1 OR @p1 IS NULL)", "%" + nome + "%", idEmpresa);
                strQuery += "ORDER BY C.Nome ASC";
                using (Conexao contexto = new Conexao())
                {
                    MySqlCommand Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = contexto.minhaConexao
                    };
                    Cmd.Parameters.AddWithValue("@p1", idEmpresa);
                    contexto.Dr = Cmd.ExecuteReader();

                    List<Cliente> lista = new List<Cliente>();
                    while (contexto.Dr.Read())
                    {
                        Cliente c = new Cliente();
                        c.Empresa = new Empresa();

                        c.IdCliente = Convert.ToInt32(contexto.Dr["IdCliente"]);
                        c.Nome = Convert.ToString(contexto.Dr["Nome"]);
                        c.Email = Convert.ToString(contexto.Dr["Email"]);
                        c.Telefone1 = Convert.ToString(contexto.Dr["Telefone1"]);
                        c.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        c.Empresa.Nome = Convert.ToString(contexto.Dr["NomeEmpresa"]);
                        c.IdEmpresa= Convert.ToInt32(contexto.Dr["IdEmpresa"]);
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


        public Cliente ListarPorId(int id, int? idEmpresa)
        {
            try
            {
                var strQuery = "SELECT ";
                strQuery += "IdCliente, C.Nome, C.Email, C.CEP, C.Logradouro, C.Cidade, C.Bairro, C.Estado, C.Telefone1, C.Telefone2, C.DataCadastro, C.IdEmpresa  ";
                strQuery += "FROM Cliente AS C ";
                strQuery += "INNER JOIN Empresa AS E ON E.IdEmpresa = C.IdEmpresa ";
                strQuery += string.Format("WHERE IdCliente = {0} AND (C.IdEmpresa = @p1 OR @p1 IS NULL)", id);
                using (Conexao contexto = new Conexao())
                {
                    MySqlCommand Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = contexto.minhaConexao
                    };
                    Cmd.Parameters.AddWithValue("@p1", idEmpresa);
                    contexto.Dr = Cmd.ExecuteReader();

                    Cliente cliente = new Cliente();

                    while (contexto.Dr.Read())
                    {

                        cliente.IdCliente = Convert.ToInt32(contexto.Dr["IdCliente"]);
                        cliente.Nome = Convert.ToString(contexto.Dr["Nome"]);
                        cliente.Email = Convert.ToString(contexto.Dr["Email"]);
                        cliente.CEP = Convert.ToString(contexto.Dr["CEP"]);
                        cliente.Logradouro = Convert.ToString(contexto.Dr["Logradouro"]);
                        cliente.Bairro = Convert.ToString(contexto.Dr["Bairro"]);
                        cliente.Cidade = Convert.ToString(contexto.Dr["Cidade"]);
                        cliente.Estado = Convert.ToString(contexto.Dr["Estado"]);
                        cliente.Telefone1 = Convert.ToString(contexto.Dr["Telefone1"]);
                        cliente.Telefone2 = Convert.ToString(contexto.Dr["Telefone2"]);
                        cliente.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        cliente.IdEmpresa = Convert.ToInt32(contexto.Dr["IdEmpresa"]);

                    }

                    return cliente;
                }
            }
            catch (Exception)
            { throw; }

        }

        public void Excluir(int id)
        {
            try
            {
                var strQuery = string.Format("DELETE FROM Cliente WHERE IdCliente = {0}", id);
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
