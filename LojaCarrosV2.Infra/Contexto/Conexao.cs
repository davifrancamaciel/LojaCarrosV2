using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace LojaCarrosV2.Infra.Contexto
{

    /// <summary>
    /// // classe de conexao para trabalhar com  banco MySql
    /// // obs fazer o download mysql.com/products/connector/
    /// //ADO.NET Driver for MySQL (Connector/NET)
    /// // e instalar depois fazer uma referencia do projeto para "MySql.Data"
    ///  // a connectionString="server=127.0.0.1; Database=BancoSalao; User id=root; password=davi;
    /// </summary>
    public class Conexao : IDisposable
    {
        public MySqlConnection minhaConexao;
        public MySqlDataReader Dr;
        public MySqlCommand Cmd;
        public Conexao()
        {
            minhaConexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            minhaConexao.Open();   
        }
        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new MySqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };
            cmdComando.ExecuteNonQuery();
        }
       
        public MySqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando = new MySqlCommand(strQuery, minhaConexao);

            return cmdComando.ExecuteReader();
        }

        public void Dispose()
        {
            if (minhaConexao.State.Equals(ConnectionState.Open))
                minhaConexao.Close();          
        }
    }
}
