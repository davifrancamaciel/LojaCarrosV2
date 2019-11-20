using LojaCarrosV2.Infra.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class ClienteMoqDal
    {
        public List<ClienteMoq> Listar()
        {
            try
            {
                using (Conexao contexto = new Conexao())
                {
                    var strQuery = "SELECT * FROM estagiario LIMIT 200";
                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    List<ClienteMoq> lista = new List<ClienteMoq>();
                    while (contexto.Dr.Read())
                    {
                        ClienteMoq c = new ClienteMoq();
                        if (!string.IsNullOrEmpty(contexto.Dr["Id_Sexo"].ToString()))
                            c.Id_Sexo = Convert.ToInt32(contexto.Dr["Id_Sexo"]);
                        else
                            c.Id_Sexo = 3;


                        c.Bairro = Convert.ToString(contexto.Dr["Bairro"]);
                        c.Celular = Convert.ToString(contexto.Dr["Celular"]);
                        c.Cep = Convert.ToString(contexto.Dr["Cep"]);
                        c.Cidade = Convert.ToString(contexto.Dr["Cidade"]);
                        c.Complemento = Convert.ToString(contexto.Dr["Complemento"]);
                        c.CPF = Convert.ToString(contexto.Dr["CPF"]);
                        if (!string.IsNullOrEmpty(contexto.Dr["Data"].ToString()))
                            c.Data = Convert.ToDateTime(contexto.Dr["Data"]);
                        else
                            c.Data = Convert.ToDateTime(DateTime.Now);

                        if (!string.IsNullOrEmpty(contexto.Dr["DataNascimento"].ToString()))
                            c.DataNascimento = Convert.ToDateTime(contexto.Dr["DataNascimento"]);
                        else
                            c.DataNascimento = Convert.ToDateTime(DateTime.Now);
                        c.Email = Convert.ToString(contexto.Dr["Email"]);
                        c.Newsletter = Convert.ToBoolean(contexto.Dr["Newsletter"]);
                        c.Numero = Convert.ToString(contexto.Dr["Numero"]);
                        c.RG = Convert.ToString(contexto.Dr["RG"]);
                        c.Rua = Convert.ToString(contexto.Dr["Rua"]);
                        c.Telefone = Convert.ToString(contexto.Dr["Telefone"]);




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
    public class ClienteMoq
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Id_Sexo { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Newsletter { get; set; }
        public DateTime Data { get; set; }
        public string Complemento { get; set; }
        public string Celular { get; set; }

    }
}

