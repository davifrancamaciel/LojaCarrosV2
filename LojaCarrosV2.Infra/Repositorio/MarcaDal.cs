using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class MarcaDal
    {
        private void Inserir(Marca m)
        {
            try
            {
                var strQuery = "INSERT INTO Marca";
                strQuery += "(Nome, IdTipo, DataCadastro) ";
                strQuery += "VALUES(@p1, @p2,NOW())";

                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", m.Nome);
                        cmd.Parameters.AddWithValue("@p2", m.Tipo.IdTipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Salvar(Marca m)
        {
            if (m.IdMarca > 0)
                Alterar(m);
            else
                Inserir(m);
        }

        private void Alterar(Marca m)
        {
            try
            {
                var strQuery = "UPDATE Marca SET ";
                strQuery += "Nome = @p1, IdTipo = @p2 ";
                strQuery += string.Format("WHERE IdMarca = {0} ", m.IdMarca);
                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {
                        cmd.Parameters.AddWithValue("@p1", m.Nome);
                        cmd.Parameters.AddWithValue("@p2", m.Tipo.IdTipo);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Marca> Listar(int? idMarca)
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "procMarcaSELECT";

                    contexto.Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.StoredProcedure,
                        Connection = contexto.minhaConexao
                    };

                    contexto.Cmd.Parameters.AddWithValue("idMarca", idMarca);

                    contexto.Dr = contexto.Cmd.ExecuteReader();

                    List<Marca> lista = new List<Marca>();
                    while (contexto.Dr.Read())
                    {
                        Marca marca = new Marca();
                        marca.Tipo = new Tipo();

                        marca.IdMarca = Convert.ToInt32(contexto.Dr["IdMarca"]);
                        marca.Nome = Convert.ToString(contexto.Dr["Nome"]).ToUpper();
                        marca.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        marca.Tipo.Nome = Convert.ToString(contexto.Dr["NomeTipo"]).ToUpper();
                        marca.Tipo.IdTipo = Convert.ToInt32(contexto.Dr["IdTipo"]);
                        lista.Add(marca);
                    }                    
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Marca> ListarByTipo(string tipo, bool? ativo,int? idEmpresa)
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "procMarcaByTipoSELECT";

                    contexto.Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.StoredProcedure,
                        Connection = contexto.minhaConexao
                    };

                    contexto.Cmd.Parameters.AddWithValue("idTipo", tipo);
                    contexto.Cmd.Parameters.AddWithValue("ativo", ativo);
                    contexto.Cmd.Parameters.AddWithValue("idEmpresa", idEmpresa);

                    contexto.Dr = contexto.Cmd.ExecuteReader();

                    List<Marca> lista = new List<Marca>();
                    while (contexto.Dr.Read())
                    {
                        Marca marca = new Marca();
                        marca.Tipo = new Tipo();
                        marca.IdMarca = Convert.ToInt32(contexto.Dr["IdMarca"]);
                        marca.Nome = Convert.ToString(contexto.Dr["Nome"]).ToUpper();
                        marca.QtdVeiculo = Convert.ToInt32(contexto.Dr["QtdVeiculo"]);
                        marca.Tipo.Nome = Convert.ToString(contexto.Dr["TipoNome"]);

                        lista.Add(marca);
                    }
                  
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Marca> ListarByIdTipo(int? IdTipo)
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "procMarcaByIdTipoSELECT";

                    contexto.Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.StoredProcedure,
                        Connection = contexto.minhaConexao
                    };

                    contexto.Cmd.Parameters.AddWithValue("idTipo", IdTipo);

                    contexto.Dr = contexto.Cmd.ExecuteReader();

                    List<Marca> lista = new List<Marca>();
                    while (contexto.Dr.Read())
                    {
                        Marca marca = new Marca();
                        marca.IdMarca = Convert.ToInt32(contexto.Dr["IdMarca"]);
                        marca.Nome = Convert.ToString(contexto.Dr["Nome"]).ToUpper();

                        lista.Add(marca);
                    }
                   
                    return lista;
                }
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
                var strQuery = string.Format("DELETE FROM Marca WHERE IdMarca = {0}", id);
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
