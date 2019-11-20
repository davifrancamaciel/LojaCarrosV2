using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class VeiculoDal
    {

        private int Inserir(Veiculo veiculo)
        {
            try
            {
                var strQuery = "INSERT INTO Veiculo(DataCadastro, Modelo, Descricao, AnoFabricacao, Valor, IdCombustivel, Ativo, IdMarca, AnoModelo, Destaque, Renavan, IdEmpresa, ExibeValor, QtdAcesso) ";
                strQuery += "VALUES(NOW(), @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, 0)";
                long ultimoIdServico;
                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {
                        cmd.Parameters.AddWithValue("@p1", veiculo.Modelo);
                        cmd.Parameters.AddWithValue("@p2", veiculo.Descricao);
                        cmd.Parameters.AddWithValue("@p3", veiculo.AnoFabricacao);
                        cmd.Parameters.AddWithValue("@p4", veiculo.Valor);
                        cmd.Parameters.AddWithValue("@p5", veiculo.Combustivel.IdCombustivel);
                        cmd.Parameters.AddWithValue("@p6", veiculo.Ativo);
                        cmd.Parameters.AddWithValue("@p7", veiculo.Marca.IdMarca);
                        cmd.Parameters.AddWithValue("@p8", veiculo.AnoModelo);
                        cmd.Parameters.AddWithValue("@p9", veiculo.Destaque);
                        cmd.Parameters.AddWithValue("@p10", veiculo.Renavan);
                        cmd.Parameters.AddWithValue("@p11", veiculo.IdEmpresa);
                        cmd.Parameters.AddWithValue("@p12", veiculo.ExibeValor);
                        


                        cmd.ExecuteNonQuery();
                        ultimoIdServico = cmd.LastInsertedId;
                    }

                }
                return veiculo.IdVeiculo = Convert.ToInt32(ultimoIdServico);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Salvar(Veiculo v)
        {
            if (v.IdVeiculo > 0)
                Alterar(v);
            else
                Inserir(v);
            return v.IdVeiculo;
        }

        private int Alterar(Veiculo veiculo)
        {
            try
            {
                var strQuery = " UPDATE Veiculo SET ";
                strQuery += "Modelo = IFNULL(@p1, Modelo), ";
                strQuery += "Descricao = IFNULL(@p2, Descricao),";
                strQuery += "AnoFabricacao = IF(@p3 > 0,@p3, AnoFabricacao), ";
                strQuery += "Valor = IF(@p4 > 0,@p4, Valor), ";
                strQuery += "Ativo = IFNULL(@p5, Ativo), ";
                strQuery += "IdMarca = IF(@p6 > 0,@p6, IdMarca), ";
                strQuery += "AnoModelo = IF(@p7 > 0,@p7, AnoModelo), ";
                strQuery += "Destaque = IFNULL(@p8, Destaque), ";
                strQuery += "IdCombustivel = IF(@p9 > 0, @p9, IdCombustivel), ";
                strQuery += "Renavan = @p10, ";
                strQuery += "IdEmpresa = IF(@p11 > 0, @p11, IdEmpresa), ";
                strQuery += "ExibeValor = IFNULL(@p12, ExibeValor), ";
                strQuery += "QtdAcesso = IF(@p13 > 0, @p13, QtdAcesso)";
                strQuery += string.Format("WHERE IdVeiculo = {0}", veiculo.IdVeiculo);
                
                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {
                        cmd.Parameters.AddWithValue("@p1", veiculo.Modelo);
                        cmd.Parameters.AddWithValue("@p2", veiculo.Descricao);
                        cmd.Parameters.AddWithValue("@p3", veiculo.AnoFabricacao);
                        cmd.Parameters.AddWithValue("@p4", veiculo.Valor);
                        cmd.Parameters.AddWithValue("@p5", veiculo.Ativo);
                        cmd.Parameters.AddWithValue("@p6", veiculo.Marca.IdMarca);
                        cmd.Parameters.AddWithValue("@p7", veiculo.AnoModelo);
                        cmd.Parameters.AddWithValue("@p8", veiculo.Destaque);
                        cmd.Parameters.AddWithValue("@p9", veiculo.Combustivel.IdCombustivel);
                        cmd.Parameters.AddWithValue("@p10", veiculo.Renavan);
                        cmd.Parameters.AddWithValue("@p11", veiculo.IdEmpresa);
                        cmd.Parameters.AddWithValue("@p12", veiculo.ExibeValor);
                        cmd.Parameters.AddWithValue("@p13", veiculo.QtdAcesso);

                        cmd.ExecuteNonQuery();

                    }
                }
                return veiculo.IdVeiculo;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Veiculo> ListarByFilto(string tipo, string marca, int? anoInicio, int? anoFim, int? idEmpresa)
        {
            try
            {

                var strQuery = "procVeiculoByFiltroSELECT";//(TipoV,MarcaV,anoInicio,anoFim)";

                using (Conexao contexto = new Conexao())
                {
                    MySqlCommand Cmd = new MySqlCommand
                     {
                         CommandText = strQuery,
                         CommandType = CommandType.StoredProcedure,
                         Connection = contexto.minhaConexao
                     };

                    Cmd.Parameters.AddWithValue("TipoV", tipo);
                    Cmd.Parameters.AddWithValue("MarcaV", marca);
                    Cmd.Parameters.AddWithValue("anoInicio", anoInicio);
                    Cmd.Parameters.AddWithValue("anoFim", anoFim);
                    Cmd.Parameters.AddWithValue("idEmpresa", idEmpresa);

                    contexto.Dr = Cmd.ExecuteReader();

                    List<Veiculo> lista = new List<Veiculo>();
                    while (contexto.Dr.Read())
                    {
                        Veiculo v = new Veiculo();
                        v.Arquivo = new Arquivo();
                        v.Combustivel = new Combustivel();
                        v.Marca = new Marca();

                        v.IdVeiculo = Convert.ToInt32(contexto.Dr["IdVeiculo"]);
                        v.Modelo = Convert.ToString(contexto.Dr["Modelo"]);
                        v.ExibeValor = Convert.ToBoolean(contexto.Dr["ExibeValor"]);
                        v.AnoFabricacao = Convert.ToInt32(contexto.Dr["AnoFabricacao"]);
                        v.AnoModelo = Convert.ToInt32(contexto.Dr["AnoModelo"]);
                        v.Valor = Convert.ToDecimal(contexto.Dr["Valor"]);
                        v.Combustivel.Nome = Convert.ToString(contexto.Dr["CombustivelNome"]);
                        v.Arquivo.Nome = Convert.ToString(contexto.Dr["aNome"]);
                        v.Marca.Nome = Convert.ToString(contexto.Dr["MarcaNome"]);

                        if (string.IsNullOrEmpty(Convert.ToString(contexto.Dr["aNome"])))
                            v.Arquivo.Nome = Convert.ToString("_semfoto.jpg");

                        lista.Add(v);
                    }
                    
                    return lista;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Veiculo> Listar(int? id, bool? ativo, int? idEmpresa)
        {
            try
            {
                var strQuery = "procVeiculoSELECT";

                using (Conexao contexto = new Conexao())
                {
                    MySqlCommand Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.StoredProcedure,
                        Connection = contexto.minhaConexao
                    };


                    Cmd.Parameters.AddWithValue("idVeiculo", id);
                    Cmd.Parameters.AddWithValue("ativo", ativo);
                    Cmd.Parameters.AddWithValue("idEmpresa", idEmpresa);


                    contexto.Dr = Cmd.ExecuteReader();

                    List<Veiculo> lista = new List<Veiculo>();
                    while (contexto.Dr.Read())
                    {
                        Veiculo v = new Veiculo();
                        v.Marca = new Marca();
                        v.Marca.Tipo = new Tipo();
                        v.Arquivo = new Arquivo();
                        v.Combustivel = new Combustivel();
                        v.Empresa = new Empresa();

                        v.ExibeValor = Convert.ToBoolean(contexto.Dr["ExibeValor"]);
                        v.Renavan = Convert.ToString(contexto.Dr["Renavan"]);
                        v.IdVeiculo = Convert.ToInt32(contexto.Dr["IdVeiculo"]);
                        v.QtdAcesso = Convert.ToInt32(contexto.Dr["QtdAcesso"]);
                        v.Modelo = Convert.ToString(contexto.Dr["Modelo"]);
                        v.Descricao = Convert.ToString(contexto.Dr["Descricao"]);
                        v.AnoFabricacao = Convert.ToInt32(contexto.Dr["AnoFabricacao"]);
                        v.AnoModelo = Convert.ToInt32(contexto.Dr["AnoModelo"]);
                        v.Valor = Convert.ToDecimal(contexto.Dr["Valor"]);
                        v.Destaque = Convert.ToBoolean(contexto.Dr["Destaque"]);
                        v.Marca.Nome = Convert.ToString(contexto.Dr["MarcaNome"]);
                        v.Marca.IdMarca = Convert.ToInt32(contexto.Dr["IdMarca"]);
                        v.Arquivo.Nome = Convert.ToString(contexto.Dr["aNome"]);
                        v.Destaque = Convert.ToBoolean(contexto.Dr["Destaque"]);
                        v.Ativo = Convert.ToBoolean(contexto.Dr["Ativo"]);
                        v.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        v.Combustivel.Nome = Convert.ToString(contexto.Dr["CombustivelNome"]);
                        v.Combustivel.IdCombustivel = Convert.ToInt32(contexto.Dr["idCombustivel"]);
                        v.Marca.Tipo.IdTipo = Convert.ToInt32(contexto.Dr["idTipo"]);
                        v.IdEmpresa = Convert.ToInt32(contexto.Dr["IdEmpresa"]);
                        
                        v.Empresa.Nome = Convert.ToString(contexto.Dr["NomeEmpresa"]);
                        v.Empresa.URL = Convert.ToString(contexto.Dr["URL"]);
                        if (string.IsNullOrEmpty(Convert.ToString(contexto.Dr["aNome"])))
                            v.Arquivo.Nome = Convert.ToString("_semfoto.jpg");

                        lista.Add(v);
                    }

                    return lista;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Veiculo ListarById(int? id, bool? ativo, int? idEmpresa)
        {
            try
            {
                return Listar(id, ativo, idEmpresa).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Excluir(int id)
        {
            try
            {
                var strQuery = string.Format("DELETE FROM Veiculo WHERE IdVeiculo = {0}", id);
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
