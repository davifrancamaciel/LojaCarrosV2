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
    public class MultaDal
    {
        private static void Inserir(Multa m)
        {
            try
            {
                if (m.StatusPagamento.Contains("N"))
                {
                    m.StatusPagamento = "NÃO PAGO";
                }

                var strQuery = "INSERT INTO Multa";
                strQuery += "(AutoDeInfracao, AutoDeRenainf, DataPgtoDesconto, Enquadramento, DatadaInfracao, HoraDaInfracao, Descricao, PlacaRelacionada, LocalInfracao, ValorOriginal, ValorSerPago, StatusPagamento, OrgaoEmissor, AgenteEmissor, IdConsulta) ";
                strQuery += "VALUES(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15)";

                using (Conexao contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", m.AutoDeInfracao);
                        cmd.Parameters.AddWithValue("@p2", m.AutoDeRenainf);
                        cmd.Parameters.AddWithValue("@p3", m.DataPgtoDesconto);
                        cmd.Parameters.AddWithValue("@p4", m.Enquadramento);
                        cmd.Parameters.AddWithValue("@p5", m.DatadaInfracao);
                        cmd.Parameters.AddWithValue("@p6", m.HoraDaInfracao);
                        cmd.Parameters.AddWithValue("@p7", m.Descricao);
                        cmd.Parameters.AddWithValue("@p8", m.PlacaRelacionada);
                        cmd.Parameters.AddWithValue("@p9", Replace( m.LocalInfracao));
                        cmd.Parameters.AddWithValue("@p10", m.ValorOriginal);
                        cmd.Parameters.AddWithValue("@p11", m.ValorSerPago);
                        cmd.Parameters.AddWithValue("@p12", m.StatusPagamento);
                        cmd.Parameters.AddWithValue("@p13", Replace(m.OrgaoEmissor));
                        cmd.Parameters.AddWithValue("@p14", m.AgenteEmissor);
                        cmd.Parameters.AddWithValue("@p15", m.IdConsulta);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string Replace(string texto)
        {
            texto = texto.Replace("(", "");
            texto = texto.Replace(")", "");
            texto = texto.Replace("\u0089", "");
            return texto;
         }
       
        public static void Salvar(Multa m)
        {
            Inserir(m);
        }

        public static List<Multa> Listar(int? idConsulta)
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "procMultaSELECT";

                    contexto.Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.StoredProcedure,
                        Connection = contexto.minhaConexao
                    };

                    contexto.Cmd.Parameters.AddWithValue("ConsultaID", idConsulta);

                    contexto.Dr = contexto.Cmd.ExecuteReader();

                    List<Multa> lista = new List<Multa>();
                    while (contexto.Dr.Read())
                    {
                        Multa multa = new Multa();


                        multa.AgenteEmissor = Convert.ToString(contexto.Dr["AgenteEmissor"]);
                        multa.AutoDeInfracao = Convert.ToString(contexto.Dr["AutoDeInfracao"]);
                        multa.AutoDeRenainf = Convert.ToString(contexto.Dr["AutoDeRenainf"]);
                        multa.DatadaInfracao = Convert.ToString(contexto.Dr["DatadaInfracao"]);
                        multa.DataPgtoDesconto = Convert.ToString(contexto.Dr["DataPgtoDesconto"]);
                        multa.Descricao = Convert.ToString(contexto.Dr["Descricao"]);
                        multa.Enquadramento = Convert.ToString(contexto.Dr["Enquadramento"]);
                        multa.HoraDaInfracao = Convert.ToString(contexto.Dr["HoraDaInfracao"]);
                        multa.IdMulta = Convert.ToInt32(contexto.Dr["IdMulta"]);
                        multa.LocalInfracao = Convert.ToString(contexto.Dr["LocalInfracao"]);
                        multa.OrgaoEmissor = Convert.ToString(contexto.Dr["OrgaoEmissor"]);
                        multa.PlacaRelacionada = Convert.ToString(contexto.Dr["PlacaRelacionada"]);
                        multa.StatusPagamento = Convert.ToString(contexto.Dr["StatusPagamento"]);
                        multa.ValorOriginal = Convert.ToString(contexto.Dr["ValorOriginal"]);
                        multa.ValorSerPago = Convert.ToString(contexto.Dr["ValorSerPago"]);

                        lista.Add(multa);
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
