using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class ConsultaDal
    {
        private static int Inserir(Consulta c)
        {
            try
            {
                var strQuery = "INSERT INTO Consulta";
                strQuery += "(DataCadastro, IdVeiculo, DataConsulta, Renavan, QtdMultas) ";
                strQuery += "VALUES(NOW(), @p1, @p2, @p3, @p4)";
                long ultimoId;
                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", c.IdVeiculo);
                        cmd.Parameters.AddWithValue("@p2", Convert.ToString(c.DataConsulta));
                        cmd.Parameters.AddWithValue("@p3", c.Renavan);
                        cmd.Parameters.AddWithValue("@p4", c.QtdMultas);
                        
                        cmd.ExecuteNonQuery();
                        ultimoId = cmd.LastInsertedId;
                    }
                }
                return c.IdConsulta = Convert.ToInt32(ultimoId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Salvar(Consulta c)
        {
            return Inserir(c);
        }


        public static List<Consulta> Listar(int? idVeiculo)
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "procConsultaSELECT";

                    contexto.Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.StoredProcedure,
                        Connection = contexto.minhaConexao
                    };

                    contexto.Cmd.Parameters.AddWithValue("VeiculoID", idVeiculo);


                    contexto.Dr = contexto.Cmd.ExecuteReader();

                    List<Consulta> lista = new List<Consulta>();
                    while (contexto.Dr.Read())
                    {
                        Consulta consulta = new Consulta();

                        consulta.IdConsulta = Convert.ToInt32(contexto.Dr["IdConsulta"]);
                        consulta.QtdMultas = Convert.ToString(contexto.Dr["QtdMultas"]);
                        consulta.DataCadastro = Convert.ToDateTime(contexto.Dr["DataCadastro"]);
                        consulta.DataConsulta = Convert.ToString(contexto.Dr["DataConsulta"]);
                        consulta.IdVeiculo = Convert.ToInt32(contexto.Dr["IdVeiculo"]);
                        consulta.Renavan = Convert.ToString(contexto.Dr["Renavan"]);

                        lista.Add(consulta);
                    }
                    
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




        public static void Excluir(int id)
        {
            try
            {
                var strQuery = string.Format("DELETE FROM Consulta WHERE IdConsulta = {0}", id);
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
