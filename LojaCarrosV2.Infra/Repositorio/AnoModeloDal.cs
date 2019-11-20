using LojaCarrosV2.Infra.Contexto;
using System;
using System.Collections.Generic;
using LojaCarrosV2.DomainEntidade;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class AnoModeloDal
    {

        public List<AnoModelo> ListarAno(string marca)
        {
            AnoModelo anoMod = new AnoModelo();

            anoMod = ListarAnoByMarca(marca);

            int datas = Convert.ToInt32(anoMod.AnoFim);

            List<AnoModelo> lista = new List<AnoModelo>();
            for (int i = datas; i >= anoMod.AnoInicio; i--)
            {
                AnoModelo ano = new AnoModelo();
                ano.AnoLista = i;
                lista.Add(ano);
            }

            return lista;
        }
        public AnoModelo ListarAnoByMarca(string marca)
        {

            var strQuery = "SELECT MIN(AnoFabricacao) AS AnoIncio, MAX(AnoFabricacao) AS AnoFim ";
            strQuery += "FROM Veiculo AS V ";
            strQuery += "INNER JOIN Marca AS M ON M.IdMarca = V.IdMarca ";
            strQuery += string.Format("WHERE Ativo = 1 AND M.Nome = '{0}'", marca);

            using (Conexao contexto = new Conexao())
            {
                contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);

                AnoModelo ano = new AnoModelo();
                while (contexto.Dr.Read())
                {
                    ano.AnoInicio = Convert.ToInt32(contexto.Dr["AnoIncio"]);
                    ano.AnoFim = Convert.ToInt32(contexto.Dr["AnoFim"]);
                }

                return ano;
            }
        }
        public List<AnoModelo> ListarAnoFim(int anoInicio, string marca)
        {
            AnoModelo anoMod = new AnoModelo();
          
            anoMod = ListarAnoByMarca(marca);
          
            int datas = Convert.ToInt32(anoMod.AnoFim);
            List<AnoModelo> lista = new List<AnoModelo>();
            for (int i = datas; i >= anoInicio; i--)
            {
                AnoModelo ano = new AnoModelo();
                ano.AnoLista = i;
                lista.Add(ano);
            }

            return lista;
        }

        public List<AnoModelo> ListarAno1(int anoIncio)
        {
            int anoFim = anoIncio + 1;
            List<AnoModelo> lista = new List<AnoModelo>();
            for (int i = anoIncio; i <= anoFim; i++)
            {
                AnoModelo ano = new AnoModelo();
                ano.AnoLista = i;
                lista.Add(ano);
            }

            return lista;
        }
        public List<AnoModelo> ListarAno2()
        {
            int datas = Convert.ToInt32(DateTime.Now.Year + 1);
            List<AnoModelo> lista = new List<AnoModelo>();
            for (int i = datas; i > datas - 100; i--)
            {
                AnoModelo ano = new AnoModelo();
                ano.AnoLista = i;
                lista.Add(ano);
            }

            return lista;
        }

        public  List<AnoModelo> ListarAnoFabricacao()
        {
            // usado na pagina de cadastro e editar
            int datas = Convert.ToInt32(DateTime.Now.Year);
            List<AnoModelo> lista = new List<AnoModelo>();
            for (int i = datas; i > datas - 100; i--)
            {
                AnoModelo ano = new AnoModelo();
                ano.AnoLista = i;
                lista.Add(ano);
            }

            return lista;
        }
    }
}