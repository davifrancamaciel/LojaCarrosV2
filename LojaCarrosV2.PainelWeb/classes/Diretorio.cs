using System.Configuration;

namespace LojaCarrosV2.PainelWeb.classes
{
    public static class Diretorio
    {
        public static string ArquivoRedimencionados
        {
            //diretorio de arquivos redimencionados
            get
            {
                return ConfigurationManager.AppSettings["DiretorioArquivoRed"].ToString();
            }
        }

        public static string ArquivoNormais
        {
            //diretorio de arquivos normais 
            get
            {
                return ConfigurationManager.AppSettings["DiretorioArquivoNor"].ToString();
            }
        }
        public static string ArquivoMin
        {
            //diretorio de arquivos normais 
            get
            {
                return ConfigurationManager.AppSettings["DiretorioArquivoMin"].ToString();
            }
        }
        public static string DominioAppCliente
        {  
            //dominio raiz da aplicacao do cliente
            get
            {
                return ConfigurationManager.AppSettings["DominioAppCliente"].ToString();
            }
        }
    }
}